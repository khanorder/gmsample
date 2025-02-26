// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Lobby
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct CQ_FishingCasting : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_23_1_20(); }
  public static CQ_FishingCasting GetRootAsCQ_FishingCasting(ByteBuffer _bb) { return GetRootAsCQ_FishingCasting(_bb, new CQ_FishingCasting()); }
  public static CQ_FishingCasting GetRootAsCQ_FishingCasting(ByteBuffer _bb, CQ_FishingCasting obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public CQ_FishingCasting __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public int ZoneID { get { int o = __p.__offset(4); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int BaitID { get { int o = __p.__offset(6); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }

  public static Offset<Lobby.CQ_FishingCasting> CreateCQ_FishingCasting(FlatBufferBuilder builder,
      int ZoneID = 0,
      int BaitID = 0) {
    builder.StartTable(2);
    CQ_FishingCasting.AddBaitID(builder, BaitID);
    CQ_FishingCasting.AddZoneID(builder, ZoneID);
    return CQ_FishingCasting.EndCQ_FishingCasting(builder);
  }

  public static void StartCQ_FishingCasting(FlatBufferBuilder builder) { builder.StartTable(2); }
  public static void AddZoneID(FlatBufferBuilder builder, int ZoneID) { builder.AddInt(0, ZoneID, 0); }
  public static void AddBaitID(FlatBufferBuilder builder, int BaitID) { builder.AddInt(1, BaitID, 0); }
  public static Offset<Lobby.CQ_FishingCasting> EndCQ_FishingCasting(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<Lobby.CQ_FishingCasting>(o);
  }
  public CQ_FishingCastingT UnPack() {
    var _o = new CQ_FishingCastingT();
    this.UnPackTo(_o);
    return _o;
  }
  public void UnPackTo(CQ_FishingCastingT _o) {
    _o.ZoneID = this.ZoneID;
    _o.BaitID = this.BaitID;
  }
  public static Offset<Lobby.CQ_FishingCasting> Pack(FlatBufferBuilder builder, CQ_FishingCastingT _o) {
    if (_o == null) return default(Offset<Lobby.CQ_FishingCasting>);
    return CreateCQ_FishingCasting(
      builder,
      _o.ZoneID,
      _o.BaitID);
  }
}

public class CQ_FishingCastingT
{
  public int ZoneID { get; set; }
  public int BaitID { get; set; }

  public CQ_FishingCastingT() {
    this.ZoneID = 0;
    this.BaitID = 0;
  }
}


}
