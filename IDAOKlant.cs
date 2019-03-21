using System;

namespace LocationUtil
{
    public interface IDAOKlant
    {
        void insert(Klant _klant);
        void update(Klant _klant);
        void delete(Klant _klant);
        Klant display(Klant _klant);
    }
}
