// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Lobby
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct SA_WonderCubeReward : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_23_1_20(); }
  public static SA_WonderCubeReward GetRootAsSA_WonderCubeReward(ByteBuffer _bb) { return GetRootAsSA_WonderCubeReward(_bb, new SA_WonderCubeReward()); }
  public static SA_WonderCubeReward GetRootAsSA_WonderCubeReward(ByteBuffer _bb, SA_WonderCubeReward obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public SA_WonderCubeReward __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public byte SlotID { get { int o = __p.__offset(4); return o != 0 ? __p.bb.Get(o + __p.bb_pos) : (byte)0; } }
  public Lobby.RewardData? RewardInfo { get { int o = __p.__offset(6); return o != 0 ? (Lobby.RewardData?)(new Lobby.RewardData()).__assign(__p.__indirect(o + __p.bb_pos), __p.bb) : null; } }

  public static Offset<Lobby.SA_WonderCubeReward> CreateSA_WonderCubeReward(FlatBufferBuilder builder,
      byte SlotID = 0,
      Offset<Lobby.RewardData> RewardInfoOffset = default(Offset<Lobby.RewardData>)) {
    builder.StartTable(2);
    SA_WonderCubeReward.AddRewardInfo(builder, RewardInfoOffset);
    SA_WonderCubeReward.AddSlotID(builder, SlotID);
    return SA_WonderCubeReward.EndSA_WonderCubeReward(builder);
  }

  public static void StartSA_WonderCubeReward(FlatBufferBuilder builder) { builder.StartTable(2); }
  public static void AddSlotID(FlatBufferBuilder builder, byte SlotID) { builder.AddByte(0, SlotID, 0); }
  public static void AddRewardInfo(FlatBufferBuilder builder, Offset<Lobby.RewardData> RewardInfoOffset) { builder.AddOffset(1, RewardInfoOffset.Value, 0); }
  public static Offset<Lobby.SA_WonderCubeReward> EndSA_WonderCubeReward(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<Lobby.SA_WonderCubeReward>(o);
  }
  public SA_WonderCubeRewardT UnPack() {
    var _o = new SA_WonderCubeRewardT();
    this.UnPackTo(_o);
    return _o;
  }
  public void UnPackTo(SA_WonderCubeRewardT _o) {
    _o.SlotID = this.SlotID;
    _o.RewardInfo = this.RewardInfo.HasValue ? this.RewardInfo.Value.UnPack() : null;
  }
  public static Offset<Lobby.SA_WonderCubeReward> Pack(FlatBufferBuilder builder, SA_WonderCubeRewardT _o) {
    if (_o == null) return default(Offset<Lobby.SA_WonderCubeReward>);
    var _RewardInfo = _o.RewardInfo == null ? default(Offset<Lobby.RewardData>) : Lobby.RewardData.Pack(builder, _o.RewardInfo);
    return CreateSA_WonderCubeReward(
      builder,
      _o.SlotID,
      _RewardInfo);
  }
}

public class SA_WonderCubeRewardT
{
  public byte SlotID { get; set; }
  public Lobby.RewardDataT RewardInfo { get; set; }

  public SA_WonderCubeRewardT() {
    this.SlotID = 0;
    this.RewardInfo = null;
  }
}


}
