// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Lobby
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct SA_AttendanceList : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_23_1_20(); }
  public static SA_AttendanceList GetRootAsSA_AttendanceList(ByteBuffer _bb) { return GetRootAsSA_AttendanceList(_bb, new SA_AttendanceList()); }
  public static SA_AttendanceList GetRootAsSA_AttendanceList(ByteBuffer _bb, SA_AttendanceList obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public SA_AttendanceList __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public bool IsAttendance { get { int o = __p.__offset(4); return o != 0 ? 0!=__p.bb.Get(o + __p.bb_pos) : (bool)false; } }
  public Lobby.AttendanceData? AttendanceList(int j) { int o = __p.__offset(6); return o != 0 ? (Lobby.AttendanceData?)(new Lobby.AttendanceData()).__assign(__p.__indirect(__p.__vector(o) + j * 4), __p.bb) : null; }
  public int AttendanceListLength { get { int o = __p.__offset(6); return o != 0 ? __p.__vector_len(o) : 0; } }
  public int NextAttendanceResetAt { get { int o = __p.__offset(8); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }

  public static Offset<Lobby.SA_AttendanceList> CreateSA_AttendanceList(FlatBufferBuilder builder,
      bool IsAttendance = false,
      VectorOffset AttendanceListOffset = default(VectorOffset),
      int NextAttendanceResetAt = 0) {
    builder.StartTable(3);
    SA_AttendanceList.AddNextAttendanceResetAt(builder, NextAttendanceResetAt);
    SA_AttendanceList.AddAttendanceList(builder, AttendanceListOffset);
    SA_AttendanceList.AddIsAttendance(builder, IsAttendance);
    return SA_AttendanceList.EndSA_AttendanceList(builder);
  }

  public static void StartSA_AttendanceList(FlatBufferBuilder builder) { builder.StartTable(3); }
  public static void AddIsAttendance(FlatBufferBuilder builder, bool IsAttendance) { builder.AddBool(0, IsAttendance, false); }
  public static void AddAttendanceList(FlatBufferBuilder builder, VectorOffset AttendanceListOffset) { builder.AddOffset(1, AttendanceListOffset.Value, 0); }
  public static VectorOffset CreateAttendanceListVector(FlatBufferBuilder builder, Offset<Lobby.AttendanceData>[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddOffset(data[i].Value); return builder.EndVector(); }
  public static VectorOffset CreateAttendanceListVectorBlock(FlatBufferBuilder builder, Offset<Lobby.AttendanceData>[] data) { builder.StartVector(4, data.Length, 4); builder.Add(data); return builder.EndVector(); }
  public static VectorOffset CreateAttendanceListVectorBlock(FlatBufferBuilder builder, ArraySegment<Offset<Lobby.AttendanceData>> data) { builder.StartVector(4, data.Count, 4); builder.Add(data); return builder.EndVector(); }
  public static VectorOffset CreateAttendanceListVectorBlock(FlatBufferBuilder builder, IntPtr dataPtr, int sizeInBytes) { builder.StartVector(1, sizeInBytes, 1); builder.Add<Offset<Lobby.AttendanceData>>(dataPtr, sizeInBytes); return builder.EndVector(); }
  public static void StartAttendanceListVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static void AddNextAttendanceResetAt(FlatBufferBuilder builder, int NextAttendanceResetAt) { builder.AddInt(2, NextAttendanceResetAt, 0); }
  public static Offset<Lobby.SA_AttendanceList> EndSA_AttendanceList(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<Lobby.SA_AttendanceList>(o);
  }
  public SA_AttendanceListT UnPack() {
    var _o = new SA_AttendanceListT();
    this.UnPackTo(_o);
    return _o;
  }
  public void UnPackTo(SA_AttendanceListT _o) {
    _o.IsAttendance = this.IsAttendance;
    _o.AttendanceList = new List<Lobby.AttendanceDataT>();
    for (var _j = 0; _j < this.AttendanceListLength; ++_j) {_o.AttendanceList.Add(this.AttendanceList(_j).HasValue ? this.AttendanceList(_j).Value.UnPack() : null);}
    _o.NextAttendanceResetAt = this.NextAttendanceResetAt;
  }
  public static Offset<Lobby.SA_AttendanceList> Pack(FlatBufferBuilder builder, SA_AttendanceListT _o) {
    if (_o == null) return default(Offset<Lobby.SA_AttendanceList>);
    var _AttendanceList = default(VectorOffset);
    if (_o.AttendanceList != null) {
      var __AttendanceList = new Offset<Lobby.AttendanceData>[_o.AttendanceList.Count];
      for (var _j = 0; _j < __AttendanceList.Length; ++_j) { __AttendanceList[_j] = Lobby.AttendanceData.Pack(builder, _o.AttendanceList[_j]); }
      _AttendanceList = CreateAttendanceListVector(builder, __AttendanceList);
    }
    return CreateSA_AttendanceList(
      builder,
      _o.IsAttendance,
      _AttendanceList,
      _o.NextAttendanceResetAt);
  }
}

public class SA_AttendanceListT
{
  public bool IsAttendance { get; set; }
  public List<Lobby.AttendanceDataT> AttendanceList { get; set; }
  public int NextAttendanceResetAt { get; set; }

  public SA_AttendanceListT() {
    this.IsAttendance = false;
    this.AttendanceList = null;
    this.NextAttendanceResetAt = 0;
  }
}


}
