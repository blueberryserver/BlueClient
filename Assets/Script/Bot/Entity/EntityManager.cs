using System.Collections;
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

    public void RegisterEntity(int key, BaseGameEntity entity)
    {
        if (_entityDict.ContainsKey(key))
        {
            return;
        }

        _entityDict.Add(key, entity);
    }

    public void RemoveEntity(int key)
    {
        if (_entityDict.ContainsKey(key) == false)
        {
            return;
        }

        _entityDict.Remove(key);
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
        return null;
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
        //Thread.Sleep(1000);
        //EntityManager.Instance.Update();
        foreach (KeyValuePair<int, BaseGameEntity> pair in _entityDict)
        {
            pair.Value.Update();
        }

        MessageDispatcher.Instance.DispatchDelayedMessages();
    }
}