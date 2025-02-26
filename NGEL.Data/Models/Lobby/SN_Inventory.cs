// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Lobby
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct SN_Inventory : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_23_1_20(); }
  public static SN_Inventory GetRootAsSN_Inventory(ByteBuffer _bb) { return GetRootAsSN_Inventory(_bb, new SN_Inventory()); }
  public static SN_Inventory GetRootAsSN_Inventory(ByteBuffer _bb, SN_Inventory obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public SN_Inventory __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public Lobby.ItemData? ChangeItemList(int j) { int o = __p.__offset(4); return o != 0 ? (Lobby.ItemData?)(new Lobby.ItemData()).__assign(__p.__vector(o) + j * 8, __p.bb) : null; }
  public int ChangeItemListLength { get { int o = __p.__offset(4); return o != 0 ? __p.__vector_len(o) : 0; } }

  public static Offset<Lobby.SN_Inventory> CreateSN_Inventory(FlatBufferBuilder builder,
      VectorOffset ChangeItemListOffset = default(VectorOffset)) {
    builder.StartTable(1);
    SN_Inventory.AddChangeItemList(builder, ChangeItemListOffset);
    return SN_Inventory.EndSN_Inventory(builder);
  }

  public static void StartSN_Inventory(FlatBufferBuilder builder) { builder.StartTable(1); }
  public static void AddChangeItemList(FlatBufferBuilder builder, VectorOffset ChangeItemListOffset) { builder.AddOffset(0, ChangeItemListOffset.Value, 0); }
  public static void StartChangeItemListVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(8, numElems, 4); }
  public static Offset<Lobby.SN_Inventory> EndSN_Inventory(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<Lobby.SN_Inventory>(o);
  }
  public SN_InventoryT UnPack() {
    var _o = new SN_InventoryT();
    this.UnPackTo(_o);
    return _o;
  }
  public void UnPackTo(SN_InventoryT _o) {
    _o.ChangeItemList = new List<Lobby.ItemDataT>();
    for (var _j = 0; _j < this.ChangeItemListLength; ++_j) {_o.ChangeItemList.Add(this.ChangeItemList(_j).HasValue ? this.ChangeItemList(_j).Value.UnPack() : null);}
  }
  public static Offset<Lobby.SN_Inventory> Pack(FlatBufferBuilder builder, SN_InventoryT _o) {
    if (_o == null) return default(Offset<Lobby.SN_Inventory>);
    var _ChangeItemList = default(VectorOffset);
    if (_o.ChangeItemList != null) {
      StartChangeItemListVector(builder, _o.ChangeItemList.Count);
      for (var _j = _o.ChangeItemList.Count - 1; _j >= 0; --_j) { Lobby.ItemData.Pack(builder, _o.ChangeItemList[_j]); }
      _ChangeItemList = builder.EndVector();
    }
    return CreateSN_Inventory(
      builder,
      _ChangeItemList);
  }
}

public class SN_InventoryT
{
  public List<Lobby.ItemDataT> ChangeItemList { get; set; }

  public SN_InventoryT() {
    this.ChangeItemList = null;
  }
}


}
