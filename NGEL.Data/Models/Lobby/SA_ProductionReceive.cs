// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Lobby
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct SA_ProductionReceive : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_23_1_20(); }
  public static SA_ProductionReceive GetRootAsSA_ProductionReceive(ByteBuffer _bb) { return GetRootAsSA_ProductionReceive(_bb, new SA_ProductionReceive()); }
  public static SA_ProductionReceive GetRootAsSA_ProductionReceive(ByteBuffer _bb, SA_ProductionReceive obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public SA_ProductionReceive __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public Lobby.ProductionData? ProductionList(int j) { int o = __p.__offset(4); return o != 0 ? (Lobby.ProductionData?)(new Lobby.ProductionData()).__assign(__p.__vector(o) + j * 12, __p.bb) : null; }
  public int ProductionListLength { get { int o = __p.__offset(4); return o != 0 ? __p.__vector_len(o) : 0; } }
  public Lobby.ItemData? ChangeItemList(int j) { int o = __p.__offset(6); return o != 0 ? (Lobby.ItemData?)(new Lobby.ItemData()).__assign(__p.__vector(o) + j * 8, __p.bb) : null; }
  public int ChangeItemListLength { get { int o = __p.__offset(6); return o != 0 ? __p.__vector_len(o) : 0; } }

  public static Offset<Lobby.SA_ProductionReceive> CreateSA_ProductionReceive(FlatBufferBuilder builder,
      VectorOffset ProductionListOffset = default(VectorOffset),
      VectorOffset ChangeItemListOffset = default(VectorOffset)) {
    builder.StartTable(2);
    SA_ProductionReceive.AddChangeItemList(builder, ChangeItemListOffset);
    SA_ProductionReceive.AddProductionList(builder, ProductionListOffset);
    return SA_ProductionReceive.EndSA_ProductionReceive(builder);
  }

  public static void StartSA_ProductionReceive(FlatBufferBuilder builder) { builder.StartTable(2); }
  public static void AddProductionList(FlatBufferBuilder builder, VectorOffset ProductionListOffset) { builder.AddOffset(0, ProductionListOffset.Value, 0); }
  public static void StartProductionListVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(12, numElems, 4); }
  public static void AddChangeItemList(FlatBufferBuilder builder, VectorOffset ChangeItemListOffset) { builder.AddOffset(1, ChangeItemListOffset.Value, 0); }
  public static void StartChangeItemListVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(8, numElems, 4); }
  public static Offset<Lobby.SA_ProductionReceive> EndSA_ProductionReceive(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<Lobby.SA_ProductionReceive>(o);
  }
  public SA_ProductionReceiveT UnPack() {
    var _o = new SA_ProductionReceiveT();
    this.UnPackTo(_o);
    return _o;
  }
  public void UnPackTo(SA_ProductionReceiveT _o) {
    _o.ProductionList = new List<Lobby.ProductionDataT>();
    for (var _j = 0; _j < this.ProductionListLength; ++_j) {_o.ProductionList.Add(this.ProductionList(_j).HasValue ? this.ProductionList(_j).Value.UnPack() : null);}
    _o.ChangeItemList = new List<Lobby.ItemDataT>();
    for (var _j = 0; _j < this.ChangeItemListLength; ++_j) {_o.ChangeItemList.Add(this.ChangeItemList(_j).HasValue ? this.ChangeItemList(_j).Value.UnPack() : null);}
  }
  public static Offset<Lobby.SA_ProductionReceive> Pack(FlatBufferBuilder builder, SA_ProductionReceiveT _o) {
    if (_o == null) return default(Offset<Lobby.SA_ProductionReceive>);
    var _ProductionList = default(VectorOffset);
    if (_o.ProductionList != null) {
      StartProductionListVector(builder, _o.ProductionList.Count);
      for (var _j = _o.ProductionList.Count - 1; _j >= 0; --_j) { Lobby.ProductionData.Pack(builder, _o.ProductionList[_j]); }
      _ProductionList = builder.EndVector();
    }
    var _ChangeItemList = default(VectorOffset);
    if (_o.ChangeItemList != null) {
      StartChangeItemListVector(builder, _o.ChangeItemList.Count);
      for (var _j = _o.ChangeItemList.Count - 1; _j >= 0; --_j) { Lobby.ItemData.Pack(builder, _o.ChangeItemList[_j]); }
      _ChangeItemList = builder.EndVector();
    }
    return CreateSA_ProductionReceive(
      builder,
      _ProductionList,
      _ChangeItemList);
  }
}

public class SA_ProductionReceiveT
{
  public List<Lobby.ProductionDataT> ProductionList { get; set; }
  public List<Lobby.ItemDataT> ChangeItemList { get; set; }

  public SA_ProductionReceiveT() {
    this.ProductionList = null;
    this.ChangeItemList = null;
  }
}


}
