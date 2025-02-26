// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Lobby
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct CQ_EntitlementReward : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_23_1_20(); }
  public static CQ_EntitlementReward GetRootAsCQ_EntitlementReward(ByteBuffer _bb) { return GetRootAsCQ_EntitlementReward(_bb, new CQ_EntitlementReward()); }
  public static CQ_EntitlementReward GetRootAsCQ_EntitlementReward(ByteBuffer _bb, CQ_EntitlementReward obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public CQ_EntitlementReward __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public int AchievementGroupID { get { int o = __p.__offset(4); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }

  public static Offset<Lobby.CQ_EntitlementReward> CreateCQ_EntitlementReward(FlatBufferBuilder builder,
      int AchievementGroupID = 0) {
    builder.StartTable(1);
    CQ_EntitlementReward.AddAchievementGroupID(builder, AchievementGroupID);
    return CQ_EntitlementReward.EndCQ_EntitlementReward(builder);
  }

  public static void StartCQ_EntitlementReward(FlatBufferBuilder builder) { builder.StartTable(1); }
  public static void AddAchievementGroupID(FlatBufferBuilder builder, int AchievementGroupID) { builder.AddInt(0, AchievementGroupID, 0); }
  public static Offset<Lobby.CQ_EntitlementReward> EndCQ_EntitlementReward(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<Lobby.CQ_EntitlementReward>(o);
  }
  public CQ_EntitlementRewardT UnPack() {
    var _o = new CQ_EntitlementRewardT();
    this.UnPackTo(_o);
    return _o;
  }
  public void UnPackTo(CQ_EntitlementRewardT _o) {
    _o.AchievementGroupID = this.AchievementGroupID;
  }
  public static Offset<Lobby.CQ_EntitlementReward> Pack(FlatBufferBuilder builder, CQ_EntitlementRewardT _o) {
    if (_o == null) return default(Offset<Lobby.CQ_EntitlementReward>);
    return CreateCQ_EntitlementReward(
      builder,
      _o.AchievementGroupID);
  }
}

public class CQ_EntitlementRewardT
{
  public int AchievementGroupID { get; set; }

  public CQ_EntitlementRewardT() {
    this.AchievementGroupID = 0;
  }
}


}
