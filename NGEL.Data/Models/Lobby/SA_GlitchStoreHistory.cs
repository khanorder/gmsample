// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Lobby
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct SA_GlitchStoreHistory : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_23_1_20(); }
  public static SA_GlitchStoreHistory GetRootAsSA_GlitchStoreHistory(ByteBuffer _bb) { return GetRootAsSA_GlitchStoreHistory(_bb, new SA_GlitchStoreHistory()); }
  public static SA_GlitchStoreHistory GetRootAsSA_GlitchStoreHistory(ByteBuffer _bb, SA_GlitchStoreHistory obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public SA_GlitchStoreHistory __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public int StoreIDList(int j) { int o = __p.__offset(4); return o != 0 ? __p.bb.GetInt(__p.__vector(o) + j * 4) : (int)0; }
  public int StoreIDListLength { get { int o = __p.__offset(4); return o != 0 ? __p.__vector_len(o) : 0; } }
#if ENABLE_SPAN_T
  public Span<int> GetStoreIDListBytes() { return __p.__vector_as_span<int>(4, 4); }
#else
  public ArraySegment<byte>? GetStoreIDListBytes() { return __p.__vector_as_arraysegment(4); }
#endif
  public int[] GetStoreIDListArray() { return __p.__vector_as_array<int>(4); }

  public static Offset<Lobby.SA_GlitchStoreHistory> CreateSA_GlitchStoreHistory(FlatBufferBuilder builder,
      VectorOffset StoreIDListOffset = default(VectorOffset)) {
    builder.StartTable(1);
    SA_GlitchStoreHistory.AddStoreIDList(builder, StoreIDListOffset);
    return SA_GlitchStoreHistory.EndSA_GlitchStoreHistory(builder);
  }

  public static void StartSA_GlitchStoreHistory(FlatBufferBuilder builder) { builder.StartTable(1); }
  public static void AddStoreIDList(FlatBufferBuilder builder, VectorOffset StoreIDListOffset) { builder.AddOffset(0, StoreIDListOffset.Value, 0); }
  public static VectorOffset CreateStoreIDListVector(FlatBufferBuilder builder, int[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddInt(data[i]); return builder.EndVector(); }
  public static VectorOffset CreateStoreIDListVectorBlock(FlatBufferBuilder builder, int[] data) { builder.StartVector(4, data.Length, 4); builder.Add(data); return builder.EndVector(); }
  public static VectorOffset CreateStoreIDListVectorBlock(FlatBufferBuilder builder, ArraySegment<int> data) { builder.StartVector(4, data.Count, 4); builder.Add(data); return builder.EndVector(); }
  public static VectorOffset CreateStoreIDListVectorBlock(FlatBufferBuilder builder, IntPtr dataPtr, int sizeInBytes) { builder.StartVector(1, sizeInBytes, 1); builder.Add<int>(dataPtr, sizeInBytes); return builder.EndVector(); }
  public static void StartStoreIDListVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static Offset<Lobby.SA_GlitchStoreHistory> EndSA_GlitchStoreHistory(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<Lobby.SA_GlitchStoreHistory>(o);
  }
  public SA_GlitchStoreHistoryT UnPack() {
    var _o = new SA_GlitchStoreHistoryT();
    this.UnPackTo(_o);
    return _o;
  }
  public void UnPackTo(SA_GlitchStoreHistoryT _o) {
    _o.StoreIDList = new List<int>();
    for (var _j = 0; _j < this.StoreIDListLength; ++_j) {_o.StoreIDList.Add(this.StoreIDList(_j));}
  }
  public static Offset<Lobby.SA_GlitchStoreHistory> Pack(FlatBufferBuilder builder, SA_GlitchStoreHistoryT _o) {
    if (_o == null) return default(Offset<Lobby.SA_GlitchStoreHistory>);
    var _StoreIDList = default(VectorOffset);
    if (_o.StoreIDList != null) {
      var __StoreIDList = _o.StoreIDList.ToArray();
      _StoreIDList = CreateStoreIDListVector(builder, __StoreIDList);
    }
    return CreateSA_GlitchStoreHistory(
      builder,
      _StoreIDList);
  }
}

public class SA_GlitchStoreHistoryT
{
  public List<int> StoreIDList { get; set; }

  public SA_GlitchStoreHistoryT() {
    this.StoreIDList = null;
  }
}


}
