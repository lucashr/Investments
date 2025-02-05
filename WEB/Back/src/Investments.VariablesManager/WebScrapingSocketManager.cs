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
        // Alterado para armazenar múltiplos WebSockets por sessionId
        private static ConcurrentDictionary<string, ConcurrentBag<WebSocket>> ConectionsWebSocket = 
            new ConcurrentDictionary<string, ConcurrentBag<WebSocket>>();

        // Obtém o primeiro WebSocket associado a um sessionId
        public WebSocket GetSocketBySessionId(string sessionId)
        {
            if (ConectionsWebSocket.ContainsKey(sessionId))
            {
                return ConectionsWebSocket[sessionId].FirstOrDefault();
            }
            return null;
        }

        // Obtém todas as conexões (WebSockets) de todas as sessões
        public ConcurrentDictionary<string, ConcurrentBag<WebSocket>> GetAll()
        {
            return ConectionsWebSocket;
        }

        // Obtém o sessionId associado a um WebSocket
        public string GetSessionId(WebSocket socket)
        {
            return ConectionsWebSocket.FirstOrDefault(p => p.Value.Contains(socket)).Key;
        }

        // Adiciona um WebSocket à coleção do sessionId correspondente
        public string AddSocket(string sessionId, WebSocket socket)
        {
            // Verifica se a sessão já existe, se não, cria uma nova bag de WebSockets
            var webSocketBag = ConectionsWebSocket.GetOrAdd(sessionId, new ConcurrentBag<WebSocket>());
            webSocketBag.Add(socket);
            return sessionId;
        }

        // Remove um WebSocket de uma sessão
        public async Task RemoveSocket(string sessionId, WebSocket socket)
        {
            if (ConectionsWebSocket.ContainsKey(sessionId))
            {
                var webSocketBag = ConectionsWebSocket[sessionId];

                // Remove o WebSocket da coleção
                if (webSocketBag.TryTake(out _))
                {
                    // Fecha o WebSocket
                    await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, 
                                             "Closed by the WebSocketManager", 
                                             CancellationToken.None);
                }

                if (webSocketBag.IsEmpty)
                {
                    ConectionsWebSocket.TryRemove(sessionId, out _);
                }
            }
        }

        // Envia uma mensagem para todos os WebSockets de uma sessão
        public async Task SendMessageToAllAsync(string sessionId, string message)
        {
            if (ConectionsWebSocket.ContainsKey(sessionId))
            {
                var webSocketBag = ConectionsWebSocket[sessionId];

                foreach (var socket in webSocketBag)
                {
                    if (socket.State == WebSocketState.Open)
                    {
                        await SendMessageAsync(socket, message);
                    }
                }
            }
        }

        // Envia uma mensagem para um WebSocket específico
        public async Task SendMessageAsync(string sessionId, string message)
        {
            var socket = GetSocketBySessionId(sessionId);
            if (socket != null)
            {
                await SendMessageAsync(socket, message);
            }
        }

        // Envia uma mensagem para o WebSocket específico
        public async Task SendMessageAsync(WebSocket socket, string message)
        {
            if (socket.State == WebSocketState.Open)
            {
                await socket.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(message)),
                                       WebSocketMessageType.Text,
                                       true, CancellationToken.None);
            }
        }
    }
}
