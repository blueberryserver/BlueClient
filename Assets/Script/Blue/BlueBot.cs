using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System.IO;
using System.Net.Sockets;
using System;

public class BlueBot : BaseGameEntity, NetHandler
{
    StateManager<BlueBot> _stateMachine = null;
    //E_LOCATION_TYPE _location;
    NetClient _netClient;
    string _ip = "13.124.76.58";
    ushort _port = 20000;
    BlueUser _user = new BlueUser();
    bool _isConnect;
    bool _isLogin;
    //bool _waitForReceive;

    public BlueBot(int id) : base(id)
    {
        _stateMachine = new StateManager<BlueBot>(this);
        _stateMachine.SetCurrentState(null/*BlueBotConnect.Instance*/);
        _stateMachine.SetGlobalState(null/*BlueBotLogin::Instance()*/);
        _netClient = NetClientManager.Instance.FindNetClient(id);
        //_location = E_LOCATION_TYPE.SHACK;
        _isConnect = false;
        _isLogin = false;
        //_waitForReceive = false;
    }

    public override void Update()
    {
        if (_stateMachine != null)
        {
            _stateMachine.Update();
        }
    }

    public override bool HandleMessage(ref Telegram msg)
    {
        return _stateMachine.HandleMessage(ref msg);
    }

    public void ChangeState(State<BlueBot> state)
    {
        _stateMachine.ChangeState(state);
    }

    public void RevertToPreviousState()
    {
        _stateMachine.RevertToPreviousState();
    }

    public void SetUser(BlueUser user)
    {
        _user = user;
    }

    public void SetConnect(bool isConnect)
    {
        _isConnect = isConnect;
    }

    public bool IsConnect()
    {
        return _isConnect;
    }

    public void SetLogin(bool isLogin)
    {
        _isLogin = isLogin;
    }

    public bool IsLogin()
    {
        return _isLogin;
    }

    //public void SetWaitForReceive(bool waitForReceive)
    //{
    //    _waitForReceive = waitForReceive;
    //}

    //public bool IsWaitForReceive()
    //{
    //    return _waitForReceive;
    //}









    public void Connect()
    {
        if (_netClient)
        {
            _netClient.AsyncConnect(_ip, _port);
        }
    }

    public void Login()
    {
        Debug.Log("Login");

        if (_netClient.IsConnected() == false)
        {
            Debug.Log("Not connected");
            return;
        }

        MSG.LoginReq req = new MSG.LoginReq();
        req.name = _user._data.name;
        _netClient.SendPacket(MSG.MsgId.LOGIN_REQ, req);
    }

    public void OnConnected(NetClient netClient, SocketError errorCode)
    {
        Debug.Log("OnConnected : " + errorCode);
        //textDisplayUpdate = "OnConnected : " + errorCode;

        int key = NetClientManager.Instance.FindKey(netClient);
        string name = "user" + key;

        _user._data.name = name;

        _isConnect = true;
        //_waitForReceive = false;
        //Regist(netClient, name);
        //Login(netClient, name);
    }
    public void OnClosed(NetClient netClient)
    {

    }
    public void OnMessage_Login_Ans(NetClient netClient, MemoryStream stream)
    {
        Debug.Log("OnMessage_Login_Ans");
        //textDisplayUpdate = "OnMessage_Login_Ans";

        MSG.LoginAns ans = ProtoBuf.Serializer.Deserialize<MSG.LoginAns>(stream);

        BlueUser user = new BlueUser();
        user._data = ans.data;
        user._session = netClient;
        user._sessionKey = ans.sessionKey;
        BlueUserManager.Instance.Add(user);

        int key = NetClientManager.Instance.FindKey(netClient);
        ControlPanel controlPanel = ControlPanelManager.Instance.FindControlPanel(key);
        controlPanel.SetPanelImageColor(Color.green);
        //controlPanel.SetTextDisplayUpdate("abcd");
    }
    public void OnMessage_Pong_Ans(NetClient netClient, MemoryStream stream)
    {
    }
    public void OnMessage_Regist_Ans(NetClient netClient, MemoryStream stream)
    {
    }
    public void OnMessage_Version_Ans(NetClient netClient, MemoryStream stream)
    {
    }
    public void OnMessage_Chat_Ans(NetClient netClient, MemoryStream stream)
    {
    }
    public void OnMessage_Chat_Not(NetClient netClient, MemoryStream stream)
    {
    }
    public void OnMessage_CreateChatRoom_Ans(NetClient netClient, MemoryStream stream)
    {
    }
    public void OnMessage_CreateChatRoom_Not(NetClient netClient, MemoryStream stream)
    {
    }
    public void OnMessage_InviteChatRoom_Ans(NetClient netClient, MemoryStream stream)
    {
    }
    public void OnMessage_InviteChatRoom_Not(NetClient netClient, MemoryStream stream)
    {
    }
    public void OnMessage_EnterChatRoom_Ans(NetClient netClient, MemoryStream stream)
    {
    }
    public void OnMessage_EnterChatRoom_Not(NetClient netClient, MemoryStream stream)
    {
    }
    public void OnMessage_LeaveChatRoom_Ans(NetClient netClient, MemoryStream stream)
    {
    }
    public void OnMessage_LeaveChatRoom_Not(NetClient netClient, MemoryStream stream)
    {
    }
    public void OnMessage_CreateChar_Ans(NetClient netClient, MemoryStream stream)
    {
    }
    public void OnMessage_Contents_Not(NetClient netClient, MemoryStream stream)
    {
    }
    public void OnMessage_Currency_Not(NetClient netClient, MemoryStream stream)
    {

    }
    public void OnMessage_PlayDungeon_Ans(NetClient netClient, MemoryStream stream)
    {
    }
    public void OnMessage_PlayDungeon_Not(NetClient netClient, MemoryStream stream)
    {
    }
    public void OnMessage_LevelUpChar_Ans(NetClient netClient, MemoryStream stream)
    {
    }
    public void OnMessage_TierUpChar_Ans(NetClient netClient, MemoryStream stream)
    {
    }
}