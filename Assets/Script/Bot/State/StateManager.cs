using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager<T> /*: MonoBehaviour*/
{
    T _owner;
    State<T> _currentState;
    State<T> _previousState;
    State<T> _globalState;
    State<T> _nextState;

    //private static StateManager<T> _instance = null;

    //public static StateManager<T> Instance
    //{
    //    get
    //    {
    //        if (_instance == null)
    //        {
    //            if (_instance == null)
    //            {
    //                _instance = new StateManager<T>();
    //            }
    //        }

    //        return _instance;
    //    }
    //}

    public StateManager(T owner)
    {
        _owner = owner;
        _currentState = null;
        _previousState = null;
        _globalState = null;
        _nextState = null;
    }

    public void SetCurrentState(State<T> state)
    {
        _currentState = state;
    }
    public void SetPreviousState(State<T> state)
    {
        _previousState = state;
    }
    public void SetGlobalState(State<T> state)
    {
        _globalState = state;
    }
    public void SetNextState(State<T> state)
    {
        _nextState = state;
    }
    public State<T> GetCurrentState()
    {
        return _currentState;
    }
    public State<T> GetPreviousState()
    {
        return _previousState;
    }
    public State<T> GetGlobalState()
    {
        return _globalState;
    }
    public State<T> GetNextState()
    {
        return _nextState;
    }
    public void Update()
    {
        if (_globalState != null)
        {
            _globalState.Execute(_owner);
        }

        if (_currentState != null)
        {
            _currentState.Execute(_owner);
        }

        if (_nextState != null)
        {
            ChangeState(_nextState);
            _nextState = null;
        }
    }
    public bool HandleMessage(ref Telegram msg)
    {
        if (_currentState != null && _currentState.OnMessage(_owner, msg))
        {
            return true;
        }

        if (_globalState != null && _globalState.OnMessage(_owner, msg))
        {
            return true;
        }

        return false;
    }
    public void ChangeState(State<T> state)
    {
        _previousState = _currentState;

        if (_currentState != null)
        {
            _currentState.Exit(_owner);
        }

        _currentState = state;

        if (_currentState != null)
        {
            _currentState.Enter(_owner);
        } 
    }
    public void ChangeDelayedState(State<T> state)
    {
        if (_nextState != null)
        {
            //ChangeState(_nextState);
            Debug.Log("_nextState != null");
        }

        _nextState = state;
    }
    public void RevertToPreviousState()
    {
        ChangeState(_previousState);
    }
    public bool IsInState(State<T> state)
    {
        return _currentState == state;
    }
}
