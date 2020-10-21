using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WebApplication_mc_02.Controllers
{
    public class WebSocketHandler
    {
        public static async Task websocketHandler(HttpContext context, WebSocket webSocket)
        {
            int PATH_FORWARDSLASH_OFFSET = 1;
            var buffer = new byte[1024 * 4];
            WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            while (!result.CloseStatus.HasValue)
            {
                //get StudentID
                int number = context.Request.Path.Value.LastIndexOf('/') + PATH_FORWARDSLASH_OFFSET;
                string value = context.Request.Path.Value.Substring(number);
                int StudentID = Convert.ToInt32(value);

                List<dynamic> notifications = await ChatsController.GetNotifications(StudentID);
                Global.websockets.Add(StudentID, webSocket);

                byte[] bytes2send = Encoding.UTF8.GetBytes(notifications[0].Data);
                await webSocket.SendAsync(new ArraySegment<byte>(bytes2send), WebSocketMessageType.Text, true, CancellationToken.None);

                result = await webSocket.ReceiveAsync(new ArraySegment<byte>(bytes2send), CancellationToken.None);
            }
            //await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
        }
    }
}
