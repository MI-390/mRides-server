using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mRides_server.Logic
{
    public interface ICatalog<T> 
    {
        T create(T obj, int userId);

    }
}
