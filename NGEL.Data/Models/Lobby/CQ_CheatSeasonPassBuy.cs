// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Lobby
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct CQ_CheatSeasonPassBuy : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_23_1_20(); }
  public static CQ_CheatSeasonPassBuy GetRootAsCQ_CheatSeasonPassBuy(ByteBuffer _bb) { return GetRootAsCQ_CheatSeasonPassBuy(_bb, new CQ_CheatSeasonPassBuy()); }
  public static CQ_CheatSeasonPassBuy GetRootAsCQ_CheatSeasonPassBuy(ByteBuffer _bb, CQ_CheatSeasonPassBuy obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public CQ_CheatSeasonPassBuy __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }


  public static void StartCQ_CheatSeasonPassBuy(FlatBufferBuilder builder) { builder.StartTable(0); }
  public static Offset<Lobby.CQ_CheatSeasonPassBuy> EndCQ_CheatSeasonPassBuy(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<Lobby.CQ_CheatSeasonPassBuy>(o);
  }
  public CQ_CheatSeasonPassBuyT UnPack() {
    var _o = new CQ_CheatSeasonPassBuyT();
    this.UnPackTo(_o);
    return _o;
  }
  public void UnPackTo(CQ_CheatSeasonPassBuyT _o) {
  }
  public static Offset<Lobby.CQ_CheatSeasonPassBuy> Pack(FlatBufferBuilder builder, CQ_CheatSeasonPassBuyT _o) {
    if (_o == null) return default(Offset<Lobby.CQ_CheatSeasonPassBuy>);
    StartCQ_CheatSeasonPassBuy(builder);
    return EndCQ_CheatSeasonPassBuy(builder);
  }
}

public class CQ_CheatSeasonPassBuyT
{

  public CQ_CheatSeasonPassBuyT() {
  }
}


}
