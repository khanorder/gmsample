// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Lobby
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct SA_MailHistory : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_23_1_20(); }
  public static SA_MailHistory GetRootAsSA_MailHistory(ByteBuffer _bb) { return GetRootAsSA_MailHistory(_bb, new SA_MailHistory()); }
  public static SA_MailHistory GetRootAsSA_MailHistory(ByteBuffer _bb, SA_MailHistory obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public SA_MailHistory __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public Lobby.MailInfo? AddHistoryList(int j) { int o = __p.__offset(4); return o != 0 ? (Lobby.MailInfo?)(new Lobby.MailInfo()).__assign(__p.__indirect(__p.__vector(o) + j * 4), __p.bb) : null; }
  public int AddHistoryListLength { get { int o = __p.__offset(4); return o != 0 ? __p.__vector_len(o) : 0; } }
  public long DeletedHistoryIDList(int j) { int o = __p.__offset(6); return o != 0 ? __p.bb.GetLong(__p.__vector(o) + j * 8) : (long)0; }
  public int DeletedHistoryIDListLength { get { int o = __p.__offset(6); return o != 0 ? __p.__vector_len(o) : 0; } }
#if ENABLE_SPAN_T
  public Span<long> GetDeletedHistoryIDListBytes() { return __p.__vector_as_span<long>(6, 8); }
#else
  public ArraySegment<byte>? GetDeletedHistoryIDListBytes() { return __p.__vector_as_arraysegment(6); }
#endif
  public long[] GetDeletedHistoryIDListArray() { return __p.__vector_as_array<long>(6); }

  public static Offset<Lobby.SA_MailHistory> CreateSA_MailHistory(FlatBufferBuilder builder,
      VectorOffset AddHistoryListOffset = default(VectorOffset),
      VectorOffset DeletedHistoryIDListOffset = default(VectorOffset)) {
    builder.StartTable(2);
    SA_MailHistory.AddDeletedHistoryIDList(builder, DeletedHistoryIDListOffset);
    SA_MailHistory.AddAddHistoryList(builder, AddHistoryListOffset);
    return SA_MailHistory.EndSA_MailHistory(builder);
  }

  public static void StartSA_MailHistory(FlatBufferBuilder builder) { builder.StartTable(2); }
  public static void AddAddHistoryList(FlatBufferBuilder builder, VectorOffset AddHistoryListOffset) { builder.AddOffset(0, AddHistoryListOffset.Value, 0); }
  public static VectorOffset CreateAddHistoryListVector(FlatBufferBuilder builder, Offset<Lobby.MailInfo>[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddOffset(data[i].Value); return builder.EndVector(); }
  public static VectorOffset CreateAddHistoryListVectorBlock(FlatBufferBuilder builder, Offset<Lobby.MailInfo>[] data) { builder.StartVector(4, data.Length, 4); builder.Add(data); return builder.EndVector(); }
  public static VectorOffset CreateAddHistoryListVectorBlock(FlatBufferBuilder builder, ArraySegment<Offset<Lobby.MailInfo>> data) { builder.StartVector(4, data.Count, 4); builder.Add(data); return builder.EndVector(); }
  public static VectorOffset CreateAddHistoryListVectorBlock(FlatBufferBuilder builder, IntPtr dataPtr, int sizeInBytes) { builder.StartVector(1, sizeInBytes, 1); builder.Add<Offset<Lobby.MailInfo>>(dataPtr, sizeInBytes); return builder.EndVector(); }
  public static void StartAddHistoryListVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static void AddDeletedHistoryIDList(FlatBufferBuilder builder, VectorOffset DeletedHistoryIDListOffset) { builder.AddOffset(1, DeletedHistoryIDListOffset.Value, 0); }
  public static VectorOffset CreateDeletedHistoryIDListVector(FlatBufferBuilder builder, long[] data) { builder.StartVector(8, data.Length, 8); for (int i = data.Length - 1; i >= 0; i--) builder.AddLong(data[i]); return builder.EndVector(); }
  public static VectorOffset CreateDeletedHistoryIDListVectorBlock(FlatBufferBuilder builder, long[] data) { builder.StartVector(8, data.Length, 8); builder.Add(data); return builder.EndVector(); }
  public static VectorOffset CreateDeletedHistoryIDListVectorBlock(FlatBufferBuilder builder, ArraySegment<long> data) { builder.StartVector(8, data.Count, 8); builder.Add(data); return builder.EndVector(); }
  public static VectorOffset CreateDeletedHistoryIDListVectorBlock(FlatBufferBuilder builder, IntPtr dataPtr, int sizeInBytes) { builder.StartVector(1, sizeInBytes, 1); builder.Add<long>(dataPtr, sizeInBytes); return builder.EndVector(); }
  public static void StartDeletedHistoryIDListVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(8, numElems, 8); }
  public static Offset<Lobby.SA_MailHistory> EndSA_MailHistory(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<Lobby.SA_MailHistory>(o);
  }
  public SA_MailHistoryT UnPack() {
    var _o = new SA_MailHistoryT();
    this.UnPackTo(_o);
    return _o;
  }
  public void UnPackTo(SA_MailHistoryT _o) {
    _o.AddHistoryList = new List<Lobby.MailInfoT>();
    for (var _j = 0; _j < this.AddHistoryListLength; ++_j) {_o.AddHistoryList.Add(this.AddHistoryList(_j).HasValue ? this.AddHistoryList(_j).Value.UnPack() : null);}
    _o.DeletedHistoryIDList = new List<long>();
    for (var _j = 0; _j < this.DeletedHistoryIDListLength; ++_j) {_o.DeletedHistoryIDList.Add(this.DeletedHistoryIDList(_j));}
  }
  public static Offset<Lobby.SA_MailHistory> Pack(FlatBufferBuilder builder, SA_MailHistoryT _o) {
    if (_o == null) return default(Offset<Lobby.SA_MailHistory>);
    var _AddHistoryList = default(VectorOffset);
    if (_o.AddHistoryList != null) {
      var __AddHistoryList = new Offset<Lobby.MailInfo>[_o.AddHistoryList.Count];
      for (var _j = 0; _j < __AddHistoryList.Length; ++_j) { __AddHistoryList[_j] = Lobby.MailInfo.Pack(builder, _o.AddHistoryList[_j]); }
      _AddHistoryList = CreateAddHistoryListVector(builder, __AddHistoryList);
    }
    var _DeletedHistoryIDList = default(VectorOffset);
    if (_o.DeletedHistoryIDList != null) {
      var __DeletedHistoryIDList = _o.DeletedHistoryIDList.ToArray();
      _DeletedHistoryIDList = CreateDeletedHistoryIDListVector(builder, __DeletedHistoryIDList);
    }
    return CreateSA_MailHistory(
      builder,
      _AddHistoryList,
      _DeletedHistoryIDList);
  }
}

public class SA_MailHistoryT
{
  public List<Lobby.MailInfoT> AddHistoryList { get; set; }
  public List<long> DeletedHistoryIDList { get; set; }

  public SA_MailHistoryT() {
    this.AddHistoryList = null;
    this.DeletedHistoryIDList = null;
  }
}


}
