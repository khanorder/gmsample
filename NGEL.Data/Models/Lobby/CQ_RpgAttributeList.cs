// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Lobby
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct CQ_RpgAttributeList : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_23_1_20(); }
  public static CQ_RpgAttributeList GetRootAsCQ_RpgAttributeList(ByteBuffer _bb) { return GetRootAsCQ_RpgAttributeList(_bb, new CQ_RpgAttributeList()); }
  public static CQ_RpgAttributeList GetRootAsCQ_RpgAttributeList(ByteBuffer _bb, CQ_RpgAttributeList obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public CQ_RpgAttributeList __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }


  public static void StartCQ_RpgAttributeList(FlatBufferBuilder builder) { builder.StartTable(0); }
  public static Offset<Lobby.CQ_RpgAttributeList> EndCQ_RpgAttributeList(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<Lobby.CQ_RpgAttributeList>(o);
  }
  public CQ_RpgAttributeListT UnPack() {
    var _o = new CQ_RpgAttributeListT();
    this.UnPackTo(_o);
    return _o;
  }
  public void UnPackTo(CQ_RpgAttributeListT _o) {
  }
  public static Offset<Lobby.CQ_RpgAttributeList> Pack(FlatBufferBuilder builder, CQ_RpgAttributeListT _o) {
    if (_o == null) return default(Offset<Lobby.CQ_RpgAttributeList>);
    StartCQ_RpgAttributeList(builder);
    return EndCQ_RpgAttributeList(builder);
  }
}

public class CQ_RpgAttributeListT
{

  public CQ_RpgAttributeListT() {
  }
}


}
