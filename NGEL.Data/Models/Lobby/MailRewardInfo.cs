// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Lobby
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct MailRewardInfo : IFlatbufferObject
{
  private Struct __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public void __init(int _i, ByteBuffer _bb) { __p = new Struct(_i, _bb); }
  public MailRewardInfo __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public Lobby.ERewardType RewardType { get { return (Lobby.ERewardType)__p.bb.Get(__p.bb_pos + 0); } }
  public int RewardID { get { return __p.bb.GetInt(__p.bb_pos + 4); } }
  public int RewardCount { get { return __p.bb.GetInt(__p.bb_pos + 8); } }

  public static Offset<Lobby.MailRewardInfo> CreateMailRewardInfo(FlatBufferBuilder builder, Lobby.ERewardType RewardType, int RewardID, int RewardCount) {
    builder.Prep(4, 12);
    builder.PutInt(RewardCount);
    builder.PutInt(RewardID);
    builder.Pad(3);
    builder.PutByte((byte)RewardType);
    return new Offset<Lobby.MailRewardInfo>(builder.Offset);
  }
  public MailRewardInfoT UnPack() {
    var _o = new MailRewardInfoT();
    this.UnPackTo(_o);
    return _o;
  }
  public void UnPackTo(MailRewardInfoT _o) {
    _o.RewardType = this.RewardType;
    _o.RewardID = this.RewardID;
    _o.RewardCount = this.RewardCount;
  }
  public static Offset<Lobby.MailRewardInfo> Pack(FlatBufferBuilder builder, MailRewardInfoT _o) {
    if (_o == null) return default(Offset<Lobby.MailRewardInfo>);
    return CreateMailRewardInfo(
      builder,
      _o.RewardType,
      _o.RewardID,
      _o.RewardCount);
  }
}

public class MailRewardInfoT
{
  public Lobby.ERewardType RewardType { get; set; }
  public int RewardID { get; set; }
  public int RewardCount { get; set; }

  public MailRewardInfoT() {
    this.RewardType = Lobby.ERewardType.None;
    this.RewardID = 0;
    this.RewardCount = 0;
  }
}


}
