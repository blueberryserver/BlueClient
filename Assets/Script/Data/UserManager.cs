using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserManager// : MonoBehaviour {
{
    // Use this for initialization
    //void Start () {

    //}

    //// Update is called once per frame
    //void Update () {

    //}
    private Dictionary<string, User> _jobsBySessionKey = new Dictionary<string, User>();

    private static UserManager _instance = null;

    public static UserManager Instance
    {
        get
        {
            if (_instance == null)
            {
                //_instance = FindObjectOfType(typeof(UserManager)) as UserManager;
                if (_instance == null)
                {
                    //GameObject prefab = Resources.Load("UIManager") as GameObject;
                    //GameObject obj = Instantiate(prefab) as GameObject;

                    _instance = new UserManager();

                    //Destroy( prefab );
                    //prefab = null;
                }
            }

            return _instance;
        }
    }
    
    public User Find(string sessionKey_)
    {
        if (_jobsBySessionKey.ContainsKey(sessionKey_))
        {
            return _jobsBySessionKey[sessionKey_];
        }

        return new User();
    }
    //User Find(Session sessionPtr_)
    //{

    //}
    public User FindByUid(ulong uid_)
    {
        foreach (var user in _jobsBySessionKey)
        {
            if (user.Value._data.uid == uid_)
            {
                return user.Value;
            }
        }

        return new User();
    }
    public User FindByName(string name_)
    {
        foreach (var user in _jobsBySessionKey)
        {
            if (user.Value._data.name.Equals(name_))
            {
                return user.Value;
            }
        }

        return new User();
    }

    public void Add(User user_)
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
	//void Remove(void* sessionPtr_)
 //   {

 //   }

    //void start();
    //void stop();
}
