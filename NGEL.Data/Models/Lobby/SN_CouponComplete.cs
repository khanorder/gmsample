// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Lobby
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct SN_CouponComplete : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_23_1_20(); }
  public static SN_CouponComplete GetRootAsSN_CouponComplete(ByteBuffer _bb) { return GetRootAsSN_CouponComplete(_bb, new SN_CouponComplete()); }
  public static SN_CouponComplete GetRootAsSN_CouponComplete(ByteBuffer _bb, SN_CouponComplete obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public SN_CouponComplete __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public string TID { get { int o = __p.__offset(4); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetTIDBytes() { return __p.__vector_as_span<byte>(4, 1); }
#else
  public ArraySegment<byte>? GetTIDBytes() { return __p.__vector_as_arraysegment(4); }
#endif
  public byte[] GetTIDArray() { return __p.__vector_as_array<byte>(4); }
  public string ItemID { get { int o = __p.__offset(6); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetItemIDBytes() { return __p.__vector_as_span<byte>(6, 1); }
#else
  public ArraySegment<byte>? GetItemIDBytes() { return __p.__vector_as_arraysegment(6); }
#endif
  public byte[] GetItemIDArray() { return __p.__vector_as_array<byte>(6); }
  public int ItemCount { get { int o = __p.__offset(8); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }

  public static Offset<Lobby.SN_CouponComplete> CreateSN_CouponComplete(FlatBufferBuilder builder,
      StringOffset TIDOffset = default(StringOffset),
      StringOffset ItemIDOffset = default(StringOffset),
      int ItemCount = 0) {
    builder.StartTable(3);
    SN_CouponComplete.AddItemCount(builder, ItemCount);
    SN_CouponComplete.AddItemID(builder, ItemIDOffset);
    SN_CouponComplete.AddTID(builder, TIDOffset);
    return SN_CouponComplete.EndSN_CouponComplete(builder);
  }

  public static void StartSN_CouponComplete(FlatBufferBuilder builder) { builder.StartTable(3); }
  public static void AddTID(FlatBufferBuilder builder, StringOffset TIDOffset) { builder.AddOffset(0, TIDOffset.Value, 0); }
  public static void AddItemID(FlatBufferBuilder builder, StringOffset ItemIDOffset) { builder.AddOffset(1, ItemIDOffset.Value, 0); }
  public static void AddItemCount(FlatBufferBuilder builder, int ItemCount) { builder.AddInt(2, ItemCount, 0); }
  public static Offset<Lobby.SN_CouponComplete> EndSN_CouponComplete(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<Lobby.SN_CouponComplete>(o);
  }
  public SN_CouponCompleteT UnPack() {
    var _o = new SN_CouponCompleteT();
    this.UnPackTo(_o);
    return _o;
  }
  public void UnPackTo(SN_CouponCompleteT _o) {
    _o.TID = this.TID;
    _o.ItemID = this.ItemID;
    _o.ItemCount = this.ItemCount;
  }
  public static Offset<Lobby.SN_CouponComplete> Pack(FlatBufferBuilder builder, SN_CouponCompleteT _o) {
    if (_o == null) return default(Offset<Lobby.SN_CouponComplete>);
    var _TID = _o.TID == null ? default(StringOffset) : builder.CreateString(_o.TID);
    var _ItemID = _o.ItemID == null ? default(StringOffset) : builder.CreateString(_o.ItemID);
    return CreateSN_CouponComplete(
      builder,
      _TID,
      _ItemID,
      _o.ItemCount);
  }
}

public class SN_CouponCompleteT
{
  public string TID { get; set; }
  public string ItemID { get; set; }
  public int ItemCount { get; set; }

  public SN_CouponCompleteT() {
    this.TID = null;
    this.ItemID = null;
    this.ItemCount = 0;
  }
}


}
