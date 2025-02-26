// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Lobby
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct SA_CharacterPassBuyHero : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_23_1_20(); }
  public static SA_CharacterPassBuyHero GetRootAsSA_CharacterPassBuyHero(ByteBuffer _bb) { return GetRootAsSA_CharacterPassBuyHero(_bb, new SA_CharacterPassBuyHero()); }
  public static SA_CharacterPassBuyHero GetRootAsSA_CharacterPassBuyHero(ByteBuffer _bb, SA_CharacterPassBuyHero obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public SA_CharacterPassBuyHero __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public Lobby.NewHeroData? NewHero { get { int o = __p.__offset(4); return o != 0 ? (Lobby.NewHeroData?)(new Lobby.NewHeroData()).__assign(__p.__indirect(o + __p.bb_pos), __p.bb) : null; } }
  public Lobby.AssetData? ChangeAssetList(int j) { int o = __p.__offset(6); return o != 0 ? (Lobby.AssetData?)(new Lobby.AssetData()).__assign(__p.__vector(o) + j * 12, __p.bb) : null; }
  public int ChangeAssetListLength { get { int o = __p.__offset(6); return o != 0 ? __p.__vector_len(o) : 0; } }

  public static Offset<Lobby.SA_CharacterPassBuyHero> CreateSA_CharacterPassBuyHero(FlatBufferBuilder builder,
      Offset<Lobby.NewHeroData> newHeroOffset = default(Offset<Lobby.NewHeroData>),
      VectorOffset ChangeAssetListOffset = default(VectorOffset)) {
    builder.StartTable(2);
    SA_CharacterPassBuyHero.AddChangeAssetList(builder, ChangeAssetListOffset);
    SA_CharacterPassBuyHero.AddNewHero(builder, newHeroOffset);
    return SA_CharacterPassBuyHero.EndSA_CharacterPassBuyHero(builder);
  }

  public static void StartSA_CharacterPassBuyHero(FlatBufferBuilder builder) { builder.StartTable(2); }
  public static void AddNewHero(FlatBufferBuilder builder, Offset<Lobby.NewHeroData> newHeroOffset) { builder.AddOffset(0, newHeroOffset.Value, 0); }
  public static void AddChangeAssetList(FlatBufferBuilder builder, VectorOffset ChangeAssetListOffset) { builder.AddOffset(1, ChangeAssetListOffset.Value, 0); }
  public static void StartChangeAssetListVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(12, numElems, 4); }
  public static Offset<Lobby.SA_CharacterPassBuyHero> EndSA_CharacterPassBuyHero(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<Lobby.SA_CharacterPassBuyHero>(o);
  }
  public SA_CharacterPassBuyHeroT UnPack() {
    var _o = new SA_CharacterPassBuyHeroT();
    this.UnPackTo(_o);
    return _o;
  }
  public void UnPackTo(SA_CharacterPassBuyHeroT _o) {
    _o.NewHero = this.NewHero.HasValue ? this.NewHero.Value.UnPack() : null;
    _o.ChangeAssetList = new List<Lobby.AssetDataT>();
    for (var _j = 0; _j < this.ChangeAssetListLength; ++_j) {_o.ChangeAssetList.Add(this.ChangeAssetList(_j).HasValue ? this.ChangeAssetList(_j).Value.UnPack() : null);}
  }
  public static Offset<Lobby.SA_CharacterPassBuyHero> Pack(FlatBufferBuilder builder, SA_CharacterPassBuyHeroT _o) {
    if (_o == null) return default(Offset<Lobby.SA_CharacterPassBuyHero>);
    var _newHero = _o.NewHero == null ? default(Offset<Lobby.NewHeroData>) : Lobby.NewHeroData.Pack(builder, _o.NewHero);
    var _ChangeAssetList = default(VectorOffset);
    if (_o.ChangeAssetList != null) {
      StartChangeAssetListVector(builder, _o.ChangeAssetList.Count);
      for (var _j = _o.ChangeAssetList.Count - 1; _j >= 0; --_j) { Lobby.AssetData.Pack(builder, _o.ChangeAssetList[_j]); }
      _ChangeAssetList = builder.EndVector();
    }
    return CreateSA_CharacterPassBuyHero(
      builder,
      _newHero,
      _ChangeAssetList);
  }
}

public class SA_CharacterPassBuyHeroT
{
  public Lobby.NewHeroDataT NewHero { get; set; }
  public List<Lobby.AssetDataT> ChangeAssetList { get; set; }

  public SA_CharacterPassBuyHeroT() {
    this.NewHero = null;
    this.ChangeAssetList = null;
  }
}


}
