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
    void OnConnected(SocketError errorCode);
    void OnClosed();
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

public class NetData
{
    int _userMax = 0;
    int _userCurrIndex = 0;
    List<string> _userNameList = new List<string>();
    string _ip = "";
    ushort _port = 0;

    private static NetData _instance = null;
    public static NetData Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new NetData();
            }

            return _instance;
        }
    }

    public NetData()
    {
        _userMax = 500;
        for (int i = 0; i < _userMax; i++)
        {
            _userNameList.Add("iii" + i);
        }
        _ip = "13.124.76.58";
        _port = 20000;
    }

    public int GetUserCount()
    {
        return _userNameList.Count;
    }

    public string GetUserName()
    {
        int index = _userCurrIndex;
        _userCurrIndex++;
        return _userNameList[index];
    }
    public string GetIP()
    {
        return _ip;
    }

    public ushort GetPort()
    {
        return _port;
    }
}

public class NetControlPanel : MonoBehaviour, NetHandler
{
    NetClient _netClient = null;
    string _sessionKey = "";
    public NetClient netClient
    {
        get
        {
            return _netClient;
        }
        set
        {
            _netClient = value;
        }
    }
    Text _textDisplay = null;
    public Text textDisplay
    {
        get
        {
            return _textDisplay;
        }
        set
        {
            _textDisplay = value;
        }
    }
    string textDisplayUpdate = "";
    bool _isLogin = false;
    Image _image = null;
    List<InputField> _inputFieldList = new List<InputField>();

    string GetInputFieldText(string name)
    {
        foreach (InputField inputFIend in _inputFieldList)
        {
            if (inputFIend.name == name)
            {
                return inputFIend.text;
            }
        }

        return null;
    }

    void Start()
    {
        //_netClient.AsyncConnect(NetData.Instance.GetIP(), NetData.Instance.GetPort());
    }

    void Update()
    {
        if (_textDisplay)
        {
            _textDisplay.text = textDisplayUpdate;
        }

        if (_image == null)
        {
            _image = GetComponent<Image>();
            _image.color = Color.red;
        }
        else
        {
            _image.color = _isLogin ? Color.green : Color.red;
        }
    }

    public void SetTextDisplay(Text textDisplay)
    {
        _textDisplay = textDisplay;
    }

    public void AddInputField(InputField inputField)
    {
        _inputFieldList.Add(inputField);
    }

    public void OnButtonLogin()
    {
        Debug.Log("OnButtonLogin");
        //_netClient.AsyncConnect(NetData.Instance.GetIP(), NetData.Instance.GetPort());
        _netClient.AsyncConnect("13.124.76.58", 20000);
        //image.color = Color.red;
    }

    public void OnButtonLogout()
    {
        Debug.Log("OnButtonLogout");
        _netClient.Close();
    }

    public void OnButtonPlayDungeonReq()
    {
        Debug.Log("OnButtonPlayDungeonReq(no : " + GetInputFieldText("PlayDungeonReq.dungeonNo") + ", tier : " + GetInputFieldText("PlayDungeonReq.tier") + ")");
        textDisplayUpdate = "OnButtonPlayDungeonReq(no : " + GetInputFieldText("PlayDungeonReq.dungeonNo") + ", tier : " + GetInputFieldText("PlayDungeonReq.tier") + ")";

        MSG.PlayDungeonReq req = new MSG.PlayDungeonReq();
        req.dungeonNo = Convert.ToUInt32(GetInputFieldText("PlayDungeonReq.dungeonNo"));
        req.tier = Convert.ToUInt32(GetInputFieldText("PlayDungeonReq.tier"));
        _netClient.SendPacket(MSG.MsgId.PLAYDUNGEON_REQ, req);
    }

    public void OnButtonLevelupCharReq()
    {
        Debug.Log("OnButtonLevelupCharReq");
        textDisplayUpdate = "OnButtonLevelupCharReq";

        User user = UserManager.Instance.Find(_sessionKey);

        uint slotNo = Convert.ToUInt32(GetInputFieldText("LevelupCharReq.slotNo"));
        MSG.CharData_ charData = null;
        foreach (MSG.CharData_ cd in user._data.chars)
        {
            if (cd.slotNo == slotNo)
            {
                charData = cd;
                break;
            }
        }

        textDisplayUpdate += "\n";
        textDisplayUpdate += "cid : " + charData.cid.ToString() + "\n";
        textDisplayUpdate += "uid : " + charData.uid.ToString() + "\n";
        textDisplayUpdate += "slotNo : " + charData.slotNo.ToString() + "\n";
        textDisplayUpdate += "typeNo : " + charData.typeNo.ToString() + "\n";
        textDisplayUpdate += "level : " + charData.level.ToString() + "\n";
        textDisplayUpdate += "tier : " + charData.tier.ToString() + "\n";
        textDisplayUpdate += "regDate : " + charData.regDate + "\n";
        
        MSG.LevelupCharReq req = new MSG.LevelupCharReq();
        req.slotNo = slotNo;
        _netClient.SendPacket(MSG.MsgId.LEVELUPCHAR_REQ, req);
    }

