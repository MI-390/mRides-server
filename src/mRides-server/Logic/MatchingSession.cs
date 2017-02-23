using mRides_server.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mRides_server.Logic
{
    public class MatchingSession
    {
        ServerContext _context;
        public MatchingSession(ServerContext context)
        {
            _context = context;
        }
    }
}
