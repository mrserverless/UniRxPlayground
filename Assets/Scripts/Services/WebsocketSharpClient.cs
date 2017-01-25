using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;
using WebSocketSharp;

public class WebsocketSharpClient : IWebSocketClient
{
    private string url = "wss://a2qu7oonfd0b2x.iot.ap-southeast-2.amazonaws.com/mqtt";

    public WebsocketSharpClient()
    {
        using (var ws = new WebSocket (url)) {
            ws.OnMessage += (sender, e) =>
                Console.WriteLine ("Laputa says: " + e.Data);

            ws.Connect ();
            ws.Send ("BALUS");
            Console.ReadKey (true);
        }
    }

    public void Send(byte[] data)
    {
        throw new NotImplementedException();
    }
}
