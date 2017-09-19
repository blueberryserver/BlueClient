using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

// net simulator
public class NetSimulator : MonoBehaviour
{
    public List<NetClient> _netClientList = new List<NetClient>();
    public List<NetControlPanel> _netControlPanelList = new List<NetControlPanel>();
    public Rect _simulatorRect = new Rect();
    // main control panel
    public Rect _mainControlPanelRect = new Rect();
    //public Rect _mainControlPanelButtonRect = new Rect();
    // client control panel
    public Rect _clientControlPanelRect = new Rect();
    public Rect _clientControlPanelButtonRect = new Rect();
    // user data
    
    void Start()
    {
        //SettingSinglePanel();
        //SettingMultiPanel();
        //SettingSinglePanelTest();
        SettingMultiPanelAutoTest();
        //SettingSinglePanelProtocolTest();
    }

    void SettingSinglePanel()
    {
        // 전체 화면 크기
        _simulatorRect.position = Vector3.zero;
        _simulatorRect.size = new Vector2(Screen.width, Screen.height);

        // 메인 제어판 생성
        _mainControlPanelRect = UIFactory.Instance.GetRectHorizontal(_simulatorRect, 3, 2);
        NetControlPanel netControlPanel = AddNetControlPanel(transform, "MainControlPanel", _mainControlPanelRect);
        UIFactory.Instance.CreateButton(netControlPanel.transform, "ButtonLogin", UIFactory.Instance.GetRectVertical(_mainControlPanelRect, 3, 0), netControlPanel.OnButtonLogin);
        UIFactory.Instance.CreateButton(netControlPanel.transform, "ButtonLogout", UIFactory.Instance.GetRectVertical(_mainControlPanelRect, 3, 1), netControlPanel.OnButtonLogout);

        Rect temp1 = UIFactory.Instance.GetRectHorizontal(_simulatorRect, 3, 0);
        Rect temp2 = UIFactory.Instance.GetRectHorizontal(_simulatorRect, 3, 1);
        Rect subPanelRect = UIFactory.Instance.GetRectHorizontalSum(temp1, temp2);
        GameObject subPanel = UIFactory.Instance.CreatePanel(transform, "SubPanel", subPanelRect);
        int rowCount = 20;
        int columnCount = 25;
        for (int i = 0; i < rowCount; i++)
        {
            for (int j = 0; j < columnCount; j++)
            {
                Rect clientControlPanelRect = UIFactory.Instance.GetRectGrid(subPanelRect, rowCount, columnCount, i, j);
                NetControlPanel netControlPanel2 = AddNetControlPanel(subPanel.transform, "ClientControlPanel", clientControlPanelRect);
                netControlPanel2.OnButtonLogin();
            }
        }
    }

