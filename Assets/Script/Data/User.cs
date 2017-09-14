using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Session = NetClient;
using ChatRoom = NetClient; // dummy

public class User// : MonoBehaviour
{
    // Use this for initialization
    //void Start () {

    //}

    //// Update is called once per frame
    //void Update () {

    //}
    public string _sessionKey = "";
    public MSG.UserData_ _data = new MSG.UserData_();
    public Session _session = new Session();
    public long _pingTime = 0;
    public HashSet<ChatRoom> _rooms = new HashSet<ChatRoom>();
}
