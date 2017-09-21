using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Miner : BaseGameEntity
{
    public enum E_LOCATION_TYPE
    {
        SHACK,
        HOME,
        MINE,
        BANK
    }

    StateManager<Miner> _stateMachine;
    E_LOCATION_TYPE _location;
    int _goldCarried;
    int _moneyInBank;
    int _thirst;
    int _fatigue;
    
    public Miner(int id) : base(id)
    {
        _stateMachine = new StateManager<Miner>(this);
        _stateMachine.SetCurrentState(null/*GoHomeAndSleepTilRested::Instance()*/);
        _stateMachine.SetGlobalState(null/*MinerGlobalState::Instance()*/);
        _location = E_LOCATION_TYPE.SHACK;
        _goldCarried = 0;
        _moneyInBank = 0;
        _thirst = 0;
        _fatigue = 0;
    }

    public override void Update()
    {
        _thirst++;
        if (_stateMachine != null)
        {
            _stateMachine.Update();
        }
    }

    public override bool HandleMessage(ref Telegram msg)
    {
        return _stateMachine.HandleMessage(ref msg);
    }

    public void ChangeState(State<Miner> state)
    {
        _stateMachine.ChangeState(state);
    }

    public void RevertToPreviousState()
    {
        _stateMachine.RevertToPreviousState();
    }

    public void ChangeLocation(E_LOCATION_TYPE location)
    {
        if (_location != location)
        {
            _location = location;
        }
    }

    public E_LOCATION_TYPE GetLocation()
    {
        return _location;
    }

    public void AddToGoldCarried(int goldCarried)
    {
        _goldCarried += goldCarried;
    }

    public void DepositMoneyInBank()
    {
        _moneyInBank += _goldCarried;
        _goldCarried = 0;
    }

    public void SetFatigue(int fatigue)
    {
        _fatigue = fatigue;
    }

    public void IncreaseFatigue()
    {
        _fatigue++;
    }

    public void QuenchThirst()
    {
        _thirst = 0;
    }

    public bool IsPocketsFull()
    {
        return 100 <= _goldCarried ? true : false;
    }

    public bool IsThirsty()
    {
        return 50 <= _thirst ? true : false;
    }

    public bool IsFatigue()
    {
        return 10 <= _fatigue ? true : false;
    }
}