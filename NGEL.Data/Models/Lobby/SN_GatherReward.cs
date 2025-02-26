// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Lobby
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct SN_GatherReward : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_23_1_20(); }
  public static SN_GatherReward GetRootAsSN_GatherReward(ByteBuffer _bb) { return GetRootAsSN_GatherReward(_bb, new SN_GatherReward()); }
  public static SN_GatherReward GetRootAsSN_GatherReward(ByteBuffer _bb, SN_GatherReward obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public SN_GatherReward __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public Lobby.AssetData? ChangeAssetList(int j) { int o = __p.__offset(4); return o != 0 ? (Lobby.AssetData?)(new Lobby.AssetData()).__assign(__p.__vector(o) + j * 12, __p.bb) : null; }
  public int ChangeAssetListLength { get { int o = __p.__offset(4); return o != 0 ? __p.__vector_len(o) : 0; } }
  public Lobby.ItemData? ChangeItemList(int j) { int o = __p.__offset(6); return o != 0 ? (Lobby.ItemData?)(new Lobby.ItemData()).__assign(__p.__vector(o) + j * 8, __p.bb) : null; }
  public int ChangeItemListLength { get { int o = __p.__offset(6); return o != 0 ? __p.__vector_len(o) : 0; } }

  public static Offset<Lobby.SN_GatherReward> CreateSN_GatherReward(FlatBufferBuilder builder,
      VectorOffset ChangeAssetListOffset = default(VectorOffset),
      VectorOffset ChangeItemListOffset = default(VectorOffset)) {
    builder.StartTable(2);
    SN_GatherReward.AddChangeItemList(builder, ChangeItemListOffset);
    SN_GatherReward.AddChangeAssetList(builder, ChangeAssetListOffset);
    return SN_GatherReward.EndSN_GatherReward(builder);
  }

  public static void StartSN_GatherReward(FlatBufferBuilder builder) { builder.StartTable(2); }
  public static void AddChangeAssetList(FlatBufferBuilder builder, VectorOffset ChangeAssetListOffset) { builder.AddOffset(0, ChangeAssetListOffset.Value, 0); }
  public static void StartChangeAssetListVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(12, numElems, 4); }
  public static void AddChangeItemList(FlatBufferBuilder builder, VectorOffset ChangeItemListOffset) { builder.AddOffset(1, ChangeItemListOffset.Value, 0); }
  public static void StartChangeItemListVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(8, numElems, 4); }
  public static Offset<Lobby.SN_GatherReward> EndSN_GatherReward(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<Lobby.SN_GatherReward>(o);
  }
  public SN_GatherRewardT UnPack() {
    var _o = new SN_GatherRewardT();
    this.UnPackTo(_o);
    return _o;
  }
  public void UnPackTo(SN_GatherRewardT _o) {
    _o.ChangeAssetList = new List<Lobby.AssetDataT>();
    for (var _j = 0; _j < this.ChangeAssetListLength; ++_j) {_o.ChangeAssetList.Add(this.ChangeAssetList(_j).HasValue ? this.ChangeAssetList(_j).Value.UnPack() : null);}
    _o.ChangeItemList = new List<Lobby.ItemDataT>();
    for (var _j = 0; _j < this.ChangeItemListLength; ++_j) {_o.ChangeItemList.Add(this.ChangeItemList(_j).HasValue ? this.ChangeItemList(_j).Value.UnPack() : null);}
  }
  public static Offset<Lobby.SN_GatherReward> Pack(FlatBufferBuilder builder, SN_GatherRewardT _o) {
    if (_o == null) return default(Offset<Lobby.SN_GatherReward>);
    var _ChangeAssetList = default(VectorOffset);
    if (_o.ChangeAssetList != null) {
      StartChangeAssetListVector(builder, _o.ChangeAssetList.Count);
      for (var _j = _o.ChangeAssetList.Count - 1; _j >= 0; --_j) { Lobby.AssetData.Pack(builder, _o.ChangeAssetList[_j]); }
      _ChangeAssetList = builder.EndVector();
    }
    var _ChangeItemList = default(VectorOffset);
    if (_o.ChangeItemList != null) {
      StartChangeItemListVector(builder, _o.ChangeItemList.Count);
      for (var _j = _o.ChangeItemList.Count - 1; _j >= 0; --_j) { Lobby.ItemData.Pack(builder, _o.ChangeItemList[_j]); }
      _ChangeItemList = builder.EndVector();
    }
    return CreateSN_GatherReward(
      builder,
      _ChangeAssetList,
      _ChangeItemList);
  }
}

public class SN_GatherRewardT
{
  public List<Lobby.AssetDataT> ChangeAssetList { get; set; }
  public List<Lobby.ItemDataT> ChangeItemList { get; set; }

  public SN_GatherRewardT() {
    this.ChangeAssetList = null;
    this.ChangeItemList = null;
  }
}


}
