using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System.IO;
using System.Net.Sockets;
using System;

public class BlueBotCreateChar : State<BlueBot>
{
    private static BlueBotCreateChar _instance = null;

    public static BlueBotCreateChar Instance
    {
        get
        {
            if (_instance == null)
            {
                if (_instance == null)
                {
                    _instance = new BlueBotCreateChar();
                }
            }

            return _instance;
        }
    }

    public override void Enter(BlueBot entity)
    {
        Debug.Log("BlueBotCreateChar.Enter");

        List<MSG.CharData_> charList = entity.GetCharList();

        List<int> charNoList = new List<int>();
        charNoList.Add(1);
        charNoList.Add(2);

        for (int i = 0; i < charList.Count; i++)
        {
            if (charNoList[0] == charList[i].typeNo)
            {
                entity.CreateChar(charNoList[1]);
                return;
            }
            else if (charNoList[1] == charList[i].typeNo)
            {
                entity.CreateChar(charNoList[0]);
                return;
            }
        }

        entity.CreateChar(charNoList[0]);
    }

    public override void Execute(BlueBot entity)
    {
        Debug.Log("BlueBotCreateChar.Execute");
    }

    public override void Exit(BlueBot entity)
    {
        Debug.Log("BlueBotCreateChar.Exit");
    }

    public override bool OnMessage(BlueBot entity, Telegram msg)
    {
        Debug.Log("BlueBotCreateChar.OnMessage");

        if (msg._msg == (int)MSG.MsgId.CREATECHAR_ANS)
        {
            MSG.CreateCharAns ans = (MSG.CreateCharAns)msg._extraInfo;

            State<BlueBot> state = entity.SelectAction();

            if (MSG.ErrorCode.ERR_SUCCESS == ans.err)
            {
                // 내용
                if (entity.GetCharCount() <= 1)
                {
                    state = BlueBotCreateChar.Instance;
                }
            }

            entity.ChangeDelayedState(state);
        }

        return false;
    }
}