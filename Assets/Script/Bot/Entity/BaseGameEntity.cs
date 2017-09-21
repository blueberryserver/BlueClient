using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseGameEntity// : MonoBehaviour
{
    protected int _id = 0;

    public BaseGameEntity(int id)
    {
        _id = id;
    }

    public virtual void Update()
    {
    }

    public virtual bool HandleMessage(ref Telegram msg)
    {
        return true;
    }

    public int GetID()
    {
        return _id;
    }

    public virtual void HandleMessage(Telegram msg)
    {

    }
}