    void SettingMultiPanel()
    {
        // 전체 화면 크기
        _simulatorRect.position = Vector3.zero;
        _simulatorRect.size = new Vector2(Screen.width, Screen.height);

        // 메인 제어판 생성
        _mainControlPanelRect = UIFactory.Instance.GetRectHorizontal(_simulatorRect, 3, 2);
        NetControlPanel netControlPanel = AddNetControlPanel(transform, "MainControlPanel", _mainControlPanelRect);
        UIFactory.Instance.CreateButton(netControlPanel.transform, "ButtonLogin", UIFactory.Instance.GetRectVertical(_mainControlPanelRect, 3, 0), netControlPanel.OnButtonLogin);
        UIFactory.Instance.CreateButton(netControlPanel.transform, "ButtonLogout", UIFactory.Instance.GetRectVertical(_mainControlPanelRect, 3, 1), netControlPanel.OnButtonLogout);
        UIFactory.Instance.CreateButton(netControlPanel.transform, "ButtonPlayDungeonReq", UIFactory.Instance.GetRectVertical(_mainControlPanelRect, 3, 2), netControlPanel.OnButtonPlayDungeonReq);
        //NetControlPanel mainControlPanel = AddNetControlPanel(transform, _mainControlPanelRect);

        // 클라 제어판 생성
        Rect temp1 = UIFactory.Instance.GetRectHorizontal(_simulatorRect, 3, 0);
        Rect temp2 = UIFactory.Instance.GetRectHorizontal(_simulatorRect, 3, 1);
        Rect subPanelRect = UIFactory.Instance.GetRectHorizontalSum(temp1, temp2);
        GameObject subPanel = UIFactory.Instance.CreatePanel(transform, "SubPanel", subPanelRect);
        int rowCount = 3;
        int columnCount = 3;
        for (int i = 0; i < rowCount; i++)
        {
            for (int j = 0; j < columnCount; j++)
            {
                Rect clientControlPanelRect = UIFactory.Instance.GetRectGrid(subPanelRect, rowCount, columnCount, i, j);
                NetControlPanel netControlPanel2 = AddNetControlPanel(subPanel.transform, "ClientControlPanel", clientControlPanelRect);
                //UIFactory.Instance.CreateButton(netControlPanel2.transform, "ButtonTest", UIFactory.Instance.GetRectVertical(clientControlPanelRect, 3, 0), netControlPanel.OnButtonTest);
                UIFactory.Instance.CreateText(netControlPanel2.transform, "Text", "", UIFactory.Instance.GetRectVertical(clientControlPanelRect, 3, 0), netControlPanel2.SetTextDisplay);
                UIFactory.Instance.CreateButton(netControlPanel2.transform, "ButtonLogin", UIFactory.Instance.GetRectVertical(clientControlPanelRect, 3, 1), netControlPanel2.OnButtonLogin);
                UIFactory.Instance.CreateButton(netControlPanel2.transform, "ButtonLogout", UIFactory.Instance.GetRectVertical(clientControlPanelRect, 3, 2), netControlPanel2.OnButtonLogout);
            }
        }
    }

    void SettingSinglePanelTest()
    {
        // 전체 화면 크기
        _simulatorRect.position = Vector3.zero;
        _simulatorRect.size = new Vector2(Screen.width, Screen.height);

        // 메인 제어판 생성
        _mainControlPanelRect = UIFactory.Instance.GetRectHorizontal(_simulatorRect, 1, 0);
        NetControlPanel netControlPanel = AddNetControlPanel(transform, "MainControlPanel", _mainControlPanelRect);
        UIFactory.Instance.CreateText(netControlPanel.transform, "Text", "", UIFactory.Instance.GetRectVertical(_mainControlPanelRect, 4, 0), netControlPanel.SetTextDisplay);
        UIFactory.Instance.CreateButton(netControlPanel.transform, "ButtonLogin", UIFactory.Instance.GetRectVertical(_mainControlPanelRect, 4, 1), netControlPanel.OnButtonLogin);
        UIFactory.Instance.CreateButton(netControlPanel.transform, "ButtonLogout", UIFactory.Instance.GetRectVertical(_mainControlPanelRect, 4, 2), netControlPanel.OnButtonLogout);
        UIFactory.Instance.CreateButton(netControlPanel.transform, "ButtonPlayDungeonReq", UIFactory.Instance.GetRectVertical(_mainControlPanelRect, 4, 3), netControlPanel.OnButtonPlayDungeonReq);
        //UIFactory.Instance.CreateButton(netControlPanel.transform, "ButtonPlayDungeonReq", UIFactory.Instance.GetRectVertical(_mainControlPanelRect, 3, 2), netControlPanel.OnButtonPlayDungeonReq);
        //NetControlPanel mainControlPanel = AddNetControlPanel(transform, _mainControlPanelRect);

        // 클라 제어판 생성
        //Rect temp1 = UIFactory.Instance.GetRectHorizontal(_simulatorRect, 3, 0);
        //Rect temp2 = UIFactory.Instance.GetRectHorizontal(_simulatorRect, 3, 1);
        //Rect subPanelRect = UIFactory.Instance.GetRectHorizontalSum(temp1, temp2);
        //GameObject subPanel = UIFactory.Instance.CreatePanel(transform, "SubPanel", subPanelRect);
        //int rowCount = 3;
        //int columnCount = 3;
        //for (int i = 0; i < rowCount; i++)
        //{
        //    for (int j = 0; j < columnCount; j++)
        //    {
        //        Rect clientControlPanelRect = UIFactory.Instance.GetRectGrid(subPanelRect, rowCount, columnCount, i, j);
        //        NetControlPanel netControlPanel2 = AddNetControlPanel(subPanel.transform, "ClientControlPanel", clientControlPanelRect);
        //        //UIFactory.Instance.CreateButton(netControlPanel2.transform, "ButtonTest", UIFactory.Instance.GetRectVertical(clientControlPanelRect, 3, 0), netControlPanel.OnButtonTest);
        //        UIFactory.Instance.CreateText(netControlPanel2.transform, "", UIFactory.Instance.GetRectVertical(clientControlPanelRect, 3, 0), netControlPanel2.SetTextDisplay);
        //        UIFactory.Instance.CreateButton(netControlPanel2.transform, "ButtonLogin", UIFactory.Instance.GetRectVertical(clientControlPanelRect, 3, 1), netControlPanel2.OnButtonLogin);
        //        UIFactory.Instance.CreateButton(netControlPanel2.transform, "ButtonLogout", UIFactory.Instance.GetRectVertical(clientControlPanelRect, 3, 2), netControlPanel2.OnButtonLogout);
        //    }
        //}
    }

