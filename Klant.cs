using System;

namespace LocationUtil
{
    public class Klant
    {
        private int klantid { get; }
        private String naam { get; }
        private String pwd { get; }
        private String email { get; }

        public int getKlantId()
        {
            return this.klantid;
        }

        public String getNaam()
        {
            return this.naam;
        }

        public String getPwd()
        {
            return this.pwd;
        }

        public String getEmail()
        {
            return this.email;
        }

        public Klant(int _klantid, String _naam, String _pwd, String _email)
        {
            this.klantid = _klantid;
            this.naam = _naam;
            this.pwd = _pwd;
            this.email = _email;
        }

        public String toString()
        {
            return String.Format("KlantId %d, Naam %s Pwd %s, Email %s",
                this.klantid,
                this.naam,
                this.pwd,
                this.email);
        }
    }
}