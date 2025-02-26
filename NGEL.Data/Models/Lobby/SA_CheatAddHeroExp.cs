// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Lobby
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct SA_CheatAddHeroExp : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_23_1_20(); }
  public static SA_CheatAddHeroExp GetRootAsSA_CheatAddHeroExp(ByteBuffer _bb) { return GetRootAsSA_CheatAddHeroExp(_bb, new SA_CheatAddHeroExp()); }
  public static SA_CheatAddHeroExp GetRootAsSA_CheatAddHeroExp(ByteBuffer _bb, SA_CheatAddHeroExp obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public SA_CheatAddHeroExp __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public Lobby.HeroData? Hero { get { int o = __p.__offset(4); return o != 0 ? (Lobby.HeroData?)(new Lobby.HeroData()).__assign(o + __p.bb_pos, __p.bb) : null; } }
  public Lobby.AssetData? ChangeAssetList(int j) { int o = __p.__offset(6); return o != 0 ? (Lobby.AssetData?)(new Lobby.AssetData()).__assign(__p.__vector(o) + j * 8, __p.bb) : null; }
  public int ChangeAssetListLength { get { int o = __p.__offset(6); return o != 0 ? __p.__vector_len(o) : 0; } }
  public Lobby.ItemData? ChangeItemList(int j) { int o = __p.__offset(8); return o != 0 ? (Lobby.ItemData?)(new Lobby.ItemData()).__assign(__p.__vector(o) + j * 8, __p.bb) : null; }
  public int ChangeItemListLength { get { int o = __p.__offset(8); return o != 0 ? __p.__vector_len(o) : 0; } }

  public static Offset<Lobby.SA_CheatAddHeroExp> CreateSA_CheatAddHeroExp(FlatBufferBuilder builder,
      Lobby.HeroDataT Hero = null,
      VectorOffset ChangeAssetListOffset = default(VectorOffset),
      VectorOffset ChangeItemListOffset = default(VectorOffset)) {
    builder.StartTable(3);
    SA_CheatAddHeroExp.AddChangeItemList(builder, ChangeItemListOffset);
    SA_CheatAddHeroExp.AddChangeAssetList(builder, ChangeAssetListOffset);
    SA_CheatAddHeroExp.AddHero(builder, Lobby.HeroData.Pack(builder, Hero));
    return SA_CheatAddHeroExp.EndSA_CheatAddHeroExp(builder);
  }

  public static void StartSA_CheatAddHeroExp(FlatBufferBuilder builder) { builder.StartTable(3); }
  public static void AddHero(FlatBufferBuilder builder, Offset<Lobby.HeroData> HeroOffset) { builder.AddStruct(0, HeroOffset.Value, 0); }
  public static void AddChangeAssetList(FlatBufferBuilder builder, VectorOffset ChangeAssetListOffset) { builder.AddOffset(1, ChangeAssetListOffset.Value, 0); }
  public static void StartChangeAssetListVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(8, numElems, 4); }
  public static void AddChangeItemList(FlatBufferBuilder builder, VectorOffset ChangeItemListOffset) { builder.AddOffset(2, ChangeItemListOffset.Value, 0); }
  public static void StartChangeItemListVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(8, numElems, 4); }
  public static Offset<Lobby.SA_CheatAddHeroExp> EndSA_CheatAddHeroExp(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<Lobby.SA_CheatAddHeroExp>(o);
  }
  public SA_CheatAddHeroExpT UnPack() {
    var _o = new SA_CheatAddHeroExpT();
    this.UnPackTo(_o);
    return _o;
  }
  public void UnPackTo(SA_CheatAddHeroExpT _o) {
    _o.Hero = this.Hero.HasValue ? this.Hero.Value.UnPack() : null;
    _o.ChangeAssetList = new List<Lobby.AssetDataT>();
    for (var _j = 0; _j < this.ChangeAssetListLength; ++_j) {_o.ChangeAssetList.Add(this.ChangeAssetList(_j).HasValue ? this.ChangeAssetList(_j).Value.UnPack() : null);}
    _o.ChangeItemList = new List<Lobby.ItemDataT>();
    for (var _j = 0; _j < this.ChangeItemListLength; ++_j) {_o.ChangeItemList.Add(this.ChangeItemList(_j).HasValue ? this.ChangeItemList(_j).Value.UnPack() : null);}
  }
  public static Offset<Lobby.SA_CheatAddHeroExp> Pack(FlatBufferBuilder builder, SA_CheatAddHeroExpT _o) {
    if (_o == null) return default(Offset<Lobby.SA_CheatAddHeroExp>);
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
    return CreateSA_CheatAddHeroExp(
      builder,
      _o.Hero,
      _ChangeAssetList,
      _ChangeItemList);
  }
}

public class SA_CheatAddHeroExpT
{
  public Lobby.HeroDataT Hero { get; set; }
  public List<Lobby.AssetDataT> ChangeAssetList { get; set; }
  public List<Lobby.ItemDataT> ChangeItemList { get; set; }

  public SA_CheatAddHeroExpT() {
    this.Hero = new Lobby.HeroDataT();
    this.ChangeAssetList = null;
    this.ChangeItemList = null;
  }
}


}
