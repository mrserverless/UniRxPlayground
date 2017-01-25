using Models;
using NUnit.Framework;
using UnityEditor.SceneManagement;

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
            var wsClient = new MockWsClient();
            var playerService = new PlayerService(wsClient);
            var player = new Player();

            // when
            byte[] data = playerService.SendPlayer(player);

            // then
            Assert.IsNotEmpty(data);
        }

    }

    class MockWsClient : IWebSocketClient
    {
        public void Send(byte[] data)
        {
        }
    }
}