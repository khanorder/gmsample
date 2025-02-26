// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Lobby
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct CQ_IncubationReduceTime : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_23_1_20(); }
  public static CQ_IncubationReduceTime GetRootAsCQ_IncubationReduceTime(ByteBuffer _bb) { return GetRootAsCQ_IncubationReduceTime(_bb, new CQ_IncubationReduceTime()); }
  public static CQ_IncubationReduceTime GetRootAsCQ_IncubationReduceTime(ByteBuffer _bb, CQ_IncubationReduceTime obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public CQ_IncubationReduceTime __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public int IncubatorID { get { int o = __p.__offset(4); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public Lobby.EUserAssetType AssetType { get { int o = __p.__offset(6); return o != 0 ? (Lobby.EUserAssetType)__p.bb.GetUint(o + __p.bb_pos) : Lobby.EUserAssetType.None; } }

  public static Offset<Lobby.CQ_IncubationReduceTime> CreateCQ_IncubationReduceTime(FlatBufferBuilder builder,
      int IncubatorID = 0,
      Lobby.EUserAssetType AssetType = Lobby.EUserAssetType.None) {
    builder.StartTable(2);
    CQ_IncubationReduceTime.AddAssetType(builder, AssetType);
    CQ_IncubationReduceTime.AddIncubatorID(builder, IncubatorID);
    return CQ_IncubationReduceTime.EndCQ_IncubationReduceTime(builder);
  }

  public static void StartCQ_IncubationReduceTime(FlatBufferBuilder builder) { builder.StartTable(2); }
  public static void AddIncubatorID(FlatBufferBuilder builder, int IncubatorID) { builder.AddInt(0, IncubatorID, 0); }
  public static void AddAssetType(FlatBufferBuilder builder, Lobby.EUserAssetType AssetType) { builder.AddUint(1, (uint)AssetType, 0); }
  public static Offset<Lobby.CQ_IncubationReduceTime> EndCQ_IncubationReduceTime(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<Lobby.CQ_IncubationReduceTime>(o);
  }
  public CQ_IncubationReduceTimeT UnPack() {
    var _o = new CQ_IncubationReduceTimeT();
    this.UnPackTo(_o);
    return _o;
  }
  public void UnPackTo(CQ_IncubationReduceTimeT _o) {
    _o.IncubatorID = this.IncubatorID;
    _o.AssetType = this.AssetType;
  }
  public static Offset<Lobby.CQ_IncubationReduceTime> Pack(FlatBufferBuilder builder, CQ_IncubationReduceTimeT _o) {
    if (_o == null) return default(Offset<Lobby.CQ_IncubationReduceTime>);
    return CreateCQ_IncubationReduceTime(
      builder,
      _o.IncubatorID,
      _o.AssetType);
  }
}

public class CQ_IncubationReduceTimeT
{
  public int IncubatorID { get; set; }
  public Lobby.EUserAssetType AssetType { get; set; }

  public CQ_IncubationReduceTimeT() {
    this.IncubatorID = 0;
    this.AssetType = Lobby.EUserAssetType.None;
  }
}


}
