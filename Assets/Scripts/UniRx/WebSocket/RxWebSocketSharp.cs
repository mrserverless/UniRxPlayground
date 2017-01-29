using System;
using WebSocketSharp;

namespace UniRx.WebSocket
{
    public class RxWebSocketSharp : IObservableWS
    {
        private readonly WebSocketSharp.WebSocket _ws;

        public RxWebSocketSharp(Uri url)
        {
            if (!url.Scheme.Equals("wss") && !url.Scheme.Equals("ws"))
                throw new ArgumentException("Unsupported protocol: " + url.Scheme);

            _ws = new WebSocketSharp.WebSocket(url.ToString());
        }

        public void Connect()
        {
            _ws.Connect();
        }

        public void Connect(EventHandler<MessageEventArgs> receiver)
        {
            _ws.OnMessage += receiver;
            _ws.Connect();
        }

        public void Send(byte[] data)
        {
            _ws.Send(data);
        }

        public void SendAsync(byte[] data)
        {
            _ws.SendAsync(data, null);
        }

        public WebSocketState State()
        {
            return _ws.ReadyState;
        }

        public IObservable<MessageEventArgs> Receive()
        {
            return Observable
                .FromEvent<EventHandler<MessageEventArgs>, MessageEventArgs>(
                h => (sender, e) => h(e), h => _ws.OnMessage += h, h => _ws.OnMessage -= h
            );
        }

        public IObservable<ErrorEventArgs> Errors()
        {
            return Observable.FromEvent<EventHandler<ErrorEventArgs>, ErrorEventArgs>(
                h => (sender, e) => h(e), h => _ws.OnError += h, h => _ws.OnError -= h
            );
        }

        public void Close()
        {
            _ws.CloseAsync();
        }

        public void OnError(Exception e)
        {
            Console.WriteLine("error: " + e);
        }

        [Serializable]
        public class Config
        {
            public string url = "";
        }
    }
}