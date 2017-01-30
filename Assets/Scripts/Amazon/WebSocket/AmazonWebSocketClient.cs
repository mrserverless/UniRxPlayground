using System;
using Amazon.Runtime;
using Amazon.Runtime.Internal;

namespace Amazon.WebSocket
{
    public class AmazonWebSocketClient
    {
        private const string ServiceName = "iotdevicegateway";

        private readonly AWS4UnitySigner _unitySigner;

        private readonly IClientConfig _config;
        private readonly AWSCredentials _credentials;


        public AmazonWebSocketClient(AWSCredentials credentials, RegionEndpoint region)
        {
            _credentials = credentials;
            _config = new AmazonWebSocketConfig {RegionEndpoint = region};
            _unitySigner = new AWS4UnitySigner();
        }

        public AmazonWebSocketClient(string awsAccessKeyId, string awsSecretAccessKey, RegionEndpoint region)
            : this(new BasicAWSCredentials(awsAccessKeyId, awsSecretAccessKey), region)
        {
        }

        public string SignRequest(Uri endpoint)
        {
            var uri = "/mqtt";
            var algorithm = "AWS4-HMAC-SHA256";

            var request = new DefaultRequest(new UpgradeWebSocketRequest(), ServiceName)
            {
                HttpMethod = "GET",
                UseSigV4 = true,
                Endpoint = endpoint
            };

            _unitySigner.SignRequest(request, _config, null, _credentials.GetCredentials().AccessKey,
                _credentials.GetCredentials().SecretKey);

            return request.ToString();
        }
    }
}