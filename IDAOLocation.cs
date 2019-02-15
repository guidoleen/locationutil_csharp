using System;

namespace LocationUtil
{
    public interface IDAOLocation
    {
        void save(Location _loc);
        void update(Location _loc);
        void delete(Location _loc);
        void show(int _id);
    }
}


