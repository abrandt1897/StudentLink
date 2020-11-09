using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Web.WebSockets;

namespace WebApplication_mc_02
{
    public static class Global
    {
        public static Dictionary<int, WebSocketCollection> websockets = new Dictionary<int, WebSocketCollection>();

    }
}
