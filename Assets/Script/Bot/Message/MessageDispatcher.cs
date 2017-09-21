using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum E_MESSAGE_TYPE
{
    HI_HONEY_IM_HOME,
    STEW_READY
}

public class MessageDispatcher// : MonoBehaviour
{
    //list.Sort((l, r) => -1 * l.value.CompareTo(r.value)); // (a, b) => a.CompareTo(b
    List<Telegram> _priorityQueue = new List<Telegram>();
    //SortedSet<Telegram> _priorityQueue = new SortedSet<Telegram>(new Telegram.TelegramComparer());

    private static MessageDispatcher _instance = null;

    public static MessageDispatcher Instance
    {
        get
        {
            if (_instance == null)
            {
                if (_instance == null)
                {
                    _instance = new MessageDispatcher();
                }
            }

            return _instance;
        }
    }

    public void DispatchMessage(int sender, int receiver, int msg, ulong delay, object extraInfo)
    {
        BaseGameEntity entity = EntityManager.Instance.GetEntityFromID(receiver);

        Telegram telegram = new Telegram(sender, receiver, msg, delay, extraInfo);

        if (delay <= 0)
        {
            Discharge(entity, ref telegram);
        }
        else
        {
            ulong currentTime = (ulong)Time.time;

            telegram._dispatchTime = currentTime + delay;

            _priorityQueue.Add(telegram);
        }
    }

    public void DispatchDelayedMessages()
    {
        ulong currentTime = (ulong)Time.time; //Clock->GetCurrentTime();

        while (0 < _priorityQueue.Count
            && (0 < _priorityQueue[0]._dispatchTime)
            && (_priorityQueue[0]._dispatchTime < currentTime))
        {
            Telegram telegram = _priorityQueue[0];

            BaseGameEntity receiver = EntityManager.Instance.GetEntityFromID(telegram._receiver);

            Discharge(receiver, ref telegram);

            _priorityQueue.Remove(_priorityQueue[0]);
        }
    }

	private void Discharge(BaseGameEntity receiver, ref Telegram msg)
    {
        receiver.HandleMessage(msg);
    }
}

public class Telegram
{
    public int _sender;
    public int _receiver;
    public int _msg;
    public ulong _dispatchTime;
    public object _extraInfo;

    public Telegram(int sender, int receiver, int msg, ulong dispatchTime, object extraInfo)
    {
        _sender = sender;
        _receiver = receiver;
        _msg = msg;
        _dispatchTime = dispatchTime;
        _extraInfo = extraInfo;
    }
}