// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Lobby
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct SpecialLevelData : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_23_1_20(); }
  public static SpecialLevelData GetRootAsSpecialLevelData(ByteBuffer _bb) { return GetRootAsSpecialLevelData(_bb, new SpecialLevelData()); }
  public static SpecialLevelData GetRootAsSpecialLevelData(ByteBuffer _bb, SpecialLevelData obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public SpecialLevelData __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public int LevelID { get { int o = __p.__offset(4); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int CompletedAt { get { int o = __p.__offset(6); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int BoxIDList(int j) { int o = __p.__offset(8); return o != 0 ? __p.bb.GetInt(__p.__vector(o) + j * 4) : (int)0; }
  public int BoxIDListLength { get { int o = __p.__offset(8); return o != 0 ? __p.__vector_len(o) : 0; } }
#if ENABLE_SPAN_T
  public Span<int> GetBoxIDListBytes() { return __p.__vector_as_span<int>(8, 4); }
#else
  public ArraySegment<byte>? GetBoxIDListBytes() { return __p.__vector_as_arraysegment(8); }
#endif
  public int[] GetBoxIDListArray() { return __p.__vector_as_array<int>(8); }

  public static Offset<Lobby.SpecialLevelData> CreateSpecialLevelData(FlatBufferBuilder builder,
      int LevelID = 0,
      int CompletedAt = 0,
      VectorOffset BoxIDListOffset = default(VectorOffset)) {
    builder.StartTable(3);
    SpecialLevelData.AddBoxIDList(builder, BoxIDListOffset);
    SpecialLevelData.AddCompletedAt(builder, CompletedAt);
    SpecialLevelData.AddLevelID(builder, LevelID);
    return SpecialLevelData.EndSpecialLevelData(builder);
  }

  public static void StartSpecialLevelData(FlatBufferBuilder builder) { builder.StartTable(3); }
  public static void AddLevelID(FlatBufferBuilder builder, int LevelID) { builder.AddInt(0, LevelID, 0); }
  public static void AddCompletedAt(FlatBufferBuilder builder, int CompletedAt) { builder.AddInt(1, CompletedAt, 0); }
  public static void AddBoxIDList(FlatBufferBuilder builder, VectorOffset BoxIDListOffset) { builder.AddOffset(2, BoxIDListOffset.Value, 0); }
  public static VectorOffset CreateBoxIDListVector(FlatBufferBuilder builder, int[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddInt(data[i]); return builder.EndVector(); }
  public static VectorOffset CreateBoxIDListVectorBlock(FlatBufferBuilder builder, int[] data) { builder.StartVector(4, data.Length, 4); builder.Add(data); return builder.EndVector(); }
  public static VectorOffset CreateBoxIDListVectorBlock(FlatBufferBuilder builder, ArraySegment<int> data) { builder.StartVector(4, data.Count, 4); builder.Add(data); return builder.EndVector(); }
  public static VectorOffset CreateBoxIDListVectorBlock(FlatBufferBuilder builder, IntPtr dataPtr, int sizeInBytes) { builder.StartVector(1, sizeInBytes, 1); builder.Add<int>(dataPtr, sizeInBytes); return builder.EndVector(); }
  public static void StartBoxIDListVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static Offset<Lobby.SpecialLevelData> EndSpecialLevelData(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<Lobby.SpecialLevelData>(o);
  }
  public SpecialLevelDataT UnPack() {
    var _o = new SpecialLevelDataT();
    this.UnPackTo(_o);
    return _o;
  }
  public void UnPackTo(SpecialLevelDataT _o) {
    _o.LevelID = this.LevelID;
    _o.CompletedAt = this.CompletedAt;
    _o.BoxIDList = new List<int>();
    for (var _j = 0; _j < this.BoxIDListLength; ++_j) {_o.BoxIDList.Add(this.BoxIDList(_j));}
  }
  public static Offset<Lobby.SpecialLevelData> Pack(FlatBufferBuilder builder, SpecialLevelDataT _o) {
    if (_o == null) return default(Offset<Lobby.SpecialLevelData>);
    var _BoxIDList = default(VectorOffset);
    if (_o.BoxIDList != null) {
      var __BoxIDList = _o.BoxIDList.ToArray();
      _BoxIDList = CreateBoxIDListVector(builder, __BoxIDList);
    }
    return CreateSpecialLevelData(
      builder,
      _o.LevelID,
      _o.CompletedAt,
      _BoxIDList);
  }
}

public class SpecialLevelDataT
{
  public int LevelID { get; set; }
  public int CompletedAt { get; set; }
  public List<int> BoxIDList { get; set; }

  public SpecialLevelDataT() {
    this.LevelID = 0;
    this.CompletedAt = 0;
    this.BoxIDList = null;
  }
}


}
