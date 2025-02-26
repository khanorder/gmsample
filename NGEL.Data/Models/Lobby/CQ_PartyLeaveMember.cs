// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Lobby
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct CQ_PartyLeaveMember : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_23_1_20(); }
  public static CQ_PartyLeaveMember GetRootAsCQ_PartyLeaveMember(ByteBuffer _bb) { return GetRootAsCQ_PartyLeaveMember(_bb, new CQ_PartyLeaveMember()); }
  public static CQ_PartyLeaveMember GetRootAsCQ_PartyLeaveMember(ByteBuffer _bb, CQ_PartyLeaveMember obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public CQ_PartyLeaveMember __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public string PartyID { get { int o = __p.__offset(4); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetPartyIDBytes() { return __p.__vector_as_span<byte>(4, 1); }
#else
  public ArraySegment<byte>? GetPartyIDBytes() { return __p.__vector_as_arraysegment(4); }
#endif
  public byte[] GetPartyIDArray() { return __p.__vector_as_array<byte>(4); }

  public static Offset<Lobby.CQ_PartyLeaveMember> CreateCQ_PartyLeaveMember(FlatBufferBuilder builder,
      StringOffset PartyIDOffset = default(StringOffset)) {
    builder.StartTable(1);
    CQ_PartyLeaveMember.AddPartyID(builder, PartyIDOffset);
    return CQ_PartyLeaveMember.EndCQ_PartyLeaveMember(builder);
  }

  public static void StartCQ_PartyLeaveMember(FlatBufferBuilder builder) { builder.StartTable(1); }
  public static void AddPartyID(FlatBufferBuilder builder, StringOffset PartyIDOffset) { builder.AddOffset(0, PartyIDOffset.Value, 0); }
  public static Offset<Lobby.CQ_PartyLeaveMember> EndCQ_PartyLeaveMember(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<Lobby.CQ_PartyLeaveMember>(o);
  }
  public CQ_PartyLeaveMemberT UnPack() {
    var _o = new CQ_PartyLeaveMemberT();
    this.UnPackTo(_o);
    return _o;
  }
  public void UnPackTo(CQ_PartyLeaveMemberT _o) {
    _o.PartyID = this.PartyID;
  }
  public static Offset<Lobby.CQ_PartyLeaveMember> Pack(FlatBufferBuilder builder, CQ_PartyLeaveMemberT _o) {
    if (_o == null) return default(Offset<Lobby.CQ_PartyLeaveMember>);
    var _PartyID = _o.PartyID == null ? default(StringOffset) : builder.CreateString(_o.PartyID);
    return CreateCQ_PartyLeaveMember(
      builder,
      _PartyID);
  }
}

public class CQ_PartyLeaveMemberT
{
  public string PartyID { get; set; }

  public CQ_PartyLeaveMemberT() {
    this.PartyID = null;
  }
}


}
