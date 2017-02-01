using System;
using System.Collections.Specialized;
using System.Globalization;
using System.Text;
using Amazon.CognitoIdentity.Model;
using Amazon.Runtime;
using Amazon.Runtime.Internal;
using Amazon.Util;

namespace Amazon.WebSocket
{
    public class AmazonWebSocketClient
    {
        public const string Algorithm = "AWS4-HMAC-SHA256";

        public const string ISO8601BasicDateTimeFormat = "yyyyMMddTHHmmssZ";
        public const string ISO8601BasicDateFormat = "yyyyMMdd";
        public const string Service = "iotdevicegateway";
        public const string Terminator = "aws4_request";

        public string ComputeSignature(Uri host, Credentials credentials, RegionEndpoint region)
        {

            var signedAt = DateTime.UtcNow;

            var canonicalQueryString = GetCanonicalQueryString(credentials, region, signedAt);
            GetCanonicalRequest(canonicalQueryString);
            return "";
        }

        // http://docs.aws.amazon.com/general/latest/gr/sigv4-create-canonical-request.html
        private static string GetCanonicalRequest(string canonicalQueryString)
        {
            const string method = "GET";
            const string canonicalUri = "/mqtt";


            var canonicalRequest = new StringBuilder()
                .AppendFormat("{0}\n", method)
                .AppendFormat("{0}\n", canonicalUri)
                .AppendFormat("{0}\n", canonicalQueryString);
//            canonicalRequest.AppendFormat("{0}\n", GetCanonicalHeaders(request, signedHeaders));
//            canonicalRequest.AppendFormat("{0}\n", String.Join(";", signedHeaders));
//            canonicalRequest.Append(GetPayloadHash(request));
            return canonicalRequest.ToString();
        }

        public string GetCanonicalQueryString(Credentials credentials, RegionEndpoint region, DateTime signedAt)
        {
            var amzdate = signedAt.ToString(ISO8601BasicDateTimeFormat, CultureInfo.InvariantCulture);

            var dateStamp = signedAt.ToString(AWSSDKUtils.ISO8601BasicDateFormat, CultureInfo.InvariantCulture);
            var scope = string.Format(CultureInfo.InvariantCulture, "{0}/{1}/{2}/{3}", dateStamp, region.SystemName,
                Service, Terminator);

            var secretKey = credentials.SecretKey;
            var accessKey = credentials.AccessKeyId;

            var queryString = new StringBuilder()
                .Append("&X-Amz-Algorithm=" + Algorithm)
                .Append("&X-Amz-Credential=" + accessKey + '/' + scope)
                .Append("&" + HeaderKeys.XAmzDateHeader + "=" + amzdate)
                .Append("&X-Amz-Expires=86400")
                .Append("&" + HeaderKeys.XAmzSignedHeadersHeader + "=host");

            return queryString.ToString();
        }
    }

    static class Utils
    {
        private const string ValidUrlCharacters =
            "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_.~";

        public static string UrlEncode(string data)
        {
            StringBuilder encoded = new StringBuilder();
            foreach (char symbol in Encoding.UTF8.GetBytes(data))
            {
                if (ValidUrlCharacters.IndexOf(symbol) != -1)
                {
                    encoded.Append(symbol);
                }
                else
                {
                    encoded.Append("%").Append(string.Format(CultureInfo.InvariantCulture, "{0:X2}", (int) symbol));
                }
            }
            return encoded.ToString();
        }
    }
}