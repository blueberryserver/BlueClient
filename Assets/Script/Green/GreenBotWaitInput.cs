using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System.IO;
using System.Net.Sockets;
using System;

public class GreenBotWaitInput : State<GreenBot>
{
    private static GreenBotWaitInput _instance = null;

    public static GreenBotWaitInput Instance
    {
        get
        {
            if (_instance == null)
            {
                if (_instance == null)
                {
                    _instance = new GreenBotWaitInput();
                }
            }

            return _instance;
        }
    }

    public override void Enter(GreenBot entity)
    {
        Debug.Log("GreenBotWaitInput.Enter");
    }

    public override void Execute(GreenBot entity)
    {
        Debug.Log("GreenBotWaitInput.Execute");
    }

    public override void Exit(GreenBot entity)
    {
        Debug.Log("GreenBotWaitInput.Exit");
    }

    public override bool OnMessage(GreenBot entity, Telegram msg)
    {
        Debug.Log("GreenBotWaitInput.OnMessage");

        return false;
    }
}