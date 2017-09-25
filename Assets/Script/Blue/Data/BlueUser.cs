using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Session = NetClient;
using ChatRoom = NetClient; // dummy

public class BlueUser// : MonoBehaviour
{
    public string _sessionKey;// = "";
    public MSG.UserData_ _data;// = new MSG.UserData_();
    public Session _session;// = new Session();
    public long _pingTime;// = 0;
    public HashSet<ChatRoom> _rooms;// = new HashSet<ChatRoom>();

    public BlueUser()
    {
        _sessionKey = "";
        _data = new MSG.UserData_();
        _data.name = "";
        _data.did = "";
        _data.groupName = "";
        _data.language = "";
        _data.loginDate = "";
        _data.logoutDate = "";
        _data.regDate = "";
        _session = new Session();
        _pingTime = 0;
        _rooms = new HashSet<ChatRoom>();
    }
}
