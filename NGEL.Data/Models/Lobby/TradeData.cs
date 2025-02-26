// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Lobby
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct TradeData : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_23_1_20(); }
  public static TradeData GetRootAsTradeData(ByteBuffer _bb) { return GetRootAsTradeData(_bb, new TradeData()); }
  public static TradeData GetRootAsTradeData(ByteBuffer _bb, TradeData obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public TradeData __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public Lobby.ETradeType TradeType { get { int o = __p.__offset(4); return o != 0 ? (Lobby.ETradeType)__p.bb.Get(o + __p.bb_pos) : Lobby.ETradeType.None; } }
  public int TradeLevel { get { int o = __p.__offset(6); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int TradeEndAt { get { int o = __p.__offset(8); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public Lobby.ItemData? TradeItemList(int j) { int o = __p.__offset(10); return o != 0 ? (Lobby.ItemData?)(new Lobby.ItemData()).__assign(__p.__vector(o) + j * 8, __p.bb) : null; }
  public int TradeItemListLength { get { int o = __p.__offset(10); return o != 0 ? __p.__vector_len(o) : 0; } }

  public static Offset<Lobby.TradeData> CreateTradeData(FlatBufferBuilder builder,
      Lobby.ETradeType TradeType = Lobby.ETradeType.None,
      int TradeLevel = 0,
      int TradeEndAt = 0,
      VectorOffset TradeItemListOffset = default(VectorOffset)) {
    builder.StartTable(4);
    TradeData.AddTradeItemList(builder, TradeItemListOffset);
    TradeData.AddTradeEndAt(builder, TradeEndAt);
    TradeData.AddTradeLevel(builder, TradeLevel);
    TradeData.AddTradeType(builder, TradeType);
    return TradeData.EndTradeData(builder);
  }

  public static void StartTradeData(FlatBufferBuilder builder) { builder.StartTable(4); }
  public static void AddTradeType(FlatBufferBuilder builder, Lobby.ETradeType TradeType) { builder.AddByte(0, (byte)TradeType, 0); }
  public static void AddTradeLevel(FlatBufferBuilder builder, int TradeLevel) { builder.AddInt(1, TradeLevel, 0); }
  public static void AddTradeEndAt(FlatBufferBuilder builder, int TradeEndAt) { builder.AddInt(2, TradeEndAt, 0); }
  public static void AddTradeItemList(FlatBufferBuilder builder, VectorOffset TradeItemListOffset) { builder.AddOffset(3, TradeItemListOffset.Value, 0); }
  public static void StartTradeItemListVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(8, numElems, 4); }
  public static Offset<Lobby.TradeData> EndTradeData(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<Lobby.TradeData>(o);
  }
  public TradeDataT UnPack() {
    var _o = new TradeDataT();
    this.UnPackTo(_o);
    return _o;
  }
  public void UnPackTo(TradeDataT _o) {
    _o.TradeType = this.TradeType;
    _o.TradeLevel = this.TradeLevel;
    _o.TradeEndAt = this.TradeEndAt;
    _o.TradeItemList = new List<Lobby.ItemDataT>();
    for (var _j = 0; _j < this.TradeItemListLength; ++_j) {_o.TradeItemList.Add(this.TradeItemList(_j).HasValue ? this.TradeItemList(_j).Value.UnPack() : null);}
  }
  public static Offset<Lobby.TradeData> Pack(FlatBufferBuilder builder, TradeDataT _o) {
    if (_o == null) return default(Offset<Lobby.TradeData>);
    var _TradeItemList = default(VectorOffset);
    if (_o.TradeItemList != null) {
      StartTradeItemListVector(builder, _o.TradeItemList.Count);
      for (var _j = _o.TradeItemList.Count - 1; _j >= 0; --_j) { Lobby.ItemData.Pack(builder, _o.TradeItemList[_j]); }
      _TradeItemList = builder.EndVector();
    }
    return CreateTradeData(
      builder,
      _o.TradeType,
      _o.TradeLevel,
      _o.TradeEndAt,
      _TradeItemList);
  }
}

public class TradeDataT
{
  public Lobby.ETradeType TradeType { get; set; }
  public int TradeLevel { get; set; }
  public int TradeEndAt { get; set; }
  public List<Lobby.ItemDataT> TradeItemList { get; set; }

  public TradeDataT() {
    this.TradeType = Lobby.ETradeType.None;
    this.TradeLevel = 0;
    this.TradeEndAt = 0;
    this.TradeItemList = null;
  }
}


}
