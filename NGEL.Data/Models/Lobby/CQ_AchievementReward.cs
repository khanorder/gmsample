// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Lobby
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct CQ_AchievementReward : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_23_1_20(); }
  public static CQ_AchievementReward GetRootAsCQ_AchievementReward(ByteBuffer _bb) { return GetRootAsCQ_AchievementReward(_bb, new CQ_AchievementReward()); }
  public static CQ_AchievementReward GetRootAsCQ_AchievementReward(ByteBuffer _bb, CQ_AchievementReward obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public CQ_AchievementReward __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public int AchievementID { get { int o = __p.__offset(4); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }

  public static Offset<Lobby.CQ_AchievementReward> CreateCQ_AchievementReward(FlatBufferBuilder builder,
      int AchievementID = 0) {
    builder.StartTable(1);
    CQ_AchievementReward.AddAchievementID(builder, AchievementID);
    return CQ_AchievementReward.EndCQ_AchievementReward(builder);
  }

  public static void StartCQ_AchievementReward(FlatBufferBuilder builder) { builder.StartTable(1); }
  public static void AddAchievementID(FlatBufferBuilder builder, int AchievementID) { builder.AddInt(0, AchievementID, 0); }
  public static Offset<Lobby.CQ_AchievementReward> EndCQ_AchievementReward(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<Lobby.CQ_AchievementReward>(o);
  }
  public CQ_AchievementRewardT UnPack() {
    var _o = new CQ_AchievementRewardT();
    this.UnPackTo(_o);
    return _o;
  }
  public void UnPackTo(CQ_AchievementRewardT _o) {
    _o.AchievementID = this.AchievementID;
  }
  public static Offset<Lobby.CQ_AchievementReward> Pack(FlatBufferBuilder builder, CQ_AchievementRewardT _o) {
    if (_o == null) return default(Offset<Lobby.CQ_AchievementReward>);
    return CreateCQ_AchievementReward(
      builder,
      _o.AchievementID);
  }
}

public class CQ_AchievementRewardT
{
  public int AchievementID { get; set; }

  public CQ_AchievementRewardT() {
    this.AchievementID = 0;
  }
}


}
