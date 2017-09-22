using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System.IO;
using System.Net.Sockets;
using System;

public class BlueBotPlayDungeon : State<BlueBot>
{
    private static BlueBotPlayDungeon _instance = null;

    public static BlueBotPlayDungeon Instance
    {
        get
        {
            if (_instance == null)
            {
                if (_instance == null)
                {
                    _instance = new BlueBotPlayDungeon();
                }
            }

            return _instance;
        }
    }

    public override void Enter(BlueBot entity)
    {
        Debug.Log("BlueBotPlayDungeon.Enter");

        entity.PlayDungeon();
    }

    public override void Execute(BlueBot entity)
    {
        Debug.Log("BlueBotPlayDungeon.Execute");

    }

    public override void Exit(BlueBot entity)
    {
        Debug.Log("BlueBotPlayDungeon.Exit");
    }

    public override bool OnMessage(BlueBot entity, Telegram msg)
    {
        Debug.Log("BlueBotPlayDungeon.OnMessage");

        if (msg._msg == (int)MSG.MsgId.PLAYDUNGEON_ANS)
        {
            MSG.PlayDungeonAns ans = (MSG.PlayDungeonAns)msg._extraInfo;

            State<BlueBot> state = BlueBotSelectAction.Instance;

            if (MSG.ErrorCode.ERR_SUCCESS == ans.err)
            {
                //ans.
                // 클리어? -> SelectAction
                //if (ans.err == )
                {
                    //state = BlueBotSelectAction.Instance;
                }
                // 안 클리어? -> Levelup or Tierup
                {
                    // 레벨업 가능?
                    state = BlueBotLevelup.Instance;
                    // 티어업 가능?
                    state = BlueBotTierup.Instance;
                }
            }
            
            entity.ChangeState(state);
        }

        return false;
    }
}