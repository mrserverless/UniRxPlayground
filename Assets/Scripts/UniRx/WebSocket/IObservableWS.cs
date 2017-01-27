using WebSocketSharp;

namespace UniRx.WebSocket
{
    public interface IObservableWS
    {
        void Connect();
        void Close();

        void Send(byte[] data);
        void SendAsync(byte[] data);

        IObservable<MessageEventArgs> Receive();
        IObservable<ErrorEventArgs> Errors();

    }

}