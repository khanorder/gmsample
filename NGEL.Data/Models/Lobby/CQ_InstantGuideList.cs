// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Lobby
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct CQ_InstantGuideList : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_23_1_20(); }
  public static CQ_InstantGuideList GetRootAsCQ_InstantGuideList(ByteBuffer _bb) { return GetRootAsCQ_InstantGuideList(_bb, new CQ_InstantGuideList()); }
  public static CQ_InstantGuideList GetRootAsCQ_InstantGuideList(ByteBuffer _bb, CQ_InstantGuideList obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public CQ_InstantGuideList __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }


  public static void StartCQ_InstantGuideList(FlatBufferBuilder builder) { builder.StartTable(0); }
  public static Offset<Lobby.CQ_InstantGuideList> EndCQ_InstantGuideList(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<Lobby.CQ_InstantGuideList>(o);
  }
  public CQ_InstantGuideListT UnPack() {
    var _o = new CQ_InstantGuideListT();
    this.UnPackTo(_o);
    return _o;
  }
  public void UnPackTo(CQ_InstantGuideListT _o) {
  }
  public static Offset<Lobby.CQ_InstantGuideList> Pack(FlatBufferBuilder builder, CQ_InstantGuideListT _o) {
    if (_o == null) return default(Offset<Lobby.CQ_InstantGuideList>);
    StartCQ_InstantGuideList(builder);
    return EndCQ_InstantGuideList(builder);
  }
}

public class CQ_InstantGuideListT
{

  public CQ_InstantGuideListT() {
  }
}


}
