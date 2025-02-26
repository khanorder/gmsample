// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Lobby
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct SA_IncubationStart : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_23_1_20(); }
  public static SA_IncubationStart GetRootAsSA_IncubationStart(ByteBuffer _bb) { return GetRootAsSA_IncubationStart(_bb, new SA_IncubationStart()); }
  public static SA_IncubationStart GetRootAsSA_IncubationStart(ByteBuffer _bb, SA_IncubationStart obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public SA_IncubationStart __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public Lobby.IncubationData? ChangeIncubation { get { int o = __p.__offset(4); return o != 0 ? (Lobby.IncubationData?)(new Lobby.IncubationData()).__assign(o + __p.bb_pos, __p.bb) : null; } }
  public Lobby.ItemData? ChangeItemList(int j) { int o = __p.__offset(6); return o != 0 ? (Lobby.ItemData?)(new Lobby.ItemData()).__assign(__p.__vector(o) + j * 8, __p.bb) : null; }
  public int ChangeItemListLength { get { int o = __p.__offset(6); return o != 0 ? __p.__vector_len(o) : 0; } }

  public static Offset<Lobby.SA_IncubationStart> CreateSA_IncubationStart(FlatBufferBuilder builder,
      Lobby.IncubationDataT ChangeIncubation = null,
      VectorOffset ChangeItemListOffset = default(VectorOffset)) {
    builder.StartTable(2);
    SA_IncubationStart.AddChangeItemList(builder, ChangeItemListOffset);
    SA_IncubationStart.AddChangeIncubation(builder, Lobby.IncubationData.Pack(builder, ChangeIncubation));
    return SA_IncubationStart.EndSA_IncubationStart(builder);
  }

  public static void StartSA_IncubationStart(FlatBufferBuilder builder) { builder.StartTable(2); }
  public static void AddChangeIncubation(FlatBufferBuilder builder, Offset<Lobby.IncubationData> ChangeIncubationOffset) { builder.AddStruct(0, ChangeIncubationOffset.Value, 0); }
  public static void AddChangeItemList(FlatBufferBuilder builder, VectorOffset ChangeItemListOffset) { builder.AddOffset(1, ChangeItemListOffset.Value, 0); }
  public static void StartChangeItemListVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(8, numElems, 4); }
  public static Offset<Lobby.SA_IncubationStart> EndSA_IncubationStart(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<Lobby.SA_IncubationStart>(o);
  }
  public SA_IncubationStartT UnPack() {
    var _o = new SA_IncubationStartT();
    this.UnPackTo(_o);
    return _o;
  }
  public void UnPackTo(SA_IncubationStartT _o) {
    _o.ChangeIncubation = this.ChangeIncubation.HasValue ? this.ChangeIncubation.Value.UnPack() : null;
    _o.ChangeItemList = new List<Lobby.ItemDataT>();
    for (var _j = 0; _j < this.ChangeItemListLength; ++_j) {_o.ChangeItemList.Add(this.ChangeItemList(_j).HasValue ? this.ChangeItemList(_j).Value.UnPack() : null);}
  }
  public static Offset<Lobby.SA_IncubationStart> Pack(FlatBufferBuilder builder, SA_IncubationStartT _o) {
    if (_o == null) return default(Offset<Lobby.SA_IncubationStart>);
    var _ChangeItemList = default(VectorOffset);
    if (_o.ChangeItemList != null) {
      StartChangeItemListVector(builder, _o.ChangeItemList.Count);
      for (var _j = _o.ChangeItemList.Count - 1; _j >= 0; --_j) { Lobby.ItemData.Pack(builder, _o.ChangeItemList[_j]); }
      _ChangeItemList = builder.EndVector();
    }
    return CreateSA_IncubationStart(
      builder,
      _o.ChangeIncubation,
      _ChangeItemList);
  }
}

public class SA_IncubationStartT
{
  public Lobby.IncubationDataT ChangeIncubation { get; set; }
  public List<Lobby.ItemDataT> ChangeItemList { get; set; }

  public SA_IncubationStartT() {
    this.ChangeIncubation = new Lobby.IncubationDataT();
    this.ChangeItemList = null;
  }
}


}
