using System.Collections.Concurrent;
using System.Net.WebSockets;

namespace Investimentos.VariablesManager
{
    public static class ConectionsWebSocket
    {
        public static readonly WebScrapingSocketManager socketManager = new WebScrapingSocketManager();
        public static ConcurrentDictionary<string, WebSocket> sockets = new ConcurrentDictionary<string, WebSocket>();
    }
}