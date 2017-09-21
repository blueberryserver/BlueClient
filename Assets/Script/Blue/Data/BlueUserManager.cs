using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueUserManager// : MonoBehaviour {
{
    private Dictionary<string, BlueUser> _jobsBySessionKey = new Dictionary<string, BlueUser>();

    private static BlueUserManager _instance = null;
    
    public static BlueUserManager Instance
    {
        get
        {
            if (_instance == null)
            {
                if (_instance == null)
                {
                    _instance = new BlueUserManager();
                }
            }

            return _instance;
        }
    }
    
    public BlueUser Find(string sessionKey_)
    {
        if (_jobsBySessionKey.ContainsKey(sessionKey_))
        {
            return _jobsBySessionKey[sessionKey_];
        }

        return null;
    }

    public BlueUser FindByUid(ulong uid_)
    {
        foreach (var user in _jobsBySessionKey)
        {
            if (user.Value._data.uid == uid_)
            {
                return user.Value;
            }
        }

        return null;
    }
    public BlueUser FindByName(string name_)
    {
        foreach (var user in _jobsBySessionKey)
        {
            if (user.Value._data.name.Equals(name_))
            {
                return user.Value;
            }
        }

        return null;
    }

    public void Add(BlueUser user_)
    {
        var sessionKey = user_._sessionKey;
        _jobsBySessionKey.Add(sessionKey, user_);
    }
    public void Remove(string sessionKey_)
    {
        if (_jobsBySessionKey.ContainsKey(sessionKey_))
        {
            _jobsBySessionKey.Remove(sessionKey_);
        }
    }
}
