using System;
using Amazon.Runtime;

namespace Amazon.WebSocket
{
    public interface IAmazonWebSocket: IAmazonService
    {
        void CreateWebSocketAsync(Uri url, Action<object> action);
    }


}