using UnityEngine;
using System.Net.Sockets;
using System.Net;
using System;
using System.IO;
using System.Collections.Generic;
using ProtoBuf;
using ProtoBuf.Meta;

public class NetClient : MonoBehaviour
{
    public class NetDispatcher
    {
        public delegate void OnPacket(NetClient netClient, MemoryStream stream);
        private Dictionary<ushort, OnPacket> _packetHandlerMap = new Dictionary<ushort, OnPacket>();

        public void SetPacketHandler(ushort msgId, OnPacket handler)
        {
            if (_packetHandlerMap.ContainsKey(msgId))
            {
                _packetHandlerMap[msgId] = handler;
            }
            else
            {
                _packetHandlerMap.Add(msgId, handler);
            }
        }

        public void RemovePacketHandler(ushort msgId)
        {
            _packetHandlerMap.Remove(msgId);
        }

        public void Dispatch(ushort msgId, NetClient netClient, MemoryStream s)
        {
            if (_packetHandlerMap.ContainsKey(msgId))
            {
                OnPacket handler = _packetHandlerMap[msgId];
                if (handler != null)
                {
                    handler(netClient, s);
                }
            }
            else
            {
                //TorchLogger.LogError( String.Format( "[Dispatch] cannot find msg id {0}", msgId ) );
            }
        }
    }
    public class NetProtoParser
    {
        enum ParsePhase
        {
            LENGTH,
            MSGID,
            BODY
        };

        private ParsePhase phase_ = ParsePhase.LENGTH;
        private ushort len_ = 0;
        private ushort seq_ = 0;    // for udp
        private ushort msgId_ = 0;
        private ushort roomNo_ = 0;
        //private int checksum_ = 0;
        //private int calculatedChecksum_ = 0;
        private ushort offset_ = 0;
        private byte[] recvBuf_ = new byte[4096 * 4];

        public ushort GetMsgId()
        {
            return msgId_;
        }

        public ushort GetRoomNo()
        {
            return roomNo_;
        }

        public ushort GetSeq()
        {
            return seq_;
        }

        // for tcp
        public MemoryStream Do(NetworkStream s)
        {
            if (ParsePhase.LENGTH == phase_)
            {
                // read 2 bytes
                int len = s.Read(recvBuf_, offset_, sizeof(ushort) - offset_);
                if (len <= 0)
                {
                    return null;
                }
                offset_ += (ushort)len;

                // need more bytes
                if (offset_ < sizeof(ushort))
                {
                    //DebugConsole.Log (String.Format("NetProtoParser: need more bytes (pahse:{0})", phase_));
                    return null;
                }

                // recv length header
                len_ = (ushort)/*IPAddress.NetworkToHostOrder*/(BitConverter.ToInt16(recvBuf_, 0));

                // clear offset
                offset_ = 0;
                //phase_ = ParsePhase.SEQ;
                phase_ = ParsePhase.MSGID;
            }

            if (ParsePhase.MSGID == phase_)
            {
                // read 2 bytes
                int len = s.Read(recvBuf_, offset_, sizeof(ushort) - offset_);
                if (len <= 0)
                {
                    return null;
                }
                offset_ += (ushort)len;

                // need more bytes
                if (offset_ < sizeof(ushort))
                {
                    //DebugConsole.Log (String.Format("NetProtoParser: need more bytes (pahse:{0})", phase_));
                    return null;
                }

                // recv msg id header
                msgId_ = (ushort)/*IPAddress.NetworkToHostOrder*/(BitConverter.ToInt16(recvBuf_, 0));

                // clear offset
                offset_ = 0;
                //phase_ = ParsePhase.ROOMNO;
                phase_ = ParsePhase.BODY;
            }

            if (ParsePhase.BODY == phase_)
            {
                int bodyLen = (int)len_ - sizeof(ushort) * 2;
                if (bodyLen < 0)
                {
                    return null;
                }

                if (bodyLen == 0)
                {
                    // clear offset
                    offset_ = 0;
                    phase_ = ParsePhase.LENGTH;

                    // no body -> return empty stream
                    return new MemoryStream();
                }

                if (offset_ + (bodyLen - offset_) > recvBuf_.Length)
                {
                    ////TorchLogger.LogError( String.Format( "[ERROR] argument out of range - {0}, {1}", offset_, bodyLen ) );
                }

                // read (len_ - 6) bytes
                int len = s.Read(recvBuf_, offset_, bodyLen - offset_);
                if (len <= 0)
                {
                    return null;
                }
                offset_ += (ushort)len;

                // need more bytes
                if (offset_ < bodyLen)
                {
                    //DebugConsole.Log (String.Format("NetProtoParser: need more bytes (pahse:{0})", phase_));
                    return null;
                }

                // recv body complete
                MemoryStream ms = new MemoryStream();
                ms.Write(recvBuf_, 0, (int)bodyLen);

                // clear offset
                offset_ = 0;
                phase_ = ParsePhase.LENGTH;

                return ms;
            }

            return null;
        }

