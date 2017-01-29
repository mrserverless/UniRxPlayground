using Amazon.Runtime;
using Amazon.SQS;
using Amazon.Util.Internal;

namespace Amazon.WebSocket
{
    public class AmazonWebSocketConfig: ClientConfig
    {
        private static readonly string UserAgentString = InternalSDKUtils.BuildUserAgentString("3.3.1.7");
        private string _userAgent = AmazonWebSocketConfig.UserAgentString;

        public override string ServiceVersion
        {
            get { throw new System.NotImplementedException(); }
        }

        public override string UserAgent
        {
            get { throw new System.NotImplementedException(); }
        }

        public override string RegionEndpointServiceName
        {
            get { throw new System.NotImplementedException(); }
        }
    }
}