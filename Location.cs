using System;
using Newtonsoft.Json;

namespace LocationUtil
{
    [JsonObject]
    public class Location
    {
        public double latitude { get; }
        public double longitude { get; }
        public int locid { get; }

        public double getLatitude()
        {
            return this.latitude;
        }
        public double getLongitude()
        {
            return this.longitude;
        }
        public int getLocId()
        {
            return this.locid;
        }

        public Location(int _locid)
        {
            this.locid = _locid;
        }

        public Location(double _lat, double _long)
        {
            this.latitude = _lat;
            this.longitude = _long;
        }

        public Location(int _locid, double _lat, double _long)
        {
            this.locid = _locid;
            this.latitude = _lat;
            this.longitude = _long;
        }

        public String toString()
        {
            return String.Format("Uw locatie lat {0} long {1} met het id {2}",
                this.latitude,
                this.longitude,
                this.locid
                );
        }
    }
}
