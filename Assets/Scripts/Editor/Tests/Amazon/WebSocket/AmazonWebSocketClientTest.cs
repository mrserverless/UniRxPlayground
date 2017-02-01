using System;
using System.Globalization;
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
            var cred = new Credentials{AccessKeyId = accessKey,SecretKey = secretKey};
            var client = new AmazonWebSocketClient();
            var region = RegionEndpoint.APSoutheast2;
            var signedAt = DateTime.UtcNow;

            // when
            var signedUrl = client.GetCanonicalQueryString(cred, region, signedAt);

            // then
            var expectedUrl = "&X-Amz-Algorithm=AWS4-HMAC-SHA256"+
                              "&X-Amz-Credential=access/20170201/ap-southeast-2/iotdevicegateway/aws4_request"+
                              "&X-Amz-Date=" + signedAt.ToString(AmazonWebSocketClient.ISO8601BasicDateTimeFormat, CultureInfo.InvariantCulture) + "&X-Amz-Expires=86400"+
                              "&X-Amz-SignedHeaders=host";
            Assert.AreEqual(expectedUrl, signedUrl);
        }
    }
}