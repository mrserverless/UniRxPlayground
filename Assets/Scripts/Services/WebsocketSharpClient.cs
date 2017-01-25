using System;
using WebSocketSharp;

namespace Services
{
    public class WebsocketSharpClient : IWebSocketClient
    {
        private const string Url = "wss://a2qu7oonfd0b2x.iot.ap-southeast-2.amazonaws.com/mqtt";

        private readonly WebSocket _ws;

        public WebsocketSharpClient()
        {
            _ws = new WebSocket(Url);

            _ws.OnMessage += (sender, e) =>
                Console.WriteLine("Laputa says: " + e.Data);

            _ws.Connect();
            _ws.Send("BALUS");
            Console.ReadKey(true);
        }

        public void Send(byte[] data)
        {
            _ws.Send(data);
        }
    }
}