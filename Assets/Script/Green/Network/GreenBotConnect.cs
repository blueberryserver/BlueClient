using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System.IO;
using System.Net.Sockets;
using System;

public class GreenBotConnect : State<GreenBot>//, BlueNetHandler
{
    private static GreenBotConnect _instance = null;

    public static GreenBotConnect Instance
    {
        get
        {
            if (_instance == null)
            {
                if (_instance == null)
                {
                    _instance = new GreenBotConnect();
                }
            }

            return _instance;
        }
    }

    public override void Enter(GreenBot entity)
    {
        Debug.Log("GreenBotConnect.Enter");
        //NetClientManager.Instance.find

        //if (entity.GetLocation() != Miner.E_LOCATION_TYPE.SHACK)
        //{
        //    Debug.Log("ID : " + entity.GetID());
        //    Debug.Log("Hi~");

        //    entity.ChangeLocation(Miner.E_LOCATION_TYPE.SHACK);

        //    //MessageDispatcher.Instance.DispatchMessage(entity.GetID(), (int)E_ENTITY_ID_LIST.ELSA, (int)E_MESSAGE_TYPE.HI_HONEY_IM_HOME, 0, null);

        //    _asg = true;
        //}

        if (entity.IsConnect() == false)
        {
            entity.Connect();
        }
    }

    public override void Execute(GreenBot entity)
    {
        Debug.Log("GreenBotConnect.Execute");
        //Debug.Log("ID : " + entity.GetID());
        //Debug.Log("Good");

        //entity.SetFatigue(0);

        //if (entity.IsFatigue() == false && _asg == false)
        //{
        //    entity.ChangeState(null/*EnterMineAndDig::Instance()*/);
        //}
        //if (entity.IsConnect() == false && entity.IsWaitForReceive() == false)
        //{
        //    entity.Connect();
        //    entity.SetWaitForReceive(true);
        //}

        if (entity.IsConnect())
        {
            //entity.Login();
            //entity.ChangeState(GreenBotLogin.Instance);
            entity.ChangeState(GreenBotMove.Instance);
        }
    }

    public override void Exit(GreenBot entity)
    {
        Debug.Log("GreenBotConnect.Exit");
        Debug.Log("ID : " + entity.GetID());
        Debug.Log("Bye");
    }

    public override bool OnMessage(GreenBot entity, Telegram msg)
    {
        Debug.Log("GreenBotConnect.OnMessage");
        //switch (msg._msg)
        //{
        //    case (int)E_MESSAGE_TYPE.STEW_READY:
        //        {
        //            Debug.Log("Message handled by id(" + entity.GetID() + ") ");
        //            Debug.Log("ID : " + entity.GetID());
        //            Debug.Log("Okay hun, ahm a-comin'!");
        //            Debug.Log("at time : " + Time.time);

        //            entity.ChangeState(null/*EatStew::Instance()*/);
        //        }
        //        break;
        //    default:
        //        {
        //            return false;
        //        }
        //}

        return false;
    }
}