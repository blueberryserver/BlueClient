using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System.IO;
using System.Net.Sockets;
using System;

public class BlueBotTierup : State<BlueBot>
{
    private static BlueBotTierup _instance = null;

    public static BlueBotTierup Instance
    {
        get
        {
            if (_instance == null)
            {
                if (_instance == null)
                {
                    _instance = new BlueBotTierup();
                }
            }

            return _instance;
        }
    }

    public override void Enter(BlueBot entity)
    {
        Debug.Log("BlueBotTierup.Enter");

        //State<BlueBot> state = entity.SelectAction();

        //entity.ChangeState(state);

        entity.TierupChar();
    }

    public override void Execute(BlueBot entity)
    {
        Debug.Log("BlueBotTierup.Execute");

    }

    public override void Exit(BlueBot entity)
    {
        Debug.Log("BlueBotTierup.Exit");
    }

    public override bool OnMessage(BlueBot entity, Telegram msg)
    {
        Debug.Log("BlueBotTierup.OnMessage");

        if (msg._msg == (int)MSG.MsgId.TIERUPCHAR_ANS)
        {
            MSG.TierupCharAns ans = (MSG.TierupCharAns)msg._extraInfo;

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