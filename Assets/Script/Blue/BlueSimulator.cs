using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System.IO;
using System.Net.Sockets;
using System;

// net simulator
public class BlueSimulator : MonoBehaviour//, NetHandler
{
    //List<BaseGameEntity> _botList = new List<BaseGameEntity>();
    //List<int> _keyList = new List<int>();
    //string _ip = "13.124.76.58";
    //ushort _port = 20000;
    
    void Start()
    {
        //SettingSinglePanel();
        //SettingMultiPanel();
        //SettingSinglePanelTest();
        //SettingMultiPanelAutoTest();
        //SettingSinglePanelProtocolTest();

        Rect mainRect = new Rect(Vector3.zero, new Vector2(Screen.width, Screen.height));
        int row = 4;
        int col = 4;
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < col; j++)
            {
                int id = i * 100 + j;

                // BlueBot
                //BlueBot bot = new BlueBot(id);
                BlueBot bot = EntityManager.Instance.AddEntity<BlueBot>(id);
                //EntityManager.Instance.RegisterEntity(id, bot);
                bot.ChangeState(BlueBotConnect.Instance);

                // ControlPanel
                Rect rect = UIFactory.Instance.GetRectGrid(mainRect, row, col, i, j);
                //ControlPanel clientPanel = ControlPanelManager.Instance.CreateControlPanel(transform, "MainPanel", rect);
                Transform tf = ControlPanelManager.Instance.transform;
                ControlPanel clientPanel = ControlPanelManager.Instance.AddControlPanel(id, tf, "MainPanel", rect);
                UIFactory.Instance.CreateText(tf, "Text", "", rect, clientPanel.SetTextDisplay);
                UIFactory.Instance.CreateButton(tf, "Text", rect, bot.OnButton);
            }
        }
    }

    //void SettingSinglePanel()
    //{
    //    // 전체 화면 크기
    //    _simulatorRect.position = Vector3.zero;
    //    _simulatorRect.size = new Vector2(Screen.width, Screen.height);

    //    // 메인 제어판 생성
    //    _mainControlPanelRect = UIFactory.Instance.GetRectHorizontal(_simulatorRect, 3, 2);
    //    ControlPanel netControlPanel = AddNetControlPanel(transform, "MainControlPanel", _mainControlPanelRect);
    //    UIFactory.Instance.CreateButton(netControlPanel.transform, "ButtonLogin", UIFactory.Instance.GetRectVertical(_mainControlPanelRect, 3, 0), netControlPanel.OnButtonLogin);
    //    UIFactory.Instance.CreateButton(netControlPanel.transform, "ButtonLogout", UIFactory.Instance.GetRectVertical(_mainControlPanelRect, 3, 1), netControlPanel.OnButtonLogout);

    //    Rect temp1 = UIFactory.Instance.GetRectHorizontal(_simulatorRect, 3, 0);
    //    Rect temp2 = UIFactory.Instance.GetRectHorizontal(_simulatorRect, 3, 1);
    //    Rect subPanelRect = UIFactory.Instance.GetRectHorizontalSum(temp1, temp2);
    //    GameObject subPanel = UIFactory.Instance.CreatePanel(transform, "SubPanel", subPanelRect);
    //    int rowCount = 20;
    //    int columnCount = 25;
    //    for (int i = 0; i < rowCount; i++)
    //    {
    //        for (int j = 0; j < columnCount; j++)
    //        {
    //            Rect clientControlPanelRect = UIFactory.Instance.GetRectGrid(subPanelRect, rowCount, columnCount, i, j);
    //            ControlPanel netControlPanel2 = AddNetControlPanel(subPanel.transform, "ClientControlPanel", clientControlPanelRect);
    //            netControlPanel2.OnButtonLogin();
    //        }
    //    }
    //}

    //void SettingMultiPanel()
    //{
    //    // 전체 화면 크기
    //    _simulatorRect.position = Vector3.zero;
    //    _simulatorRect.size = new Vector2(Screen.width, Screen.height);

    //    // 메인 제어판 생성
    //    _mainControlPanelRect = UIFactory.Instance.GetRectHorizontal(_simulatorRect, 3, 2);
    //    ControlPanel netControlPanel = AddNetControlPanel(transform, "MainControlPanel", _mainControlPanelRect);
    //    UIFactory.Instance.CreateButton(netControlPanel.transform, "ButtonLogin", UIFactory.Instance.GetRectVertical(_mainControlPanelRect, 3, 0), netControlPanel.OnButtonLogin);
    //    UIFactory.Instance.CreateButton(netControlPanel.transform, "ButtonLogout", UIFactory.Instance.GetRectVertical(_mainControlPanelRect, 3, 1), netControlPanel.OnButtonLogout);
    //    UIFactory.Instance.CreateButton(netControlPanel.transform, "ButtonPlayDungeonReq", UIFactory.Instance.GetRectVertical(_mainControlPanelRect, 3, 2), netControlPanel.OnButtonPlayDungeonReq);
    //    //ControlPanel mainControlPanel = AddNetControlPanel(transform, _mainControlPanelRect);

    //    // 클라 제어판 생성
    //    Rect temp1 = UIFactory.Instance.GetRectHorizontal(_simulatorRect, 3, 0);
    //    Rect temp2 = UIFactory.Instance.GetRectHorizontal(_simulatorRect, 3, 1);
    //    Rect subPanelRect = UIFactory.Instance.GetRectHorizontalSum(temp1, temp2);
    //    GameObject subPanel = UIFactory.Instance.CreatePanel(transform, "SubPanel", subPanelRect);
    //    int rowCount = 3;
    //    int columnCount = 3;
    //    for (int i = 0; i < rowCount; i++)
    //    {
    //        for (int j = 0; j < columnCount; j++)
    //        {
    //            Rect clientControlPanelRect = UIFactory.Instance.GetRectGrid(subPanelRect, rowCount, columnCount, i, j);
    //            ControlPanel netControlPanel2 = AddNetControlPanel(subPanel.transform, "ClientControlPanel", clientControlPanelRect);
    //            //UIFactory.Instance.CreateButton(netControlPanel2.transform, "ButtonTest", UIFactory.Instance.GetRectVertical(clientControlPanelRect, 3, 0), netControlPanel.OnButtonTest);
    //            UIFactory.Instance.CreateText(netControlPanel2.transform, "Text", "", UIFactory.Instance.GetRectVertical(clientControlPanelRect, 3, 0), netControlPanel2.SetTextDisplay);
    //            UIFactory.Instance.CreateButton(netControlPanel2.transform, "ButtonLogin", UIFactory.Instance.GetRectVertical(clientControlPanelRect, 3, 1), netControlPanel2.OnButtonLogin);
    //            UIFactory.Instance.CreateButton(netControlPanel2.transform, "ButtonLogout", UIFactory.Instance.GetRectVertical(clientControlPanelRect, 3, 2), netControlPanel2.OnButtonLogout);
    //        }
    //    }
    //}

    //void SettingSinglePanelTest()
    //{
    //    // 전체 화면 크기
    //    _simulatorRect.position = Vector3.zero;
    //    _simulatorRect.size = new Vector2(Screen.width, Screen.height);

    //    // 메인 제어판 생성
    //    _mainControlPanelRect = UIFactory.Instance.GetRectHorizontal(_simulatorRect, 1, 0);
    //    ControlPanel netControlPanel = AddNetControlPanel(transform, "MainControlPanel", _mainControlPanelRect);
    //    UIFactory.Instance.CreateText(netControlPanel.transform, "Text", "", UIFactory.Instance.GetRectVertical(_mainControlPanelRect, 4, 0), netControlPanel.SetTextDisplay);
    //    UIFactory.Instance.CreateButton(netControlPanel.transform, "ButtonLogin", UIFactory.Instance.GetRectVertical(_mainControlPanelRect, 4, 1), netControlPanel.OnButtonLogin);
    //    UIFactory.Instance.CreateButton(netControlPanel.transform, "ButtonLogout", UIFactory.Instance.GetRectVertical(_mainControlPanelRect, 4, 2), netControlPanel.OnButtonLogout);
    //    UIFactory.Instance.CreateButton(netControlPanel.transform, "ButtonPlayDungeonReq", UIFactory.Instance.GetRectVertical(_mainControlPanelRect, 4, 3), netControlPanel.OnButtonPlayDungeonReq);
    //    //UIFactory.Instance.CreateButton(netControlPanel.transform, "ButtonPlayDungeonReq", UIFactory.Instance.GetRectVertical(_mainControlPanelRect, 3, 2), netControlPanel.OnButtonPlayDungeonReq);
    //    //ControlPanel mainControlPanel = AddNetControlPanel(transform, _mainControlPanelRect);

    //    // 클라 제어판 생성
    //    //Rect temp1 = UIFactory.Instance.GetRectHorizontal(_simulatorRect, 3, 0);
    //    //Rect temp2 = UIFactory.Instance.GetRectHorizontal(_simulatorRect, 3, 1);
    //    //Rect subPanelRect = UIFactory.Instance.GetRectHorizontalSum(temp1, temp2);
    //    //GameObject subPanel = UIFactory.Instance.CreatePanel(transform, "SubPanel", subPanelRect);
    //    //int rowCount = 3;
    //    //int columnCount = 3;
    //    //for (int i = 0; i < rowCount; i++)
    //    //{
    //    //    for (int j = 0; j < columnCount; j++)
    //    //    {
    //    //        Rect clientControlPanelRect = UIFactory.Instance.GetRectGrid(subPanelRect, rowCount, columnCount, i, j);
    //    //        ControlPanel netControlPanel2 = AddNetControlPanel(subPanel.transform, "ClientControlPanel", clientControlPanelRect);
    //    //        //UIFactory.Instance.CreateButton(netControlPanel2.transform, "ButtonTest", UIFactory.Instance.GetRectVertical(clientControlPanelRect, 3, 0), netControlPanel.OnButtonTest);
    //    //        UIFactory.Instance.CreateText(netControlPanel2.transform, "", UIFactory.Instance.GetRectVertical(clientControlPanelRect, 3, 0), netControlPanel2.SetTextDisplay);
    //    //        UIFactory.Instance.CreateButton(netControlPanel2.transform, "ButtonLogin", UIFactory.Instance.GetRectVertical(clientControlPanelRect, 3, 1), netControlPanel2.OnButtonLogin);
    //    //        UIFactory.Instance.CreateButton(netControlPanel2.transform, "ButtonLogout", UIFactory.Instance.GetRectVertical(clientControlPanelRect, 3, 2), netControlPanel2.OnButtonLogout);
    //    //    }
    //    //}
    //}

    //void SettingSinglePanelAutoTest()
    //{
    //    // 전체 화면 크기
    //    _simulatorRect.position = Vector3.zero;
    //    _simulatorRect.size = new Vector2(Screen.width, Screen.height);

    //    // 메인 제어판 생성
    //    _mainControlPanelRect = UIFactory.Instance.GetRectHorizontal(_simulatorRect, 1, 0);
    //    ControlPanel netControlPanel = AddNetControlPanel(transform, "MainControlPanel", _mainControlPanelRect);
    //    UIFactory.Instance.CreateText(netControlPanel.transform, "Text", "", UIFactory.Instance.GetRectVertical(_mainControlPanelRect, 2, 0), netControlPanel.SetTextDisplay);
    //    UIFactory.Instance.CreateButton(netControlPanel.transform, "ButtonAutoPlayDungeonReq", UIFactory.Instance.GetRectVertical(_mainControlPanelRect, 2, 1), netControlPanel.OnButtonAutoPlayDungeonReq);

    //    // 자동 로그인
    //    netControlPanel.OnButtonLogin();
    //}

    //void SettingMultiPanelAutoTest()
    //{
    //    // 전체 화면 크기
    //    _simulatorRect.position = Vector3.zero;
    //    _simulatorRect.size = new Vector2(Screen.width, Screen.height);

    //    // 메인 제어판 생성
    //    _mainControlPanelRect = _simulatorRect;
    //    GameObject mainControlPanel = UIFactory.Instance.CreatePanel(transform, "MainControlPanel", _mainControlPanelRect);

    //    int userCount = 10;
    //    int userPosIndex = 0;
    //    int protoCount = 5;
    //    int protoPosIndex = 0;
    //    Rect protocolPanelRect = new Rect();
    //    GameObject protocolPanel = null;
    //    ControlPanel clientPanel = null;

    //    // 클라이언트 패널 생성
    //    for (int i = 0; i < userCount; i++)
    //    {
    //        protocolPanelRect = UIFactory.Instance.GetRectVertical(_mainControlPanelRect, userCount + 1, userPosIndex++);
    //        clientPanel = AddNetControlPanel(mainControlPanel.transform, "Panel", protocolPanelRect);
    //        UIFactory.Instance.CreateText(mainControlPanel.transform, "Text", "Client", protocolPanelRect, clientPanel.SetTextDisplay);
    //    }

    //    // Regist & Create Char & Login & AutoTest
    //    protocolPanelRect = UIFactory.Instance.GetRectVertical(_mainControlPanelRect, userCount + 1, userPosIndex++);
    //    protocolPanel = UIFactory.Instance.CreatePanel(mainControlPanel.transform, "Panel", protocolPanelRect);
    //    UIFactory.Instance.CreateButton(protocolPanel.transform, "Regist", UIFactory.Instance.GetRectHorizontal(protocolPanelRect, protoCount, protoPosIndex++), () =>
    //    {
    //        ulong uid = 100;
    //        string name = "user";

    //        // Regist User
    //        foreach (ControlPanel panel in _netControlPanelList)
    //        {
    //            string ip = "13.124.76.58";
    //            ushort port = 20000;
    //            User user = new User();
    //            user._data.groupName = "korea0";
    //            user._data.language = "kr";
    //            user._data.platform = MSG.PlatForm.ANDROID;
    //            user._data.regDate = "2017-09-11 11:52:52";
    //            user._data.did = "abcd";
    //            user._data.name = name + uid;
    //            user._data.uid = 0;
    //            user._data.vc1 = 10000000;
    //            user._data.vc2 = 10000000;
    //            user._data.vc3 = 10000000;
    //            uid++;

    //            panel.Regist(ip, port, user);
    //        }
    //    });
    //    UIFactory.Instance.CreateButton(protocolPanel.transform, "Login", UIFactory.Instance.GetRectHorizontal(protocolPanelRect, protoCount, protoPosIndex++), () =>
    //    {
    //        // Regist User & Create Char
    //        foreach (ControlPanel panel in _netControlPanelList)
    //        {
    //            string ip = "13.124.76.58";
    //            ushort port = 20000;
    //            string id = panel.netData.userId;
    //            panel.Login(ip, port, id);
    //        }
    //    });
    //    UIFactory.Instance.CreateButton(protocolPanel.transform, "CreateChar", UIFactory.Instance.GetRectHorizontal(protocolPanelRect, protoCount, protoPosIndex++), () =>
    //    {
    //        int charNo = 0;

    //        // Create Char
    //        foreach (ControlPanel panel in _netControlPanelList)
    //        {
    //            int no = charNo + 1;
    //            charNo = (charNo + 1) % 2;

    //            panel.CreateChar(no);
    //        }
    //    });
    //    UIFactory.Instance.CreateButton(protocolPanel.transform, "Logout", UIFactory.Instance.GetRectHorizontal(protocolPanelRect, protoCount, protoPosIndex++), () =>
    //    {
    //        foreach (ControlPanel panel in _netControlPanelList)
    //        {
    //            panel.Logout();
    //        }
    //    });
    //    UIFactory.Instance.CreateButton(protocolPanel.transform, "Logout", UIFactory.Instance.GetRectHorizontal(protocolPanelRect, protoCount, protoPosIndex++), () =>
    //    {
    //        foreach (ControlPanel panel in _netControlPanelList)
    //        {
    //            panel.Logout();
    //        }
    //    });

    //    // 자동 로그인
    //    //netControlPanel.OnButtonLogin();
    //}

    //void SettingSinglePanelProtocolTest()
    //{
    //    // 전체 화면 크기
    //    _simulatorRect.position = Vector3.zero;
    //    _simulatorRect.size = new Vector2(Screen.width, Screen.height);

    //    // 메인 패널 생성
    //    _mainControlPanelRect = _simulatorRect;
    //    ControlPanel mainControlPanel = AddNetControlPanel(transform, "MainControlPanel", _mainControlPanelRect);
    //    Rect displayPanelRectUpper = UIFactory.Instance.GetRectVertical(_mainControlPanelRect, 5, 0);
    //    Rect displayPanelRectLower = UIFactory.Instance.GetRectVertical(_mainControlPanelRect, 5, 1);
    //    Rect displayPanelRect = UIFactory.Instance.GetRectVerticalSum(displayPanelRectUpper, displayPanelRectLower);
    //    UIFactory.Instance.CreateText(mainControlPanel.transform, "Text", "", displayPanelRect, mainControlPanel.SetTextDisplay);

    //    // 구분 패널 생성
    //    Rect temp1 = UIFactory.Instance.GetRectVertical(_mainControlPanelRect, 5, 2);
    //    Rect temp2 = UIFactory.Instance.GetRectVertical(_mainControlPanelRect, 5, 3);
    //    Rect temp3 = UIFactory.Instance.GetRectVertical(_mainControlPanelRect, 5, 4);
    //    Rect tempSubPanelRect = UIFactory.Instance.GetRectVerticalSum(temp1, temp2);
    //    Rect subPanelRect = UIFactory.Instance.GetRectVerticalSum(tempSubPanelRect, temp3);
    //    GameObject subPanel = UIFactory.Instance.CreatePanel(mainControlPanel.transform, "SubPanel", subPanelRect);

    //    int protocolCount = 5;
    //    int posIndex = 0;
    //    Rect protocolPanelRect = new Rect();
    //    GameObject protocolPanel = null;

    //    // LoginReq
    //    protocolPanelRect = UIFactory.Instance.GetRectVertical(subPanelRect, protocolCount, posIndex++);
    //    protocolPanel = UIFactory.Instance.CreatePanel(subPanel.transform, "Panel", protocolPanelRect);
    //    UIFactory.Instance.CreateInputField(protocolPanel.transform, "IP", "13.124.76.58", UIFactory.Instance.GetRectHorizontal(protocolPanelRect, 4, 0), mainControlPanel.AddInputField);
    //    UIFactory.Instance.CreateInputField(protocolPanel.transform, "Port", "20000", UIFactory.Instance.GetRectHorizontal(protocolPanelRect, 4, 1), mainControlPanel.AddInputField);
    //    UIFactory.Instance.CreateInputField(protocolPanel.transform, "ID", "katarn30", UIFactory.Instance.GetRectHorizontal(protocolPanelRect, 4, 2), mainControlPanel.AddInputField);
    //    UIFactory.Instance.CreateButton(protocolPanel.transform, "LoginReq", UIFactory.Instance.GetRectHorizontal(protocolPanelRect, 4, 3), mainControlPanel.OnButtonLogin);
    //    //UIFactory.Instance.CreateButton(subPanel.transform, "LoginReq", protocolPanelRect, mainControlPanel.OnButtonLogin);

    //    // LogoutReq
    //    protocolPanelRect = UIFactory.Instance.GetRectVertical(subPanelRect, protocolCount, posIndex++);
    //    //protocolPanel = UIFactory.Instance.CreatePanel(subPanel.transform, "Panel", protocolPanelRect);
    //    UIFactory.Instance.CreateButton(subPanel.transform, "LogoutReq", protocolPanelRect, mainControlPanel.OnButtonLogout);

    //    // PlayDungeonReq
    //    protocolPanelRect = UIFactory.Instance.GetRectVertical(subPanelRect, protocolCount, posIndex++);
    //    protocolPanel = UIFactory.Instance.CreatePanel(subPanel.transform, "Panel", protocolPanelRect);
    //    UIFactory.Instance.CreateInputField(protocolPanel.transform, "PlayDungeonReq.dungeonNo", "", UIFactory.Instance.GetRectHorizontal(protocolPanelRect, 3, 0), mainControlPanel.AddInputField);
    //    UIFactory.Instance.CreateInputField(protocolPanel.transform, "PlayDungeonReq.tier", "", UIFactory.Instance.GetRectHorizontal(protocolPanelRect, 3, 1), mainControlPanel.AddInputField);
    //    UIFactory.Instance.CreateButton(protocolPanel.transform, "PlayDungeonReq", UIFactory.Instance.GetRectHorizontal(protocolPanelRect, 3, 2), mainControlPanel.OnButtonPlayDungeonReq);

    //    // LevelupCharReq
    //    protocolPanelRect = UIFactory.Instance.GetRectVertical(subPanelRect, protocolCount, posIndex++);
    //    protocolPanel = UIFactory.Instance.CreatePanel(subPanel.transform, "Panel", protocolPanelRect);
    //    UIFactory.Instance.CreateInputField(protocolPanel.transform, "LevelupCharReq.slotNo", "", UIFactory.Instance.GetRectHorizontal(protocolPanelRect, 2, 0), mainControlPanel.AddInputField);
    //    UIFactory.Instance.CreateButton(protocolPanel.transform, "LevelupCharReq", UIFactory.Instance.GetRectHorizontal(protocolPanelRect, 2, 1), mainControlPanel.OnButtonLevelupCharReq);

    //    // TierupCharReq
    //    protocolPanelRect = UIFactory.Instance.GetRectVertical(subPanelRect, protocolCount, posIndex++);
    //    protocolPanel = UIFactory.Instance.CreatePanel(subPanel.transform, "Panel", protocolPanelRect);
    //    UIFactory.Instance.CreateInputField(protocolPanel.transform, "TierupCharReq.slotNo", "", UIFactory.Instance.GetRectHorizontal(protocolPanelRect, 2, 0), mainControlPanel.AddInputField);
    //    UIFactory.Instance.CreateButton(protocolPanel.transform, "TierupCharReq", UIFactory.Instance.GetRectHorizontal(protocolPanelRect, 2, 1), mainControlPanel.OnButtonTierupCharReq);
    //}
    
    //public void Regist(NetClient netClient, string name)
    //{
    //    Debug.Log("Regist");

    //    if (netClient.IsConnected() == false)
    //    {
    //        Debug.Log("Not connected");
    //        return;
    //    }

    //    BlueUser user = new BlueUser();
    //    user._data.groupName = "korea0";
    //    user._data.language = "kr";
    //    user._data.platform = MSG.PlatForm.ANDROID;
    //    user._data.regDate = "2017-09-11 11:52:52";
    //    user._data.did = "abcd";
    //    user._data.name = name;
    //    user._data.uid = 0;
    //    user._data.vc1 = 10000000;
    //    user._data.vc2 = 10000000;
    //    user._data.vc3 = 10000000;

    //    MSG.RegistReq req = new MSG.RegistReq();
    //    req.data = user._data;
    //    netClient.SendPacket(MSG.MsgId.REGIST_REQ, req);
    //}

    //public void Login(NetClient netClient, string userName)
    //{
    //    Debug.Log("Login");

    //    if (netClient.IsConnected() == false)
    //    {
    //        Debug.Log("Not connected");
    //        return;
    //    }

    //    MSG.LoginReq req = new MSG.LoginReq();
    //    req.name = userName;
    //    netClient.SendPacket(MSG.MsgId.LOGIN_REQ, req);
    //}

    //public void Logout(NetClient netClient)
    //{
    //    Debug.Log("Logout");

    //    netClient.Close();
    //}

    //public void CreateChar(NetClient netClient, int charNo)
    //{
    //    Debug.Log("CreateChar");

    //    MSG.CreateCharReq req = new MSG.CreateCharReq();
    //    req.charNo = (uint)charNo;
    //    netClient.SendPacket(MSG.MsgId.CREATECHAR_REQ, req);
    //}

    ////public void OnButtonLogin()
    ////{
    ////    Debug.Log("OnButtonLogin");

    ////    string ip = GetInputFieldText("IP");
    ////    ushort port = Convert.ToUInt16(GetInputFieldText("Port"));
    ////    string id = GetInputFieldText("ID");

    ////    Login(ip, port, id);

    ////}
    ////public void OnButtonLogout()
    ////{
    ////    Debug.Log("OnButtonLogout");
    ////    _netClient.Close();
    ////}

    ////public void OnButtonPlayDungeonReq()
    ////{
    ////    Debug.Log("OnButtonPlayDungeonReq(no : " + GetInputFieldText("PlayDungeonReq.dungeonNo") + ", tier : " + GetInputFieldText("PlayDungeonReq.tier") + ")");
    ////    textDisplayUpdate = "OnButtonPlayDungeonReq(no : " + GetInputFieldText("PlayDungeonReq.dungeonNo") + ", tier : " + GetInputFieldText("PlayDungeonReq.tier") + ")";

    ////    MSG.PlayDungeonReq req = new MSG.PlayDungeonReq();
    ////    req.dungeonNo = Convert.ToUInt32(GetInputFieldText("PlayDungeonReq.dungeonNo"));
    ////    req.tier = Convert.ToUInt32(GetInputFieldText("PlayDungeonReq.tier"));
    ////    _netClient.SendPacket(MSG.MsgId.PLAYDUNGEON_REQ, req);
    ////}

    ////public void OnButtonLevelupCharReq()
    ////{
    ////    Debug.Log("OnButtonLevelupCharReq");
    ////    textDisplayUpdate = "OnButtonLevelupCharReq";

    ////    User user = UserManager.Instance.Find(_netData.sessionKey);

    ////    uint slotNo = Convert.ToUInt32(GetInputFieldText("LevelupCharReq.slotNo"));
    ////    MSG.CharData_ charData = null;
    ////    foreach (MSG.CharData_ cd in user._data.chars)
    ////    {
    ////        if (cd.slotNo == slotNo)
    ////        {
    ////            charData = cd;
    ////            break;
    ////        }
    ////    }

    ////    textDisplayUpdate += "\n";
    ////    textDisplayUpdate += "cid : " + charData.cid.ToString() + "\n";
    ////    textDisplayUpdate += "uid : " + charData.uid.ToString() + "\n";
    ////    textDisplayUpdate += "slotNo : " + charData.slotNo.ToString() + "\n";
    ////    textDisplayUpdate += "typeNo : " + charData.typeNo.ToString() + "\n";
    ////    textDisplayUpdate += "level : " + charData.level.ToString() + "\n";
    ////    textDisplayUpdate += "tier : " + charData.tier.ToString() + "\n";
    ////    textDisplayUpdate += "regDate : " + charData.regDate + "\n";

    ////    MSG.LevelupCharReq req = new MSG.LevelupCharReq();
    ////    req.slotNo = slotNo;
    ////    _netClient.SendPacket(MSG.MsgId.LEVELUPCHAR_REQ, req);
    ////}

    ////public void OnButtonTierupCharReq()
    ////{
    ////    Debug.Log("OnButtonTierupCharReq");
    ////    textDisplayUpdate = "OnButtonTierupCharReq";

    ////    User user = UserManager.Instance.Find(_netData.sessionKey);

    ////    uint slotNo = Convert.ToUInt32(GetInputFieldText("TierupCharReq.slotNo"));
    ////    MSG.CharData_ charData = null;
    ////    foreach (MSG.CharData_ cd in user._data.chars)
    ////    {
    ////        if (cd.slotNo == slotNo)
    ////        {
    ////            charData = cd;
    ////            break;
    ////        }
    ////    }

    ////    textDisplayUpdate += "\n";
    ////    textDisplayUpdate += "cid : " + charData.cid.ToString() + "\n";
    ////    textDisplayUpdate += "uid : " + charData.uid.ToString() + "\n";
    ////    textDisplayUpdate += "slotNo : " + charData.slotNo.ToString() + "\n";
    ////    textDisplayUpdate += "typeNo : " + charData.typeNo.ToString() + "\n";
    ////    textDisplayUpdate += "level : " + charData.level.ToString() + "\n";
    ////    textDisplayUpdate += "tier : " + charData.tier.ToString() + "\n";
    ////    textDisplayUpdate += "regDate : " + charData.regDate + "\n";

    ////    MSG.TierupCharReq req = new MSG.TierupCharReq();
    ////    req.slotNo = slotNo;
    ////    _netClient.SendPacket(MSG.MsgId.TIERUPCHAR_REQ, req);
    ////}


    ////public void OnButtonAutoPlayDungeonReq()
    ////{
    ////    Debug.Log("OnButtonAutoPlayDungeonReq");
    ////    textDisplayUpdate = "OnButtonAutoPlayDungeonReq";
    ////}

    //public void OnConnected(NetClient netClient, SocketError errorCode)
    //{
    //    Debug.Log("OnConnected : " + errorCode);
    //    //textDisplayUpdate = "OnConnected : " + errorCode;
        
    //    int key = NetClientManager.Instance.FindKey(netClient);
    //    string name = "user" + key;
    //    //Regist(netClient, name);
    //    Login(netClient, name);
    //}

    //public void OnClosed(NetClient netClient)
    //{
    //    //Debug.Log("OnClosed");
    //    //textDisplayUpdate = "OnClosed";

    //    //UserManager.Instance.Remove(_netData.sessionKey);
    //}

    //public void OnMessage_Login_Ans(NetClient netClient, MemoryStream stream)
    //{
    //    Debug.Log("OnMessage_Login_Ans");
    //    //textDisplayUpdate = "OnMessage_Login_Ans";

    //    MSG.LoginAns ans = ProtoBuf.Serializer.Deserialize<MSG.LoginAns>(stream);

    //    BlueUser user = new BlueUser();
    //    user._data = ans.data;
    //    user._session = netClient;
    //    user._sessionKey = ans.sessionKey;
    //    BlueUserManager.Instance.Add(user);

    //    int key = NetClientManager.Instance.FindKey(netClient);
    //    ControlPanel controlPanel = ControlPanelManager.Instance.FindControlPanel(key);
    //    controlPanel.SetPanelImageColor(Color.green);
    //    //controlPanel.SetTextDisplayUpdate("abcd");
    //}

    //public void OnMessage_Pong_Ans(NetClient netClient, MemoryStream stream)
    //{

    //}

    //public void OnMessage_Regist_Ans(NetClient netClient, MemoryStream stream)
    //{
    //    MSG.RegistAns ans = ProtoBuf.Serializer.Deserialize<MSG.RegistAns>(stream);

    //    Debug.Log("OnMessage_Regist_Ans : " + ans.err);

    //    int key = NetClientManager.Instance.FindKey(netClient);
    //    string name = "user" + key;
        
    //    MSG.LoginReq req = new MSG.LoginReq();
    //    req.name = name;
    //    NetClient.Instance.SendPacket(MSG.MsgId.LOGIN_REQ, req);
    //}

    //public void OnMessage_Version_Ans(NetClient netClient, MemoryStream stream)
    //{

    //}

    //public void OnMessage_Chat_Ans(NetClient netClient, MemoryStream stream)
    //{

    //}

    //public void OnMessage_Chat_Not(NetClient netClient, MemoryStream stream)
    //{

    //}

    //public void OnMessage_CreateChatRoom_Ans(NetClient netClient, MemoryStream stream)
    //{

    //}

    //public void OnMessage_CreateChatRoom_Not(NetClient netClient, MemoryStream stream)
    //{

    //}

    //public void OnMessage_InviteChatRoom_Ans(NetClient netClient, MemoryStream stream)
    //{

    //}

    //public void OnMessage_InviteChatRoom_Not(NetClient netClient, MemoryStream stream)
    //{

    //}

    //public void OnMessage_EnterChatRoom_Ans(NetClient netClient, MemoryStream stream)
    //{

    //}

    //public void OnMessage_EnterChatRoom_Not(NetClient netClient, MemoryStream stream)
    //{

    //}

    //public void OnMessage_LeaveChatRoom_Ans(NetClient netClient, MemoryStream stream)
    //{

    //}

    //public void OnMessage_LeaveChatRoom_Not(NetClient netClient, MemoryStream stream)
    //{

    //}

    //public void OnMessage_CreateChar_Ans(NetClient netClient, MemoryStream stream)
    //{
    //    //MSG.CreateCharAns ans = ProtoBuf.Serializer.Deserialize<MSG.CreateCharAns>(stream);

    //    //Debug.Log("OnMessage_CreateChar_Ans : " + ans.err);
    //    //textDisplayUpdate = "OnMessage_CreateChar_Ans";
    //}

    //public void OnMessage_Contents_Not(NetClient netClient, MemoryStream stream)
    //{

    //}

    //public void OnMessage_Currency_Not(NetClient netClient, MemoryStream stream)
    //{

    //}

    //public void OnMessage_PlayDungeon_Ans(NetClient netClient, MemoryStream stream)
    //{
    //    //MSG.PlayDungeonAns ans = ProtoBuf.Serializer.Deserialize<MSG.PlayDungeonAns>(stream);

    //    //Debug.Log("OnMessage_PlayDungeon_Ans : " + ans.err);
    //    //textDisplayUpdate = "";

    //    //foreach (var battle in ans.battles)
    //    //{
    //    //    var srcNo = battle.srcNo;

    //    //    var team = battle.team;
    //    //    if (team == MSG.BattleData_.Team.ALLY)
    //    //    {

    //    //    }

    //    //    var targets = battle.targets;
    //    //    foreach (var target in targets)
    //    //    {
    //    //        var targetNo = target.no;
    //    //        var damage = target.damage;
    //    //        var result = target.result;

    //    //        Debug.Log("battle : [team: " + team + ", srcno:" + srcNo + ", targetNo:" + targetNo + ", damage:" + damage + ", result:" + result + "]");
    //    //        textDisplayUpdate += "battle : [team: " + team + ", srcno:" + srcNo + ", targetNo:" + targetNo + ", damage:" + damage + ", result:" + result + "]\n";
    //    //    }
    //    //}

    //    //foreach (var character in ans.chars)
    //    //{
    //    //    var cid = character.cid;
    //    //    var uid = character.uid;
    //    //    var slotNo = character.slotNo;
    //    //    var typeNo = character.typeNo;
    //    //    var level = character.level;
    //    //    var tier = character.tier;
    //    //    var regDate = character.regDate;

    //    //    Debug.Log("char : [cid" + cid + ", uid" + uid + ", slotNo" + slotNo + ", typeNo" + typeNo + ", level" + level + ", tier" + tier + ", regDate" + regDate + "]");
    //    //    textDisplayUpdate += "char : [cid" + cid + ", uid" + uid + ", slotNo" + slotNo + ", typeNo" + typeNo + ", level" + level + ", tier" + tier + ", regDate" + regDate + "]\n";
    //    //}

    //    //foreach (var mob in ans.mobs)
    //    //{
    //    //    var cid = mob.cid;
    //    //    var uid = mob.uid;
    //    //    var slotNo = mob.slotNo;
    //    //    var typeNo = mob.typeNo;
    //    //    var level = mob.level;
    //    //    var tier = mob.tier;
    //    //    var regDate = mob.regDate;

    //    //    Debug.Log("mob : [cid" + cid + ", uid" + uid + ", slotNo" + slotNo + ", typeNo" + typeNo + ", level" + level + ", tier" + tier + ", regDate" + regDate + "]");
    //    //    textDisplayUpdate += "mob : [cid" + cid + ", uid" + uid + ", slotNo" + slotNo + ", typeNo" + typeNo + ", level" + level + ", tier" + tier + ", regDate" + regDate + "]\n";
    //    //}
    //}

    //public void OnMessage_PlayDungeon_Not(NetClient netClient, MemoryStream stream)
    //{
    //    //Debug.Log("OnMessage_PlayDungeon_Not");
    //    //textDisplayUpdate = "OnMessage_PlayDungeon_Not";
    //}

    //public void OnMessage_LevelUpChar_Ans(NetClient netClient, MemoryStream stream)
    //{
    //    //MSG.LevelupCharAns ans = ProtoBuf.Serializer.Deserialize<MSG.LevelupCharAns>(stream);

    //    //Debug.Log("OnMessage_LevelUpChar_Ans : " + ans.err);
    //    //textDisplayUpdate = "OnMessage_LevelUpChar_Ans : " + ans.err;

    //    //MSG.CharData_ charData = ans.char_;

    //    //if (charData == null)
    //    //{
    //    //    return;
    //    //}

    //    //User user = UserManager.Instance.Find(_netData.sessionKey);

    //    //for (int i = 0; i < user._data.chars.Count; i++)
    //    //{
    //    //    if (user._data.chars[i].slotNo == charData.slotNo)
    //    //    {
    //    //        user._data.chars[i] = charData;
    //    //        break;
    //    //    }
    //    //}

    //    //textDisplayUpdate += "\n";
    //    //textDisplayUpdate += "cid : " + charData.cid.ToString() + "\n";
    //    //textDisplayUpdate += "uid : " + charData.uid.ToString() + "\n";
    //    //textDisplayUpdate += "slotNo : " + charData.slotNo.ToString() + "\n";
    //    //textDisplayUpdate += "typeNo : " + charData.typeNo.ToString() + "\n";
    //    //textDisplayUpdate += "level : " + charData.level.ToString() + "\n";
    //    //textDisplayUpdate += "tier : " + charData.tier.ToString() + "\n";
    //    //textDisplayUpdate += "regDate : " + charData.regDate + "\n";
    //}

    //public void OnMessage_TierUpChar_Ans(NetClient netClient, MemoryStream stream)
    //{
    //    //MSG.TierupCharAns ans = ProtoBuf.Serializer.Deserialize<MSG.TierupCharAns>(stream);

    //    //Debug.Log("OnMessage_TierUpChar_Ans : " + ans.err);
    //    //textDisplayUpdate = "OnMessage_TierUpChar_Ans : " + ans.err;

    //    //MSG.CharData_ charData = ans.char_;

    //    //if (charData == null)
    //    //{
    //    //    return;
    //    //}

    //    //User user = UserManager.Instance.Find(_netData.sessionKey);

    //    //for (int i = 0; i < user._data.chars.Count; i++)
    //    //{
    //    //    if (user._data.chars[i].slotNo == charData.slotNo)
    //    //    {
    //    //        user._data.chars[i] = charData;
    //    //        break;
    //    //    }
    //    //}

    //    //textDisplayUpdate += "\n";
    //    //textDisplayUpdate += "cid : " + charData.cid.ToString() + "\n";
    //    //textDisplayUpdate += "uid : " + charData.uid.ToString() + "\n";
    //    //textDisplayUpdate += "slotNo : " + charData.slotNo.ToString() + "\n";
    //    //textDisplayUpdate += "typeNo : " + charData.typeNo.ToString() + "\n";
    //    //textDisplayUpdate += "level : " + charData.level.ToString() + "\n";
    //    //textDisplayUpdate += "tier : " + charData.tier.ToString() + "\n";
    //    //textDisplayUpdate += "regDate : " + charData.regDate + "\n";
    //}
}
