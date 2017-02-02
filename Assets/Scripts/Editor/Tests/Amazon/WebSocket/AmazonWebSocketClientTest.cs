using System;
using System.Globalization;
using Amazon.CognitoIdentity.Model;
using NUnit.Framework;

namespace Amazon.WebSocket
{
    public class AmazonWebSocketClientTest
    {
        [Test]
        public void TestCanonicalRequest()
        {
            // given
            var client = new AmazonWebSocketClient();
            var signedAt = DateTime.UtcNow;
            var queryString = "&X-Amz-Algorithm=AWS4-HMAC-SHA256" +
                              "&X-Amz-Credential=access/20170201/ap-southeast-2/iotdevicegateway/aws4_request" +
                              "&X-Amz-Date=" +
                              signedAt.ToString(AmazonWebSocketClient.ISO8601BasicDateTimeFormat,
                                  CultureInfo.InvariantCulture) + "&X-Amz-Expires=86400" +
                              "&X-Amz-SignedHeaders=host";
            var headers = "host:host";

            // when
            var request = client.GetCanonicalRequest(queryString, headers, TODO);

            // then
            var expectedRequest = "GET\n" +
                                  "/mqtt\n" +
                                  queryString + "\n" +
                                  headers + "\n";
            Assert.AreEqual(expectedRequest, request);
        }

        [Test]
        public void TestCanonicalQueryString()
        {
            // given
            var client = new AmazonWebSocketClient();
            var region = RegionEndpoint.APSoutheast2;
            var signedAt = DateTime.UtcNow;

            // when
            var signedUrl = client.GetCanonicalQueryString(GivenCredentials(), region, signedAt);

            // then
            var expectedUrl = "&X-Amz-Algorithm=AWS4-HMAC-SHA256" +
                              "&X-Amz-Credential=access/20170201/ap-southeast-2/iotdevicegateway/aws4_request" +
                              "&X-Amz-Date=" +
                              signedAt.ToString(AmazonWebSocketClient.ISO8601BasicDateTimeFormat,
                                  CultureInfo.InvariantCulture) + "&X-Amz-Expires=86400" +
                              "&X-Amz-SignedHeaders=host";
            Assert.AreEqual(expectedUrl, signedUrl);
        }

        public Credentials GivenCredentials()
        {
            var accessKey = "access";
            var secretKey = "secret";
            return new Credentials {AccessKeyId = accessKey, SecretKey = secretKey};
        }
    }
}