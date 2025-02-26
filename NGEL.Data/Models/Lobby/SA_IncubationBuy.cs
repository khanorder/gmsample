// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Lobby
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct SA_IncubationBuy : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_23_1_20(); }
  public static SA_IncubationBuy GetRootAsSA_IncubationBuy(ByteBuffer _bb) { return GetRootAsSA_IncubationBuy(_bb, new SA_IncubationBuy()); }
  public static SA_IncubationBuy GetRootAsSA_IncubationBuy(ByteBuffer _bb, SA_IncubationBuy obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public SA_IncubationBuy __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public Lobby.IncubationData? Incubation { get { int o = __p.__offset(4); return o != 0 ? (Lobby.IncubationData?)(new Lobby.IncubationData()).__assign(o + __p.bb_pos, __p.bb) : null; } }
  public Lobby.AssetData? ChangeAssetList(int j) { int o = __p.__offset(6); return o != 0 ? (Lobby.AssetData?)(new Lobby.AssetData()).__assign(__p.__vector(o) + j * 12, __p.bb) : null; }
  public int ChangeAssetListLength { get { int o = __p.__offset(6); return o != 0 ? __p.__vector_len(o) : 0; } }

  public static Offset<Lobby.SA_IncubationBuy> CreateSA_IncubationBuy(FlatBufferBuilder builder,
      Lobby.IncubationDataT Incubation = null,
      VectorOffset ChangeAssetListOffset = default(VectorOffset)) {
    builder.StartTable(2);
    SA_IncubationBuy.AddChangeAssetList(builder, ChangeAssetListOffset);
    SA_IncubationBuy.AddIncubation(builder, Lobby.IncubationData.Pack(builder, Incubation));
    return SA_IncubationBuy.EndSA_IncubationBuy(builder);
  }

  public static void StartSA_IncubationBuy(FlatBufferBuilder builder) { builder.StartTable(2); }
  public static void AddIncubation(FlatBufferBuilder builder, Offset<Lobby.IncubationData> IncubationOffset) { builder.AddStruct(0, IncubationOffset.Value, 0); }
  public static void AddChangeAssetList(FlatBufferBuilder builder, VectorOffset ChangeAssetListOffset) { builder.AddOffset(1, ChangeAssetListOffset.Value, 0); }
  public static void StartChangeAssetListVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(12, numElems, 4); }
  public static Offset<Lobby.SA_IncubationBuy> EndSA_IncubationBuy(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<Lobby.SA_IncubationBuy>(o);
  }
  public SA_IncubationBuyT UnPack() {
    var _o = new SA_IncubationBuyT();
    this.UnPackTo(_o);
    return _o;
  }
  public void UnPackTo(SA_IncubationBuyT _o) {
    _o.Incubation = this.Incubation.HasValue ? this.Incubation.Value.UnPack() : null;
    _o.ChangeAssetList = new List<Lobby.AssetDataT>();
    for (var _j = 0; _j < this.ChangeAssetListLength; ++_j) {_o.ChangeAssetList.Add(this.ChangeAssetList(_j).HasValue ? this.ChangeAssetList(_j).Value.UnPack() : null);}
  }
  public static Offset<Lobby.SA_IncubationBuy> Pack(FlatBufferBuilder builder, SA_IncubationBuyT _o) {
    if (_o == null) return default(Offset<Lobby.SA_IncubationBuy>);
    var _ChangeAssetList = default(VectorOffset);
    if (_o.ChangeAssetList != null) {
      StartChangeAssetListVector(builder, _o.ChangeAssetList.Count);
      for (var _j = _o.ChangeAssetList.Count - 1; _j >= 0; --_j) { Lobby.AssetData.Pack(builder, _o.ChangeAssetList[_j]); }
      _ChangeAssetList = builder.EndVector();
    }
    return CreateSA_IncubationBuy(
      builder,
      _o.Incubation,
      _ChangeAssetList);
  }
}

public class SA_IncubationBuyT
{
  public Lobby.IncubationDataT Incubation { get; set; }
  public List<Lobby.AssetDataT> ChangeAssetList { get; set; }

  public SA_IncubationBuyT() {
    this.Incubation = new Lobby.IncubationDataT();
    this.ChangeAssetList = null;
  }
}


}
