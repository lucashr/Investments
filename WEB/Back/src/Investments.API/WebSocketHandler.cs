using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

public static class WebSocketHandler
{
    private static bool _isRunning = false;
    private static CancellationTokenSource _cts;

    public static async Task HandleWebSocketAsync(WebSocket webSocket)
    {
        var buffer = new byte[1024 * 4];
        while (webSocket.State == WebSocketState.Open)
        {
            var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            var message = Encoding.UTF8.GetString(buffer, 0, result.Count);

            if (message == "start")
            {
                if (!_isRunning)
                {
                    _isRunning = true;
                    _cts = new CancellationTokenSource();
                    _ = RunLongTask(_cts.Token);
                    await SendMessageAsync(webSocket, "Task started");
                }
                else
                {
                    await SendMessageAsync(webSocket, "Task already running");
                }
            }
            else if (message == "stop")
            {
                if (_isRunning)
                {
                    _isRunning = false;
                    _cts.Cancel();
                    await SendMessageAsync(webSocket, "Task stopped");
                }
                else
                {
                    await SendMessageAsync(webSocket, "No task to stop");
                }
            }
            else
            {
                await SendMessageAsync(webSocket, "Unknown command");
            }

            if (result.CloseStatus.HasValue)
            {
                await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
            }
        }
    }

    private static async Task SendMessageAsync(WebSocket webSocket, string message)
    {
        var encodedMessage = Encoding.UTF8.GetBytes(message);
        await webSocket.SendAsync(new ArraySegment<byte>(encodedMessage), WebSocketMessageType.Text, true, CancellationToken.None);
    }

    private static async Task RunLongTask(CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            Console.WriteLine("Task running...");
            await Task.Delay(1000, token); // Simula uma operação longa
        }
    }
}
