// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Lobby
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct SN_UncheaterCookie : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_23_1_20(); }
  public static SN_UncheaterCookie GetRootAsSN_UncheaterCookie(ByteBuffer _bb) { return GetRootAsSN_UncheaterCookie(_bb, new SN_UncheaterCookie()); }
  public static SN_UncheaterCookie GetRootAsSN_UncheaterCookie(ByteBuffer _bb, SN_UncheaterCookie obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public SN_UncheaterCookie __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public string Buffer { get { int o = __p.__offset(4); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetBufferBytes() { return __p.__vector_as_span<byte>(4, 1); }
#else
  public ArraySegment<byte>? GetBufferBytes() { return __p.__vector_as_arraysegment(4); }
#endif
  public byte[] GetBufferArray() { return __p.__vector_as_array<byte>(4); }

  public static Offset<Lobby.SN_UncheaterCookie> CreateSN_UncheaterCookie(FlatBufferBuilder builder,
      StringOffset bufferOffset = default(StringOffset)) {
    builder.StartTable(1);
    SN_UncheaterCookie.AddBuffer(builder, bufferOffset);
    return SN_UncheaterCookie.EndSN_UncheaterCookie(builder);
  }

  public static void StartSN_UncheaterCookie(FlatBufferBuilder builder) { builder.StartTable(1); }
  public static void AddBuffer(FlatBufferBuilder builder, StringOffset bufferOffset) { builder.AddOffset(0, bufferOffset.Value, 0); }
  public static Offset<Lobby.SN_UncheaterCookie> EndSN_UncheaterCookie(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<Lobby.SN_UncheaterCookie>(o);
  }
  public SN_UncheaterCookieT UnPack() {
    var _o = new SN_UncheaterCookieT();
    this.UnPackTo(_o);
    return _o;
  }
  public void UnPackTo(SN_UncheaterCookieT _o) {
    _o.Buffer = this.Buffer;
  }
  public static Offset<Lobby.SN_UncheaterCookie> Pack(FlatBufferBuilder builder, SN_UncheaterCookieT _o) {
    if (_o == null) return default(Offset<Lobby.SN_UncheaterCookie>);
    var _buffer = _o.Buffer == null ? default(StringOffset) : builder.CreateString(_o.Buffer);
    return CreateSN_UncheaterCookie(
      builder,
      _buffer);
  }
}

public class SN_UncheaterCookieT
{
  public string Buffer { get; set; }

  public SN_UncheaterCookieT() {
    this.Buffer = null;
  }
}


}
