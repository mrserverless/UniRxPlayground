using Amazon.Runtime;
using Amazon.Runtime.Internal;

namespace Amazon.WebSocket
{
    public class UpgradeWebSocketRequest: AmazonWebServiceRequest
    {
        public UpgradeWebSocketRequest()
        {
            ((IAmazonWebServiceRequest) this).UseSigV4 = true;
        }
    }
}