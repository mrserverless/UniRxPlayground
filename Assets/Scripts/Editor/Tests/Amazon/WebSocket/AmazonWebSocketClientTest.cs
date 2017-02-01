using System;
using Amazon.CognitoIdentity.Model;
using Amazon.WebSocket;
using NUnit.Framework;

namespace Amazon.WebSocket
{
    public class AmazonWebSocketClientTest
    {
        [Test]
        public void TestCanonicalQueryString()
        {
            // given
            var accessKey = "access";
            var secretKey = "secret";
            var cred = new Credentials
            {
                AccessKeyId = accessKey,
                SecretKey = secretKey
            };

            var client = new AmazonWebSocketClient();


            var uri = new Uri("wss://a2qu7oonfd0b2x.iot.ap-southeast-2.amazonaws.com/mqtt");

            var region = RegionEndpoint.APSoutheast2;

            // when
            var signedUrl = client.GetCanonicalQueryString(cred, region);

            // then
            var expectedUrl = "&X-Amz-Algorithm=AWS4-HMAC-SHA256&X-Amz-Credential=access/20170201/ap-southeast";
            Console.WriteLine(signedUrl);
            Assert.AreEqual(expectedUrl, signedUrl);
        }
    }
}