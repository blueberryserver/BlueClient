﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System.IO;
using System.Net.Sockets;
using System;

public class GreenBot : BaseGameEntity, NetHandler
{
    StateManager<GreenBot> _stateMachine = null;
    NetClient _netClient = null;
    string _ip = "";
    ushort _port = 0;
    bool _isConnect;
    bool _isRecv = true;
    float _updateDelayed = 0.0f;
    static GameObject _bodyPrefab = null;
    GameObject _body = null;
    //NeuralNetwork _neuralNetwork = null;
    Vector3 _dirction = Vector3.zero;
    float _speed = 0.0f;

    public GreenBot(int id) : base(id)
    {
        Init();
    }

    public override void Init()
    {
        _stateMachine = new StateManager<GreenBot>(this);
        _stateMachine.SetCurrentState(null);// GreenBotMove.Instance);
        _stateMachine.SetGlobalState(null/*BlueBotLogin::Instance()*/);
        _netClient = NetClientManager.Instance.AddNetClient(_id);
        NetHanderInitializer.Instance.InitNetHandler(_netClient, this);
        _ip = "127.0.0.1";
        _port = 20000;
        _updateDelayed = 0.0f;
        _isRecv = true;
        if (_bodyPrefab == null)
        {
            _bodyPrefab = (GameObject)Resources.Load("Prefab/GreenBot", typeof(GameObject));
        }
        Vector3 position = Vector3.zero;
        float totalGroundWidth = GreenGround.Instance.GetTotalGroundWidth();
        float totalGroundHeight = GreenGround.Instance.GetTotalGroundHeight();
        position.x = UnityEngine.Random.Range(-totalGroundWidth/2.0f, totalGroundWidth/2.0f);
        position.z = UnityEngine.Random.Range(-totalGroundHeight/2.0f, totalGroundHeight/2.0f);
        Quaternion rotation = Quaternion.identity;
        _body = Instantiate(_bodyPrefab, position, rotation);
        float r = UnityEngine.Random.Range(0.0f, 1.0f);
        float g = UnityEngine.Random.Range(0.0f, 1.0f);
        float b = UnityEngine.Random.Range(0.0f, 1.0f);
        _body.GetComponent<Renderer>().material.color = new Color(r, g, b, 1.0f);
        //int totalGroundCount = GreenGround.Instance.GetTotalGroundCount();
        //_neuralNetwork = new NeuralNetwork(/*_fogArray.Length +*/ 3 + 3, 10, 3);
        float dirx = UnityEngine.Random.Range(-1.0f, 1.0f);
        float dirz = UnityEngine.Random.Range(-1.0f, 1.0f);
        _dirction = new Vector3(dirx, 0.0f, dirz);
        _speed = 5.0f;
    }

    public override void Update()
    {
        if (Time.time <= _updateDelayed)
        {
            return;
        }
        else
        {
            _updateDelayed = Time.time + 0.0f;// UnityEngine.Random.Range(0.3f, 1.7f);
        }

        if (_stateMachine != null)
        {
            _stateMachine.Update();
        }
    }

    public override bool HandleMessage(ref Telegram msg)
    {
        return _stateMachine.HandleMessage(ref msg);
    }

    public void ChangeState(State<GreenBot> state)
    {
        _stateMachine.ChangeState(state);
    }

    public void ChangeDelayedState(State<GreenBot> state)
    {
        _stateMachine.ChangeDelayedState(state);
    }

    public void RevertToPreviousState()
    {
        _stateMachine.RevertToPreviousState();
    }

    public NeuralNetwork GetNeuralNetwork()
    {
        return null;// _neuralNetwork;
    }

    public Vector3 GetDirection()
    {
        return _dirction;
    }

    public Vector3 GetPosition()
    {
        return _body.transform.position;
    }

    public void Think()
    {
        Debug.Log("Think");

        //////////////////////////////////////////////////
        if (_netClient.IsConnected() == false)
        {
            Debug.Log("Not connected");
            return;
        }

        if (_isRecv)
        {
            MSG.LoginAns req = new MSG.LoginAns();
            req.err = MSG.ErrorCode.ERR_SUCCESS;
            req.sessionKey = "";
            _netClient.SendPacket(MSG.MsgId.LOGIN_ANS, req);
            _isRecv = false;
        }
        ////////////////////////////////////////////////////

        int inputSize = /*_fogArray.Length +*/ 3 + 3; // fog + pos + dir
        float[] x = new float[inputSize];
        //_fogArray.CopyTo(x, 0);
        //for (int i = 0; i < _fogArray.Length; i++)
        //{
        //    x[i] = _fogArray[i] * 0.01f;
        //}
        Vector3 pos = GetPosition();
        //x[_fogArray.Length] = pos.x;
        //x[_fogArray.Length + 1] = pos.y;
        //x[_fogArray.Length + 2] = pos.z;
        //x[_fogArray.Length + 3] = _dirction.x;
        //x[_fogArray.Length + 4] = _dirction.y;
        //x[_fogArray.Length + 5] = _dirction.z;
        pos.Normalize();
        x[0] = pos.x;
        x[1] = pos.y;
        x[2] = pos.z;
        x[3] = _dirction.x;
        x[4] = _dirction.y;
        x[5] = _dirction.z;
        Vector3 y = Vector3.zero;// _neuralNetwork.Predict(x);
        _dirction = y;
    }

