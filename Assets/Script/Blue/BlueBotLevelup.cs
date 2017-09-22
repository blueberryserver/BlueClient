using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System.IO;
using System.Net.Sockets;
using System;

public class BlueBotLevelup : State<BlueBot>
{
    private static BlueBotLevelup _instance = null;

    public static BlueBotLevelup Instance
    {
        get
        {
            if (_instance == null)
            {
                if (_instance == null)
                {
                    _instance = new BlueBotLevelup();
                }
            }

            return _instance;
        }
    }

    public override void Enter(BlueBot entity)
    {
        Debug.Log("BlueBotLevelup.Enter");

        //State<BlueBot> state = entity.SelectAction();

        //entity.ChangeState(state);

        entity.LevelupChar();
    }

    public override void Execute(BlueBot entity)
    {
        Debug.Log("BlueBotLevelup.Execute");

    }

    public override void Exit(BlueBot entity)
    {
        Debug.Log("BlueBotLevelup.Exit");
    }

    public override bool OnMessage(BlueBot entity, Telegram msg)
    {
        Debug.Log("BlueBotLevelup.OnMessage");

        if (msg._msg == (int)MSG.MsgId.LEVELUPCHAR_ANS)
        {
            MSG.LevelupCharAns ans = (MSG.LevelupCharAns)msg._extraInfo;

            State<BlueBot> state = entity.SelectAction();

            if (MSG.ErrorCode.ERR_SUCCESS == ans.err)
            {
                // 내용
            }

            entity.ChangeState(state);
        }

        return false;
    }
}