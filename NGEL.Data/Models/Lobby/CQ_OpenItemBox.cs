// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Lobby
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct CQ_OpenItemBox : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_23_1_20(); }
  public static CQ_OpenItemBox GetRootAsCQ_OpenItemBox(ByteBuffer _bb) { return GetRootAsCQ_OpenItemBox(_bb, new CQ_OpenItemBox()); }
  public static CQ_OpenItemBox GetRootAsCQ_OpenItemBox(ByteBuffer _bb, CQ_OpenItemBox obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public CQ_OpenItemBox __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public int ItemBoxID { get { int o = __p.__offset(4); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int ItemIndexList(int j) { int o = __p.__offset(6); return o != 0 ? __p.bb.GetInt(__p.__vector(o) + j * 4) : (int)0; }
  public int ItemIndexListLength { get { int o = __p.__offset(6); return o != 0 ? __p.__vector_len(o) : 0; } }
#if ENABLE_SPAN_T
  public Span<int> GetItemIndexListBytes() { return __p.__vector_as_span<int>(6, 4); }
#else
  public ArraySegment<byte>? GetItemIndexListBytes() { return __p.__vector_as_arraysegment(6); }
#endif
  public int[] GetItemIndexListArray() { return __p.__vector_as_array<int>(6); }
  public int Count { get { int o = __p.__offset(8); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }

  public static Offset<Lobby.CQ_OpenItemBox> CreateCQ_OpenItemBox(FlatBufferBuilder builder,
      int ItemBoxID = 0,
      VectorOffset ItemIndexListOffset = default(VectorOffset),
      int Count = 0) {
    builder.StartTable(3);
    CQ_OpenItemBox.AddCount(builder, Count);
    CQ_OpenItemBox.AddItemIndexList(builder, ItemIndexListOffset);
    CQ_OpenItemBox.AddItemBoxID(builder, ItemBoxID);
    return CQ_OpenItemBox.EndCQ_OpenItemBox(builder);
  }

  public static void StartCQ_OpenItemBox(FlatBufferBuilder builder) { builder.StartTable(3); }
  public static void AddItemBoxID(FlatBufferBuilder builder, int ItemBoxID) { builder.AddInt(0, ItemBoxID, 0); }
  public static void AddItemIndexList(FlatBufferBuilder builder, VectorOffset ItemIndexListOffset) { builder.AddOffset(1, ItemIndexListOffset.Value, 0); }
  public static VectorOffset CreateItemIndexListVector(FlatBufferBuilder builder, int[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddInt(data[i]); return builder.EndVector(); }
  public static VectorOffset CreateItemIndexListVectorBlock(FlatBufferBuilder builder, int[] data) { builder.StartVector(4, data.Length, 4); builder.Add(data); return builder.EndVector(); }
  public static VectorOffset CreateItemIndexListVectorBlock(FlatBufferBuilder builder, ArraySegment<int> data) { builder.StartVector(4, data.Count, 4); builder.Add(data); return builder.EndVector(); }
  public static VectorOffset CreateItemIndexListVectorBlock(FlatBufferBuilder builder, IntPtr dataPtr, int sizeInBytes) { builder.StartVector(1, sizeInBytes, 1); builder.Add<int>(dataPtr, sizeInBytes); return builder.EndVector(); }
  public static void StartItemIndexListVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static void AddCount(FlatBufferBuilder builder, int Count) { builder.AddInt(2, Count, 0); }
  public static Offset<Lobby.CQ_OpenItemBox> EndCQ_OpenItemBox(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<Lobby.CQ_OpenItemBox>(o);
  }
  public CQ_OpenItemBoxT UnPack() {
    var _o = new CQ_OpenItemBoxT();
    this.UnPackTo(_o);
    return _o;
  }
  public void UnPackTo(CQ_OpenItemBoxT _o) {
    _o.ItemBoxID = this.ItemBoxID;
    _o.ItemIndexList = new List<int>();
    for (var _j = 0; _j < this.ItemIndexListLength; ++_j) {_o.ItemIndexList.Add(this.ItemIndexList(_j));}
    _o.Count = this.Count;
  }
  public static Offset<Lobby.CQ_OpenItemBox> Pack(FlatBufferBuilder builder, CQ_OpenItemBoxT _o) {
    if (_o == null) return default(Offset<Lobby.CQ_OpenItemBox>);
    var _ItemIndexList = default(VectorOffset);
    if (_o.ItemIndexList != null) {
      var __ItemIndexList = _o.ItemIndexList.ToArray();
      _ItemIndexList = CreateItemIndexListVector(builder, __ItemIndexList);
    }
    return CreateCQ_OpenItemBox(
      builder,
      _o.ItemBoxID,
      _ItemIndexList,
      _o.Count);
  }
}

public class CQ_OpenItemBoxT
{
  public int ItemBoxID { get; set; }
  public List<int> ItemIndexList { get; set; }
  public int Count { get; set; }

  public CQ_OpenItemBoxT() {
    this.ItemBoxID = 0;
    this.ItemIndexList = null;
    this.Count = 0;
  }
}


}
