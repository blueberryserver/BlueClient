﻿using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public enum E_ENTITY_ID_LIST
{
    ELSA,
    BOB,
    BOT,
};

public class EntityManager : MonoBehaviour
{
    Dictionary<int, BaseGameEntity>	_entityDict = new Dictionary<int, BaseGameEntity>();

    private static EntityManager _instance = null;

    public static EntityManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject gameObj = new GameObject("EntityManager");
                _instance = gameObj.AddComponent<EntityManager>();

                if (_instance == null)
                {
                    _instance = new EntityManager();
                }
            }

            return _instance;
        }
    }

    public T AddEntity<T>(int id)
    {
        //gameObject.AddComponent(typeof(T));
        //gameObject.AddComponent<T>();
        Component component = gameObject.AddComponent(typeof(T));
        object obj = component;
        BaseGameEntity entity = (BaseGameEntity)obj;
        entity.SetID(id);
        entity.Init();
        RegisterEntity(id, entity);
        return (T)obj;
    }

    public void RegisterEntity(int id, BaseGameEntity entity)
    {
        if (_entityDict.ContainsKey(id))
        {
            return;
        }

        _entityDict.Add(id, entity);
    }

    public void RemoveEntity(int id)
    {
        if (_entityDict.ContainsKey(id) == false)
        {
            return;
        }

        _entityDict.Remove(id);
    }

    public void RemoveEntity(BaseGameEntity entity)
    {
        if (_entityDict.ContainsValue(entity) == false)
        {
            return;
        }

        foreach (KeyValuePair<int, BaseGameEntity> pair in _entityDict)
        {
            if (pair.Value == entity)
            {
                _entityDict.Remove(pair.Key);
                return;
            }
        }
    }

    public BaseGameEntity GetEntityFromID(int id)
    {
        if (_entityDict.ContainsKey(id) == false)
        {
            return null;
        }

        return _entityDict[id];
    }

    // Use this for initialization
    void Start()
    {
        //Miner miner(EEIL_BOB);
        //Wife wife(EEIL_ELSA);

        //EntityMgr->RegisterEntity(&miner);
        //EntityMgr->RegisterEntity(&wife);
    }

    // Update is called once per frame
    public void Update()
    {
        //if (Time.time <= _delayTime)
        //{
        //    return;
        //}
        //else
        //{
        //    _delayTime = Time.time + 5.0f;
        //}

        //Thread.Sleep(100);
        //foreach (KeyValuePair<int, BaseGameEntity> pair in _entityDict)
        //{
        //    pair.Value.Update();
        //}

        MessageDispatcher.Instance.DispatchDelayedMessages();
    }
}