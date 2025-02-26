// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Lobby
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct CQ_GetOpenWorldInfoEditor : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_23_1_20(); }
  public static CQ_GetOpenWorldInfoEditor GetRootAsCQ_GetOpenWorldInfoEditor(ByteBuffer _bb) { return GetRootAsCQ_GetOpenWorldInfoEditor(_bb, new CQ_GetOpenWorldInfoEditor()); }
  public static CQ_GetOpenWorldInfoEditor GetRootAsCQ_GetOpenWorldInfoEditor(ByteBuffer _bb, CQ_GetOpenWorldInfoEditor obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public CQ_GetOpenWorldInfoEditor __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public string DedicatedID { get { int o = __p.__offset(4); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetDedicatedIDBytes() { return __p.__vector_as_span<byte>(4, 1); }
#else
  public ArraySegment<byte>? GetDedicatedIDBytes() { return __p.__vector_as_arraysegment(4); }
#endif
  public byte[] GetDedicatedIDArray() { return __p.__vector_as_array<byte>(4); }
  public string ServerUrl { get { int o = __p.__offset(6); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetServerUrlBytes() { return __p.__vector_as_span<byte>(6, 1); }
#else
  public ArraySegment<byte>? GetServerUrlBytes() { return __p.__vector_as_arraysegment(6); }
#endif
  public byte[] GetServerUrlArray() { return __p.__vector_as_array<byte>(6); }

  public static Offset<Lobby.CQ_GetOpenWorldInfoEditor> CreateCQ_GetOpenWorldInfoEditor(FlatBufferBuilder builder,
      StringOffset DedicatedIDOffset = default(StringOffset),
      StringOffset ServerUrlOffset = default(StringOffset)) {
    builder.StartTable(2);
    CQ_GetOpenWorldInfoEditor.AddServerUrl(builder, ServerUrlOffset);
    CQ_GetOpenWorldInfoEditor.AddDedicatedID(builder, DedicatedIDOffset);
    return CQ_GetOpenWorldInfoEditor.EndCQ_GetOpenWorldInfoEditor(builder);
  }

  public static void StartCQ_GetOpenWorldInfoEditor(FlatBufferBuilder builder) { builder.StartTable(2); }
  public static void AddDedicatedID(FlatBufferBuilder builder, StringOffset DedicatedIDOffset) { builder.AddOffset(0, DedicatedIDOffset.Value, 0); }
  public static void AddServerUrl(FlatBufferBuilder builder, StringOffset ServerUrlOffset) { builder.AddOffset(1, ServerUrlOffset.Value, 0); }
  public static Offset<Lobby.CQ_GetOpenWorldInfoEditor> EndCQ_GetOpenWorldInfoEditor(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<Lobby.CQ_GetOpenWorldInfoEditor>(o);
  }
  public CQ_GetOpenWorldInfoEditorT UnPack() {
    var _o = new CQ_GetOpenWorldInfoEditorT();
    this.UnPackTo(_o);
    return _o;
  }
  public void UnPackTo(CQ_GetOpenWorldInfoEditorT _o) {
    _o.DedicatedID = this.DedicatedID;
    _o.ServerUrl = this.ServerUrl;
  }
  public static Offset<Lobby.CQ_GetOpenWorldInfoEditor> Pack(FlatBufferBuilder builder, CQ_GetOpenWorldInfoEditorT _o) {
    if (_o == null) return default(Offset<Lobby.CQ_GetOpenWorldInfoEditor>);
    var _DedicatedID = _o.DedicatedID == null ? default(StringOffset) : builder.CreateString(_o.DedicatedID);
    var _ServerUrl = _o.ServerUrl == null ? default(StringOffset) : builder.CreateString(_o.ServerUrl);
    return CreateCQ_GetOpenWorldInfoEditor(
      builder,
      _DedicatedID,
      _ServerUrl);
  }
}

public class CQ_GetOpenWorldInfoEditorT
{
  public string DedicatedID { get; set; }
  public string ServerUrl { get; set; }

  public CQ_GetOpenWorldInfoEditorT() {
    this.DedicatedID = null;
    this.ServerUrl = null;
  }
}


}
