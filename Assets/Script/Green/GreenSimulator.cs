using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System.IO;
using System.Net.Sockets;
using System;

public class GreenSimulator : MonoBehaviour
{
    void Start()
    {
        // Time setting
        Time.timeScale = 5.0f;

        // Ground
        GreenGround.Instance.Init(10, 10, 10, 10);

        // GreenBot
        int botCount = 1;
        int id = 0;

        Rect mainRect = new Rect(Vector3.zero, new Vector2(Screen.width, Screen.height));
        for (int i = 0; i < botCount; i++)
        {
            GreenBot bot = EntityManager.Instance.AddEntity<GreenBot>(id);
            bot.ChangeDelayedState(GreenBotWaitInput.Instance);
            
            // ControlPanel
            //Rect rect = UIFactory.Instance.GetRectGrid(mainRect, 1, 1, 0, 0);
            Transform tf = ControlPanelManager.Instance.transform;
            ControlPanel clientPanel = ControlPanelManager.Instance.AddControlPanel(id, tf, "MainPanel", mainRect);
            UIFactory.Instance.CreateInputField(clientPanel.transform, "IP", "127.0.0.1", UIFactory.Instance.GetRectVertical(mainRect, 3, 0), bot.SetInputIP);
            UIFactory.Instance.CreateInputField(clientPanel.transform, "Port", "20000", UIFactory.Instance.GetRectVertical(mainRect, 3, 1), bot.SetInputPort);
            UIFactory.Instance.CreateButton(clientPanel.transform, "Start", UIFactory.Instance.GetRectVertical(mainRect, 3, 2), bot.OnStartButton);

            id++;
        }
    }
}

