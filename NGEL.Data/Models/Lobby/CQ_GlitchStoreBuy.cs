// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Lobby
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct CQ_GlitchStoreBuy : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_23_1_20(); }
  public static CQ_GlitchStoreBuy GetRootAsCQ_GlitchStoreBuy(ByteBuffer _bb) { return GetRootAsCQ_GlitchStoreBuy(_bb, new CQ_GlitchStoreBuy()); }
  public static CQ_GlitchStoreBuy GetRootAsCQ_GlitchStoreBuy(ByteBuffer _bb, CQ_GlitchStoreBuy obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public CQ_GlitchStoreBuy __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public int StoreID { get { int o = __p.__offset(4); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }

  public static Offset<Lobby.CQ_GlitchStoreBuy> CreateCQ_GlitchStoreBuy(FlatBufferBuilder builder,
      int StoreID = 0) {
    builder.StartTable(1);
    CQ_GlitchStoreBuy.AddStoreID(builder, StoreID);
    return CQ_GlitchStoreBuy.EndCQ_GlitchStoreBuy(builder);
  }

  public static void StartCQ_GlitchStoreBuy(FlatBufferBuilder builder) { builder.StartTable(1); }
  public static void AddStoreID(FlatBufferBuilder builder, int StoreID) { builder.AddInt(0, StoreID, 0); }
  public static Offset<Lobby.CQ_GlitchStoreBuy> EndCQ_GlitchStoreBuy(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<Lobby.CQ_GlitchStoreBuy>(o);
  }
  public CQ_GlitchStoreBuyT UnPack() {
    var _o = new CQ_GlitchStoreBuyT();
    this.UnPackTo(_o);
    return _o;
  }
  public void UnPackTo(CQ_GlitchStoreBuyT _o) {
    _o.StoreID = this.StoreID;
  }
  public static Offset<Lobby.CQ_GlitchStoreBuy> Pack(FlatBufferBuilder builder, CQ_GlitchStoreBuyT _o) {
    if (_o == null) return default(Offset<Lobby.CQ_GlitchStoreBuy>);
    return CreateCQ_GlitchStoreBuy(
      builder,
      _o.StoreID);
  }
}

public class CQ_GlitchStoreBuyT
{
  public int StoreID { get; set; }

  public CQ_GlitchStoreBuyT() {
    this.StoreID = 0;
  }
}


}
