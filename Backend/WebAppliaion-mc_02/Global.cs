using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;
using Microsoft.Web.WebSockets;

namespace WebApplication_mc_02
{
    public static class Global
    {
        public static Dictionary<int, WebSocketCollection> websocketcollection = new Dictionary<int, WebSocketCollection>();
        public static Dictionary<int, WebSocket> websockets = new Dictionary<int, WebSocket>();
    }
}
