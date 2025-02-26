// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Lobby
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct SA_ArtifactDeckSlotBuy : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_23_1_20(); }
  public static SA_ArtifactDeckSlotBuy GetRootAsSA_ArtifactDeckSlotBuy(ByteBuffer _bb) { return GetRootAsSA_ArtifactDeckSlotBuy(_bb, new SA_ArtifactDeckSlotBuy()); }
  public static SA_ArtifactDeckSlotBuy GetRootAsSA_ArtifactDeckSlotBuy(ByteBuffer _bb, SA_ArtifactDeckSlotBuy obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public SA_ArtifactDeckSlotBuy __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public int AddArtifactDeckCount { get { int o = __p.__offset(4); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public Lobby.AssetData? ChangeAssetList(int j) { int o = __p.__offset(6); return o != 0 ? (Lobby.AssetData?)(new Lobby.AssetData()).__assign(__p.__vector(o) + j * 12, __p.bb) : null; }
  public int ChangeAssetListLength { get { int o = __p.__offset(6); return o != 0 ? __p.__vector_len(o) : 0; } }

  public static Offset<Lobby.SA_ArtifactDeckSlotBuy> CreateSA_ArtifactDeckSlotBuy(FlatBufferBuilder builder,
      int AddArtifactDeckCount = 0,
      VectorOffset ChangeAssetListOffset = default(VectorOffset)) {
    builder.StartTable(2);
    SA_ArtifactDeckSlotBuy.AddChangeAssetList(builder, ChangeAssetListOffset);
    SA_ArtifactDeckSlotBuy.AddAddArtifactDeckCount(builder, AddArtifactDeckCount);
    return SA_ArtifactDeckSlotBuy.EndSA_ArtifactDeckSlotBuy(builder);
  }

  public static void StartSA_ArtifactDeckSlotBuy(FlatBufferBuilder builder) { builder.StartTable(2); }
  public static void AddAddArtifactDeckCount(FlatBufferBuilder builder, int AddArtifactDeckCount) { builder.AddInt(0, AddArtifactDeckCount, 0); }
  public static void AddChangeAssetList(FlatBufferBuilder builder, VectorOffset ChangeAssetListOffset) { builder.AddOffset(1, ChangeAssetListOffset.Value, 0); }
  public static void StartChangeAssetListVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(12, numElems, 4); }
  public static Offset<Lobby.SA_ArtifactDeckSlotBuy> EndSA_ArtifactDeckSlotBuy(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<Lobby.SA_ArtifactDeckSlotBuy>(o);
  }
  public SA_ArtifactDeckSlotBuyT UnPack() {
    var _o = new SA_ArtifactDeckSlotBuyT();
    this.UnPackTo(_o);
    return _o;
  }
  public void UnPackTo(SA_ArtifactDeckSlotBuyT _o) {
    _o.AddArtifactDeckCount = this.AddArtifactDeckCount;
    _o.ChangeAssetList = new List<Lobby.AssetDataT>();
    for (var _j = 0; _j < this.ChangeAssetListLength; ++_j) {_o.ChangeAssetList.Add(this.ChangeAssetList(_j).HasValue ? this.ChangeAssetList(_j).Value.UnPack() : null);}
  }
  public static Offset<Lobby.SA_ArtifactDeckSlotBuy> Pack(FlatBufferBuilder builder, SA_ArtifactDeckSlotBuyT _o) {
    if (_o == null) return default(Offset<Lobby.SA_ArtifactDeckSlotBuy>);
    var _ChangeAssetList = default(VectorOffset);
    if (_o.ChangeAssetList != null) {
      StartChangeAssetListVector(builder, _o.ChangeAssetList.Count);
      for (var _j = _o.ChangeAssetList.Count - 1; _j >= 0; --_j) { Lobby.AssetData.Pack(builder, _o.ChangeAssetList[_j]); }
      _ChangeAssetList = builder.EndVector();
    }
    return CreateSA_ArtifactDeckSlotBuy(
      builder,
      _o.AddArtifactDeckCount,
      _ChangeAssetList);
  }
}

public class SA_ArtifactDeckSlotBuyT
{
  public int AddArtifactDeckCount { get; set; }
  public List<Lobby.AssetDataT> ChangeAssetList { get; set; }

  public SA_ArtifactDeckSlotBuyT() {
    this.AddArtifactDeckCount = 0;
    this.ChangeAssetList = null;
  }
}


}
