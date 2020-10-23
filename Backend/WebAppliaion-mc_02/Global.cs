using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.WebSockets;

namespace WebApplication_mc_02
{
    public static class Global
    {
        public static Dictionary<int, WebSocket> websockets = new Dictionary<int, WebSocket>();

    }
}
