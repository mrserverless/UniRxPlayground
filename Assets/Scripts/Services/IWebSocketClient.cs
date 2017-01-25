using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public interface IWebSocketClient
{
    void Send(byte[] data);
}