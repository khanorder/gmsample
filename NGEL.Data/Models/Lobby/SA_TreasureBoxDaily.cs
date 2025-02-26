// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Lobby
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct SA_TreasureBoxDaily : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_23_1_20(); }
  public static SA_TreasureBoxDaily GetRootAsSA_TreasureBoxDaily(ByteBuffer _bb) { return GetRootAsSA_TreasureBoxDaily(_bb, new SA_TreasureBoxDaily()); }
  public static SA_TreasureBoxDaily GetRootAsSA_TreasureBoxDaily(ByteBuffer _bb, SA_TreasureBoxDaily obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public SA_TreasureBoxDaily __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public int DailyTreasureBoxList(int j) { int o = __p.__offset(4); return o != 0 ? __p.bb.GetInt(__p.__vector(o) + j * 4) : (int)0; }
  public int DailyTreasureBoxListLength { get { int o = __p.__offset(4); return o != 0 ? __p.__vector_len(o) : 0; } }
#if ENABLE_SPAN_T
  public Span<int> GetDailyTreasureBoxListBytes() { return __p.__vector_as_span<int>(4, 4); }
#else
  public ArraySegment<byte>? GetDailyTreasureBoxListBytes() { return __p.__vector_as_arraysegment(4); }
#endif
  public int[] GetDailyTreasureBoxListArray() { return __p.__vector_as_array<int>(4); }
  public int ExpiredBoxList(int j) { int o = __p.__offset(6); return o != 0 ? __p.bb.GetInt(__p.__vector(o) + j * 4) : (int)0; }
  public int ExpiredBoxListLength { get { int o = __p.__offset(6); return o != 0 ? __p.__vector_len(o) : 0; } }
#if ENABLE_SPAN_T
  public Span<int> GetExpiredBoxListBytes() { return __p.__vector_as_span<int>(6, 4); }
#else
  public ArraySegment<byte>? GetExpiredBoxListBytes() { return __p.__vector_as_arraysegment(6); }
#endif
  public int[] GetExpiredBoxListArray() { return __p.__vector_as_array<int>(6); }

  public static Offset<Lobby.SA_TreasureBoxDaily> CreateSA_TreasureBoxDaily(FlatBufferBuilder builder,
      VectorOffset DailyTreasureBoxListOffset = default(VectorOffset),
      VectorOffset ExpiredBoxListOffset = default(VectorOffset)) {
    builder.StartTable(2);
    SA_TreasureBoxDaily.AddExpiredBoxList(builder, ExpiredBoxListOffset);
    SA_TreasureBoxDaily.AddDailyTreasureBoxList(builder, DailyTreasureBoxListOffset);
    return SA_TreasureBoxDaily.EndSA_TreasureBoxDaily(builder);
  }

  public static void StartSA_TreasureBoxDaily(FlatBufferBuilder builder) { builder.StartTable(2); }
  public static void AddDailyTreasureBoxList(FlatBufferBuilder builder, VectorOffset DailyTreasureBoxListOffset) { builder.AddOffset(0, DailyTreasureBoxListOffset.Value, 0); }
  public static VectorOffset CreateDailyTreasureBoxListVector(FlatBufferBuilder builder, int[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddInt(data[i]); return builder.EndVector(); }
  public static VectorOffset CreateDailyTreasureBoxListVectorBlock(FlatBufferBuilder builder, int[] data) { builder.StartVector(4, data.Length, 4); builder.Add(data); return builder.EndVector(); }
  public static VectorOffset CreateDailyTreasureBoxListVectorBlock(FlatBufferBuilder builder, ArraySegment<int> data) { builder.StartVector(4, data.Count, 4); builder.Add(data); return builder.EndVector(); }
  public static VectorOffset CreateDailyTreasureBoxListVectorBlock(FlatBufferBuilder builder, IntPtr dataPtr, int sizeInBytes) { builder.StartVector(1, sizeInBytes, 1); builder.Add<int>(dataPtr, sizeInBytes); return builder.EndVector(); }
  public static void StartDailyTreasureBoxListVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static void AddExpiredBoxList(FlatBufferBuilder builder, VectorOffset ExpiredBoxListOffset) { builder.AddOffset(1, ExpiredBoxListOffset.Value, 0); }
  public static VectorOffset CreateExpiredBoxListVector(FlatBufferBuilder builder, int[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddInt(data[i]); return builder.EndVector(); }
  public static VectorOffset CreateExpiredBoxListVectorBlock(FlatBufferBuilder builder, int[] data) { builder.StartVector(4, data.Length, 4); builder.Add(data); return builder.EndVector(); }
  public static VectorOffset CreateExpiredBoxListVectorBlock(FlatBufferBuilder builder, ArraySegment<int> data) { builder.StartVector(4, data.Count, 4); builder.Add(data); return builder.EndVector(); }
  public static VectorOffset CreateExpiredBoxListVectorBlock(FlatBufferBuilder builder, IntPtr dataPtr, int sizeInBytes) { builder.StartVector(1, sizeInBytes, 1); builder.Add<int>(dataPtr, sizeInBytes); return builder.EndVector(); }
  public static void StartExpiredBoxListVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static Offset<Lobby.SA_TreasureBoxDaily> EndSA_TreasureBoxDaily(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<Lobby.SA_TreasureBoxDaily>(o);
  }
  public SA_TreasureBoxDailyT UnPack() {
    var _o = new SA_TreasureBoxDailyT();
    this.UnPackTo(_o);
    return _o;
  }
  public void UnPackTo(SA_TreasureBoxDailyT _o) {
    _o.DailyTreasureBoxList = new List<int>();
    for (var _j = 0; _j < this.DailyTreasureBoxListLength; ++_j) {_o.DailyTreasureBoxList.Add(this.DailyTreasureBoxList(_j));}
    _o.ExpiredBoxList = new List<int>();
    for (var _j = 0; _j < this.ExpiredBoxListLength; ++_j) {_o.ExpiredBoxList.Add(this.ExpiredBoxList(_j));}
  }
  public static Offset<Lobby.SA_TreasureBoxDaily> Pack(FlatBufferBuilder builder, SA_TreasureBoxDailyT _o) {
    if (_o == null) return default(Offset<Lobby.SA_TreasureBoxDaily>);
    var _DailyTreasureBoxList = default(VectorOffset);
    if (_o.DailyTreasureBoxList != null) {
      var __DailyTreasureBoxList = _o.DailyTreasureBoxList.ToArray();
      _DailyTreasureBoxList = CreateDailyTreasureBoxListVector(builder, __DailyTreasureBoxList);
    }
    var _ExpiredBoxList = default(VectorOffset);
    if (_o.ExpiredBoxList != null) {
      var __ExpiredBoxList = _o.ExpiredBoxList.ToArray();
      _ExpiredBoxList = CreateExpiredBoxListVector(builder, __ExpiredBoxList);
    }
    return CreateSA_TreasureBoxDaily(
      builder,
      _DailyTreasureBoxList,
      _ExpiredBoxList);
  }
}

public class SA_TreasureBoxDailyT
{
  public List<int> DailyTreasureBoxList { get; set; }
  public List<int> ExpiredBoxList { get; set; }

  public SA_TreasureBoxDailyT() {
    this.DailyTreasureBoxList = null;
    this.ExpiredBoxList = null;
  }
}


}
