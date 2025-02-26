// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Lobby
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct SA_PetCombine : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_23_1_20(); }
  public static SA_PetCombine GetRootAsSA_PetCombine(ByteBuffer _bb) { return GetRootAsSA_PetCombine(_bb, new SA_PetCombine()); }
  public static SA_PetCombine GetRootAsSA_PetCombine(ByteBuffer _bb, SA_PetCombine obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public SA_PetCombine __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public long DelPetUniqueIDList(int j) { int o = __p.__offset(4); return o != 0 ? __p.bb.GetLong(__p.__vector(o) + j * 8) : (long)0; }
  public int DelPetUniqueIDListLength { get { int o = __p.__offset(4); return o != 0 ? __p.__vector_len(o) : 0; } }
#if ENABLE_SPAN_T
  public Span<long> GetDelPetUniqueIDListBytes() { return __p.__vector_as_span<long>(4, 8); }
#else
  public ArraySegment<byte>? GetDelPetUniqueIDListBytes() { return __p.__vector_as_arraysegment(4); }
#endif
  public long[] GetDelPetUniqueIDListArray() { return __p.__vector_as_array<long>(4); }
  public Lobby.PetData? NewPetList(int j) { int o = __p.__offset(6); return o != 0 ? (Lobby.PetData?)(new Lobby.PetData()).__assign(__p.__indirect(__p.__vector(o) + j * 4), __p.bb) : null; }
  public int NewPetListLength { get { int o = __p.__offset(6); return o != 0 ? __p.__vector_len(o) : 0; } }

  public static Offset<Lobby.SA_PetCombine> CreateSA_PetCombine(FlatBufferBuilder builder,
      VectorOffset DelPetUniqueIDListOffset = default(VectorOffset),
      VectorOffset NewPetListOffset = default(VectorOffset)) {
    builder.StartTable(2);
    SA_PetCombine.AddNewPetList(builder, NewPetListOffset);
    SA_PetCombine.AddDelPetUniqueIDList(builder, DelPetUniqueIDListOffset);
    return SA_PetCombine.EndSA_PetCombine(builder);
  }

  public static void StartSA_PetCombine(FlatBufferBuilder builder) { builder.StartTable(2); }
  public static void AddDelPetUniqueIDList(FlatBufferBuilder builder, VectorOffset DelPetUniqueIDListOffset) { builder.AddOffset(0, DelPetUniqueIDListOffset.Value, 0); }
  public static VectorOffset CreateDelPetUniqueIDListVector(FlatBufferBuilder builder, long[] data) { builder.StartVector(8, data.Length, 8); for (int i = data.Length - 1; i >= 0; i--) builder.AddLong(data[i]); return builder.EndVector(); }
  public static VectorOffset CreateDelPetUniqueIDListVectorBlock(FlatBufferBuilder builder, long[] data) { builder.StartVector(8, data.Length, 8); builder.Add(data); return builder.EndVector(); }
  public static VectorOffset CreateDelPetUniqueIDListVectorBlock(FlatBufferBuilder builder, ArraySegment<long> data) { builder.StartVector(8, data.Count, 8); builder.Add(data); return builder.EndVector(); }
  public static VectorOffset CreateDelPetUniqueIDListVectorBlock(FlatBufferBuilder builder, IntPtr dataPtr, int sizeInBytes) { builder.StartVector(1, sizeInBytes, 1); builder.Add<long>(dataPtr, sizeInBytes); return builder.EndVector(); }
  public static void StartDelPetUniqueIDListVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(8, numElems, 8); }
  public static void AddNewPetList(FlatBufferBuilder builder, VectorOffset NewPetListOffset) { builder.AddOffset(1, NewPetListOffset.Value, 0); }
  public static VectorOffset CreateNewPetListVector(FlatBufferBuilder builder, Offset<Lobby.PetData>[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddOffset(data[i].Value); return builder.EndVector(); }
  public static VectorOffset CreateNewPetListVectorBlock(FlatBufferBuilder builder, Offset<Lobby.PetData>[] data) { builder.StartVector(4, data.Length, 4); builder.Add(data); return builder.EndVector(); }
  public static VectorOffset CreateNewPetListVectorBlock(FlatBufferBuilder builder, ArraySegment<Offset<Lobby.PetData>> data) { builder.StartVector(4, data.Count, 4); builder.Add(data); return builder.EndVector(); }
  public static VectorOffset CreateNewPetListVectorBlock(FlatBufferBuilder builder, IntPtr dataPtr, int sizeInBytes) { builder.StartVector(1, sizeInBytes, 1); builder.Add<Offset<Lobby.PetData>>(dataPtr, sizeInBytes); return builder.EndVector(); }
  public static void StartNewPetListVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static Offset<Lobby.SA_PetCombine> EndSA_PetCombine(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<Lobby.SA_PetCombine>(o);
  }
  public SA_PetCombineT UnPack() {
    var _o = new SA_PetCombineT();
    this.UnPackTo(_o);
    return _o;
  }
  public void UnPackTo(SA_PetCombineT _o) {
    _o.DelPetUniqueIDList = new List<long>();
    for (var _j = 0; _j < this.DelPetUniqueIDListLength; ++_j) {_o.DelPetUniqueIDList.Add(this.DelPetUniqueIDList(_j));}
    _o.NewPetList = new List<Lobby.PetDataT>();
    for (var _j = 0; _j < this.NewPetListLength; ++_j) {_o.NewPetList.Add(this.NewPetList(_j).HasValue ? this.NewPetList(_j).Value.UnPack() : null);}
  }
  public static Offset<Lobby.SA_PetCombine> Pack(FlatBufferBuilder builder, SA_PetCombineT _o) {
    if (_o == null) return default(Offset<Lobby.SA_PetCombine>);
    var _DelPetUniqueIDList = default(VectorOffset);
    if (_o.DelPetUniqueIDList != null) {
      var __DelPetUniqueIDList = _o.DelPetUniqueIDList.ToArray();
      _DelPetUniqueIDList = CreateDelPetUniqueIDListVector(builder, __DelPetUniqueIDList);
    }
    var _NewPetList = default(VectorOffset);
    if (_o.NewPetList != null) {
      var __NewPetList = new Offset<Lobby.PetData>[_o.NewPetList.Count];
      for (var _j = 0; _j < __NewPetList.Length; ++_j) { __NewPetList[_j] = Lobby.PetData.Pack(builder, _o.NewPetList[_j]); }
      _NewPetList = CreateNewPetListVector(builder, __NewPetList);
    }
    return CreateSA_PetCombine(
      builder,
      _DelPetUniqueIDList,
      _NewPetList);
  }
}

public class SA_PetCombineT
{
  public List<long> DelPetUniqueIDList { get; set; }
  public List<Lobby.PetDataT> NewPetList { get; set; }

  public SA_PetCombineT() {
    this.DelPetUniqueIDList = null;
    this.NewPetList = null;
  }
}


}
