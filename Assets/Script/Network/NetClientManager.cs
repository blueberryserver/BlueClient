using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetClientManager : MonoBehaviour
{
    Dictionary<int, NetClient> _netClientDict = new Dictionary<int, NetClient>();

    private static NetClientManager _instance = null;

    public static NetClientManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject gameObj = new GameObject("NetClientManager");
                _instance = gameObj.AddComponent<NetClientManager>();

                if (_instance == null)
                {
                    _instance = new NetClientManager();
                }
            }

            return _instance;
        }
    }

    void Start()
    {

    }

    void Update()
    {

    }

    //public NetClient CreateNetClient()
    //{
    //    return gameObject.AddComponent<NetClient>();
    //}

    public NetClient AddNetClient(int id)
    {
        if (_netClientDict.ContainsKey(id))
        {
            //_netClientDict[id] = netClient;
            return _netClientDict[id];
        }

        NetClient netClient = gameObject.AddComponent<NetClient>();
        _netClientDict.Add(id, netClient);

        return _netClientDict[id];
    }

    public void RemoveNetClient(int id)
    {
        if (_netClientDict.ContainsKey(id) == false)
        {
            return;
        }

        DestroyObject(_netClientDict[id]); // pooling 방식으로 변경 가능
        _netClientDict.Remove(id);
    }

    public NetClient FindNetClient(int id)
    {
        if (_netClientDict.ContainsKey(id) == false)
        {
            return null;
        }

        return _netClientDict[id];
    }

    public int FindID(NetClient value)
    {
        foreach (KeyValuePair<int, NetClient> pair in _netClientDict)
        {
            if (pair.Value == value)
            {
                return pair.Key;
            }
        }

        return -1;
    }
}
