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

        //entity.ChangeState(BlueBotSelectAction.Instance);
        if (entity.PlayDungeon() == false)
        {
            State<BlueBot> state = entity.SelectAction();
            entity.ChangeState(state);
        }
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
                if (ans.winner.team == MSG.BattleData_.Team.ENEMY)
                {
                    // 레벨업 가능?
                    for (int i = 0; i < entity.GetCharCount(); i++)
                    {
                        int charLevel = entity.GetCharLevel(i);
                        int charTier = entity.GetCharTier(i);
                        if (1 <= charLevel && charLevel < 20)
                        {
                            state = BlueBotLevelup.Instance;
                            break;
                        }
                        else if (20 <= charLevel && 0 <= charTier && charTier < 9)
                        {
                            state = BlueBotTierup.Instance;
                            break;
                        }
                    }
                }
            }
            
            entity.ChangeState(state);
        }

        return false;
    }
}