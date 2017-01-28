using System;
using System.Text;
using NUnit.Framework;
using UniRx;
using UniRx.WebSocket;
using WebSocketSharp;

namespace UniRx.WebSocket
{
    public class RxWebSocketSharpTest
    {
        private RxWebSocketSharp _ws;
        private readonly Uri _url = new Uri("ws://echo.websocket.org");
        private readonly CompositeDisposable _disposables = new CompositeDisposable();

        [SetUp]
        public void WSConnect()
        {
            _ws = new RxWebSocketSharp(_url);
            _ws.Connect();

            Assert.AreEqual(WebSocketState.Open, _ws.State());
        }

        [Test]
        public void TestEcho()
        {
            // given
            var ws = new RxWebSocketSharp(_url);
            ws.Connect();

            var received = string.Empty;
            ws.Receive()
                .Subscribe(
                    e =>
                    {
                        received = Encoding.ASCII.GetString(e.RawData);
                        Console.WriteLine(e.RawData);
                    }
                )
                .AddTo(_disposables);

            // when
            const string sent = "async message";
            ws.Send(Encoding.ASCII.GetBytes(sent));

            // then
            System.Threading.Thread.Sleep(2000);
            Assert.AreEqual(sent, received);

            ws.Close();
        }

        [TearDown]
        public void WSClose()
        {
            _disposables.Dispose();
            _ws.Close();
        }
    }
}