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

        public ArrayList getLocations()
        {
            this.show();
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

            this.comm = this.conn.CreateCommand();
            this.comm.CommandText = "DELETE FROM location WHERE locid = @param1";
            this.comm.CommandType = CommandType.Text;
            this.comm.Parameters.Add(new SQLiteParameter("@param1", _loc.getLocId()));

            try
            {
                this.conn.Open();
                this.comm.ExecuteNonQuery();
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
            this.comm = this.conn.CreateCommand();

            this.comm.CommandText = "INSERT INTO location (longitude, latitude) VALUES(@param1, @param2)";
            this.comm.CommandType = CommandType.Text;

            this.comm.Parameters.Add(new SQLiteParameter("@param1", _loc.getLongitude()));
            this.comm.Parameters.Add(new SQLiteParameter("@param2", _loc.getLatitude()));

            try
            {
                this.conn.Open();
                this.comm.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                System.Console.WriteLine(ex.ToString());
                this.conn.Close();
            }
            this.conn.Close();
        }

        public void addLocation(Location _loc)
        {
            locations.Add(_loc);
        }

        public void show()
        {
            String strSql = "select locid, longitude, latitude, title, bericht from klantlocation, klant, location, bericht" +
            "where id_klant = klantid and" +
            "id_location = locid and" +
            "id_bericht = berid";

            strSql = "select locid, latitude, longitude from location";

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

                    // Location(int _locid, double _lat, double _long, String _bertitle, String _bertext)
                    addLocation(new Location((int)drdr.GetInt32(0),
                                    (double)drdr.GetDouble(1),
                                    (double)drdr.GetDouble(2)
                                    ));
                                    // (String)drdr.GetString(3),
                                    // (String)drdr.GetString(4)
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
            this.comm = this.conn.CreateCommand();

            this.comm.CommandText = "UPDATE location SET longitude = @param1, latitude = @param2 WHERE locid = @param3";
            this.comm.CommandType = CommandType.Text;
            this.comm.Parameters.Add(new SQLiteParameter("@param1", _loc.getLongitude()));
            this.comm.Parameters.Add(new SQLiteParameter("@param1", _loc.getLatitude()));
            this.comm.Parameters.Add(new SQLiteParameter("@param3", _loc.getLocId()));

            try
            {
                this.conn.Open();
                this.comm.ExecuteNonQuery();
            }
            catch(SQLiteException ex)
            {
                Console.Write(ex.ToString());
                this.conn.Close();
            }
            this.conn.Close();
        }
    }
}


