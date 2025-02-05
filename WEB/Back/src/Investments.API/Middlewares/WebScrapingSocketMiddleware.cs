using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Investments.VariablesManager;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Investments.API
{
    public class WebScrapingSocketMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly WebScrapingSocketManager _socketManager;

        public WebScrapingSocketMiddleware(RequestDelegate next,
                                           WebScrapingSocketManager socketManager)
        {
            _next = next;
            _socketManager = socketManager;
        }

        public async Task Invoke(HttpContext context)
        {
            if (!context.WebSockets.IsWebSocketRequest)
            {
                await _next.Invoke(context);
                return;
            }

            string sessionId = context.Request.Query["sessionId"];

            var sessionContext = context.RequestServices.GetRequiredService<SessionContext>();
            sessionContext.SessionId = sessionId;

            var socket = await context.WebSockets.AcceptWebSocketAsync();
            _socketManager.AddSocket(sessionId, socket);

            await Receive(socket, async (result, buffer) =>
            {
                if (result.MessageType == WebSocketMessageType.Close)
                {
                    await _socketManager.RemoveSocket(sessionId, socket);
                    return;
                }
            });
            
        }

        private async Task Receive(WebSocket socket, Action<WebSocketReceiveResult, byte[]> handleMessage)
        {
            
            var buffer = new byte[1024 * 4];

            while (socket.State == WebSocketState.Open)
            {
                var result = await socket.ReceiveAsync(buffer: new ArraySegment<byte>(buffer),
                                                       cancellationToken: CancellationToken.None);
                
                var message = Encoding.UTF8.GetString(buffer, 0, result.Count);

                handleMessage(result, buffer);
            }
        }
    }
}