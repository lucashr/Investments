using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Investments.VariablesManager
{
    public class WebScrapingSocketManager
    {
        // private static ConcurrentDictionary<string, WebSocket> _sockets = new ConcurrentDictionary<string, WebSocket>();
        public WebSocket GetSocketById(string id)
        {
            return ConectionsWebSocket.sockets.FirstOrDefault(p => p.Key == id).Value;
        }

        public ConcurrentDictionary<string, WebSocket> GetAll()
        {
            return ConectionsWebSocket.sockets;
        }

        public string GetId(WebSocket socket)
        {
            return ConectionsWebSocket.sockets.FirstOrDefault(p => p.Value == socket).Key;
        }
        public string AddSocket(WebSocket socket)
        {
            var id = CreateConnectionId();
            // ConectionsWebSocket.sockets.TryAdd(CreateConnectionId(), socket);
            ConectionsWebSocket.sockets.TryAdd(id, socket);

            return id;
        }

        public async Task RemoveSocket(string id)
        {
            WebSocket socket;
            ConectionsWebSocket.sockets.TryRemove(id, out socket);

            await socket.CloseAsync(closeStatus: WebSocketCloseStatus.NormalClosure,
                                    statusDescription: "Closed by the WebSocketManager",
                                    cancellationToken: CancellationToken.None);
        }

        private string CreateConnectionId()
        {
            return Guid.NewGuid().ToString();
        }

        public async Task SendMessageToAllAsync(string message)
        {
            foreach (var pair in ConectionsWebSocket.sockets)
            {
                if (pair.Value.State == WebSocketState.Open)
                    await SendMessageAsync(pair.Value, message);
            }
        }

        private async Task SendMessageAsync(WebSocket socket, string message)
        {
            if (socket.State != WebSocketState.Open)
                return;

            await socket.SendAsync(buffer: new ArraySegment<byte>(array: Encoding.ASCII.GetBytes(message),
                                                                  offset: 0,
                                                                  count: message.Length),
                                   messageType: WebSocketMessageType.Text,
                                   endOfMessage: true,
                                   cancellationToken: CancellationToken.None);
        }
    }

}
