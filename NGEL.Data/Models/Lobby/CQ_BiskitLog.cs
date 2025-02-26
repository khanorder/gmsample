// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Lobby
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct CQ_BiskitLog : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_23_1_20(); }
  public static CQ_BiskitLog GetRootAsCQ_BiskitLog(ByteBuffer _bb) { return GetRootAsCQ_BiskitLog(_bb, new CQ_BiskitLog()); }
  public static CQ_BiskitLog GetRootAsCQ_BiskitLog(ByteBuffer _bb, CQ_BiskitLog obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public CQ_BiskitLog __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public Lobby.BiskitLog? Loglist(int j) { int o = __p.__offset(4); return o != 0 ? (Lobby.BiskitLog?)(new Lobby.BiskitLog()).__assign(__p.__indirect(__p.__vector(o) + j * 4), __p.bb) : null; }
  public int LoglistLength { get { int o = __p.__offset(4); return o != 0 ? __p.__vector_len(o) : 0; } }

  public static Offset<Lobby.CQ_BiskitLog> CreateCQ_BiskitLog(FlatBufferBuilder builder,
      VectorOffset LoglistOffset = default(VectorOffset)) {
    builder.StartTable(1);
    CQ_BiskitLog.AddLoglist(builder, LoglistOffset);
    return CQ_BiskitLog.EndCQ_BiskitLog(builder);
  }

  public static void StartCQ_BiskitLog(FlatBufferBuilder builder) { builder.StartTable(1); }
  public static void AddLoglist(FlatBufferBuilder builder, VectorOffset LoglistOffset) { builder.AddOffset(0, LoglistOffset.Value, 0); }
  public static VectorOffset CreateLoglistVector(FlatBufferBuilder builder, Offset<Lobby.BiskitLog>[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddOffset(data[i].Value); return builder.EndVector(); }
  public static VectorOffset CreateLoglistVectorBlock(FlatBufferBuilder builder, Offset<Lobby.BiskitLog>[] data) { builder.StartVector(4, data.Length, 4); builder.Add(data); return builder.EndVector(); }
  public static VectorOffset CreateLoglistVectorBlock(FlatBufferBuilder builder, ArraySegment<Offset<Lobby.BiskitLog>> data) { builder.StartVector(4, data.Count, 4); builder.Add(data); return builder.EndVector(); }
  public static VectorOffset CreateLoglistVectorBlock(FlatBufferBuilder builder, IntPtr dataPtr, int sizeInBytes) { builder.StartVector(1, sizeInBytes, 1); builder.Add<Offset<Lobby.BiskitLog>>(dataPtr, sizeInBytes); return builder.EndVector(); }
  public static void StartLoglistVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static Offset<Lobby.CQ_BiskitLog> EndCQ_BiskitLog(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<Lobby.CQ_BiskitLog>(o);
  }
  public CQ_BiskitLogT UnPack() {
    var _o = new CQ_BiskitLogT();
    this.UnPackTo(_o);
    return _o;
  }
  public void UnPackTo(CQ_BiskitLogT _o) {
    _o.Loglist = new List<Lobby.BiskitLogT>();
    for (var _j = 0; _j < this.LoglistLength; ++_j) {_o.Loglist.Add(this.Loglist(_j).HasValue ? this.Loglist(_j).Value.UnPack() : null);}
  }
  public static Offset<Lobby.CQ_BiskitLog> Pack(FlatBufferBuilder builder, CQ_BiskitLogT _o) {
    if (_o == null) return default(Offset<Lobby.CQ_BiskitLog>);
    var _Loglist = default(VectorOffset);
    if (_o.Loglist != null) {
      var __Loglist = new Offset<Lobby.BiskitLog>[_o.Loglist.Count];
      for (var _j = 0; _j < __Loglist.Length; ++_j) { __Loglist[_j] = Lobby.BiskitLog.Pack(builder, _o.Loglist[_j]); }
      _Loglist = CreateLoglistVector(builder, __Loglist);
    }
    return CreateCQ_BiskitLog(
      builder,
      _Loglist);
  }
}

public class CQ_BiskitLogT
{
  public List<Lobby.BiskitLogT> Loglist { get; set; }

  public CQ_BiskitLogT() {
    this.Loglist = null;
  }
}


}