        // for udp
        public MemoryStream Do(byte[] buffer)
        {
            // recv length header
            len_ = (ushort)/*IPAddress.NetworkToHostOrder*/(BitConverter.ToInt16(buffer, 0));
            offset_ += sizeof(ushort);

            if (len_ > buffer.Length)
            {
                // not enough data
                return null;
            }

            // recv msg id header
            msgId_ = (ushort)/*IPAddress.NetworkToHostOrder*/(BitConverter.ToInt16(buffer, offset_));
            offset_ += sizeof(ushort);

            int bodyLen = (int)len_ - (int)offset_;
            if (bodyLen < 0)
            {
                // not enough data
                return null;
            }

            // recv body complete
            MemoryStream ms = new MemoryStream();
            ms.Write(buffer, offset_, (int)bodyLen);

            // clear offset
            offset_ = 0;

            return ms;
        }
    }

    // delegate
    public delegate void OnConnected(SocketError errorCode);
	public delegate void OnClosed();
	
	// memebers
	private TcpClient sock_ = null;
    private NetProtoParser parser_ = new NetProtoParser();
	private string errorMessage_;
	private OnConnected onConnected_;
	private OnClosed onClosed_;
	private bool connected_ = false;
	private string serverIp_ = "";
    private IPAddress serverIPAdress;
	private ushort serverPort_;			// tcp port
	private ushort seq_;				// tcp seq
	private System.DateTime lastSend_;
	private System.DateTime lastRecv_;

    // for udp
    private UdpClient udpSock_ = null;
    private NetProtoParser udpParser_ = new NetProtoParser();
	private ushort serverUdpPort_;		// udp port
	private ushort udpSeq_;				// udp seq
	private System.Net.IPEndPoint endPoint_ = new System.Net.IPEndPoint( 0, 0 );
	private IAsyncResult result_;
	private ushort udpRecvSeq_ = 0;

    // message
    private NetDispatcher dispatcher_ = new NetDispatcher();
    //private MessageSerializer serializer_ = new MessageSerializer();

#if UNITY_EDITOR
    static bool _quited = false;
#endif
    private static NetClient _instance = null;
    public static int cur_frame = 0;
    //private NetClient _client = new NetClient();
    private RuntimeTypeModel _typeModel = TypeModel.Create();

    public static NetClient Instance
    {
        get
        {
#if UNITY_EDITOR
            if (_quited)
            {
                return null;
            }
#endif
            if (_instance == null)
            {
                _instance = FindObjectOfType(typeof(NetClient)) as NetClient;
                if (_instance == null)
                {
                    GameObject prefab = Resources.Load("NetClient") as GameObject;
                    GameObject obj = Instantiate(prefab) as GameObject;

                    _instance = FindObjectOfType(typeof(NetClient)) as NetClient;

                    //Destroy( prefab );
                    prefab = null;
                }
            }

            return _instance;
        }
    }
    static public void DestroyInstance()
    {
        //TorchLogger.Log("[Network] DestroyInstance - _instance {0}", _instance);

        if (null != _instance)
        {
            GameObject.Destroy(_instance.gameObject);
            _instance = null;
        }
    }

#if UNITY_EDITOR
    void OnApplicationQuit()
    {
        //TorchLogger.Log("[Network] quit!!");

        _quited = true;

        GameObject.DestroyImmediate(gameObject);
    }
#endif
    void Awake()
    {
        _instance = this;

        DontDestroyOnLoad(transform.gameObject);
        DontDestroyOnLoad(this);
    }

    // Use this for initialization
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        cur_frame = Time.frameCount;

