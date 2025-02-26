// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Lobby
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct ExpressionPresetData : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_23_1_20(); }
  public static ExpressionPresetData GetRootAsExpressionPresetData(ByteBuffer _bb) { return GetRootAsExpressionPresetData(_bb, new ExpressionPresetData()); }
  public static ExpressionPresetData GetRootAsExpressionPresetData(ByteBuffer _bb, ExpressionPresetData obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public ExpressionPresetData __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public int HeroID { get { int o = __p.__offset(4); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int Preset(int j) { int o = __p.__offset(6); return o != 0 ? __p.bb.GetInt(__p.__vector(o) + j * 4) : (int)0; }
  public int PresetLength { get { int o = __p.__offset(6); return o != 0 ? __p.__vector_len(o) : 0; } }
#if ENABLE_SPAN_T
  public Span<int> GetPresetBytes() { return __p.__vector_as_span<int>(6, 4); }
#else
  public ArraySegment<byte>? GetPresetBytes() { return __p.__vector_as_arraysegment(6); }
#endif
  public int[] GetPresetArray() { return __p.__vector_as_array<int>(6); }

  public static Offset<Lobby.ExpressionPresetData> CreateExpressionPresetData(FlatBufferBuilder builder,
      int HeroID = 0,
      VectorOffset PresetOffset = default(VectorOffset)) {
    builder.StartTable(2);
    ExpressionPresetData.AddPreset(builder, PresetOffset);
    ExpressionPresetData.AddHeroID(builder, HeroID);
    return ExpressionPresetData.EndExpressionPresetData(builder);
  }

  public static void StartExpressionPresetData(FlatBufferBuilder builder) { builder.StartTable(2); }
  public static void AddHeroID(FlatBufferBuilder builder, int HeroID) { builder.AddInt(0, HeroID, 0); }
  public static void AddPreset(FlatBufferBuilder builder, VectorOffset PresetOffset) { builder.AddOffset(1, PresetOffset.Value, 0); }
  public static VectorOffset CreatePresetVector(FlatBufferBuilder builder, int[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddInt(data[i]); return builder.EndVector(); }
  public static VectorOffset CreatePresetVectorBlock(FlatBufferBuilder builder, int[] data) { builder.StartVector(4, data.Length, 4); builder.Add(data); return builder.EndVector(); }
  public static VectorOffset CreatePresetVectorBlock(FlatBufferBuilder builder, ArraySegment<int> data) { builder.StartVector(4, data.Count, 4); builder.Add(data); return builder.EndVector(); }
  public static VectorOffset CreatePresetVectorBlock(FlatBufferBuilder builder, IntPtr dataPtr, int sizeInBytes) { builder.StartVector(1, sizeInBytes, 1); builder.Add<int>(dataPtr, sizeInBytes); return builder.EndVector(); }
  public static void StartPresetVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static Offset<Lobby.ExpressionPresetData> EndExpressionPresetData(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<Lobby.ExpressionPresetData>(o);
  }
  public ExpressionPresetDataT UnPack() {
    var _o = new ExpressionPresetDataT();
    this.UnPackTo(_o);
    return _o;
  }
  public void UnPackTo(ExpressionPresetDataT _o) {
    _o.HeroID = this.HeroID;
    _o.Preset = new List<int>();
    for (var _j = 0; _j < this.PresetLength; ++_j) {_o.Preset.Add(this.Preset(_j));}
  }
  public static Offset<Lobby.ExpressionPresetData> Pack(FlatBufferBuilder builder, ExpressionPresetDataT _o) {
    if (_o == null) return default(Offset<Lobby.ExpressionPresetData>);
    var _Preset = default(VectorOffset);
    if (_o.Preset != null) {
      var __Preset = _o.Preset.ToArray();
      _Preset = CreatePresetVector(builder, __Preset);
    }
    return CreateExpressionPresetData(
      builder,
      _o.HeroID,
      _Preset);
  }
}

public class ExpressionPresetDataT
{
  public int HeroID { get; set; }
  public List<int> Preset { get; set; }

  public ExpressionPresetDataT() {
    this.HeroID = 0;
    this.Preset = null;
  }
}


}
