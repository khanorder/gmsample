// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Lobby
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct CQ_WeaponCategoryUpgrade : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_23_1_20(); }
  public static CQ_WeaponCategoryUpgrade GetRootAsCQ_WeaponCategoryUpgrade(ByteBuffer _bb) { return GetRootAsCQ_WeaponCategoryUpgrade(_bb, new CQ_WeaponCategoryUpgrade()); }
  public static CQ_WeaponCategoryUpgrade GetRootAsCQ_WeaponCategoryUpgrade(ByteBuffer _bb, CQ_WeaponCategoryUpgrade obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public CQ_WeaponCategoryUpgrade __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public int WeaponCategoryID { get { int o = __p.__offset(4); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public Lobby.ItemData? UseItemList(int j) { int o = __p.__offset(6); return o != 0 ? (Lobby.ItemData?)(new Lobby.ItemData()).__assign(__p.__vector(o) + j * 8, __p.bb) : null; }
  public int UseItemListLength { get { int o = __p.__offset(6); return o != 0 ? __p.__vector_len(o) : 0; } }

  public static Offset<Lobby.CQ_WeaponCategoryUpgrade> CreateCQ_WeaponCategoryUpgrade(FlatBufferBuilder builder,
      int WeaponCategoryID = 0,
      VectorOffset UseItemListOffset = default(VectorOffset)) {
    builder.StartTable(2);
    CQ_WeaponCategoryUpgrade.AddUseItemList(builder, UseItemListOffset);
    CQ_WeaponCategoryUpgrade.AddWeaponCategoryID(builder, WeaponCategoryID);
    return CQ_WeaponCategoryUpgrade.EndCQ_WeaponCategoryUpgrade(builder);
  }

  public static void StartCQ_WeaponCategoryUpgrade(FlatBufferBuilder builder) { builder.StartTable(2); }
  public static void AddWeaponCategoryID(FlatBufferBuilder builder, int WeaponCategoryID) { builder.AddInt(0, WeaponCategoryID, 0); }
  public static void AddUseItemList(FlatBufferBuilder builder, VectorOffset UseItemListOffset) { builder.AddOffset(1, UseItemListOffset.Value, 0); }
  public static void StartUseItemListVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(8, numElems, 4); }
  public static Offset<Lobby.CQ_WeaponCategoryUpgrade> EndCQ_WeaponCategoryUpgrade(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<Lobby.CQ_WeaponCategoryUpgrade>(o);
  }
  public CQ_WeaponCategoryUpgradeT UnPack() {
    var _o = new CQ_WeaponCategoryUpgradeT();
    this.UnPackTo(_o);
    return _o;
  }
  public void UnPackTo(CQ_WeaponCategoryUpgradeT _o) {
    _o.WeaponCategoryID = this.WeaponCategoryID;
    _o.UseItemList = new List<Lobby.ItemDataT>();
    for (var _j = 0; _j < this.UseItemListLength; ++_j) {_o.UseItemList.Add(this.UseItemList(_j).HasValue ? this.UseItemList(_j).Value.UnPack() : null);}
  }
  public static Offset<Lobby.CQ_WeaponCategoryUpgrade> Pack(FlatBufferBuilder builder, CQ_WeaponCategoryUpgradeT _o) {
    if (_o == null) return default(Offset<Lobby.CQ_WeaponCategoryUpgrade>);
    var _UseItemList = default(VectorOffset);
    if (_o.UseItemList != null) {
      StartUseItemListVector(builder, _o.UseItemList.Count);
      for (var _j = _o.UseItemList.Count - 1; _j >= 0; --_j) { Lobby.ItemData.Pack(builder, _o.UseItemList[_j]); }
      _UseItemList = builder.EndVector();
    }
    return CreateCQ_WeaponCategoryUpgrade(
      builder,
      _o.WeaponCategoryID,
      _UseItemList);
  }
}

public class CQ_WeaponCategoryUpgradeT
{
  public int WeaponCategoryID { get; set; }
  public List<Lobby.ItemDataT> UseItemList { get; set; }

  public CQ_WeaponCategoryUpgradeT() {
    this.WeaponCategoryID = 0;
    this.UseItemList = null;
  }
}


}