        ProcessPacket();
    }

    public bool SendPacket(MSG.MsgId msgId, object message, bool unreliable = false) // unreliable : udp인가?
    {
        MemoryStream stream = new MemoryStream();
        if (null == _typeModel)
        {
            ProtoBuf.Serializer.Serialize(stream, message);
        }
        else
        {
            if (false == _typeModel.IsDefined(message.GetType()))
            {
                _typeModel.Add(message.GetType(), true);
            }

            _typeModel.Serialize(stream, message);
        }
        
        byte[] byteData = stream.ToArray();
        return SendPacket((ushort)msgId, 0, byteData, (ushort)byteData.Length, unreliable);
    }

    public void SetLastSendTime()
	{
		lastSend_ = System.DateTime.Now;
	}
	
	public void SetLastRecvTime()
	{
		lastRecv_ = System.DateTime.Now;
	}
	
	public System.DateTime GetLastSendTime()
	{
		return lastSend_;
	}
	
	public System.DateTime GetLastRecvTime()
	{
		return lastRecv_;
	}
	
	public void Clear()
	{
		seq_ = 0;
        parser_ = new NetProtoParser();
		udpParser_ = new NetProtoParser();

        clearUDPContext();
	}
	
	public bool Connect(string ip, ushort port)
	{
		if (null != sock_)
		{
			//sock_.Close();
			internalClose();
		}
        IPAddress[] arIPAddresses;
		sock_ = new TcpClient();
		//sock_.NoDelay = true;
        try
        {

            arIPAddresses = Dns.GetHostAddresses(ip);
        }
        catch (Exception e)
        {
            errorMessage_ = e.Message;
            //TorchLogger.LogError("[DNS] failed - " + errorMessage_);
            return false;
        }
        try {
			sock_.Connect(arIPAddresses[0], (int)port);
		}
		catch (Exception e) {
			errorMessage_ = e.Message;
			
			//TorchLogger.LogError( "[Connect] failed - " + errorMessage_ );
			return false;
		}
		
		Clear();
		
		SetLastRecvTime();
		SetLastSendTime();
		
		
		serverPort_ = port;
		connected_ = sock_.Connected;
		//onClosed_ = onClosed;
		return sock_.Connected;
	}
	
	public bool AsyncConnect(string ip, ushort port)
	{
		if (null != sock_)
		{
			//sock_.Close();
			internalClose();
		}
        
        try
        {
            IAsyncResult ar = Dns.BeginGetHostAddresses(ip, new AsyncCallback ( this.DnsCallback ), sock_);
            
        }
        catch (Exception e)
        {
            errorMessage_ = e.Message;
            //TorchLogger.LogError("[DNS] failed - " + errorMessage_);
            return false;
        }
        
        serverPort_ = port;
        //onConnected_ = onConnected;
        //onClosed_ = onClosed;
        return true;
	}
	
	void clearUDPContext()
	{
		// clear udp context
		serverUdpPort_ = 0;
		udpSock_ = null;
		udpSeq_ = 0;
		udpRecvSeq_ = 0;
	}
    private void DnsCallback( IAsyncResult i_Result)
    {
        IPAddress[] arIPAddresses = null;
        try
        {
            arIPAddresses  = Dns.EndGetHostAddresses(i_Result);
            
        }
        catch (Exception e)
        {
			//TorchLogger.Log ("[DNS] failed " + e.Message); 
            errorMessage_ = e.Message;
        }

		//TorchLogger.Log ("DNS Sucess " + arIPAddresses [0]);
		sock_ = new TcpClient(arIPAddresses[0].AddressFamily);

        try
        {
            sock_.BeginConnect(arIPAddresses[0], (int)serverPort_, new AsyncCallback(this.ConnectCallback), sock_);
        }
        catch (Exception e)
        {
            errorMessage_ = e.Message;
			//TorchLogger.Log ("[TCP BeginConnect] failed " + e.Message); 
			errorMessage_ = e.Message;
        }
        serverIPAdress = arIPAddresses[0];
        serverIp_ = arIPAddresses[0].ToString();
        Clear();

    }
	
    private void ConnectCallback(IAsyncResult ar) 
	{		
		SocketError errorCode = 0;
		
        try {
			sock_.EndConnect(ar);			
        } 
		catch (SocketException e) 
		{
			//TorchLogger.Log ("[ConnectCallback] failed " + e.Message); 
			errorCode = (SocketError)e.ErrorCode;
			errorMessage_ = e.Message;
        }
		
		if (0 == errorCode)
		{
			connected_ = true;
		
			SetLastRecvTime();
			SetLastSendTime();
		}
		
		onConnected_(errorCode);
	}
	
	public bool IsConnected()
	{
		if (null == sock_)
		{
			return false;
		}
		
		bool isConnected = false;
		try {
			/*
			 * connect하기 전에 호출하면, null reference exception이 발생할 수 있다.
			 */
			isConnected = sock_.Connected;
		}
		catch
        {
			return false;
		}
		return isConnected;
	}
	
	public bool ConnectUDP(ushort port)
	{
		DisconnectUDP();
		
		try {
			if (null == udpSock_)
			{
                IPEndPoint localEnd;
                if (serverIPAdress.AddressFamily == AddressFamily.InterNetwork )
                {
                    localEnd = new IPEndPoint(IPAddress.Any, 0);
                }
                else 
                {
                    localEnd = new IPEndPoint(IPAddress.IPv6Any, 0);
                }
				udpSock_ = new UdpClient(localEnd);
			}
			serverUdpPort_ = port;
			udpSock_.Connect(serverIPAdress, serverUdpPort_ );
			result_ = udpSock_.BeginReceive( null, null );
		}
		catch (Exception e)
        {
            //TorchLogger.LogError(" [UDP] Connect fail " + e.ToString());
			return false;
		}
		
		return true;
	}
	
	public bool DisconnectUDP()
	{
		if ( null == udpSock_ )
		{
			return false;
		}
		
		try {
			udpSock_.Close();
			udpSock_ = null;
			result_ = null;
		}
		catch
        {
			return false;
		}
		
		return true;
	}
	
	public String GetErrorMessage()
	{
		return errorMessage_;
	}

	private byte[] _packetBuf = new byte[16 * 1024];
	
	public bool SendPacket(ushort msgId, uint roomNo, byte[] sendBuf, ushort len, bool unreliable = false, bool enc = true)
	{
		if ( false == IsConnected() )
		{
			return false;
		}

        const int headerLen = 4;
        short totalLen = (short)((headerLen + len));


		// set header as big-endian
		_packetBuf[0] = (byte)(totalLen);
        _packetBuf[1] = (byte)(totalLen >> 8); 
		_packetBuf[2] = (byte)(msgId);
        _packetBuf[3] = (byte)(msgId >> 8);

		Array.Copy(sendBuf, 0, _packetBuf, headerLen, len);

		if ( unreliable )
		{
			try 
			{
				if ( null != udpSock_ && udpSock_.Send(_packetBuf, totalLen) > 0 )
				{
					udpSeq_++;
					return true;
				}
			}
			catch (Exception e)
			{
#if UNITY_EDITOR || BOOLEAN_TEST_LOG
				//TorchLogger.Log( "[Socket][UDP_Send] error - " + e.Message );
#endif
			}
		}
		
		// send tcp
		try
		{
			NetworkStream s = sock_.GetStream();
			s.Write(_packetBuf, 0, totalLen);
			s.Flush();
		}
		catch (Exception e) {
			errorMessage_ = e.Message;

#if UNITY_EDITOR || BOOLEAN_TEST_LOG
			//TorchLogger.Log( "[Socket][TCP_Send] error - " + errorMessage_ );
#endif

			Close();
			return false;
		}

		if ( unreliable )
		{
			udpSeq_++;
		}
		else
		{
			seq_++;
		}

		SetLastSendTime();

		return true;
	}

	public MemoryStream RecvPacket(ref ushort msgId)
	{
		if (null == sock_)
		{
			return null;
		}
		
		if (false == IsConnected())
		{
			// check closed
			if (connected_)
			{
				// release socket resource
				Close();
			}
			return null;
		}
		
		// for tcp
		try
		{
			NetworkStream s = sock_.GetStream();
			if (s.DataAvailable)
			{
				SetLastRecvTime();
				
				// parser packet
				// if ms is not null, recved complete packet
				// if ms is null, recved incomplete packet or socket error
                if (null == parser_)
                {
                    return null;
                }

                MemoryStream ms = parser_.Do(s);
				if (ms != null)
				{
					msgId = parser_.GetMsgId();
					return ms;
				}

				return null;
			}
		}
		catch (IOException e) {
			errorMessage_ = e.Message;
			internalClose();
			return null;
		}

		if ( null == udpSock_ )
		{
			return null;
		}

		// for udp
        try
        {
			if ( null != result_ && result_.IsCompleted )
			{
				byte[] buffer = udpSock_.EndReceive( result_, ref endPoint_ );
				if ( null == buffer )
				{
					return null;
				}

                ////TorchLogger.Log( "[RecvPacket] recv udp - length " + buffer.Length.ToString() );
                if (null == udpParser_)
                {
                    return null;
                }

                MemoryStream ms = udpParser_.Do( buffer );
				if (ms != null)
				{
					msgId = udpParser_.GetMsgId();
					if ( udpRecvSeq_ > udpParser_.GetSeq() )
					{
#if BOOLEAN_TEST_LOG || UNITY_EDITOR
						//TorchLogger.LogWarning( 
						//	String.Format( "[RecvPacket][UDP] sequence duplicated - [expected:{0}][recved:{1}]", udpRecvSeq_, udpParser_.GetSeq() ) );
#endif
						// drop pakcet
						ms = null;
					}
					else
					{
						udpRecvSeq_ = (ushort)( udpParser_.GetSeq() + 1 );
					}
				}
				else
				{
					//TorchLogger.LogError( "[RecvPacket] recv invalid udp packet - length " + buffer.Length.ToString() );
				}
				
				// post receive again
				result_ = udpSock_.BeginReceive( null, null );
				return ms;
			}
		}
        catch (ObjectDisposedException ex) 
        {
            //TorchLogger.LogError(ex.ToString());
        }
        catch (SocketException ex) 
        { 
            //TorchLogger.LogError(ex.ToString());
			result_ = null;	// to prevent call EndReceive() not call BeginReceive()
        }
        catch (System.Exception ex)
        {
            //TorchLogger.LogError(ex.ToString());
        }
		
		return null;
	}
	
    public bool ProcessPacket()
    {
        if (false == IsConnected())
        {
            return false;
        }

        // recv packet
        ushort msgId = 0;
        MemoryStream s;

        while ((s = RecvPacket(ref msgId)) != null ? true : false)
        {
            // reset position zero to read from first
            s.Position = 0;

#if UNITY_EDITOR
            if (s.Length > 10 * 1024)
            {
                //TorchLogger.LogError("#### recv msgId {0}({1}), length {2}", (myiu.MessageId.MessageId)msgId, msgId, s.Length);
            }
#endif

            // dispatch packet
            dispatcher_.Dispatch(msgId, this, s);
        }

        return true;
    }

	public bool Close()
	{
		bool b = internalClose();
		
		if (connected_)
		{
			// disconnected
			connected_ = false;
			
			// callback
			if (null != onClosed_)
			{
				onClosed_();
			}
		}
		
		return b;
	}
	
	private bool internalClose()
	{
		if (null == sock_)
		{
			return false;
		}
		
		try {
			sock_.Close();
		}
		catch (IOException e) {
			errorMessage_ = e.Message;
			return false;
		}
		sock_ = null;
		return true;
	}
	
//	public static bool CheckIOS()
//	{
//		if (Application.platform == RuntimePlatform.IPhonePlayer)
//		{
//			return true;
//		}
//		return false;
//	}
	
//	[DllImport("__Internal")]
//	private static extern void testIt();

    public void SetAppProtoParser(NetProtoParser parser)
    {
        parser_ = parser;
    }
    public void SetAppProtoParserUdp(NetProtoParser parser)
    {
        udpParser_ = parser;
    }

    public void AddPacketMessage(ushort msgId, NetDispatcher.OnPacket handler)
    {
        dispatcher_.SetPacketHandler(msgId, handler);
    }

    public void AddPacketHandler(ushort msgId, NetDispatcher.OnPacket handler)
    {
        dispatcher_.SetPacketHandler(msgId, handler);
    }

    public void SetOnClosed(OnClosed onClosed)
    {
        onClosed_ = onClosed;
    }

    public void SetOnConnected(OnConnected onConnected)
    {
        onConnected_ = onConnected;
    }
}
