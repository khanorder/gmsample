// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Lobby
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct SA_CheatSeasonPassBuy : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_23_1_20(); }
  public static SA_CheatSeasonPassBuy GetRootAsSA_CheatSeasonPassBuy(ByteBuffer _bb) { return GetRootAsSA_CheatSeasonPassBuy(_bb, new SA_CheatSeasonPassBuy()); }
  public static SA_CheatSeasonPassBuy GetRootAsSA_CheatSeasonPassBuy(ByteBuffer _bb, SA_CheatSeasonPassBuy obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public SA_CheatSeasonPassBuy __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public Lobby.SeasonPassData? SeasonPass { get { int o = __p.__offset(4); return o != 0 ? (Lobby.SeasonPassData?)(new Lobby.SeasonPassData()).__assign(__p.__indirect(o + __p.bb_pos), __p.bb) : null; } }

  public static Offset<Lobby.SA_CheatSeasonPassBuy> CreateSA_CheatSeasonPassBuy(FlatBufferBuilder builder,
      Offset<Lobby.SeasonPassData> SeasonPassOffset = default(Offset<Lobby.SeasonPassData>)) {
    builder.StartTable(1);
    SA_CheatSeasonPassBuy.AddSeasonPass(builder, SeasonPassOffset);
    return SA_CheatSeasonPassBuy.EndSA_CheatSeasonPassBuy(builder);
  }

  public static void StartSA_CheatSeasonPassBuy(FlatBufferBuilder builder) { builder.StartTable(1); }
  public static void AddSeasonPass(FlatBufferBuilder builder, Offset<Lobby.SeasonPassData> SeasonPassOffset) { builder.AddOffset(0, SeasonPassOffset.Value, 0); }
  public static Offset<Lobby.SA_CheatSeasonPassBuy> EndSA_CheatSeasonPassBuy(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<Lobby.SA_CheatSeasonPassBuy>(o);
  }
  public SA_CheatSeasonPassBuyT UnPack() {
    var _o = new SA_CheatSeasonPassBuyT();
    this.UnPackTo(_o);
    return _o;
  }
  public void UnPackTo(SA_CheatSeasonPassBuyT _o) {
    _o.SeasonPass = this.SeasonPass.HasValue ? this.SeasonPass.Value.UnPack() : null;
  }
  public static Offset<Lobby.SA_CheatSeasonPassBuy> Pack(FlatBufferBuilder builder, SA_CheatSeasonPassBuyT _o) {
    if (_o == null) return default(Offset<Lobby.SA_CheatSeasonPassBuy>);
    var _SeasonPass = _o.SeasonPass == null ? default(Offset<Lobby.SeasonPassData>) : Lobby.SeasonPassData.Pack(builder, _o.SeasonPass);
    return CreateSA_CheatSeasonPassBuy(
      builder,
      _SeasonPass);
  }
}

public class SA_CheatSeasonPassBuyT
{
  public Lobby.SeasonPassDataT SeasonPass { get; set; }

  public SA_CheatSeasonPassBuyT() {
    this.SeasonPass = null;
  }
}


}
