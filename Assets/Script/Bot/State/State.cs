using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State<T>// : MonoBehaviour 
{
    public virtual void Enter(T entity) { }
    public virtual void Execute(T entity) { }
    public virtual void Exit(T entity) { }
    public virtual bool OnMessage(T entity, Telegram msg) { return true; }
}
