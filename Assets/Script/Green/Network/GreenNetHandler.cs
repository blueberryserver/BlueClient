using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System.IO;
using System.Net.Sockets;
using System;

public interface GreenNetHandler
{
    void OnConnected(NetClient netClient, SocketError errorCode);
    void OnClosed(NetClient netClient);
    void OnMessage_Think_Ans(NetClient netClient, MemoryStream stream);
}

public class GreenNetHanderInitializer
{
    private static GreenNetHanderInitializer _instance = null;
    public static GreenNetHanderInitializer Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GreenNetHanderInitializer();
                if (_instance == null)
                {
                    //_instance = FindObjectOfType(typeof(NetHanderInitializer)) as NetHanderInitializer;
                }
            }

            return _instance;
        }
    }
    public void InitNetHandler(NetClient netClient, GreenNetHandler netHandler)
    {
        // set handler    
        netClient.SetOnConnected(netHandler.OnConnected);
        netClient.SetOnClosed(netHandler.OnClosed);
        netClient.AddPacketHandler((ushort)MSG.MsgId.THINK_ANS, netHandler.OnMessage_Think_Ans);
    }
}