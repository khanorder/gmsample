// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Lobby
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct CQ_TradeList : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_23_1_20(); }
  public static CQ_TradeList GetRootAsCQ_TradeList(ByteBuffer _bb) { return GetRootAsCQ_TradeList(_bb, new CQ_TradeList()); }
  public static CQ_TradeList GetRootAsCQ_TradeList(ByteBuffer _bb, CQ_TradeList obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public CQ_TradeList __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }


  public static void StartCQ_TradeList(FlatBufferBuilder builder) { builder.StartTable(0); }
  public static Offset<Lobby.CQ_TradeList> EndCQ_TradeList(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<Lobby.CQ_TradeList>(o);
  }
  public CQ_TradeListT UnPack() {
    var _o = new CQ_TradeListT();
    this.UnPackTo(_o);
    return _o;
  }
  public void UnPackTo(CQ_TradeListT _o) {
  }
  public static Offset<Lobby.CQ_TradeList> Pack(FlatBufferBuilder builder, CQ_TradeListT _o) {
    if (_o == null) return default(Offset<Lobby.CQ_TradeList>);
    StartCQ_TradeList(builder);
    return EndCQ_TradeList(builder);
  }
}

public class CQ_TradeListT
{

  public CQ_TradeListT() {
  }
}


}
