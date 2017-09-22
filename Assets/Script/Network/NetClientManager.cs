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

    public NetClient AddNetClient(int key)
    {
        if (_netClientDict.ContainsKey(key))
        {
            //_netClientDict[key] = netClient;
            return _netClientDict[key];
        }

        NetClient netClient = gameObject.AddComponent<NetClient>();
        _netClientDict.Add(key, netClient);

        return _netClientDict[key];
    }

    public void RemoveNetClient(int key)
    {
        if (_netClientDict.ContainsKey(key) == false)
        {
            return;
        }

        DestroyObject(_netClientDict[key]); // pooling 방식으로 변경 가능
        _netClientDict.Remove(key);
    }

    public NetClient FindNetClient(int key)
    {
        if (_netClientDict.ContainsKey(key) == false)
        {
            return null;
        }

        return _netClientDict[key];
    }

    public int FindKey(NetClient value)
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
