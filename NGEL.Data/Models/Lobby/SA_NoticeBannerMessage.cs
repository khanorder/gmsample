// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Lobby
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct SA_NoticeBannerMessage : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_23_1_20(); }
  public static SA_NoticeBannerMessage GetRootAsSA_NoticeBannerMessage(ByteBuffer _bb) { return GetRootAsSA_NoticeBannerMessage(_bb, new SA_NoticeBannerMessage()); }
  public static SA_NoticeBannerMessage GetRootAsSA_NoticeBannerMessage(ByteBuffer _bb, SA_NoticeBannerMessage obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public SA_NoticeBannerMessage __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public int BannerID { get { int o = __p.__offset(4); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int StartAt { get { int o = __p.__offset(6); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int EndAt { get { int o = __p.__offset(8); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public string ImageURL { get { int o = __p.__offset(10); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetImageURLBytes() { return __p.__vector_as_span<byte>(10, 1); }
#else
  public ArraySegment<byte>? GetImageURLBytes() { return __p.__vector_as_arraysegment(10); }
#endif
  public byte[] GetImageURLArray() { return __p.__vector_as_array<byte>(10); }
  public string Title { get { int o = __p.__offset(12); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetTitleBytes() { return __p.__vector_as_span<byte>(12, 1); }
#else
  public ArraySegment<byte>? GetTitleBytes() { return __p.__vector_as_arraysegment(12); }
#endif
  public byte[] GetTitleArray() { return __p.__vector_as_array<byte>(12); }
  public string Message { get { int o = __p.__offset(14); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetMessageBytes() { return __p.__vector_as_span<byte>(14, 1); }
#else
  public ArraySegment<byte>? GetMessageBytes() { return __p.__vector_as_arraysegment(14); }
#endif
  public byte[] GetMessageArray() { return __p.__vector_as_array<byte>(14); }

  public static Offset<Lobby.SA_NoticeBannerMessage> CreateSA_NoticeBannerMessage(FlatBufferBuilder builder,
      int BannerID = 0,
      int StartAt = 0,
      int EndAt = 0,
      StringOffset ImageURLOffset = default(StringOffset),
      StringOffset TitleOffset = default(StringOffset),
      StringOffset MessageOffset = default(StringOffset)) {
    builder.StartTable(6);
    SA_NoticeBannerMessage.AddMessage(builder, MessageOffset);
    SA_NoticeBannerMessage.AddTitle(builder, TitleOffset);
    SA_NoticeBannerMessage.AddImageURL(builder, ImageURLOffset);
    SA_NoticeBannerMessage.AddEndAt(builder, EndAt);
    SA_NoticeBannerMessage.AddStartAt(builder, StartAt);
    SA_NoticeBannerMessage.AddBannerID(builder, BannerID);
    return SA_NoticeBannerMessage.EndSA_NoticeBannerMessage(builder);
  }

  public static void StartSA_NoticeBannerMessage(FlatBufferBuilder builder) { builder.StartTable(6); }
  public static void AddBannerID(FlatBufferBuilder builder, int BannerID) { builder.AddInt(0, BannerID, 0); }
  public static void AddStartAt(FlatBufferBuilder builder, int StartAt) { builder.AddInt(1, StartAt, 0); }
  public static void AddEndAt(FlatBufferBuilder builder, int EndAt) { builder.AddInt(2, EndAt, 0); }
  public static void AddImageURL(FlatBufferBuilder builder, StringOffset ImageURLOffset) { builder.AddOffset(3, ImageURLOffset.Value, 0); }
  public static void AddTitle(FlatBufferBuilder builder, StringOffset TitleOffset) { builder.AddOffset(4, TitleOffset.Value, 0); }
  public static void AddMessage(FlatBufferBuilder builder, StringOffset MessageOffset) { builder.AddOffset(5, MessageOffset.Value, 0); }
  public static Offset<Lobby.SA_NoticeBannerMessage> EndSA_NoticeBannerMessage(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<Lobby.SA_NoticeBannerMessage>(o);
  }
  public SA_NoticeBannerMessageT UnPack() {
    var _o = new SA_NoticeBannerMessageT();
    this.UnPackTo(_o);
    return _o;
  }
  public void UnPackTo(SA_NoticeBannerMessageT _o) {
    _o.BannerID = this.BannerID;
    _o.StartAt = this.StartAt;
    _o.EndAt = this.EndAt;
    _o.ImageURL = this.ImageURL;
    _o.Title = this.Title;
    _o.Message = this.Message;
  }
  public static Offset<Lobby.SA_NoticeBannerMessage> Pack(FlatBufferBuilder builder, SA_NoticeBannerMessageT _o) {
    if (_o == null) return default(Offset<Lobby.SA_NoticeBannerMessage>);
    var _ImageURL = _o.ImageURL == null ? default(StringOffset) : builder.CreateString(_o.ImageURL);
    var _Title = _o.Title == null ? default(StringOffset) : builder.CreateString(_o.Title);
    var _Message = _o.Message == null ? default(StringOffset) : builder.CreateString(_o.Message);
    return CreateSA_NoticeBannerMessage(
      builder,
      _o.BannerID,
      _o.StartAt,
      _o.EndAt,
      _ImageURL,
      _Title,
      _Message);
  }
}

public class SA_NoticeBannerMessageT
{
  public int BannerID { get; set; }
  public int StartAt { get; set; }
  public int EndAt { get; set; }
  public string ImageURL { get; set; }
  public string Title { get; set; }
  public string Message { get; set; }

  public SA_NoticeBannerMessageT() {
    this.BannerID = 0;
    this.StartAt = 0;
    this.EndAt = 0;
    this.ImageURL = null;
    this.Title = null;
    this.Message = null;
  }
}


}
