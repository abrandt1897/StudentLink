using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Nancy.Json.Simple;
using Nancy.Json;
using WebApplication_mc_02.Models.DTO;
using WebApplication_mc_02.Models;

using Microsoft.Web.WebSockets;
using Microsoft.AspNetCore.SignalR;

namespace WebApplication_mc_02.Controllers
{
    /// <summary>
    /// Websocket handler
    /// </summary>
    public class MyWebSocketHandler : Hub
    {
        private static WebSocketCollection m_Sessions = new WebSocketCollection();

        public string m_Player;

        public Task SendMessage(string user, string message)
        {
            Clients.Groups("").SendAsync("sendMessage", user, message);
            return Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public override async Task OnConnectedAsync()
        {
            string StudentID = Context.GetHttpContext().Request.Cookies["StudentID"];
            await Groups.AddToGroupAsync(StudentID, SQLConnection.get<Student2ChatMap>(typeof(Students), "WHERE Student")[0].ChatID.ToString());
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "SignalR Users");
            await base.OnDisconnectedAsync(exception);
        }

        /// <summary>
        /// handles the data coming in from the websocket
        /// </summary>
        /// <param name="context"></param>
        /// <param name="webSocket"></param>
        /// <returns></returns>
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
                List<Notifications> notifications = await ChatsController.GetNotifications(StudentID);
                if (Global.websockets.ContainsKey(StudentID))
                    Global.websockets.Remove(StudentID);
                string notiString = new JavaScriptSerializer().Serialize(notifications);
                byte[] bytes2send = Encoding.UTF8.GetBytes(notiString);
                await webSocket.SendAsync(new ArraySegment<byte>(bytes2send), WebSocketMessageType.Text, true, CancellationToken.None);

                result = await webSocket.ReceiveAsync(new ArraySegment<byte>(bytes2send), CancellationToken.None);
            }
            //await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
        }
        public static async Task<bool> sendDataAsync(WebSocket webSocket, object obj)
        {
            string notiString = new JavaScriptSerializer().Serialize(obj);
            byte[] bytes2send = Encoding.UTF8.GetBytes(notiString);
            try
            {
                await webSocket.SendAsync(new ArraySegment<byte>(bytes2send), WebSocketMessageType.Text, true, CancellationToken.None);
            }
            catch
            {
                if (SQLConnection.insert(obj))
                    return true;
            }
            return true;
        }
        public Task ThrowException()
        {
            throw new HubException("This error will be sent to the client!");
        }

        internal static void AddSocket(WebSocket webSocket, TaskCompletionSource<object> socketFinishedTcs)
        {
            throw new NotImplementedException();
        }
    }
}
