using System;
using System.Data;
using System.Data.SQLite;

namespace LocationUtil
{
    public class SessionLocationDAO
    {
        private String dir;
        private String db;
        private String strConn;
        private String dataS = "Data Source =";
        private System.Data.SQLite.SQLiteConnection conn;
        private System.Data.SQLite.SQLiteCommand comm;

        public SessionLocationDAO()
        {
        }

        public SessionLocationDAO(String _dir, String _db)
        {
            this.dir = _dir;
            this.db = _db;
        }

        private void setTheConnection()
        {
            this.strConn = "";
            this.strConn = this.dataS + this.dir + this.db;
            this.conn = new System.Data.SQLite.SQLiteConnection(this.strConn);
            SQLiteConnection.ClearAllPools();
        }

        // SAVE SESSION in db
        public void saveSession(SessionLocation _sess)
        {
            this.setTheConnection();

            try
            {
                this.conn.Open();
                this.comm = this.conn.CreateCommand();

                this.comm.CommandText = "INSERT INTO session (id_klant, sessionid, sessiontoken) VALUES (@param1, @param2, @param3)";
                this.comm.CommandType = CommandType.Text;

                this.comm.Parameters.Add(new SQLiteParameter("@param1", _sess.getKlantId()));
                this.comm.Parameters.Add(new SQLiteParameter("@param2", _sess.getSessionId().ToString()));
                this.comm.Parameters.Add(new SQLiteParameter("@param3", _sess.getSessionToken().ToString()));

                this.comm.ExecuteNonQuery();
            }
            catch(SQLiteException ee)
            {
                Console.Write(ee.ToString());
            }
            finally
            {
                this.conn.Close();
            }
            this.conn.Close();
        }

        // DELETE SESSION in db
        public void deleteSession(SessionLocation _sess)
        {
            String strSql = "DELETE FROM session WHERE id_klant = @param1";
            this.setTheConnection();

            try
            {
                this.conn.Open();
                this.comm = this.conn.CreateCommand();

                this.comm.CommandText = strSql;
                this.comm.CommandType = CommandType.Text;

                this.comm.Parameters.Add(new SQLiteParameter("@param1", _sess.getKlantId()));

                this.comm.ExecuteNonQuery();
            }
            catch (SQLiteException ee)
            {
                Console.Write(ee.ToString());
            }
            finally
            {
                this.conn.Close();
            }
        }

        // GET THE SESSION for comparing
        public Boolean isValidSession(SessionLocation _sess)
        {
            String strSql =   "SELECT id_klant, sessionid, sessiontoken FROM session WHERE id_klant = @param1" +
                              " AND sessionid = @param2" +
                              " AND sessiontoken = @param3";
            
                this.setTheConnection();

                using (SQLiteConnection c = new SQLiteConnection(this.strConn))
                {
                    c.Open();
                    using (SQLiteCommand cmd = new SQLiteCommand(strSql, c))
                    {
                        cmd.CommandType = CommandType.Text;

                        cmd.Parameters.Add(new SQLiteParameter("@param1", _sess.getKlantId()));
                        cmd.Parameters.Add(new SQLiteParameter("@param2", _sess.getSessionId()));
                        cmd.Parameters.Add(new SQLiteParameter("@param3", _sess.getSessionToken()));

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

