using System;
using WebSocketSharp;

namespace UniRx.WebSocket
{
    class MockWs : IObservableWS
    {
        public void Connect()
        {
            throw new NotImplementedException();
        }

        public void Send(byte[] data)
        {
            throw new NotImplementedException();
        }

        public void SendAsync(byte[] data)
        {
        }

        public IObservable<MessageEventArgs> Receive()
        {
            throw new NotImplementedException();
        }

        public IObservable<ErrorEventArgs> Errors()
        {
            throw new NotImplementedException();
        }

        public void Close()
        {
            throw new NotImplementedException();
        }
    }
}