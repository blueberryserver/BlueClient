using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System.IO;
using System.Net.Sockets;
using System;

public class GreenBotThink : State<GreenBot>
{
    private static GreenBotThink _instance = null;

    public static GreenBotThink Instance
    {
        get
        {
            if (_instance == null)
            {
                if (_instance == null)
                {
                    _instance = new GreenBotThink();
                }
            }

            return _instance;
        }
    }

    public override void Enter(GreenBot entity)
    {
        Debug.Log("GreenBotThink.Enter");

        entity.Think();
    }

    public override void Execute(GreenBot entity)
    {
        Debug.Log("GreenBotThink.Execute");

        //entity.Think();
    
        //entity.ChangeDelayedState(GreenBotMove.Instance);
    }

    public override void Exit(GreenBot entity)
    {
        Debug.Log("GreenBotThink.Exit");
    }

    public override bool OnMessage(GreenBot entity, Telegram msg)
    {
        Debug.Log("GreenBotThink.OnMessage");

        entity.ChangeDelayedState(GreenBotMove.Instance);

        return false;
    }
}