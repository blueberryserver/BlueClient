using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoHomeAndSleepTilRested : State<Miner>
{
    private static GoHomeAndSleepTilRested _instance = null;
    private static bool _asg = false;

    public static GoHomeAndSleepTilRested Instance
    {
        get
        {
            if (_instance == null)
            {
                if (_instance == null)
                {
                    _instance = new GoHomeAndSleepTilRested();
                }
            }

            return _instance;
        }
    }
    
    public override void Enter(Miner entity)
    {
        if (entity.GetLocation() != Miner.E_LOCATION_TYPE.SHACK)
        {
            Debug.Log("ID : " + entity.GetID());
            Debug.Log("Hi~");

            entity.ChangeLocation(Miner.E_LOCATION_TYPE.SHACK);

            MessageDispatcher.Instance.DispatchMessage(entity.GetID(), (int)E_ENTITY_ID_LIST.ELSA, (int)E_MESSAGE_TYPE.HI_HONEY_IM_HOME, 0, null);

            _asg = true;
        }
    }

    public override void Execute(Miner entity)
    {
        Debug.Log("ID : " + entity.GetID());
        Debug.Log("Good");

        entity.SetFatigue(0);

        if (entity.IsFatigue() == false && _asg == false)
        {
            entity.ChangeState(null/*EnterMineAndDig::Instance()*/);
        }
    }

    public override void Exit(Miner entity)
    {
        Debug.Log("ID : " + entity.GetID());
        Debug.Log("Bye");
    }

    public override bool OnMessage(Miner entity, Telegram msg)
    {
        switch (msg._msg)
        {
            case (int)E_MESSAGE_TYPE.STEW_READY:
                {
                    Debug.Log("Message handled by id(" + entity.GetID() + ") ");
                    Debug.Log("ID : " + entity.GetID());
                    Debug.Log("Okay hun, ahm a-comin'!");
                    Debug.Log("at time : " + Time.time);

                    entity.ChangeState(null/*EatStew::Instance()*/);
                }
                break;
            default:
                {
                    return false;
                }
        }

        return false;
    }
}