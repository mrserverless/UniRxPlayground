using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;

public class WebsocketSharpClient : IWebSocketClient {

    public WebsocketSharpClient(string url)
    {
        using (var ws = new WebSocket (url)) {
            ws.OnMessage += (sender, e) =>
                Console.WriteLine ("Laputa says: " + e.Data);

            ws.Connect ();
            ws.Send ("BALUS");
            Console.ReadKey (true);
        }
    }

}
