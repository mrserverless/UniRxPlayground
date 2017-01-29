using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using Amazon.Runtime.Internal;
using Amazon.Runtime.Internal.Auth;
using Amazon.SQS;

namespace Amazon.WebSocket
{
    public class AmazonWebSocketClient
    {
        public const string Iso8601DateTimeFormat = "yyyyMMddTHHmmssZ";
        public const string Iso8601DateFormat = "yyyyMMdd";

        private readonly string _awsSecretKey;
        private readonly string _service;
        private readonly string _region;


        private readonly AWS4Signer _signer;

        public AmazonWebSocketClient()
        {
            _signer = new AWS4Signer();
        }

        // http://docs.aws.amazon.com/general/latest/gr/sigv4-create-canonical-request.html
        private string SignRequest(string[] signedHeaders)
        {
            var method = "GET";
            var protocol = "wss";
            var uri = "/mqtt";
            var service = "iotdevicegateway";
            var algorithm = "AWS4-HMAC-SHA256";

            var request = new DefaultRequest(new CreateWebSocketRequest(), "mqtt");

            var clientConfig = new AmazonSQSConfig();

//            IDictionary<string, string> sortedHeaders = AWS4Signer.SortAndPruneHeaders((IEnumerable<KeyValuePair<string, string>>) request.Headers);
//            string canonicalRequest = AWS4Signer.CanonicalizeRequest(request.Endpoint, request.ResourcePath, request.HttpMethod, sortedHeaders, canonicalQueryString, precomputedBodyHash);


            _signer.SignRequest (request, clientConfig, null, "", "");

            return request.ToString();
//
//            var datetime = AWS.util.date.iso8601(new Date()).replace(/[:\-]|\.\d{3}/g, '');
//            var date = datetime.substr(0, 8);
//

//            var canonicalQuerystring = 'X-Amz-Algorithm=' + algorithm;
//            canonicalQuerystring += '&X-Amz-Credential=' + encodeURIComponent(credentials.accessKeyId + '/' + credentialScope);
//            canonicalQuerystring += '&X-Amz-Date=' + datetime;
//            canonicalQuerystring += '&X-Amz-SignedHeaders=host';
//
//            var canonicalHeaders = 'host:' + host + '\n';
//            var canonicalRequest = method + '\n' + uri + '\n' + canonicalQuerystring + '\n' + canonicalHeaders + '\nhost\n' + payloadHash;

//
//            var canonicalRequest = new StringBuilder();
//            canonicalRequest.AppendFormat("{0}\n", method);
//            canonicalRequest.AppendFormat("{0}\n", request.RequestUri.AbsolutePath);
//            canonicalRequest.AppendFormat("{0}\n", GetCanonicalQueryParameters(request.RequestUri.ParseQueryString()));
//            canonicalRequest.AppendFormat("{0}\n", GetCanonicalHeaders(request, signedHeaders));
//            canonicalRequest.AppendFormat("{0}\n", String.Join(";", signedHeaders));
//
//            // var payloadHash = AWS.util.crypto.sha256('', 'hex')
//            canonicalRequest.Append(GetPayloadHash(request));
//            return canonicalRequest.ToString();
        }

        // http://docs.aws.amazon.com/general/latest/gr/sigv4-create-string-to-sign.html
        private string GetStringToSign(DateTime requestDate, string canonicalRequest)
        {
//            var stringToSign = algorithm + '\n' + datetime + '\n' + credentialScope + '\n' + AWS.util.crypto.sha256(canonicalRequest, 'hex');

            var dateStamp = requestDate.ToString(Iso8601DateFormat, CultureInfo.InvariantCulture);

            //            var credentialScope = date + '/' + region + '/' + service + '/' + 'aws4_request';
            var credentialScope = string.Format("{0}/{1}/{2}/{3}", dateStamp, _region, _service, "aws4_request");

            var stringToSign = new StringBuilder();
            stringToSign.AppendFormat("AWS4-HMAC-SHA256\n{0}\n{1}\n",
                requestDate.ToString(Iso8601DateTimeFormat, CultureInfo.InvariantCulture),
                credentialScope);
            stringToSign.Append(Utils.ToHex(Utils.Hash(canonicalRequest)));
            return stringToSign.ToString();
        }

        #region Signing Key

        // http://docs.aws.amazon.com/general/latest/gr/signature-v4-examples.html#signature-v4-examples-dotnet
        static byte[] HmacSHA256(String data, byte[] key)
        {
            String algorithm = "HmacSHA256";
            KeyedHashAlgorithm kha = KeyedHashAlgorithm.Create(algorithm);
            kha.Key = key;

            return kha.ComputeHash(Encoding.UTF8.GetBytes(data));
        }

        static byte[] getSignatureKey(String key, String dateStamp, String regionName, String serviceName)
        {
            byte[] kSecret = Encoding.UTF8.GetBytes(("AWS4" + key).ToCharArray());
            byte[] kDate = HmacSHA256(dateStamp, kSecret);
            byte[] kRegion = HmacSHA256(regionName, kDate);
            byte[] kService = HmacSHA256(serviceName, kRegion);
            byte[] kSigning = HmacSHA256("aws4_request", kService);

            return kSigning;
        }

        #endregion Signing Key

        #region utils

        private static class Utils
        {
            public static byte[] Hash(string value)
            {
                return new SHA256CryptoServiceProvider().ComputeHash(Encoding.UTF8.GetBytes(value));
            }

            public static string ToHex(byte[] data)
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < data.Length; i++)
                {
                    sb.Append(data[i].ToString("x2", CultureInfo.InvariantCulture));
                }
                return sb.ToString();
            }
        }

        #endregion
    }
}