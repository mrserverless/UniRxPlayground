using System;
using Character;
using Models;
using UniRx.WebSocket;
using ZeroFormatter;

namespace Services
{
    public class PlayerService
    {
        private readonly IObservableWS _ws;
        private readonly PlayerContext _playerContext;

        public PlayerService(IObservableWS ws, PlayerContext playerContext)
        {
            _ws = ws;
            _playerContext = playerContext;
        }




        public byte[] SendPlayer(Player player)
        {
            var bytes = ZeroFormatterSerializer.Serialize(player);
            _ws.SendAsync(bytes);
            return bytes;
        }
    }
}