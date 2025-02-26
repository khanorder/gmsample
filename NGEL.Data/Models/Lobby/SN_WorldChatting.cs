// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Lobby
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct SN_WorldChatting : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_23_1_20(); }
  public static SN_WorldChatting GetRootAsSN_WorldChatting(ByteBuffer _bb) { return GetRootAsSN_WorldChatting(_bb, new SN_WorldChatting()); }
  public static SN_WorldChatting GetRootAsSN_WorldChatting(ByteBuffer _bb, SN_WorldChatting obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public SN_WorldChatting __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public int UID { get { int o = __p.__offset(4); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public string NickName { get { int o = __p.__offset(6); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetNickNameBytes() { return __p.__vector_as_span<byte>(6, 1); }
#else
  public ArraySegment<byte>? GetNickNameBytes() { return __p.__vector_as_arraysegment(6); }
#endif
  public byte[] GetNickNameArray() { return __p.__vector_as_array<byte>(6); }
  public string Message { get { int o = __p.__offset(8); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetMessageBytes() { return __p.__vector_as_span<byte>(8, 1); }
#else
  public ArraySegment<byte>? GetMessageBytes() { return __p.__vector_as_arraysegment(8); }
#endif
  public byte[] GetMessageArray() { return __p.__vector_as_array<byte>(8); }

  public static Offset<Lobby.SN_WorldChatting> CreateSN_WorldChatting(FlatBufferBuilder builder,
      int UID = 0,
      StringOffset NickNameOffset = default(StringOffset),
      StringOffset MessageOffset = default(StringOffset)) {
    builder.StartTable(3);
    SN_WorldChatting.AddMessage(builder, MessageOffset);
    SN_WorldChatting.AddNickName(builder, NickNameOffset);
    SN_WorldChatting.AddUID(builder, UID);
    return SN_WorldChatting.EndSN_WorldChatting(builder);
  }

  public static void StartSN_WorldChatting(FlatBufferBuilder builder) { builder.StartTable(3); }
  public static void AddUID(FlatBufferBuilder builder, int UID) { builder.AddInt(0, UID, 0); }
  public static void AddNickName(FlatBufferBuilder builder, StringOffset NickNameOffset) { builder.AddOffset(1, NickNameOffset.Value, 0); }
  public static void AddMessage(FlatBufferBuilder builder, StringOffset MessageOffset) { builder.AddOffset(2, MessageOffset.Value, 0); }
  public static Offset<Lobby.SN_WorldChatting> EndSN_WorldChatting(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<Lobby.SN_WorldChatting>(o);
  }
  public SN_WorldChattingT UnPack() {
    var _o = new SN_WorldChattingT();
    this.UnPackTo(_o);
    return _o;
  }
  public void UnPackTo(SN_WorldChattingT _o) {
    _o.UID = this.UID;
    _o.NickName = this.NickName;
    _o.Message = this.Message;
  }
  public static Offset<Lobby.SN_WorldChatting> Pack(FlatBufferBuilder builder, SN_WorldChattingT _o) {
    if (_o == null) return default(Offset<Lobby.SN_WorldChatting>);
    var _NickName = _o.NickName == null ? default(StringOffset) : builder.CreateString(_o.NickName);
    var _Message = _o.Message == null ? default(StringOffset) : builder.CreateString(_o.Message);
    return CreateSN_WorldChatting(
      builder,
      _o.UID,
      _NickName,
      _Message);
  }
}

public class SN_WorldChattingT
{
  public int UID { get; set; }
  public string NickName { get; set; }
  public string Message { get; set; }

  public SN_WorldChattingT() {
    this.UID = 0;
    this.NickName = null;
    this.Message = null;
  }
}


}
