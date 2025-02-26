// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Lobby
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct SA_EntitlementReward : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_23_1_20(); }
  public static SA_EntitlementReward GetRootAsSA_EntitlementReward(ByteBuffer _bb) { return GetRootAsSA_EntitlementReward(_bb, new SA_EntitlementReward()); }
  public static SA_EntitlementReward GetRootAsSA_EntitlementReward(ByteBuffer _bb, SA_EntitlementReward obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public SA_EntitlementReward __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public int EntitlementID { get { int o = __p.__offset(4); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }

  public static Offset<Lobby.SA_EntitlementReward> CreateSA_EntitlementReward(FlatBufferBuilder builder,
      int EntitlementID = 0) {
    builder.StartTable(1);
    SA_EntitlementReward.AddEntitlementID(builder, EntitlementID);
    return SA_EntitlementReward.EndSA_EntitlementReward(builder);
  }

  public static void StartSA_EntitlementReward(FlatBufferBuilder builder) { builder.StartTable(1); }
  public static void AddEntitlementID(FlatBufferBuilder builder, int EntitlementID) { builder.AddInt(0, EntitlementID, 0); }
  public static Offset<Lobby.SA_EntitlementReward> EndSA_EntitlementReward(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<Lobby.SA_EntitlementReward>(o);
  }
  public SA_EntitlementRewardT UnPack() {
    var _o = new SA_EntitlementRewardT();
    this.UnPackTo(_o);
    return _o;
  }
  public void UnPackTo(SA_EntitlementRewardT _o) {
    _o.EntitlementID = this.EntitlementID;
  }
  public static Offset<Lobby.SA_EntitlementReward> Pack(FlatBufferBuilder builder, SA_EntitlementRewardT _o) {
    if (_o == null) return default(Offset<Lobby.SA_EntitlementReward>);
    return CreateSA_EntitlementReward(
      builder,
      _o.EntitlementID);
  }
}

public class SA_EntitlementRewardT
{
  public int EntitlementID { get; set; }

  public SA_EntitlementRewardT() {
    this.EntitlementID = 0;
  }
}


}
