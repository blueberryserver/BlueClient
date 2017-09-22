using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System.IO;
using System.Net.Sockets;
using System;

public interface NetHandler
{
    void OnConnected(NetClient netClient, SocketError errorCode);
    void OnClosed(NetClient netClient);
    void OnMessage_Login_Ans(NetClient netClient, MemoryStream stream);
    void OnMessage_Pong_Ans(NetClient netClient, MemoryStream stream);
    void OnMessage_Regist_Ans(NetClient netClient, MemoryStream stream);
    void OnMessage_Version_Ans(NetClient netClient, MemoryStream stream);
    void OnMessage_Chat_Ans(NetClient netClient, MemoryStream stream);
    void OnMessage_Chat_Not(NetClient netClient, MemoryStream stream);
    void OnMessage_CreateChatRoom_Ans(NetClient netClient, MemoryStream stream);
    void OnMessage_CreateChatRoom_Not(NetClient netClient, MemoryStream stream);
    void OnMessage_InviteChatRoom_Ans(NetClient netClient, MemoryStream stream);
    void OnMessage_InviteChatRoom_Not(NetClient netClient, MemoryStream stream);
    void OnMessage_EnterChatRoom_Ans(NetClient netClient, MemoryStream stream);
    void OnMessage_EnterChatRoom_Not(NetClient netClient, MemoryStream stream);
    void OnMessage_LeaveChatRoom_Ans(NetClient netClient, MemoryStream stream);
    void OnMessage_LeaveChatRoom_Not(NetClient netClient, MemoryStream stream);
    void OnMessage_CreateChar_Ans(NetClient netClient, MemoryStream stream);
    void OnMessage_Contents_Not(NetClient netClient, MemoryStream stream);
    void OnMessage_Currency_Not(NetClient netClient, MemoryStream stream);
    void OnMessage_PlayDungeon_Ans(NetClient netClient, MemoryStream stream);
    void OnMessage_PlayDungeon_Not(NetClient netClient, MemoryStream stream);
    void OnMessage_LevelUpChar_Ans(NetClient netClient, MemoryStream stream);
    void OnMessage_TierUpChar_Ans(NetClient netClient, MemoryStream stream);
}

public class NetHanderInitializer
{
    private static NetHanderInitializer _instance = null;
    public static NetHanderInitializer Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new NetHanderInitializer();
                if (_instance == null)
                {
                    //_instance = FindObjectOfType(typeof(NetHanderInitializer)) as NetHanderInitializer;
                }
            }

            return _instance;
        }
    }
    public void InitNetHandler(NetClient netClient, NetHandler netHandler)
    {
        // set handler    
        netClient.SetOnConnected(netHandler.OnConnected);
        netClient.SetOnClosed(netHandler.OnClosed);
        netClient.AddPacketHandler((ushort)MSG.MsgId.LOGIN_ANS, netHandler.OnMessage_Login_Ans);
        netClient.AddPacketHandler((ushort)MSG.MsgId.PONG_ANS, netHandler.OnMessage_Pong_Ans);
        netClient.AddPacketHandler((ushort)MSG.MsgId.REGIST_ANS, netHandler.OnMessage_Regist_Ans);
        netClient.AddPacketHandler((ushort)MSG.MsgId.VERSION_ANS, netHandler.OnMessage_Version_Ans);
        netClient.AddPacketHandler((ushort)MSG.MsgId.CHAT_ANS, netHandler.OnMessage_Chat_Ans);
        netClient.AddPacketHandler((ushort)MSG.MsgId.CHAT_NOT, netHandler.OnMessage_Chat_Not);
        netClient.AddPacketHandler((ushort)MSG.MsgId.CREATECHATROOM_ANS, netHandler.OnMessage_CreateChatRoom_Ans);
        netClient.AddPacketHandler((ushort)MSG.MsgId.CREATECHATROOM_NOT, netHandler.OnMessage_CreateChatRoom_Not);
        netClient.AddPacketHandler((ushort)MSG.MsgId.INVITECHATROOM_ANS, netHandler.OnMessage_InviteChatRoom_Ans);
        netClient.AddPacketHandler((ushort)MSG.MsgId.INVITECHATROOM_NOT, netHandler.OnMessage_InviteChatRoom_Not);
        netClient.AddPacketHandler((ushort)MSG.MsgId.ENTERCHATROOM_ANS, netHandler.OnMessage_EnterChatRoom_Ans);
        netClient.AddPacketHandler((ushort)MSG.MsgId.ENTERCHATROOM_NOT, netHandler.OnMessage_EnterChatRoom_Not);
        netClient.AddPacketHandler((ushort)MSG.MsgId.LEAVECHATROOM_ANS, netHandler.OnMessage_LeaveChatRoom_Ans);
        netClient.AddPacketHandler((ushort)MSG.MsgId.LEAVECHATROOM_NOT, netHandler.OnMessage_LeaveChatRoom_Not);
        netClient.AddPacketHandler((ushort)MSG.MsgId.CREATECHAR_ANS, netHandler.OnMessage_CreateChar_Ans);
        netClient.AddPacketHandler((ushort)MSG.MsgId.CONTENTS_NOT, netHandler.OnMessage_Contents_Not);
        netClient.AddPacketHandler((ushort)MSG.MsgId.CURRENCY_NOT, netHandler.OnMessage_Currency_Not);
        netClient.AddPacketHandler((ushort)MSG.MsgId.PLAYDUNGEON_ANS, netHandler.OnMessage_PlayDungeon_Ans);
        netClient.AddPacketHandler((ushort)MSG.MsgId.PLAYDUNGEON_NOT, netHandler.OnMessage_PlayDungeon_Not);
        netClient.AddPacketHandler((ushort)MSG.MsgId.LEVELUPCHAR_ANS, netHandler.OnMessage_LevelUpChar_Ans);
        netClient.AddPacketHandler((ushort)MSG.MsgId.TIERUPCHAR_ANS, netHandler.OnMessage_TierUpChar_Ans);
    }
}