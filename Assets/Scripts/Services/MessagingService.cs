using System;
using Amazon;
using Amazon.CognitoIdentity;
using Amazon.Runtime;
using Amazon.Runtime.Internal.Auth;
using Amazon.SQS;
using Amazon.WebSocket;
using UnityEngine;

namespace Services
{
    public class MessagingService
    {
        //identity pool id for cognito credentials
        public string IdentityPoolId = "";

        public string CognitoIdentityRegion = RegionEndpoint.USEast1.SystemName;

        private RegionEndpoint _CognitoIdentityRegion
        {
            get { return RegionEndpoint.GetBySystemName(CognitoIdentityRegion); }
        }

        public string SQSRegion = RegionEndpoint.USEast1.SystemName;

        private RegionEndpoint _SQSRegion
        {
            get { return RegionEndpoint.GetBySystemName(SQSRegion); }
        }

        //name of the queue you want to create
        public Uri Url = new Uri("AWS_SQS_EXAMPLE_QUEUE");

        private AWSCredentials _credentials;

        private AWSCredentials Credentials
        {
            get
            {
                if (_credentials == null)
                    _credentials = new CognitoAWSCredentials(IdentityPoolId, _CognitoIdentityRegion);
                return _credentials;
            }
        }

        private IAmazonWebSocket _wsClient;

    }
}
