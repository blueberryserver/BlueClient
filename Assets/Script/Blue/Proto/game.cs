//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from: game.proto
// Note: requires additional types generated from: common.proto
namespace MSG
{
  [global::ProtoBuf.ProtoContract]
  public partial class ContentsNot
  {
    public ContentsNot() {}
    
    private readonly global::System.Collections.Generic.List<string> _contents = new global::System.Collections.Generic.List<string>();
    [global::ProtoBuf.ProtoMember(1, DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<string> contents
    {
      get { return _contents; }
    }
  
    private uint _dungeonNo;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public uint dungeonNo
    {
      get { return _dungeonNo; }
      set { _dungeonNo = value; }
    }
    private uint _tier;
    [global::ProtoBuf.ProtoMember(3, IsRequired = true, DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public uint tier
    {
      get { return _tier; }
      set { _tier = value; }
    }
  }
  
  [global::ProtoBuf.ProtoContract]
  public partial class PlayDungeonNot
  {
    public PlayDungeonNot() {}
    
    private readonly global::System.Collections.Generic.List<MSG.PlayDungeonNot.DungeonPlay> _plays = new global::System.Collections.Generic.List<MSG.PlayDungeonNot.DungeonPlay>();
    [global::ProtoBuf.ProtoMember(1, DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<MSG.PlayDungeonNot.DungeonPlay> plays
    {
      get { return _plays; }
    }
  
  [global::ProtoBuf.ProtoContract]
  public partial class DungeonPlay
  {
    public DungeonPlay() {}
    
    private uint _dungeonNo;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public uint dungeonNo
    {
      get { return _dungeonNo; }
      set { _dungeonNo = value; }
    }
    private uint _tier;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public uint tier
    {
      get { return _tier; }
      set { _tier = value; }
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
  
  }
  
  }
  
  [global::ProtoBuf.ProtoContract]
  public partial class CreateCharReq
  {
    public CreateCharReq() {}
    
    private uint _charNo;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public uint charNo
    {
      get { return _charNo; }
      set { _charNo = value; }
    }
  }
  
  [global::ProtoBuf.ProtoContract]
  public partial class CreateCharAns
  {
    public CreateCharAns() {}
    
    private MSG.ErrorCode _err;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public MSG.ErrorCode err
    {
      get { return _err; }
      set { _err = value; }
    }

    private MSG.CharData_ _char_ = null;
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public MSG.CharData_ char_
    {
      get { return _char_; }
      set { _char_ = value; }
    }
  }
  
  [global::ProtoBuf.ProtoContract]
  public partial class CurrencyNot
  {
    public CurrencyNot() {}
    
    private uint _vc1;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public uint vc1
    {
      get { return _vc1; }
      set { _vc1 = value; }
    }
    private uint _vc2;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public uint vc2
    {
      get { return _vc2; }
      set { _vc2 = value; }
    }
    private uint _vc3;
    [global::ProtoBuf.ProtoMember(3, IsRequired = true, DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public uint vc3
    {
      get { return _vc3; }
      set { _vc3 = value; }
    }
  }
  
  [global::ProtoBuf.ProtoContract]
  public partial class PlayDungeonReq
  {
    public PlayDungeonReq() {}
    
    private uint _dungeonNo;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public uint dungeonNo
    {
      get { return _dungeonNo; }
      set { _dungeonNo = value; }
    }
    private uint _tier;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public uint tier
    {
      get { return _tier; }
      set { _tier = value; }
    }
  }
  
  [global::ProtoBuf.ProtoContract]
  public partial class PlayDungeonAns
  {
    public PlayDungeonAns() {}
    
    private MSG.ErrorCode _err;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public MSG.ErrorCode err
    {
      get { return _err; }
      set { _err = value; }
    }
    private readonly global::System.Collections.Generic.List<MSG.BattleData_> _battles = new global::System.Collections.Generic.List<MSG.BattleData_>();
    [global::ProtoBuf.ProtoMember(2, DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<MSG.BattleData_> battles
    {
      get { return _battles; }
    }
  
    private readonly global::System.Collections.Generic.List<MSG.CharData_> _chars = new global::System.Collections.Generic.List<MSG.CharData_>();
    [global::ProtoBuf.ProtoMember(3, DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<MSG.CharData_> chars
    {
      get { return _chars; }
    }
  
    private readonly global::System.Collections.Generic.List<MSG.CharData_> _mobs = new global::System.Collections.Generic.List<MSG.CharData_>();
    [global::ProtoBuf.ProtoMember(4, DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<MSG.CharData_> mobs
    {
      get { return _mobs; }
    }
  
    private MSG.BattleData_ _winner;
    [global::ProtoBuf.ProtoMember(5, IsRequired = true, DataFormat = global::ProtoBuf.DataFormat.Default)]
    public MSG.BattleData_ winner
    {
      get { return _winner; }
      set { _winner = value; }
    }
  }
  
  [global::ProtoBuf.ProtoContract]
  public partial class LevelupCharReq
  {
    public LevelupCharReq() {}
    
    private uint _slotNo;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public uint slotNo
    {
      get { return _slotNo; }
      set { _slotNo = value; }
    }
  }
  
  [global::ProtoBuf.ProtoContract]
  public partial class LevelupCharAns
  {
    public LevelupCharAns() {}
    
    private MSG.ErrorCode _err;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public MSG.ErrorCode err
    {
      get { return _err; }
      set { _err = value; }
    }

    private MSG.CharData_ _char_ = null;
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public MSG.CharData_ char_
    {
      get { return _char_; }
      set { _char_ = value; }
    }
  }
  
  [global::ProtoBuf.ProtoContract]
  public partial class TierupCharReq
  {
    public TierupCharReq() {}
    
    private uint _slotNo;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public uint slotNo
    {
      get { return _slotNo; }
      set { _slotNo = value; }
    }
  }
  
  [global::ProtoBuf.ProtoContract]
  public partial class TierupCharAns
  {
    public TierupCharAns() {}
    
    private MSG.ErrorCode _err;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public MSG.ErrorCode err
    {
      get { return _err; }
      set { _err = value; }
    }

    private MSG.CharData_ _char_ = null;
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public MSG.CharData_ char_
    {
      get { return _char_; }
      set { _char_ = value; }
    }
  }
  
  [global::ProtoBuf.ProtoContract]
  public partial class BattleLogReq
  {
    public BattleLogReq() {}
    
    private ulong _lid;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public ulong lid
    {
      get { return _lid; }
      set { _lid = value; }
    }
  }
  
  [global::ProtoBuf.ProtoContract]
  public partial class BattleLogAns
  {
    public BattleLogAns() {}
    
    private MSG.ErrorCode _err;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public MSG.ErrorCode err
    {
      get { return _err; }
      set { _err = value; }
    }
    private readonly global::System.Collections.Generic.List<MSG.DungeonPlayData_> _data = new global::System.Collections.Generic.List<MSG.DungeonPlayData_>();
    [global::ProtoBuf.ProtoMember(2, DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<MSG.DungeonPlayData_> data
    {
      get { return _data; }
    }
  
  }
  
}