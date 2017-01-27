using Models;
using NUnit.Framework;
using UniRx.WebSocket;

namespace Services
{
    public class PlayerServiceTest
    {
        [SetUp]
        public void SetUp()
        {
            ZeroFormatterInitializer.Register();
        }

        [Test]
        public void TestSendPlayer()
        {
            // given
            var wsClient = new MockWs();
            var playerService = new PlayerService(wsClient);
            var player = new Player();

            // when
            byte[] data = playerService.SendPlayer(player);

            // then
            Assert.IsNotEmpty(data);
        }

    }
}