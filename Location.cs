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
        public int klantid { get; }
        public int berichtid { get; }
        public String bertitel { get; }
        public String bertext { get; }

        public String getBerTitel()
        {
            return this.bertitel;
        }

        public String getBerText()
        {
            return this.bertext;
        }

        public int getKlantId()
        {
            return this.klantid;
        }

        public int getBerichtId()
        {
            return this.berichtid;
        }

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

        public Location(int _locid, double _lat, double _long, String _bertitle, String _bertext, int _berichtid, int _klantid)
        {
            this.locid = _locid;
            this.latitude = _lat;
            this.longitude = _long;
            this.bertext = _bertext;
            this.bertitel = _bertitle;
            this.berichtid = _berichtid;
            this.klantid = _klantid;
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
