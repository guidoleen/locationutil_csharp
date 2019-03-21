using System;
using System.Data;
using System.Data.SQLite;

namespace LocationUtil
{
    public class KlantDAO : IDAOKlant
    {
        private String dir;
        private String db;
        private String strConn;
        private String dataS = "Data Source =";
        private SQLiteConnection conn;
        private SQLiteCommand comm;

        // Constructor basic
        public KlantDAO()
        {
        }

        // Constructor with params
        public KlantDAO(String _dir, String _db)
        {
            this.dir = _dir;
            this.db = _db;
        }

        // Set the connection
        public void setTheConnection()
        {
            this.strConn = "";
            this.strConn = this.dataS + this.dir + this.db;
            this.conn = new System.Data.SQLite.SQLiteConnection(this.strConn);
        }

        public void delete(Klant _klant)
        {
            throw new NotImplementedException();
        }

        public Klant display(Klant _klant)
        {
            Klant klant = null;
            this.setTheConnection();

            this.comm = this.conn.CreateCommand();

            try
            {
                this.conn.Open();
                this.comm.CommandText = "SELECT klantid, naam, email, pwd FROM klant WHERE klantid = @param1";
                this.comm.CommandType = CommandType.Text;
                this.comm.Parameters.Add(new SQLiteParameter("@param1", _klant.getKlantId()));

                SQLiteDataReader rdr = this.comm.ExecuteReader();

                while( rdr.Read() )
                {
                    klant = new Klant(
                        rdr.GetInt16(0),
                        rdr.GetString(1),
                        rdr.GetString(2),
                        rdr.GetString(3)
                        );
                }
            }
            catch(Exception ex)
            {
                Console.Write(ex.ToString());
            }
            finally
            {
                this.conn.Close();
            }

            return klant;
        }

        public void insert(Klant _klant)
        {
            throw new NotImplementedException();
        }

        

        public void update(Klant _klant)
        {
            throw new NotImplementedException();
        }

        // GET THE KLANT login comparing
        public Boolean isValidKlant(Klant _klant)
        {
            String strSql = "SELECT * FROM klant WHERE klantid = @param1" +
                              " AND email = @param2" +
                              " AND pwd = @param3";

            this.setTheConnection();

            using (SQLiteConnection c = new SQLiteConnection(this.strConn))
            {
                c.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(strSql, c))
                {
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.Add(new SQLiteParameter("@param1", _klant.getKlantId()));
                    cmd.Parameters.Add(new SQLiteParameter("@param2", _klant.getEmail()));
                    cmd.Parameters.Add(new SQLiteParameter("@param3", _klant.getPwd()));

                    using (SQLiteDataReader rdr = cmd.ExecuteReader())
                    {
                        if (rdr.HasRows)
                            return true;
                    }
                }
            }
            return false;
        }
    }
}

