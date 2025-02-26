// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Lobby
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct SA_EventStoreList : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_23_1_20(); }
  public static SA_EventStoreList GetRootAsSA_EventStoreList(ByteBuffer _bb) { return GetRootAsSA_EventStoreList(_bb, new SA_EventStoreList()); }
  public static SA_EventStoreList GetRootAsSA_EventStoreList(ByteBuffer _bb, SA_EventStoreList obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public SA_EventStoreList __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public int StoreNextResetAt { get { int o = __p.__offset(4); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public Lobby.EventStoreData? EventStoreList(int j) { int o = __p.__offset(6); return o != 0 ? (Lobby.EventStoreData?)(new Lobby.EventStoreData()).__assign(__p.__vector(o) + j * 16, __p.bb) : null; }
  public int EventStoreListLength { get { int o = __p.__offset(6); return o != 0 ? __p.__vector_len(o) : 0; } }

  public static Offset<Lobby.SA_EventStoreList> CreateSA_EventStoreList(FlatBufferBuilder builder,
      int StoreNextResetAt = 0,
      VectorOffset EventStoreListOffset = default(VectorOffset)) {
    builder.StartTable(2);
    SA_EventStoreList.AddEventStoreList(builder, EventStoreListOffset);
    SA_EventStoreList.AddStoreNextResetAt(builder, StoreNextResetAt);
    return SA_EventStoreList.EndSA_EventStoreList(builder);
  }

  public static void StartSA_EventStoreList(FlatBufferBuilder builder) { builder.StartTable(2); }
  public static void AddStoreNextResetAt(FlatBufferBuilder builder, int StoreNextResetAt) { builder.AddInt(0, StoreNextResetAt, 0); }
  public static void AddEventStoreList(FlatBufferBuilder builder, VectorOffset EventStoreListOffset) { builder.AddOffset(1, EventStoreListOffset.Value, 0); }
  public static void StartEventStoreListVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(16, numElems, 4); }
  public static Offset<Lobby.SA_EventStoreList> EndSA_EventStoreList(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<Lobby.SA_EventStoreList>(o);
  }
  public SA_EventStoreListT UnPack() {
    var _o = new SA_EventStoreListT();
    this.UnPackTo(_o);
    return _o;
  }
  public void UnPackTo(SA_EventStoreListT _o) {
    _o.StoreNextResetAt = this.StoreNextResetAt;
    _o.EventStoreList = new List<Lobby.EventStoreDataT>();
    for (var _j = 0; _j < this.EventStoreListLength; ++_j) {_o.EventStoreList.Add(this.EventStoreList(_j).HasValue ? this.EventStoreList(_j).Value.UnPack() : null);}
  }
  public static Offset<Lobby.SA_EventStoreList> Pack(FlatBufferBuilder builder, SA_EventStoreListT _o) {
    if (_o == null) return default(Offset<Lobby.SA_EventStoreList>);
    var _EventStoreList = default(VectorOffset);
    if (_o.EventStoreList != null) {
      StartEventStoreListVector(builder, _o.EventStoreList.Count);
      for (var _j = _o.EventStoreList.Count - 1; _j >= 0; --_j) { Lobby.EventStoreData.Pack(builder, _o.EventStoreList[_j]); }
      _EventStoreList = builder.EndVector();
    }
    return CreateSA_EventStoreList(
      builder,
      _o.StoreNextResetAt,
      _EventStoreList);
  }
}

public class SA_EventStoreListT
{
  public int StoreNextResetAt { get; set; }
  public List<Lobby.EventStoreDataT> EventStoreList { get; set; }

  public SA_EventStoreListT() {
    this.StoreNextResetAt = 0;
    this.EventStoreList = null;
  }
}


}
