using System;
using Amazon.CognitoIdentity.Model;
using Amazon.Runtime;
using Amazon.Runtime.Internal;
using Amazon.Runtime.Internal.Auth;
using UniRx.WebSocket;

namespace Amazon.WebSocket
{
    public class AmazonWebSocketClient
    {
        private const string ServiceName = "iotdevicegateway";

        private readonly AWS4Signer _signer;

        private readonly IClientConfig _config;
        private readonly AWSCredentials _credentials;

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
            _signer.SignRequest(request, _config, null, _credentials.GetCredentials().AccessKey,
                _credentials.GetCredentials().SecretKey);

//            DateTime signedAt = AWS4Signer.InitializeHeaders(request.Headers, request.Endpoint);
//            string service = AWS4Signer.DetermineService(clientConfig);
//            string signingRegion = AWS4Signer.DetermineSigningRegion(clientConfig, service, request.AlternateEndpoint, request);
//            string canonicalQueryString = AWS4Signer.CanonicalizeQueryParameters(AWS4Signer.GetParametersToCanonicalize(request));
//            string precomputedBodyHash = AWS4Signer.SetRequestBodyHash(request);
//            IDictionary<string, string> sortedHeaders = AWS4Signer.SortAndPruneHeaders((IEnumerable<KeyValuePair<string, string>>) request.Headers);
//            string canonicalRequest = AWS4Signer.CanonicalizeRequest(request.Endpoint, request.ResourcePath, request.HttpMethod, sortedHeaders, canonicalQueryString, precomputedBodyHash);
//            if (metrics != null)
//                metrics.AddProperty(Metric.CanonicalRequest, (object) canonicalRequest);
//            return AWS4Signer.ComputeSignature(awsAccessKeyId, awsSecretAccessKey, signingRegion, signedAt, service, AWS4Signer.CanonicalizeHeaderNames((IEnumerable<KeyValuePair<string, string>>) sortedHeaders), canonicalRequest, metrics);

            return request.ToString();
        }

        public AmazonWebSocketClient(AWSCredentials credentials, RegionEndpoint region)
        {
            _credentials = credentials;
//            _config = new AmazonWebSocketConfig {RegionEndpoint = region};
            _signer = new AWS4Signer();
        }

        public AmazonWebSocketClient(string awsAccessKeyId, string awsSecretAccessKey, RegionEndpoint region)
            : this(new BasicAWSCredentials(awsAccessKeyId, awsSecretAccessKey), region)
        {
        }
    }
}