    public void OnButtonTierupCharReq()
    {
        Debug.Log("OnButtonTierupCharReq");
        textDisplayUpdate = "OnButtonTierupCharReq";

        User user = UserManager.Instance.Find(_sessionKey);

        uint slotNo = Convert.ToUInt32(GetInputFieldText("TierupCharReq.slotNo"));
        MSG.CharData_ charData = null;
        foreach (MSG.CharData_ cd in user._data.chars)
        {
            if (cd.slotNo == slotNo)
            {
                charData = cd;
                break;
            }
        }

        textDisplayUpdate += "\n";
        textDisplayUpdate += "cid : " + charData.cid.ToString() + "\n";
        textDisplayUpdate += "uid : " + charData.uid.ToString() + "\n";
        textDisplayUpdate += "slotNo : " + charData.slotNo.ToString() + "\n";
        textDisplayUpdate += "typeNo : " + charData.typeNo.ToString() + "\n";
        textDisplayUpdate += "level : " + charData.level.ToString() + "\n";
        textDisplayUpdate += "tier : " + charData.tier.ToString() + "\n";
        textDisplayUpdate += "regDate : " + charData.regDate + "\n";
        
        MSG.TierupCharReq req = new MSG.TierupCharReq();
        req.slotNo = slotNo;
        _netClient.SendPacket(MSG.MsgId.TIERUPCHAR_REQ, req);
    }


    public void OnButtonAutoPlayDungeonReq()
    {
        Debug.Log("OnButtonAutoPlayDungeonReq");
        textDisplayUpdate = "OnButtonAutoPlayDungeonReq";
    }

    public void OnConnected(SocketError errorCode)
    {
        Debug.Log("OnConnected : " + errorCode);
        textDisplayUpdate = "OnConnected : " + errorCode;

        MSG.LoginReq req = new MSG.LoginReq();
        //req.name = NetData.Instance.GetUserName();
        req.name = "katarn30";
        _netClient.SendPacket(MSG.MsgId.LOGIN_REQ, req);
    }

    public void OnClosed()
    {
        Debug.Log("OnClosed");
        textDisplayUpdate = "OnClosed";

        _isLogin = false;

        UserManager.Instance.Remove(_sessionKey);
    }

    public void OnMessage_Login_Ans(NetClient netClient, MemoryStream stream)
    {
        Debug.Log("OnMessage_Login_Ans");
        textDisplayUpdate = "OnMessage_Login_Ans";

        _isLogin = true;

        MSG.LoginAns ans = ProtoBuf.Serializer.Deserialize<MSG.LoginAns>(stream);

        User user = new User();
        user._data = ans.data;
        user._session = netClient;
        user._sessionKey = ans.sessionKey;
        UserManager.Instance.Add(user);

        _sessionKey = user._sessionKey;
    }

    public void OnMessage_Pong_Ans(NetClient netClient, MemoryStream stream)
    {

    }

