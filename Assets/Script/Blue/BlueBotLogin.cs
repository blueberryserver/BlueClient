using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System.IO;
using System.Net.Sockets;
using System;

public class BlueBotLogin : State<BlueBot>//, NetHandler
{
    private static BlueBotLogin _instance = null;

    public static BlueBotLogin Instance
    {
        get
        {
            if (_instance == null)
            {
                if (_instance == null)
                {
                    _instance = new BlueBotLogin();
                }
            }

            return _instance;
        }
    }

    public override void Enter(BlueBot entity)
    {
        Debug.Log("BlueBotLogin.Enter");
        //NetClientManager.Instance.find

        //if (entity.GetLocation() != Miner.E_LOCATION_TYPE.SHACK)
        //{
        //    Debug.Log("ID : " + entity.GetID());
        //    Debug.Log("Hi~");

        //    entity.ChangeLocation(Miner.E_LOCATION_TYPE.SHACK);

        //    //MessageDispatcher.Instance.DispatchMessage(entity.GetID(), (int)E_ENTITY_ID_LIST.ELSA, (int)E_MESSAGE_TYPE.HI_HONEY_IM_HOME, 0, null);

        //    _asg = true;
        //}

        if (entity.IsConnect() && entity.IsLogin() == false)
        {
            entity.Login();
        }
    }

    public override void Execute(BlueBot entity)
    {
        Debug.Log("BlueBotLogin.Execute");

    }

    public override void Exit(BlueBot entity)
    {
        Debug.Log("BlueBotLogin.Exit");
    }

    public override bool OnMessage(BlueBot entity, Telegram msg)
    {
        Debug.Log("BlueBotLogin.OnMessage");

        return false;
    }
}