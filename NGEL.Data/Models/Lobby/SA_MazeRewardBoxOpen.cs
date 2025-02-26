// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Lobby
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct SA_MazeRewardBoxOpen : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_23_1_20(); }
  public static SA_MazeRewardBoxOpen GetRootAsSA_MazeRewardBoxOpen(ByteBuffer _bb) { return GetRootAsSA_MazeRewardBoxOpen(_bb, new SA_MazeRewardBoxOpen()); }
  public static SA_MazeRewardBoxOpen GetRootAsSA_MazeRewardBoxOpen(ByteBuffer _bb, SA_MazeRewardBoxOpen obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public SA_MazeRewardBoxOpen __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public Lobby.MazeRewardBoxData? ChangeBoxData { get { int o = __p.__offset(4); return o != 0 ? (Lobby.MazeRewardBoxData?)(new Lobby.MazeRewardBoxData()).__assign(o + __p.bb_pos, __p.bb) : null; } }
  public Lobby.AssetData? ChangeAssetList(int j) { int o = __p.__offset(6); return o != 0 ? (Lobby.AssetData?)(new Lobby.AssetData()).__assign(__p.__vector(o) + j * 12, __p.bb) : null; }
  public int ChangeAssetListLength { get { int o = __p.__offset(6); return o != 0 ? __p.__vector_len(o) : 0; } }
  public Lobby.ItemData? ChangeItemList(int j) { int o = __p.__offset(8); return o != 0 ? (Lobby.ItemData?)(new Lobby.ItemData()).__assign(__p.__vector(o) + j * 8, __p.bb) : null; }
  public int ChangeItemListLength { get { int o = __p.__offset(8); return o != 0 ? __p.__vector_len(o) : 0; } }

  public static Offset<Lobby.SA_MazeRewardBoxOpen> CreateSA_MazeRewardBoxOpen(FlatBufferBuilder builder,
      Lobby.MazeRewardBoxDataT ChangeBoxData = null,
      VectorOffset ChangeAssetListOffset = default(VectorOffset),
      VectorOffset ChangeItemListOffset = default(VectorOffset)) {
    builder.StartTable(3);
    SA_MazeRewardBoxOpen.AddChangeItemList(builder, ChangeItemListOffset);
    SA_MazeRewardBoxOpen.AddChangeAssetList(builder, ChangeAssetListOffset);
    SA_MazeRewardBoxOpen.AddChangeBoxData(builder, Lobby.MazeRewardBoxData.Pack(builder, ChangeBoxData));
    return SA_MazeRewardBoxOpen.EndSA_MazeRewardBoxOpen(builder);
  }

  public static void StartSA_MazeRewardBoxOpen(FlatBufferBuilder builder) { builder.StartTable(3); }
  public static void AddChangeBoxData(FlatBufferBuilder builder, Offset<Lobby.MazeRewardBoxData> ChangeBoxDataOffset) { builder.AddStruct(0, ChangeBoxDataOffset.Value, 0); }
  public static void AddChangeAssetList(FlatBufferBuilder builder, VectorOffset ChangeAssetListOffset) { builder.AddOffset(1, ChangeAssetListOffset.Value, 0); }
  public static void StartChangeAssetListVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(12, numElems, 4); }
  public static void AddChangeItemList(FlatBufferBuilder builder, VectorOffset ChangeItemListOffset) { builder.AddOffset(2, ChangeItemListOffset.Value, 0); }
  public static void StartChangeItemListVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(8, numElems, 4); }
  public static Offset<Lobby.SA_MazeRewardBoxOpen> EndSA_MazeRewardBoxOpen(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<Lobby.SA_MazeRewardBoxOpen>(o);
  }
  public SA_MazeRewardBoxOpenT UnPack() {
    var _o = new SA_MazeRewardBoxOpenT();
    this.UnPackTo(_o);
    return _o;
  }
  public void UnPackTo(SA_MazeRewardBoxOpenT _o) {
    _o.ChangeBoxData = this.ChangeBoxData.HasValue ? this.ChangeBoxData.Value.UnPack() : null;
    _o.ChangeAssetList = new List<Lobby.AssetDataT>();
    for (var _j = 0; _j < this.ChangeAssetListLength; ++_j) {_o.ChangeAssetList.Add(this.ChangeAssetList(_j).HasValue ? this.ChangeAssetList(_j).Value.UnPack() : null);}
    _o.ChangeItemList = new List<Lobby.ItemDataT>();
    for (var _j = 0; _j < this.ChangeItemListLength; ++_j) {_o.ChangeItemList.Add(this.ChangeItemList(_j).HasValue ? this.ChangeItemList(_j).Value.UnPack() : null);}
  }
  public static Offset<Lobby.SA_MazeRewardBoxOpen> Pack(FlatBufferBuilder builder, SA_MazeRewardBoxOpenT _o) {
    if (_o == null) return default(Offset<Lobby.SA_MazeRewardBoxOpen>);
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
    return CreateSA_MazeRewardBoxOpen(
      builder,
      _o.ChangeBoxData,
      _ChangeAssetList,
      _ChangeItemList);
  }
}

public class SA_MazeRewardBoxOpenT
{
  public Lobby.MazeRewardBoxDataT ChangeBoxData { get; set; }
  public List<Lobby.AssetDataT> ChangeAssetList { get; set; }
  public List<Lobby.ItemDataT> ChangeItemList { get; set; }

  public SA_MazeRewardBoxOpenT() {
    this.ChangeBoxData = new Lobby.MazeRewardBoxDataT();
    this.ChangeAssetList = null;
    this.ChangeItemList = null;
  }
}


}