    void SettingSinglePanelAutoTest()
    {
        // 전체 화면 크기
        _simulatorRect.position = Vector3.zero;
        _simulatorRect.size = new Vector2(Screen.width, Screen.height);

        // 메인 제어판 생성
        _mainControlPanelRect = UIFactory.Instance.GetRectHorizontal(_simulatorRect, 1, 0);
        NetControlPanel netControlPanel = AddNetControlPanel(transform, "MainControlPanel", _mainControlPanelRect);
        UIFactory.Instance.CreateText(netControlPanel.transform, "Text", "", UIFactory.Instance.GetRectVertical(_mainControlPanelRect, 2, 0), netControlPanel.SetTextDisplay);
        UIFactory.Instance.CreateButton(netControlPanel.transform, "ButtonAutoPlayDungeonReq", UIFactory.Instance.GetRectVertical(_mainControlPanelRect, 2, 1), netControlPanel.OnButtonAutoPlayDungeonReq);

        // 자동 로그인
        netControlPanel.OnButtonLogin();
    }

    void SettingMultiPanelAutoTest()
    {
        // 전체 화면 크기
        _simulatorRect.position = Vector3.zero;
        _simulatorRect.size = new Vector2(Screen.width, Screen.height);

        // 메인 제어판 생성
        _mainControlPanelRect = _simulatorRect;
        GameObject mainControlPanel = UIFactory.Instance.CreatePanel(transform, "MainControlPanel", _mainControlPanelRect);

        int userCount = 10;
        int userPosIndex = 0;
        int protoCount = 5;
        int protoPosIndex = 0;
        Rect protocolPanelRect = new Rect();
        GameObject protocolPanel = null;
        NetControlPanel clientPanel = null;

        // 클라이언트 패널 생성
        for (int i = 0; i < userCount; i++)
        {
            protocolPanelRect = UIFactory.Instance.GetRectVertical(_mainControlPanelRect, userCount + 1, userPosIndex++);
            clientPanel = AddNetControlPanel(mainControlPanel.transform, "Panel", protocolPanelRect);
            UIFactory.Instance.CreateText(mainControlPanel.transform, "Text", "Client", protocolPanelRect, clientPanel.SetTextDisplay);
        }

        // Regist & Create Char & Login & AutoTest
        protocolPanelRect = UIFactory.Instance.GetRectVertical(_mainControlPanelRect, userCount + 1, userPosIndex++);
        protocolPanel = UIFactory.Instance.CreatePanel(mainControlPanel.transform, "Panel", protocolPanelRect);
        UIFactory.Instance.CreateButton(protocolPanel.transform, "Regist", UIFactory.Instance.GetRectHorizontal(protocolPanelRect, protoCount, protoPosIndex++), () =>
        {
            ulong uid = 100;
            string name = "user";

            // Regist User
            foreach (NetControlPanel panel in _netControlPanelList)
            {
                string ip = "13.124.76.58";
                ushort port = 20000;
                User user = new User();
                user._data.groupName = "korea0";
                user._data.language = "kr";
                user._data.platform = MSG.PlatForm.ANDROID;
                user._data.regDate = "2017-09-11 11:52:52";
                user._data.did = "abcd";
                user._data.name = name + uid;
                user._data.uid = 0;
                user._data.vc1 = 10000000;
                user._data.vc2 = 10000000;
                user._data.vc3 = 10000000;
                uid++;

                panel.Regist(ip, port, user);
            }
        });
        UIFactory.Instance.CreateButton(protocolPanel.transform, "Login", UIFactory.Instance.GetRectHorizontal(protocolPanelRect, protoCount, protoPosIndex++), () =>
        {
            // Regist User & Create Char
            foreach (NetControlPanel panel in _netControlPanelList)
            {
                string ip = "13.124.76.58";
                ushort port = 20000;
                string id = panel.netData.userId;
                panel.Login(ip, port, id);
            }
        });
        UIFactory.Instance.CreateButton(protocolPanel.transform, "CreateChar", UIFactory.Instance.GetRectHorizontal(protocolPanelRect, protoCount, protoPosIndex++), () =>
        {
            int charNo = 0;

            // Create Char
            foreach (NetControlPanel panel in _netControlPanelList)
            {
                int no = charNo + 1;
                charNo = (charNo + 1) % 2;

                panel.CreateChar(no);
            }
        });
        UIFactory.Instance.CreateButton(protocolPanel.transform, "Logout", UIFactory.Instance.GetRectHorizontal(protocolPanelRect, protoCount, protoPosIndex++), () =>
        {
            foreach (NetControlPanel panel in _netControlPanelList)
            {
                panel.Logout();
            }
        });
        UIFactory.Instance.CreateButton(protocolPanel.transform, "Logout", UIFactory.Instance.GetRectHorizontal(protocolPanelRect, protoCount, protoPosIndex++), () =>
        {
            foreach (NetControlPanel panel in _netControlPanelList)
            {
                panel.Logout();
            }
        });

        // 자동 로그인
        //netControlPanel.OnButtonLogin();
    }

