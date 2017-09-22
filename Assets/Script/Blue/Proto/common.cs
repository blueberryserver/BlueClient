//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from: common.proto
namespace MSG
{
  [global::ProtoBuf.ProtoContract]
  public partial class UserData_
  {
    public UserData_() {}
    
    private ulong _uid;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public ulong uid
    {
      get { return _uid; }
      set { _uid = value; }
    }
    private string _name;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string name
    {
      get { return _name; }
      set { _name = value; }
    }

    private string _did = "";
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string did
    {
      get { return _did; }
      set { _did = value; }
    }

    private MSG.PlatForm _platform = MSG.PlatForm.IOS;
    [global::ProtoBuf.ProtoMember(4, IsRequired = false, DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(MSG.PlatForm.IOS)]
    public MSG.PlatForm platform
    {
      get { return _platform; }
      set { _platform = value; }
    }

    private string _loginDate = "";
    [global::ProtoBuf.ProtoMember(5, IsRequired = false, DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string loginDate
    {
      get { return _loginDate; }
      set { _loginDate = value; }
    }

    private string _logoutDate = "";
    [global::ProtoBuf.ProtoMember(6, IsRequired = false, DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string logoutDate
    {
      get { return _logoutDate; }
      set { _logoutDate = value; }
    }

    private string _regDate = "";
    [global::ProtoBuf.ProtoMember(7, IsRequired = false, DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string regDate
    {
      get { return _regDate; }
      set { _regDate = value; }
    }

    private uint _vc1 = default(uint);
    [global::ProtoBuf.ProtoMember(8, IsRequired = false, DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(uint))]
    public uint vc1
    {
      get { return _vc1; }
      set { _vc1 = value; }
    }

    private uint _vc2 = default(uint);
    [global::ProtoBuf.ProtoMember(9, IsRequired = false, DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(uint))]
    public uint vc2
    {
      get { return _vc2; }
      set { _vc2 = value; }
    }

    private uint _vc3 = default(uint);
    [global::ProtoBuf.ProtoMember(10, IsRequired = false, DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(uint))]
    public uint vc3
    {
      get { return _vc3; }
      set { _vc3 = value; }
    }

    private string _groupName = "";
    [global::ProtoBuf.ProtoMember(11, IsRequired = false, DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string groupName
    {
      get { return _groupName; }
      set { _groupName = value; }
    }

    private string _language = "";
    [global::ProtoBuf.ProtoMember(12, IsRequired = false, DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string language
    {
      get { return _language; }
      set { _language = value; }
    }
    private readonly global::System.Collections.Generic.List<MSG.CharData_> _chars = new global::System.Collections.Generic.List<MSG.CharData_>();
    [global::ProtoBuf.ProtoMember(13, DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<MSG.CharData_> chars
    {
      get { return _chars; }
    }
  

    private MSG.DungeonData_ _lastDungeon = null;
    [global::ProtoBuf.ProtoMember(14, IsRequired = false, DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public MSG.DungeonData_ lastDungeon
    {
      get { return _lastDungeon; }
      set { _lastDungeon = value; }
    }
  }
  
  [global::ProtoBuf.ProtoContract]
  public partial class Contents_
  {
    public Contents_() {}
    
    private MSG.Contents_.ContentType _type;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public MSG.Contents_.ContentType type
    {
      get { return _type; }
      set { _type = value; }
    }
    private string _name;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string name
    {
      get { return _name; }
      set { _name = value; }
    }public enum ContentType
    {
      
      CT_INGAME = 1,
      
      CT_OUTGAME = 2,
      
      CT_INAPPBUY = 3
    }
  
  }
  
  [global::ProtoBuf.ProtoContract]
  public partial class GMember_
  {
    public GMember_() {}
    
    private ulong _uid;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public ulong uid
    {
      get { return _uid; }
      set { _uid = value; }
    }
    private string _name;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string name
    {
      get { return _name; }
      set { _name = value; }
    }
    private MSG.GMember_.GradeType _grade;
    [global::ProtoBuf.ProtoMember(3, IsRequired = true, DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public MSG.GMember_.GradeType grade
    {
      get { return _grade; }
      set { _grade = value; }
    }public enum GradeType
    {
      
      Grade_1 = 1,
      
      Grade_2 = 2,
      
      Grade_3 = 3,
      
      Grade_4 = 4,
      
      Grade_5 = 5
    }
  
  }
  
  [global::ProtoBuf.ProtoContract]
  public partial class Group_
  {
    public Group_() {}
    
    private ulong _gid;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public ulong gid
    {
      get { return _gid; }
      set { _gid = value; }
    }
    private string _name;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string name
    {
      get { return _name; }
      set { _name = value; }
    }
    private string _country;
    [global::ProtoBuf.ProtoMember(3, IsRequired = true, DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string country
    {
      get { return _country; }
      set { _country = value; }
    }
    private ulong _leader;
    [global::ProtoBuf.ProtoMember(4, IsRequired = true, DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public ulong leader
    {
      get { return _leader; }
      set { _leader = value; }
    }
    private uint _limit;
    [global::ProtoBuf.ProtoMember(5, IsRequired = true, DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public uint limit
    {
      get { return _limit; }
      set { _limit = value; }
    }
    private readonly global::System.Collections.Generic.List<MSG.GMember_> _members = new global::System.Collections.Generic.List<MSG.GMember_>();
    [global::ProtoBuf.ProtoMember(6, DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<MSG.GMember_> members
    {
      get { return _members; }
    }
  
  }
  
  [global::ProtoBuf.ProtoContract]
  public partial class ChatData_
  {
    public ChatData_() {}
    
    private ulong _uid;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public ulong uid
    {
      get { return _uid; }
      set { _uid = value; }
    }
    private string _name;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string name
    {
      get { return _name; }
      set { _name = value; }
    }
    private string _groupName;
    [global::ProtoBuf.ProtoMember(3, IsRequired = true, DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string groupName
    {
      get { return _groupName; }
      set { _groupName = value; }
    }
    private string _language;
    [global::ProtoBuf.ProtoMember(4, IsRequired = true, DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string language
    {
      get { return _language; }
      set { _language = value; }
    }
    private string _chat;
    [global::ProtoBuf.ProtoMember(5, IsRequired = true, DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string chat
    {
      get { return _chat; }
      set { _chat = value; }
    }
    private ulong _regDate;
    [global::ProtoBuf.ProtoMember(6, IsRequired = true, DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public ulong regDate
    {
      get { return _regDate; }
      set { _regDate = value; }
    }
  }
  
  [global::ProtoBuf.ProtoContract]
  public partial class ChatRoom_
  {
    public ChatRoom_() {}
    
    private ulong _rid;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public ulong rid
    {
      get { return _rid; }
      set { _rid = value; }
    }
    private string _name;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string name
    {
      get { return _name; }
      set { _name = value; }
    }
    private readonly global::System.Collections.Generic.List<MSG.ChatData_> _chats = new global::System.Collections.Generic.List<MSG.ChatData_>();
    [global::ProtoBuf.ProtoMember(3, DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<MSG.ChatData_> chats
    {
      get { return _chats; }
    }
  
  }
  
  [global::ProtoBuf.ProtoContract]
  public partial class ChatChannel_
  {
    public ChatChannel_() {}
    
    private ulong _cid;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public ulong cid
    {
      get { return _cid; }
      set { _cid = value; }
    }
    private string _name;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string name
    {
      get { return _name; }
      set { _name = value; }
    }
    private readonly global::System.Collections.Generic.List<MSG.ChatData_> _chats = new global::System.Collections.Generic.List<MSG.ChatData_>();
    [global::ProtoBuf.ProtoMember(3, DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<MSG.ChatData_> chats
    {
      get { return _chats; }
    }
  
  }
  
  [global::ProtoBuf.ProtoContract]
  public partial class CharData_
  {
    public CharData_() {}
    
    private ulong _cid;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public ulong cid
    {
      get { return _cid; }
      set { _cid = value; }
    }
    private ulong _uid;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public ulong uid
    {
      get { return _uid; }
      set { _uid = value; }
    }

    private uint _slotNo = default(uint);
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(uint))]
    public uint slotNo
    {
      get { return _slotNo; }
      set { _slotNo = value; }
    }

    private uint _typeNo = default(uint);
    [global::ProtoBuf.ProtoMember(4, IsRequired = false, DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(uint))]
    public uint typeNo
    {
      get { return _typeNo; }
      set { _typeNo = value; }
    }

    private uint _level = default(uint);
    [global::ProtoBuf.ProtoMember(5, IsRequired = false, DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(uint))]
    public uint level
    {
      get { return _level; }
      set { _level = value; }
    }

    private uint _tier = default(uint);
    [global::ProtoBuf.ProtoMember(6, IsRequired = false, DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(uint))]
    public uint tier
    {
      get { return _tier; }
      set { _tier = value; }
    }

    private string _regDate = "";
    [global::ProtoBuf.ProtoMember(7, IsRequired = false, DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string regDate
    {
      get { return _regDate; }
      set { _regDate = value; }
    }
  }
  
  [global::ProtoBuf.ProtoContract]
  public partial class DungeonData_
  {
    public DungeonData_() {}
    
    private ulong _uid;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public ulong uid
    {
      get { return _uid; }
      set { _uid = value; }
    }
    private uint _dungeonNo;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public uint dungeonNo
    {
      get { return _dungeonNo; }
      set { _dungeonNo = value; }
    }
    private uint _dungeonTier;
    [global::ProtoBuf.ProtoMember(3, IsRequired = true, DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public uint dungeonTier
    {
      get { return _dungeonTier; }
      set { _dungeonTier = value; }
    }

    private string _updateDate = "";
    [global::ProtoBuf.ProtoMember(4, IsRequired = false, DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string updateDate
    {
      get { return _updateDate; }
      set { _updateDate = value; }
    }
    private readonly global::System.Collections.Generic.List<MSG.CharData_> _monsters = new global::System.Collections.Generic.List<MSG.CharData_>();
    [global::ProtoBuf.ProtoMember(5, DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<MSG.CharData_> monsters
    {
      get { return _monsters; }
    }
  
  }
  
  [global::ProtoBuf.ProtoContract]
  public partial class BattleData_
  {
    public BattleData_() {}
    
    private uint _srcNo;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public uint srcNo
    {
      get { return _srcNo; }
      set { _srcNo = value; }
    }
    private readonly global::System.Collections.Generic.List<MSG.BattleData_.Attack> _targets = new global::System.Collections.Generic.List<MSG.BattleData_.Attack>();
    [global::ProtoBuf.ProtoMember(2, DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<MSG.BattleData_.Attack> targets
    {
      get { return _targets; }
    }
  
    private MSG.BattleData_.Team _team;
    [global::ProtoBuf.ProtoMember(3, IsRequired = true, DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public MSG.BattleData_.Team team
    {
      get { return _team; }
      set { _team = value; }
    }
  [global::ProtoBuf.ProtoContract]
  public partial class Attack
  {
    public Attack() {}
    
    private uint _no;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public uint no
    {
      get { return _no; }
      set { _no = value; }
    }
    private uint _damage;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public uint damage
    {
      get { return _damage; }
      set { _damage = value; }
    }
    private MSG.BattleData_.AttackResult _result;
    [global::ProtoBuf.ProtoMember(3, IsRequired = true, DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public MSG.BattleData_.AttackResult result
    {
      get { return _result; }
      set { _result = value; }
    }
  }
  public enum AttackResult
    {
      
      ALIVE = 0,
      
      DEAD = 1
    }
  public enum Team
    {
      
      ALLY = 0,
      
      ENEMY = 1
    }
  
  }
  
  [global::ProtoBuf.ProtoContract]
  public partial class DungeonPlayData_
  {
    public DungeonPlayData_() {}
    
    private ulong _lid;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public ulong lid
    {
      get { return _lid; }
      set { _lid = value; }
    }
    private ulong _uid;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public ulong uid
    {
      get { return _uid; }
      set { _uid = value; }
    }
    private readonly global::System.Collections.Generic.List<MSG.BattleData_> _battles = new global::System.Collections.Generic.List<MSG.BattleData_>();
    [global::ProtoBuf.ProtoMember(3, DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<MSG.BattleData_> battles
    {
      get { return _battles; }
    }
  
    private readonly global::System.Collections.Generic.List<MSG.CharData_> _chars = new global::System.Collections.Generic.List<MSG.CharData_>();
    [global::ProtoBuf.ProtoMember(4, DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<MSG.CharData_> chars
    {
      get { return _chars; }
    }
  
    private readonly global::System.Collections.Generic.List<MSG.CharData_> _mobs = new global::System.Collections.Generic.List<MSG.CharData_>();
    [global::ProtoBuf.ProtoMember(5, DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<MSG.CharData_> mobs
    {
      get { return _mobs; }
    }
  
    private string _regDate;
    [global::ProtoBuf.ProtoMember(6, IsRequired = true, DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string regDate
    {
      get { return _regDate; }
      set { _regDate = value; }
    }
  }
  public enum MsgId
    {
      
      CLOSED = 10000,
      
      LOGIN_REQ = 10101,
      
      LOGIN_ANS = 10102,
      
      PING_REQ = 10103,
      
      PONG_ANS = 10104,
      
      REGIST_REQ = 10105,
      
      REGIST_ANS = 10106,
      
      VERSION_REQ = 10107,
      
      VERSION_ANS = 10108,
      
      CHAT_REQ = 20101,
      
      CHAT_ANS = 20102,
      
      CHAT_NOT = 20103,
      
      CREATECHATROOM_REQ = 20111,
      
      CREATECHATROOM_ANS = 20112,
      
      CREATECHATROOM_NOT = 20113,
      
      INVITECHATROOM_REQ = 20121,
      
      INVITECHATROOM_ANS = 20122,
      
      INVITECHATROOM_NOT = 20123,
      
      ENTERCHATROOM_REQ = 20131,
      
      ENTERCHATROOM_ANS = 20132,
      
      ENTERCHATROOM_NOT = 20133,
      
      LEAVECHATROOM_REQ = 20141,
      
      LEAVECHATROOM_ANS = 20142,
      
      LEAVECHATROOM_NOT = 20143,
      
      CREATECHAR_REQ = 20151,
      
      CREATECHAR_ANS = 20152,
      
      CONTENTS_NOT = 20161,
      
      CURRENCY_NOT = 20163,
      
      PLAYDUNGEON_REQ = 20171,
      
      PLAYDUNGEON_ANS = 20172,
      
      PLAYDUNGEON_NOT = 20173,
      
      LEVELUPCHAR_REQ = 20181,
      
      LEVELUPCHAR_ANS = 20182,
      
      TIERUPCHAR_REQ = 20191,
      
      TIERUPCHAR_ANS = 20192,
      
      BATTLELOG_REQ = 20201,
      
      BATTLELOG_ANS = 20202
    }
  public enum ErrorCode
    {
      
      ERR_SUCCESS = 0,
      
      ERR_LOGIN_FAIL = 1,
      
      ERR_ARGUMENT_FAIL = 2,
      
      ERR_AUTHORITY_FAIL = 3,
      
      ERR_SESSIONKEY_FAIL = 4
    }
  public enum PlatForm
    {
      
      IOS = 0,
      
      ANDROID = 1
    }
  public enum ChatType
    {
      
      CHAT_CHANNEL = 1,
      
      CHAT_GROUP = 2,
      
      CHAT_ROOM = 3
    }
  
}