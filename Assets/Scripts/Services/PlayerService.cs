using Models;
using UniRx.WebSocket;
using ZeroFormatter;

namespace Services
{
    public class PlayerService
    {
        private IObservableWS _ws;

        public PlayerService(IObservableWS ws)
        {
            _ws = ws;
        }

        public byte[] SendPlayer(Player player)
        {
            var bytes = ZeroFormatterSerializer.Serialize(player);
            _ws.SendAsync(bytes);
            return bytes;
        }
    }
}