    void SettingSinglePanelProtocolTest()
    {
        // 전체 화면 크기
        _simulatorRect.position = Vector3.zero;
        _simulatorRect.size = new Vector2(Screen.width, Screen.height);

        // 메인 패널 생성
        _mainControlPanelRect = _simulatorRect;
        NetControlPanel mainControlPanel = AddNetControlPanel(transform, "MainControlPanel", _mainControlPanelRect);
        Rect displayPanelRectUpper = UIFactory.Instance.GetRectVertical(_mainControlPanelRect, 5, 0);
        Rect displayPanelRectLower = UIFactory.Instance.GetRectVertical(_mainControlPanelRect, 5, 1);
        Rect displayPanelRect = UIFactory.Instance.GetRectVerticalSum(displayPanelRectUpper, displayPanelRectLower);
        UIFactory.Instance.CreateText(mainControlPanel.transform, "Text", "", displayPanelRect, mainControlPanel.SetTextDisplay);

        // 구분 패널 생성
        Rect temp1 = UIFactory.Instance.GetRectVertical(_mainControlPanelRect, 5, 2);
        Rect temp2 = UIFactory.Instance.GetRectVertical(_mainControlPanelRect, 5, 3);
        Rect temp3 = UIFactory.Instance.GetRectVertical(_mainControlPanelRect, 5, 4);
        Rect tempSubPanelRect = UIFactory.Instance.GetRectVerticalSum(temp1, temp2);
        Rect subPanelRect = UIFactory.Instance.GetRectVerticalSum(tempSubPanelRect, temp3);
        GameObject subPanel = UIFactory.Instance.CreatePanel(mainControlPanel.transform, "SubPanel", subPanelRect);

        int protocolCount = 5;
        int posIndex = 0;
        Rect protocolPanelRect = new Rect();
        GameObject protocolPanel = null;

        // LoginReq
        protocolPanelRect = UIFactory.Instance.GetRectVertical(subPanelRect, protocolCount, posIndex++);
        protocolPanel = UIFactory.Instance.CreatePanel(subPanel.transform, "Panel", protocolPanelRect);
        UIFactory.Instance.CreateInputField(protocolPanel.transform, "IP", "13.124.76.58", UIFactory.Instance.GetRectHorizontal(protocolPanelRect, 4, 0), mainControlPanel.AddInputField);
        UIFactory.Instance.CreateInputField(protocolPanel.transform, "Port", "20000", UIFactory.Instance.GetRectHorizontal(protocolPanelRect, 4, 1), mainControlPanel.AddInputField);
        UIFactory.Instance.CreateInputField(protocolPanel.transform, "ID", "katarn30", UIFactory.Instance.GetRectHorizontal(protocolPanelRect, 4, 2), mainControlPanel.AddInputField);
        UIFactory.Instance.CreateButton(protocolPanel.transform, "LoginReq", UIFactory.Instance.GetRectHorizontal(protocolPanelRect, 4, 3), mainControlPanel.OnButtonLogin);
        //UIFactory.Instance.CreateButton(subPanel.transform, "LoginReq", protocolPanelRect, mainControlPanel.OnButtonLogin);

        // LogoutReq
        protocolPanelRect = UIFactory.Instance.GetRectVertical(subPanelRect, protocolCount, posIndex++);
        //protocolPanel = UIFactory.Instance.CreatePanel(subPanel.transform, "Panel", protocolPanelRect);
        UIFactory.Instance.CreateButton(subPanel.transform, "LogoutReq", protocolPanelRect, mainControlPanel.OnButtonLogout);

        // PlayDungeonReq
        protocolPanelRect = UIFactory.Instance.GetRectVertical(subPanelRect, protocolCount, posIndex++);
        protocolPanel = UIFactory.Instance.CreatePanel(subPanel.transform, "Panel", protocolPanelRect);
        UIFactory.Instance.CreateInputField(protocolPanel.transform, "PlayDungeonReq.dungeonNo", "", UIFactory.Instance.GetRectHorizontal(protocolPanelRect, 3, 0), mainControlPanel.AddInputField);
        UIFactory.Instance.CreateInputField(protocolPanel.transform, "PlayDungeonReq.tier", "", UIFactory.Instance.GetRectHorizontal(protocolPanelRect, 3, 1), mainControlPanel.AddInputField);
        UIFactory.Instance.CreateButton(protocolPanel.transform, "PlayDungeonReq", UIFactory.Instance.GetRectHorizontal(protocolPanelRect, 3, 2), mainControlPanel.OnButtonPlayDungeonReq);

        // LevelupCharReq
        protocolPanelRect = UIFactory.Instance.GetRectVertical(subPanelRect, protocolCount, posIndex++);
        protocolPanel = UIFactory.Instance.CreatePanel(subPanel.transform, "Panel", protocolPanelRect);
        UIFactory.Instance.CreateInputField(protocolPanel.transform, "LevelupCharReq.slotNo", "", UIFactory.Instance.GetRectHorizontal(protocolPanelRect, 2, 0), mainControlPanel.AddInputField);
        UIFactory.Instance.CreateButton(protocolPanel.transform, "LevelupCharReq", UIFactory.Instance.GetRectHorizontal(protocolPanelRect, 2, 1), mainControlPanel.OnButtonLevelupCharReq);

        // TierupCharReq
        protocolPanelRect = UIFactory.Instance.GetRectVertical(subPanelRect, protocolCount, posIndex++);
        protocolPanel = UIFactory.Instance.CreatePanel(subPanel.transform, "Panel", protocolPanelRect);
        UIFactory.Instance.CreateInputField(protocolPanel.transform, "TierupCharReq.slotNo", "", UIFactory.Instance.GetRectHorizontal(protocolPanelRect, 2, 0), mainControlPanel.AddInputField);
        UIFactory.Instance.CreateButton(protocolPanel.transform, "TierupCharReq", UIFactory.Instance.GetRectHorizontal(protocolPanelRect, 2, 1), mainControlPanel.OnButtonTierupCharReq);
    }

    private NetControlPanel AddNetControlPanel(Transform parent, string name, Rect rect)
    {
        GameObject panel = UIFactory.Instance.CreatePanel(parent, name, rect);
        DragPanel dragPanel = panel.AddComponent<DragPanel>();
        NetControlPanel netControlPanel = panel.AddComponent<NetControlPanel>();

        NetClient netClient = panel.AddComponent<NetClient>();
        NetHanderInitializer.Instance.InitNetHandler(netClient, netControlPanel);
        netControlPanel.netClient = netClient;

        _netClientList.Add(netClient);
        _netControlPanelList.Add(netControlPanel);
        
        return netControlPanel;
    }
}
