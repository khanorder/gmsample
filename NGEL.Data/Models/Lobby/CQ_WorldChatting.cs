// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Lobby
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct CQ_WorldChatting : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_23_1_20(); }
  public static CQ_WorldChatting GetRootAsCQ_WorldChatting(ByteBuffer _bb) { return GetRootAsCQ_WorldChatting(_bb, new CQ_WorldChatting()); }
  public static CQ_WorldChatting GetRootAsCQ_WorldChatting(ByteBuffer _bb, CQ_WorldChatting obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public CQ_WorldChatting __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public string Message { get { int o = __p.__offset(4); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetMessageBytes() { return __p.__vector_as_span<byte>(4, 1); }
#else
  public ArraySegment<byte>? GetMessageBytes() { return __p.__vector_as_arraysegment(4); }
#endif
  public byte[] GetMessageArray() { return __p.__vector_as_array<byte>(4); }

  public static Offset<Lobby.CQ_WorldChatting> CreateCQ_WorldChatting(FlatBufferBuilder builder,
      StringOffset MessageOffset = default(StringOffset)) {
    builder.StartTable(1);
    CQ_WorldChatting.AddMessage(builder, MessageOffset);
    return CQ_WorldChatting.EndCQ_WorldChatting(builder);
  }

  public static void StartCQ_WorldChatting(FlatBufferBuilder builder) { builder.StartTable(1); }
  public static void AddMessage(FlatBufferBuilder builder, StringOffset MessageOffset) { builder.AddOffset(0, MessageOffset.Value, 0); }
  public static Offset<Lobby.CQ_WorldChatting> EndCQ_WorldChatting(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<Lobby.CQ_WorldChatting>(o);
  }
  public CQ_WorldChattingT UnPack() {
    var _o = new CQ_WorldChattingT();
    this.UnPackTo(_o);
    return _o;
  }
  public void UnPackTo(CQ_WorldChattingT _o) {
    _o.Message = this.Message;
  }
  public static Offset<Lobby.CQ_WorldChatting> Pack(FlatBufferBuilder builder, CQ_WorldChattingT _o) {
    if (_o == null) return default(Offset<Lobby.CQ_WorldChatting>);
    var _Message = _o.Message == null ? default(StringOffset) : builder.CreateString(_o.Message);
    return CreateCQ_WorldChatting(
      builder,
      _Message);
  }
}

public class CQ_WorldChattingT
{
  public string Message { get; set; }

  public CQ_WorldChattingT() {
    this.Message = null;
  }
}


}
