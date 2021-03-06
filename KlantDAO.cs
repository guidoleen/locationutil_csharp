﻿using System;
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

        public String insert(Klant _klant, String _message)
        {
            this.setTheConnection();
            this.comm = this.conn.CreateCommand();

            String strComm = "insert into klant(naam, email, pwd) values(@param1, @param2, @param3)";

            try
            {
                this.conn.Open();
                this.comm.CommandText = strComm;
                this.comm.CommandType = CommandType.Text;

                this.comm.Parameters.Add(new SQLiteParameter("@param1", _klant.getNaam()));
                this.comm.Parameters.Add(new SQLiteParameter("@param2", _klant.getEmail()));
                this.comm.Parameters.Add(new SQLiteParameter("@param3", _klant.getPwd()));

                this.comm.ExecuteNonQuery();
            }
            catch(SQLiteException ee)
            {
                Console.Write(ee.ToString());
                return ee.ToString();
            }
            finally
            {
                this.conn.Close();
            }
            return _message;
        }

        

        public void update(Klant _klant)
        {
            throw new NotImplementedException();
        }

        // GET THE KLANT login comparing
        public int isValidKlant(Klant _klant)
        {
            String strSql = "SELECT klantid FROM klant WHERE " + // klantid = @param1
                              " email = @param1" +
                              " AND pwd = @param2";

            this.setTheConnection();

            using (SQLiteConnection c = new SQLiteConnection(this.strConn))
            {
                c.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(strSql, c))
                {
                    cmd.CommandType = CommandType.Text;

                    // cmd.Parameters.Add(new SQLiteParameter("@param1", _klant.getKlantId()));
                    cmd.Parameters.Add(new SQLiteParameter("@param1", _klant.getEmail()));
                    cmd.Parameters.Add(new SQLiteParameter("@param2", _klant.getPwd()));

                    using (SQLiteDataReader rdr = cmd.ExecuteReader())
                    {
                        int returnId = 0;
                        if (rdr.HasRows)
                        {
                            while(rdr.Read())
                            {
                                returnId = rdr.GetInt32(0);
                            }
                        }
                        return returnId;
                    }
                }
            }
            return 0;
        }
    }
}

