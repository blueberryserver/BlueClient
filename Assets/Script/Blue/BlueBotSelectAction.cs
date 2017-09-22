using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System.IO;
using System.Net.Sockets;
using System;

public class BlueBotSelectAction : State<BlueBot>
{
    private static BlueBotSelectAction _instance = null;

    public static BlueBotSelectAction Instance
    {
        get
        {
            if (_instance == null)
            {
                if (_instance == null)
                {
                    _instance = new BlueBotSelectAction();
                }
            }

            return _instance;
        }
    }

    public override void Enter(BlueBot entity)
    {
        Debug.Log("BlueBotSelectAction.Enter");

        State<BlueBot> state = entity.SelectAction();

        entity.ChangeState(state);
    }

    public override void Execute(BlueBot entity)
    {
        Debug.Log("BlueBotSelectAction.Execute");
    }

    public override void Exit(BlueBot entity)
    {
        Debug.Log("BlueBotSelectAction.Exit");
    }

    public override bool OnMessage(BlueBot entity, Telegram msg)
    {
        Debug.Log("BlueBotSelectAction.OnMessage");

        return false;
    }
}