    public void Move()
    {
        _dirction.Normalize();
        Vector3 nextPos = _dirction * _speed * Time.deltaTime;
        _body.transform.position += nextPos;
    }
    
    public bool IsConnect()
    {
        return _isConnect;
    }
    public void Connect()
    {
        if (_netClient)
        {
            _netClient.AsyncConnect(_ip, _port);
        }
    }
    public void OnConnected(NetClient netClient, SocketError errorCode)
    {
        Debug.Log("OnConnected : " + errorCode);
        //textDisplayUpdate = "OnConnected : " + errorCode;

        if (SocketError.Success != errorCode)
        {
            return;
        }

        int id = NetClientManager.Instance.FindID(netClient);
        string name = "user" + id;

        //_user._data.name = name;

        _isConnect = true;
        //_waitForReceive = false;
        //Regist(netClient, name);
        //Login(netClient, name);
    }
    public void OnClosed(NetClient netClient)
    {
    }
    public void OnMessage_Login_Ans(NetClient netClient, MemoryStream stream)
    {
        Debug.Log("OnMessage_Login_Ans");

        _isRecv = true;
    }
    public void OnMessage_Pong_Ans(NetClient netClient, MemoryStream stream)
    {
    }
    public void OnMessage_Regist_Ans(NetClient netClient, MemoryStream stream)
    {
    }
    public void OnMessage_Version_Ans(NetClient netClient, MemoryStream stream)
    {
    }
    public void OnMessage_Chat_Ans(NetClient netClient, MemoryStream stream)
    {
    }
    public void OnMessage_Chat_Not(NetClient netClient, MemoryStream stream)
    {
    }
    public void OnMessage_CreateChatRoom_Ans(NetClient netClient, MemoryStream stream)
    {
    }
    public void OnMessage_CreateChatRoom_Not(NetClient netClient, MemoryStream stream)
    {
    }
    public void OnMessage_InviteChatRoom_Ans(NetClient netClient, MemoryStream stream)
    {
    }
    public void OnMessage_InviteChatRoom_Not(NetClient netClient, MemoryStream stream)
    {
    }
    public void OnMessage_EnterChatRoom_Ans(NetClient netClient, MemoryStream stream)
    {
    }
    public void OnMessage_EnterChatRoom_Not(NetClient netClient, MemoryStream stream)
    {
    }
    public void OnMessage_LeaveChatRoom_Ans(NetClient netClient, MemoryStream stream)
    {
    }
    public void OnMessage_LeaveChatRoom_Not(NetClient netClient, MemoryStream stream)
    {
    }
    public void OnMessage_CreateChar_Ans(NetClient netClient, MemoryStream stream)
    {
    }
    public void OnMessage_Contents_Not(NetClient netClient, MemoryStream stream)
    {
    }
    public void OnMessage_Currency_Not(NetClient netClient, MemoryStream stream)
    {
    }
    public void OnMessage_PlayDungeon_Ans(NetClient netClient, MemoryStream stream)
    {
    }
    public void OnMessage_PlayDungeon_Not(NetClient netClient, MemoryStream stream)
    {
    }
    public void OnMessage_LevelUpChar_Ans(NetClient netClient, MemoryStream stream)
    {
        //MSG.LevelupCharAns ans = ProtoBuf.Serializer.Deserialize<MSG.LevelupCharAns>(stream);

        //Debug.Log("OnMessage_LevelUpChar_Ans : " + ans.err);

        //// ans 처리
        //if (MSG.ErrorCode.ERR_SUCCESS == ans.err)
        //{
        //    int slotNo = (int)ans.char_.slotNo;
        //    _user._data.chars[slotNo] = ans.char_;
        //}

        //Display();

        //// state 처리
        //MessageDispatcher.Instance.DispatchMessage(_id, _id, (int)MSG.MsgId.LEVELUPCHAR_ANS, 0, ans);
    }
    public void OnMessage_TierUpChar_Ans(NetClient netClient, MemoryStream stream)
    {
        //MSG.TierupCharAns ans = ProtoBuf.Serializer.Deserialize<MSG.TierupCharAns>(stream);

        //Debug.Log("OnMessage_TierUpChar_Ans : " + ans.err);

        //// ans 처리
        //if (MSG.ErrorCode.ERR_SUCCESS == ans.err)
        //{
        //    int slotNo = (int)ans.char_.slotNo;
        //    _user._data.chars[slotNo] = ans.char_;
        //}

        //Display();

        //// state 처리
        //MessageDispatcher.Instance.DispatchMessage(_id, _id, (int)MSG.MsgId.TIERUPCHAR_ANS, 0, ans);
    }
}