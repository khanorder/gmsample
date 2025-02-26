// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Lobby
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct AttendanceData : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_23_1_20(); }
  public static AttendanceData GetRootAsAttendanceData(ByteBuffer _bb) { return GetRootAsAttendanceData(_bb, new AttendanceData()); }
  public static AttendanceData GetRootAsAttendanceData(ByteBuffer _bb, AttendanceData obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public AttendanceData __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public int AttendanceID { get { int o = __p.__offset(4); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int AttendanceDay { get { int o = __p.__offset(6); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public bool RewardState(int j) { int o = __p.__offset(8); return o != 0 ? 0!=__p.bb.Get(__p.__vector(o) + j * 1) : false; }
  public int RewardStateLength { get { int o = __p.__offset(8); return o != 0 ? __p.__vector_len(o) : 0; } }
#if ENABLE_SPAN_T
  public Span<bool> GetRewardStateBytes() { return __p.__vector_as_span<bool>(8, 1); }
#else
  public ArraySegment<byte>? GetRewardStateBytes() { return __p.__vector_as_arraysegment(8); }
#endif
  public bool[] GetRewardStateArray() { return __p.__vector_as_array<bool>(8); }
  public int LastAttendanceAt { get { int o = __p.__offset(10); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }

  public static Offset<Lobby.AttendanceData> CreateAttendanceData(FlatBufferBuilder builder,
      int AttendanceID = 0,
      int AttendanceDay = 0,
      VectorOffset RewardStateOffset = default(VectorOffset),
      int LastAttendanceAt = 0) {
    builder.StartTable(4);
    AttendanceData.AddLastAttendanceAt(builder, LastAttendanceAt);
    AttendanceData.AddRewardState(builder, RewardStateOffset);
    AttendanceData.AddAttendanceDay(builder, AttendanceDay);
    AttendanceData.AddAttendanceID(builder, AttendanceID);
    return AttendanceData.EndAttendanceData(builder);
  }

  public static void StartAttendanceData(FlatBufferBuilder builder) { builder.StartTable(4); }
  public static void AddAttendanceID(FlatBufferBuilder builder, int AttendanceID) { builder.AddInt(0, AttendanceID, 0); }
  public static void AddAttendanceDay(FlatBufferBuilder builder, int AttendanceDay) { builder.AddInt(1, AttendanceDay, 0); }
  public static void AddRewardState(FlatBufferBuilder builder, VectorOffset RewardStateOffset) { builder.AddOffset(2, RewardStateOffset.Value, 0); }
  public static VectorOffset CreateRewardStateVector(FlatBufferBuilder builder, bool[] data) { builder.StartVector(1, data.Length, 1); for (int i = data.Length - 1; i >= 0; i--) builder.AddBool(data[i]); return builder.EndVector(); }
  public static VectorOffset CreateRewardStateVectorBlock(FlatBufferBuilder builder, bool[] data) { builder.StartVector(1, data.Length, 1); builder.Add(data); return builder.EndVector(); }
  public static VectorOffset CreateRewardStateVectorBlock(FlatBufferBuilder builder, ArraySegment<bool> data) { builder.StartVector(1, data.Count, 1); builder.Add(data); return builder.EndVector(); }
  public static VectorOffset CreateRewardStateVectorBlock(FlatBufferBuilder builder, IntPtr dataPtr, int sizeInBytes) { builder.StartVector(1, sizeInBytes, 1); builder.Add<bool>(dataPtr, sizeInBytes); return builder.EndVector(); }
  public static void StartRewardStateVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(1, numElems, 1); }
  public static void AddLastAttendanceAt(FlatBufferBuilder builder, int LastAttendanceAt) { builder.AddInt(3, LastAttendanceAt, 0); }
  public static Offset<Lobby.AttendanceData> EndAttendanceData(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<Lobby.AttendanceData>(o);
  }
  public AttendanceDataT UnPack() {
    var _o = new AttendanceDataT();
    this.UnPackTo(_o);
    return _o;
  }
  public void UnPackTo(AttendanceDataT _o) {
    _o.AttendanceID = this.AttendanceID;
    _o.AttendanceDay = this.AttendanceDay;
    _o.RewardState = new List<bool>();
    for (var _j = 0; _j < this.RewardStateLength; ++_j) {_o.RewardState.Add(this.RewardState(_j));}
    _o.LastAttendanceAt = this.LastAttendanceAt;
  }
  public static Offset<Lobby.AttendanceData> Pack(FlatBufferBuilder builder, AttendanceDataT _o) {
    if (_o == null) return default(Offset<Lobby.AttendanceData>);
    var _RewardState = default(VectorOffset);
    if (_o.RewardState != null) {
      var __RewardState = _o.RewardState.ToArray();
      _RewardState = CreateRewardStateVector(builder, __RewardState);
    }
    return CreateAttendanceData(
      builder,
      _o.AttendanceID,
      _o.AttendanceDay,
      _RewardState,
      _o.LastAttendanceAt);
  }
}

public class AttendanceDataT
{
  public int AttendanceID { get; set; }
  public int AttendanceDay { get; set; }
  public List<bool> RewardState { get; set; }
  public int LastAttendanceAt { get; set; }

  public AttendanceDataT() {
    this.AttendanceID = 0;
    this.AttendanceDay = 0;
    this.RewardState = null;
    this.LastAttendanceAt = 0;
  }
}


}
