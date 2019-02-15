using System;
using System.Collections;
using System.Data;
using System.Data.SQLite;

namespace LocationUtil
{
    public class LocationDAO: IDAOLocation
    {
        private String dir = "C:\\sqlite\\";
        private String db = "world.db" + "; Version = 3;";
        private String dataS = "Data Source =";
        private String strConn = "";
        private System.Data.SQLite.SQLiteConnection conn;
        private System.Data.SQLite.SQLiteCommand comm;
        private ArrayList locations = new ArrayList();

        public ArrayList getLocations(int _id)
        {
            this.show(_id);
            return this.locations;
        }

        // Constructor
        public LocationDAO()
        {
        }

        // Constructor with params
        public LocationDAO(String _dir, String _db)
        {
            this.dir = _dir;
            this.db = _db;
        }

        public void setTheConnection()
        {
            this.strConn = "";
            this.strConn = this.dataS + this.dir + this.db;
            this.conn = new System.Data.SQLite.SQLiteConnection(this.strConn);
        }

        public void delete(Location _loc)
        {
            this.setTheConnection();

            try
            {
                this.conn.Open();

                SQLiteTransaction tr = this.conn.BeginTransaction();

                this.comm = this.conn.CreateCommand();
                this.comm.Transaction = tr;

                    this.comm.CommandText = "DELETE FROM location WHERE locid = @param1";
                    this.comm.CommandType = CommandType.Text;
                    this.comm.Parameters.Add(new SQLiteParameter("@param1", _loc.getLocId()));
                    this.comm.ExecuteNonQuery();

                    this.comm.CommandText = "DELETE FROM klantlocation WHERE klant_id = @param2 and loc_id = @param3";
                    this.comm.CommandType = CommandType.Text;
                    this.comm.Parameters.Add(new SQLiteParameter("@param2", _loc.getKlantId()));
                    this.comm.Parameters.Add(new SQLiteParameter("@param3", _loc.getLocId()));
                    this.comm.ExecuteNonQuery();

                    this.comm.CommandText = "DELETE FROM bericht WHERE berid = @param4";
                    this.comm.CommandType = CommandType.Text;
                    this.comm.Parameters.Add(new SQLiteParameter("@param4", _loc.getBerichtId()));
                    this.comm.ExecuteNonQuery();

                tr.Commit();
            }
            catch(SQLiteException ex)
            {
                Console.Write(ex.ToString());
                this.conn.Close();
            }
            this.conn.Close();
        }

        public void save(Location _loc)
        {
            this.setTheConnection();

            this.conn.Open();

            SQLiteTransaction tr = this.conn.BeginTransaction();

            this.comm = this.conn.CreateCommand();
            this.comm.Transaction = tr;

            try
            {
                    this.comm.CommandText = "INSERT INTO location (longitude, latitude) VALUES(@param1, @param2)";
                    this.comm.CommandType = CommandType.Text;
                    this.comm.Parameters.Add(new SQLiteParameter("@param1", _loc.getLongitude()));
                    this.comm.Parameters.Add(new SQLiteParameter("@param2", _loc.getLatitude()));
                    this.comm.ExecuteNonQuery();

                    long lastLocId = this.conn.LastInsertRowId; // (Int32) this.comm.ExecuteScalar(); // string strlastId = @"select last_insert_rowid()";

                    this.comm.CommandText = "INSERT INTO bericht (title, bericht) VALUES (@param3, @param4)";
                    this.comm.CommandType = CommandType.Text;
                    this.comm.Parameters.Add(new SQLiteParameter("@param3", _loc.getBerTitel()));
                    this.comm.Parameters.Add(new SQLiteParameter("@param4", _loc.getBerText()));
                    this.comm.ExecuteNonQuery();

                    long lastBerId = this.conn.LastInsertRowId;

                    this.comm.CommandText = "INSERT INTO klantlocation (id_klant, id_location, id_bericht) VALUES (@param5, @param6, @param7)";
                    this.comm.CommandType = CommandType.Text;
                    this.comm.Parameters.Add(new SQLiteParameter("@param5", _loc.getKlantId()));
                    this.comm.Parameters.Add(new SQLiteParameter("@param6", lastLocId));
                    this.comm.Parameters.Add(new SQLiteParameter("@param7", lastBerId));
                    this.comm.ExecuteNonQuery();

                tr.Commit();
            }
            catch (SQLiteException ex)
            {
                tr.Rollback();
                System.Console.WriteLine(ex.ToString());
                this.conn.Close();
            }
            finally
            {
                this.conn.Close();
            }
        }

        public void addLocation(Location _loc)
        {
            locations.Add(_loc);
        }

        public void show(int _id)
        {
            String strSql = "select locid, latitude, longitude, title, bericht, berid, klantid from klantlocation, klant, location, bericht where id_klant = klantid and id_location = locid and id_bericht = berid " +
                            "and klantid = " + _id.ToString();

            this.setTheConnection();
            this.comm = new SQLiteCommand(strSql, this.conn);

            try
            {
                this.conn.Open();
                SQLiteDataReader drdr = this.comm.ExecuteReader();
                while (drdr.Read())
                {
                    Console.WriteLine(drdr["locid"] + " LocationId");
                    Console.WriteLine(drdr["longitude"] + " Longitude");
                    Console.WriteLine(drdr["latitude"] + " latitude");

                    // Location(int _locid, double _lat, double _long, String _bertitle, String _bertext, int _berichtid, int _locid)
                    addLocation(new Location((int)drdr.GetInt32(0),
                                    (double)drdr.GetDouble(1),
                                    (double)drdr.GetDouble(2),
                                    (String)drdr.GetString(3),
                                    (String)drdr.GetString(4),
                                    (int)drdr.GetInt32(5),
                                    (int)drdr.GetInt32(5)
                                    ));
                }
            }
            catch(SQLiteException ex)
            {
                System.Console.WriteLine(ex.ToString());
                this.conn.Close();
            }            
            this.conn.Close();
        }

        public void update(Location _loc)
        {
            this.setTheConnection();
            this.conn.Open();

            SQLiteTransaction tr = this.conn.BeginTransaction();

            this.comm = this.conn.CreateCommand();
            this.comm.Transaction = tr;

            try
            {
                this.comm.CommandText = "UPDATE location SET longitude = @param1, latitude = @param2 WHERE locid = @param3";
                this.comm.CommandType = CommandType.Text;
                this.comm.Parameters.Add(new SQLiteParameter("@param1", _loc.getLongitude()));
                this.comm.Parameters.Add(new SQLiteParameter("@param1", _loc.getLatitude()));
                this.comm.Parameters.Add(new SQLiteParameter("@param3", _loc.getLocId()));
                this.comm.ExecuteNonQuery();

                this.comm.CommandText = "UPDATE bericht SET title = @param3, bericht = @param4 WHERE berid = @param5";
                this.comm.CommandType = CommandType.Text;
                this.comm.Parameters.Add(new SQLiteParameter("@param3", _loc.getBerTitel()));
                this.comm.Parameters.Add(new SQLiteParameter("@param4", _loc.getBerText()));
                this.comm.Parameters.Add(new SQLiteParameter("@param5", _loc.getBerichtId()));
                this.comm.ExecuteNonQuery();

                tr.Commit();
            }
            catch(SQLiteException ex)
            {
                Console.Write(ex.ToString());
                tr.Rollback();
                this.conn.Close();
            }
            finally
            {
                this.conn.Close();
            }
        }
    }
}


