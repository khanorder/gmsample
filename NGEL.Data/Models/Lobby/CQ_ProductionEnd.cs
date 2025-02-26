// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Lobby
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct CQ_ProductionEnd : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_23_1_20(); }
  public static CQ_ProductionEnd GetRootAsCQ_ProductionEnd(ByteBuffer _bb) { return GetRootAsCQ_ProductionEnd(_bb, new CQ_ProductionEnd()); }
  public static CQ_ProductionEnd GetRootAsCQ_ProductionEnd(ByteBuffer _bb, CQ_ProductionEnd obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public CQ_ProductionEnd __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public Lobby.EProductionType ProductionType { get { int o = __p.__offset(4); return o != 0 ? (Lobby.EProductionType)__p.bb.Get(o + __p.bb_pos) : Lobby.EProductionType.None; } }

  public static Offset<Lobby.CQ_ProductionEnd> CreateCQ_ProductionEnd(FlatBufferBuilder builder,
      Lobby.EProductionType ProductionType = Lobby.EProductionType.None) {
    builder.StartTable(1);
    CQ_ProductionEnd.AddProductionType(builder, ProductionType);
    return CQ_ProductionEnd.EndCQ_ProductionEnd(builder);
  }

  public static void StartCQ_ProductionEnd(FlatBufferBuilder builder) { builder.StartTable(1); }
  public static void AddProductionType(FlatBufferBuilder builder, Lobby.EProductionType ProductionType) { builder.AddByte(0, (byte)ProductionType, 0); }
  public static Offset<Lobby.CQ_ProductionEnd> EndCQ_ProductionEnd(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<Lobby.CQ_ProductionEnd>(o);
  }
  public CQ_ProductionEndT UnPack() {
    var _o = new CQ_ProductionEndT();
    this.UnPackTo(_o);
    return _o;
  }
  public void UnPackTo(CQ_ProductionEndT _o) {
    _o.ProductionType = this.ProductionType;
  }
  public static Offset<Lobby.CQ_ProductionEnd> Pack(FlatBufferBuilder builder, CQ_ProductionEndT _o) {
    if (_o == null) return default(Offset<Lobby.CQ_ProductionEnd>);
    return CreateCQ_ProductionEnd(
      builder,
      _o.ProductionType);
  }
}

public class CQ_ProductionEndT
{
  public Lobby.EProductionType ProductionType { get; set; }

  public CQ_ProductionEndT() {
    this.ProductionType = Lobby.EProductionType.None;
  }
}


}