    public void OnMessage_Regist_Ans(NetClient netClient, MemoryStream stream)
    {
        //MSG.RegistAns ans = ProtoBuf.Serializer.Deserialize<MSG.RegistAns>(stream);

        //Debug.Log("OnMessage_Regist_Ans : " + ans.err);

        //MSG.LoginReq req = new MSG.LoginReq();
        //req.name = _inputFieldUserID.text;
        //NetClient.Instance.SendPacket(MSG.MsgId.LOGIN_REQ, req);
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
        MSG.PlayDungeonAns ans = ProtoBuf.Serializer.Deserialize<MSG.PlayDungeonAns>(stream);

        Debug.Log("OnMessage_PlayDungeon_Ans : " + ans.err);
        textDisplayUpdate = "";

        foreach (var battle in ans.battles)
        {
            var srcNo = battle.srcNo;

            var team = battle.team;
            if (team == MSG.BattleData_.Team.ALLY)
            {

            }

            var targets = battle.targets;
            foreach (var target in targets)
            {
                var targetNo = target.no;
                var damage = target.damage;
                var result = target.result;

                Debug.Log("battle : [team: " + team + ", srcno:" + srcNo + ", targetNo:" + targetNo + ", damage:" + damage + ", result:" + result + "]");
                textDisplayUpdate += "battle : [team: " + team + ", srcno:" + srcNo + ", targetNo:" + targetNo + ", damage:" + damage + ", result:" + result + "]\n";
            }
        }

        foreach (var character in ans.chars)
        {
            var cid = character.cid;
            var uid = character.uid;
            var slotNo = character.slotNo;
            var typeNo = character.typeNo;
            var level = character.level;
            var tier = character.tier;
            var regDate = character.regDate;

            Debug.Log("char : [cid" + cid + ", uid" + uid + ", slotNo" + slotNo + ", typeNo" + typeNo + ", level" + level + ", tier" + tier + ", regDate" + regDate + "]");
            textDisplayUpdate += "char : [cid" + cid + ", uid" + uid + ", slotNo" + slotNo + ", typeNo" + typeNo + ", level" + level + ", tier" + tier + ", regDate" + regDate + "]\n";
        }

        foreach (var mob in ans.mobs)
        {
            var cid = mob.cid;
            var uid = mob.uid;
            var slotNo = mob.slotNo;
            var typeNo = mob.typeNo;
            var level = mob.level;
            var tier = mob.tier;
            var regDate = mob.regDate;

            Debug.Log("mob : [cid" + cid + ", uid" + uid + ", slotNo" + slotNo + ", typeNo" + typeNo + ", level" + level + ", tier" + tier + ", regDate" + regDate + "]");
            textDisplayUpdate += "mob : [cid" + cid + ", uid" + uid + ", slotNo" + slotNo + ", typeNo" + typeNo + ", level" + level + ", tier" + tier + ", regDate" + regDate + "]\n";
        }
    }

    public void OnMessage_PlayDungeon_Not(NetClient netClient, MemoryStream stream)
    {
        Debug.Log("OnMessage_PlayDungeon_Not");
        textDisplayUpdate = "OnMessage_PlayDungeon_Not";
    }

    public void OnMessage_LevelUpChar_Ans(NetClient netClient, MemoryStream stream)
    {
        MSG.LevelupCharAns ans = ProtoBuf.Serializer.Deserialize<MSG.LevelupCharAns>(stream);

        Debug.Log("OnMessage_LevelUpChar_Ans : " + ans.err);
        textDisplayUpdate = "OnMessage_LevelUpChar_Ans : " + ans.err;

        MSG.CharData_ charData = ans.char_;

        if (charData == null)
        {
            return;
        }

        User user = UserManager.Instance.Find(_sessionKey);
        
        for (int i = 0; i < user._data.chars.Count; i++)
        {
            if (user._data.chars[i].slotNo == charData.slotNo)
            {
                user._data.chars[i] = charData;
                break;
            }
        }

        textDisplayUpdate += "\n";
        textDisplayUpdate += "cid : " + charData.cid.ToString() + "\n";
        textDisplayUpdate += "uid : " + charData.uid.ToString() + "\n";
        textDisplayUpdate += "slotNo : " + charData.slotNo.ToString() + "\n";
        textDisplayUpdate += "typeNo : " + charData.typeNo.ToString() + "\n";
        textDisplayUpdate += "level : " + charData.level.ToString() + "\n";
        textDisplayUpdate += "tier : " + charData.tier.ToString() + "\n";
        textDisplayUpdate += "regDate : " + charData.regDate + "\n";
    }

    public void OnMessage_TierUpChar_Ans(NetClient netClient, MemoryStream stream)
    {
        MSG.TierupCharAns ans = ProtoBuf.Serializer.Deserialize<MSG.TierupCharAns>(stream);

        Debug.Log("OnMessage_TierUpChar_Ans : " + ans.err);
        textDisplayUpdate = "OnMessage_TierUpChar_Ans : " + ans.err;

        MSG.CharData_ charData = ans.char_;

        if (charData == null)
        {
            return;
        }

        User user = UserManager.Instance.Find(_sessionKey);

        for (int i = 0; i < user._data.chars.Count; i++)
        {
            if (user._data.chars[i].slotNo == charData.slotNo)
            {
                user._data.chars[i] = charData;
                break;
            }
        }

        textDisplayUpdate += "\n";
        textDisplayUpdate += "cid : " + charData.cid.ToString() + "\n";
        textDisplayUpdate += "uid : " + charData.uid.ToString() + "\n";
        textDisplayUpdate += "slotNo : " + charData.slotNo.ToString() + "\n";
        textDisplayUpdate += "typeNo : " + charData.typeNo.ToString() + "\n";
        textDisplayUpdate += "level : " + charData.level.ToString() + "\n";
        textDisplayUpdate += "tier : " + charData.tier.ToString() + "\n";
        textDisplayUpdate += "regDate : " + charData.regDate + "\n";
    }
}