using System;
using Amazon.WebSocket;
using NUnit.Framework;

namespace Amazon.WebSocket
{
    public class AmazonWebSocketClientTest
    {
        [Test]
        public void TestSignV4()
        {
            // given
            var accessKey = "";
            var secretKey = "";
            var client = new AmazonWebSocketClient(accessKey, secretKey, RegionEndpoint.APSoutheast2);

            var endpoint = new Uri("wss://a2qu7oonfd0b2x.iot.ap-southeast-2.amazonaws.com/mqtt");

            // when
            var signedUrl = client.SignRequest(endpoint);

            // then
            Assert.IsNotEmpty(signedUrl);
        }
    }
}