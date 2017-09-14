using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

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

    void Start()
    {
        //SettingSinglePanel();
        //SettingMultiPanel();
        SettingSinglePanelTest();
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
                UIFactory.Instance.CreateText(netControlPanel2.transform, "", UIFactory.Instance.GetRectVertical(clientControlPanelRect, 3, 0), netControlPanel2.SetTextDisplay);
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
        UIFactory.Instance.CreateText(netControlPanel.transform, "", UIFactory.Instance.GetRectVertical(_mainControlPanelRect, 4, 0), netControlPanel.SetTextDisplay);
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
