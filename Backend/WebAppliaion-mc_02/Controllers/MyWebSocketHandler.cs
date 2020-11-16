using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Nancy.Json.Simple;
using System.Text.Json;
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
    public class MyWebSocketHandler
    {

        /// <summary>
        /// handles the data coming in from the websocket
        /// loops until the client leaves
        /// </summary>
        /// <param name="context"></param>
        /// <param name="websocket"></param>
        /// <returns></returns>
        public static async Task websocketHandler(HttpContext context, WebSocket websocket)
        {
            
            //get add websocket to the list of websockets
            string path = context.Request.Path.Value;
            string[] pathSplit = path.Split('/');
            int ChatID = Int32.Parse(pathSplit[2]);
            //TODO check if the chatID is real in the database
            int StudentID = Int32.Parse(pathSplit[3]);

            //add the new person to correct group otherwise create a new list for the group
            if (Global.websockets.ContainsKey(ChatID))
                Global.websockets[ChatID].Add(StudentID, websocket);
            else
            {
                var tempDict = new Dictionary<int, WebSocket>();
                tempDict.Add(StudentID, websocket);
                Global.websockets.Add(ChatID, tempDict);
            }
            var buffertemp = new byte[256];
            WebSocketReceiveResult result = await websocket.ReceiveAsync(new ArraySegment<byte>(buffertemp), CancellationToken.None);
            
            while (!result.CloseStatus.HasValue)
            {
                string _str = Encoding.ASCII.GetString(buffertemp);
                _str = _str.Remove(_str.IndexOf('\0'));
                string jsonstring = $"{{\"ChatID\":{ChatID}, \"SenderID\":{StudentID}, \"Data\":\"{_str}\"}}";
                Chats saveChat = JsonSerializer.Deserialize<Chats>(jsonstring);
                await SQLConnection.Insert(saveChat);
                Console.Out.WriteLine($"WebSocket********************{jsonstring}****************************");
                var buffer = new byte[_str.Length];
                for (int i = 0; i < _str.Length; i++)
                    buffer[i] = buffertemp[i];
                foreach (KeyValuePair<int, WebSocket> kvp in Global.websockets[ChatID])
                {
                    if (kvp.Key != StudentID)
                    {
                        try
                        {
                            await kvp.Value.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None);
                        }
                        catch
                        {
                            Global.websockets[ChatID].Remove(kvp.Key);
                        }
                    }
                }
                for (int i = 0; i < buffer.Length; i++)
                {
                    buffertemp.SetValue((byte)0, i);
                    buffer.SetValue((byte)0, i);
                    Console.Out.WriteLine(i);
                }
                _str = "";
                result = await websocket.ReceiveAsync(new ArraySegment<byte>(buffertemp), CancellationToken.None);
            }
            await websocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
        }
    }
}
