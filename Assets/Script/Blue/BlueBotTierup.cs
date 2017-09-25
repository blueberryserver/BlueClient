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

        //entity.ChangeState(BlueBotSelectAction.Instance);
        List<int> slotNoList = new List<int>();
        slotNoList.Add(0);
        slotNoList.Add(1);

        for (int i = 0; i < slotNoList.Count; i++)
        {
            int charTier = entity.GetCharTier(slotNoList[i]);
            if (0 <= charTier && charTier < 9)
            {
                entity.TierupChar(slotNoList[i]);
                return;
            }
        }

        State<BlueBot> state = entity.SelectAction();
        entity.ChangeState(state);
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