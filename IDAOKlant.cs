using System;

namespace LocationUtil
{
    public interface IDAOKlant
    {
        String insert(Klant _klant, String _message);
        void update(Klant _klant);
        void delete(Klant _klant);
        Klant display(Klant _klant);
    }
}
