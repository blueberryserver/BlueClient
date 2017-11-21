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

        if (entity.IsRecv())
        {
            entity.Think();
            entity.SetRecv(false);
        }
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

        MSG.ThinkAns ans = (MSG.ThinkAns)msg._extraInfo;

        if (entity.GetID() != ans.botNo)
        {
            Debug.Log("No same bot:" + ans.botNo);
        }

        //float x = ans.actions[0];
        //float y = ans.actions[1];
        int action = (int)ans.actions[0];
        Vector3 dir = Vector3.zero;
        switch (action)
        {
            case 0:
                dir = (Vector3.forward).normalized;
                break;
            case 1:
                dir = (Vector3.forward + Vector3.left).normalized;
                break;
            case 2:
                dir = (Vector3.left).normalized;
                break;
            case 3:
                dir = (Vector3.back + Vector3.left).normalized;
                break;
            case 4:
                dir = (Vector3.back).normalized;
                break;
            case 5:
                dir = (Vector3.back + Vector3.right).normalized;
                break;
            case 6:
                dir = (Vector3.right).normalized;
                break;
            case 7:
                dir = (Vector3.forward + Vector3.right).normalized;
                break;
        }

        entity.SetDirection(dir);

        entity.SetRecv(true);

        if (ans.error == MSG.ErrorCode.ERR_LOGIN_FAIL)
        {
            entity.ResetBot();
            GreenGround.Instance.ResetGround();
        }

        entity.ChangeDelayedState(GreenBotMove.Instance);
        
        return false;
    }
}