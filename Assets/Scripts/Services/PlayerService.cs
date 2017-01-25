using Models;
using ZeroFormatter;

namespace Services
{
    public class PlayerService
    {
        private IWebSocketClient _wsClient;

        public PlayerService(IWebSocketClient wsClient)
        {
            _wsClient = wsClient;
        }

        public byte[] SendPlayer(Player player)
        {
            var bytes = ZeroFormatterSerializer.Serialize(player);
            _wsClient.Send(bytes);
            return bytes;
        }
    }
}