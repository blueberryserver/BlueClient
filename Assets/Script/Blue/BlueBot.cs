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
    string _ip = "";
    ushort _port = 0;
    float _updateDelayed = 0.0f;
    BlueUser _user;
    bool _isConnect;
    bool _isLogin;
    //bool _waitForReceive;
    Dictionary<State<BlueBot>, float> _actionRateDict;// = new Dictionary<State<BlueBot>, float>(); 

    public BlueBot(int id) : base(id)
    {
        Init();
    }

    public override void Init()
    {
        _stateMachine = new StateManager<BlueBot>(this);
        _stateMachine.SetCurrentState(null/*BlueBotConnect.Instance*/);
        _stateMachine.SetGlobalState(null/*BlueBotLogin::Instance()*/);
        // NetClient
        _netClient = NetClientManager.Instance.AddNetClient(_id);
        NetHanderInitializer.Instance.InitNetHandler(_netClient, this);
        _ip = "13.124.76.58";
        _port = 20000;
        _updateDelayed = 0.0f;
        _user = new BlueUser();
        //_location = E_LOCATION_TYPE.SHACK;
        _isConnect = false;
        _isLogin = false;
        //_waitForReceive = false;
        _actionRateDict = new Dictionary<State<BlueBot>, float>();
        //_actionRateDict.Add(MSG.MsgId.LOGIN_REQ, 1f);
        //_actionRateDict.Add(MSG.MsgId.REGIST_REQ, 1f);
        //_actionRateDict.Add(MSG.MsgId.CHAT_REQ, 0f);
        //_actionRateDict.Add(MSG.MsgId.CREATECHATROOM_REQ, 0f);
        //_actionRateDict.Add(MSG.MsgId.INVITECHATROOM_REQ, 0f);
        //_actionRateDict.Add(MSG.MsgId.ENTERCHATROOM_REQ, 0f);
        //_actionRateDict.Add(MSG.MsgId.LEAVECHATROOM_REQ, 0f);
        //_actionRateDict.Add(MSG.MsgId.CREATECHAR_REQ, 0f);
        _actionRateDict.Add(BlueBotPlayDungeon.Instance, 1f);
        _actionRateDict.Add(BlueBotLevelup.Instance, 1f);
        _actionRateDict.Add(BlueBotTierup.Instance, 1f);
    }

    public override void Update()
    {
        if (Time.time <= _updateDelayed)
        {
            return;
        }
        else
        {
            _updateDelayed = Time.time + UnityEngine.Random.Range(0.3f, 1.7f);
        }

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

    public void ChangeDelayedState(State<BlueBot> state)
    {
        _stateMachine.ChangeDelayedState(state);
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

    public float GetActionRate(State<BlueBot> state)
    {
        if (_actionRateDict.ContainsKey(state) == false)
        {
            return 0;
        }

        return _actionRateDict[state];
    }

    public State<BlueBot> SelectAction()
    {
        float totalRate = 0.0f;
        foreach (KeyValuePair<State<BlueBot>, float> pair in _actionRateDict)
        {
            totalRate += pair.Value;
        }

        float selectRate = UnityEngine.Random.Range(0f, totalRate);
        float sumRate = 0.0f;
        foreach (KeyValuePair<State<BlueBot>, float> pair in _actionRateDict)
        {
            sumRate += pair.Value;
            if (selectRate <= sumRate)
            {
                return pair.Key;
            }
        }

        return null;
    }

    public int GetCharCount()
    {
        if (_user._data.chars == null)
        {
            return 0;
        }

        return _user._data.chars.Count;
    }

    public int GetCharLevel(int slotNo)
    {
        if (_user._data.chars == null)
        {
            return 0;
        }

        foreach (var it in _user._data.chars)
        {
            if (it.slotNo == slotNo)
            {
                return (int)it.level;
            }
        }

        return 0;
    }

    public int GetCharTier(int slotNo)
    {
        if (_user._data.chars == null)
        {
            return 0;
        }

        foreach (var it in _user._data.chars)
        {
            if (it.slotNo == slotNo)
            {
                return (int)it.tier;
            }
        }

        return 0;
    }

    public List<MSG.CharData_> GetCharList()
    {
        List<MSG.CharData_> charList = new List<MSG.CharData_>();

        if (_user._data.chars == null)
        {
            return charList;
        }

        return _user._data.chars;
    }

    public void SetDisplayColor(Color color)
    {
        ControlPanel controlPanel = ControlPanelManager.Instance.FindControlPanel(_id);
        controlPanel.SetPanelImageColor(color);
    }
    public void SetDisplayText(string text)
    {
        ControlPanel controlPanel = ControlPanelManager.Instance.FindControlPanel(_id);
        controlPanel.SetTextDisplayUpdate(text);
    }
    public void Display()
    {
        string text = "";

        text += "id : " + _id + "\n";
        text += "name : " + _user._data.name + "\n";
        text += "vc1 : " + _user._data.vc1 + "\n";
        text += "vc2 : " + _user._data.vc2 + "\n";
        text += "vc3 : " + _user._data.vc3 + "\n";

        if (_user._data.chars != null && 0 < _user._data.chars.Count)
        {
            text += "slot no : " + _user._data.chars[0].slotNo + "\n";
            text += "level : " + _user._data.chars[0].level + "\n";
            text += "tier : " + _user._data.chars[0].tier + "\n";
        }

        if (_user._data.lastDungeon != null)
        {
            text += "dungeon no : " + _user._data.lastDungeon.dungeonNo + "\n";
            text += "dungeon tier : " + _user._data.lastDungeon.dungeonTier + "\n";
        }

        SetDisplayText(text);
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

    public void CreateChar(int charNo)
    {
        Debug.Log("CreateChar");

        if (_netClient.IsConnected() == false)
        {
            Debug.Log("Not connected");
            return;
        }

        MSG.CreateCharReq req = new MSG.CreateCharReq();
        req.charNo = (uint)charNo;
        _netClient.SendPacket(MSG.MsgId.CREATECHAR_REQ, req);
    }

    public bool PlayDungeon()
    {
        Debug.Log("PlayDungeon");

        if (_netClient.IsConnected() == false)
        {
            Debug.Log("Not connected");
            return false;
        }

        MSG.PlayDungeonReq req = new MSG.PlayDungeonReq();
        
        uint dungeonNo = 0;
        uint dungeonTier = 0;

        if (_user._data.lastDungeon != null)
        {
            dungeonNo = _user._data.lastDungeon.dungeonNo;
            dungeonTier = _user._data.lastDungeon.dungeonTier;
        }

        if (0 <= dungeonNo && dungeonNo < 10)
        {
            dungeonNo++;
        }
        else if (10 <= dungeonNo && 0 <= dungeonTier && dungeonTier < 9)
        {
            dungeonNo = 1;
            dungeonTier++;
        }
        else
        {
            Debug.Log("No more upgrade dungeon tier");
            return false;
        }

        req.dungeonNo = dungeonNo;
        req.tier = dungeonTier;
        return _netClient.SendPacket(MSG.MsgId.PLAYDUNGEON_REQ, req);
    }

    public void LevelupChar(int slotNo)
    {
        Debug.Log("LevelupChar");

        if (_netClient.IsConnected() == false)
        {
            Debug.Log("Not connected");
            return;
        }

        MSG.LevelupCharReq req = new MSG.LevelupCharReq();
        req.slotNo = (uint)slotNo;
        _netClient.SendPacket(MSG.MsgId.LEVELUPCHAR_REQ, req);
    }

    public void TierupChar(int slotNo)
    {
        Debug.Log("TierupChar");

        if (_netClient.IsConnected() == false)
        {
            Debug.Log("Not connected");
            return;
        }

        MSG.TierupCharReq req = new MSG.TierupCharReq();
        req.slotNo = (uint)slotNo;
        _netClient.SendPacket(MSG.MsgId.TIERUPCHAR_REQ, req);
    }

    public void OnConnected(NetClient netClient, SocketError errorCode)
    {
        Debug.Log("OnConnected : " + errorCode);
        //textDisplayUpdate = "OnConnected : " + errorCode;

        if (SocketError.Success != errorCode)
        {
            return;
        }

        int id = NetClientManager.Instance.FindID(netClient);
        string name = "user" + id;

        _user._data.name = name;

        _isConnect = true;
        //_waitForReceive = false;
        //Regist(netClient, name);
        //Login(netClient, name);
    }
    public void OnClosed(NetClient netClient)
    {
        BlueUserManager.Instance.Remove(_user._sessionKey);
        _isConnect = false;
        _isLogin = false;
    }
    public void OnMessage_Login_Ans(NetClient netClient, MemoryStream stream)
    {
        Debug.Log("OnMessage_Login_Ans");
        //textDisplayUpdate = "OnMessage_Login_Ans";

        MSG.LoginAns ans = ProtoBuf.Serializer.Deserialize<MSG.LoginAns>(stream);

        if (MSG.ErrorCode.ERR_SUCCESS == ans.err)
        {
            _user._data = ans.data;
            _user._session = netClient;
            _user._sessionKey = ans.sessionKey;
            BlueUserManager.Instance.Add(_user);

            SetDisplayColor(Color.green);
            Display();
            _isLogin = true;
        }
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
        MSG.CreateCharAns ans = ProtoBuf.Serializer.Deserialize<MSG.CreateCharAns>(stream);

        Debug.Log("OnMessage_CreateChar_Ans : " + ans.err);

        // ans 처리
        if (MSG.ErrorCode.ERR_SUCCESS == ans.err)
        {
            if (_user._data.chars != null)
            {
                _user._data.chars.Add(ans.char_);
            }
        }

        // state 처리
        MessageDispatcher.Instance.DispatchMessage(_id, _id, (int)MSG.MsgId.CREATECHAR_ANS, 0, ans);
    }
    public void OnMessage_Contents_Not(NetClient netClient, MemoryStream stream)
    {
    }
    public void OnMessage_Currency_Not(NetClient netClient, MemoryStream stream)
    {

    }
    public void OnMessage_PlayDungeon_Ans(NetClient netClient, MemoryStream stream)
    {
        MSG.PlayDungeonAns ans = ProtoBuf.Serializer.Deserialize<MSG.PlayDungeonAns>(stream);

        Debug.Log("OnMessage_PlayDungeon_Ans : " + ans.err);
        
        // ans 처리
        if (MSG.ErrorCode.ERR_SUCCESS == ans.err)
        {
            uint dungeonNo = 0;
            uint dungeonTier = 0;

            if (_user._data.lastDungeon != null)
            {
                dungeonNo = _user._data.lastDungeon.dungeonNo;
                dungeonTier = _user._data.lastDungeon.dungeonTier;
            }

            if (0 <= dungeonNo && dungeonNo < 10)
            {
                dungeonNo++;
            }
            else if (10 <= dungeonNo && 0 <= dungeonTier && dungeonTier < 9)
            {
                dungeonNo = 1;
                dungeonTier++;
            }
            else
            {
                //Debug.Log("No more upgrade dungeon tier");
                //dungeonNo
            }

            if (_user._data.lastDungeon == null)
            {
                _user._data.lastDungeon = new MSG.DungeonData_();
            }

            _user._data.lastDungeon.dungeonNo = dungeonNo;
            _user._data.lastDungeon.dungeonTier = dungeonTier;
        }

        Display();

        // state 처리
        MessageDispatcher.Instance.DispatchMessage(_id, _id, (int)MSG.MsgId.PLAYDUNGEON_ANS, 0, ans);
    }
    public void OnMessage_PlayDungeon_Not(NetClient netClient, MemoryStream stream)
    {
    }
    public void OnMessage_LevelUpChar_Ans(NetClient netClient, MemoryStream stream)
    {
        MSG.LevelupCharAns ans = ProtoBuf.Serializer.Deserialize<MSG.LevelupCharAns>(stream);

        Debug.Log("OnMessage_LevelUpChar_Ans : " + ans.err);

        // ans 처리
        if (MSG.ErrorCode.ERR_SUCCESS == ans.err)
        {
            int slotNo = (int)ans.char_.slotNo;
            _user._data.chars[slotNo] = ans.char_;
        }

        Display();

        // state 처리
        MessageDispatcher.Instance.DispatchMessage(_id, _id, (int)MSG.MsgId.LEVELUPCHAR_ANS, 0, ans);
    }
    public void OnMessage_TierUpChar_Ans(NetClient netClient, MemoryStream stream)
    {
        MSG.TierupCharAns ans = ProtoBuf.Serializer.Deserialize<MSG.TierupCharAns>(stream);

        Debug.Log("OnMessage_TierUpChar_Ans : " + ans.err);

        // ans 처리
        if (MSG.ErrorCode.ERR_SUCCESS == ans.err)
        {
            int slotNo = (int)ans.char_.slotNo;
            _user._data.chars[slotNo] = ans.char_;
        }

        Display();

        // state 처리
        MessageDispatcher.Instance.DispatchMessage(_id, _id, (int)MSG.MsgId.TIERUPCHAR_ANS, 0, ans);
    }

    public void OnButton()
    {
        Debug.Log("OnButton!!! : " + _id);
    }
}