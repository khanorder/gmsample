// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Lobby
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct CQ_HeroGiftFavorityItem : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_23_1_20(); }
  public static CQ_HeroGiftFavorityItem GetRootAsCQ_HeroGiftFavorityItem(ByteBuffer _bb) { return GetRootAsCQ_HeroGiftFavorityItem(_bb, new CQ_HeroGiftFavorityItem()); }
  public static CQ_HeroGiftFavorityItem GetRootAsCQ_HeroGiftFavorityItem(ByteBuffer _bb, CQ_HeroGiftFavorityItem obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public CQ_HeroGiftFavorityItem __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public int HeroID { get { int o = __p.__offset(4); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int ItemID { get { int o = __p.__offset(6); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }

  public static Offset<Lobby.CQ_HeroGiftFavorityItem> CreateCQ_HeroGiftFavorityItem(FlatBufferBuilder builder,
      int HeroID = 0,
      int ItemID = 0) {
    builder.StartTable(2);
    CQ_HeroGiftFavorityItem.AddItemID(builder, ItemID);
    CQ_HeroGiftFavorityItem.AddHeroID(builder, HeroID);
    return CQ_HeroGiftFavorityItem.EndCQ_HeroGiftFavorityItem(builder);
  }

  public static void StartCQ_HeroGiftFavorityItem(FlatBufferBuilder builder) { builder.StartTable(2); }
  public static void AddHeroID(FlatBufferBuilder builder, int HeroID) { builder.AddInt(0, HeroID, 0); }
  public static void AddItemID(FlatBufferBuilder builder, int ItemID) { builder.AddInt(1, ItemID, 0); }
  public static Offset<Lobby.CQ_HeroGiftFavorityItem> EndCQ_HeroGiftFavorityItem(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<Lobby.CQ_HeroGiftFavorityItem>(o);
  }
  public CQ_HeroGiftFavorityItemT UnPack() {
    var _o = new CQ_HeroGiftFavorityItemT();
    this.UnPackTo(_o);
    return _o;
  }
  public void UnPackTo(CQ_HeroGiftFavorityItemT _o) {
    _o.HeroID = this.HeroID;
    _o.ItemID = this.ItemID;
  }
  public static Offset<Lobby.CQ_HeroGiftFavorityItem> Pack(FlatBufferBuilder builder, CQ_HeroGiftFavorityItemT _o) {
    if (_o == null) return default(Offset<Lobby.CQ_HeroGiftFavorityItem>);
    return CreateCQ_HeroGiftFavorityItem(
      builder,
      _o.HeroID,
      _o.ItemID);
  }
}

public class CQ_HeroGiftFavorityItemT
{
  public int HeroID { get; set; }
  public int ItemID { get; set; }

  public CQ_HeroGiftFavorityItemT() {
    this.HeroID = 0;
    this.ItemID = 0;
  }
}


}
