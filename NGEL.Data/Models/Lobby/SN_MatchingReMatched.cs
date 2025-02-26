// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Lobby
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct SN_MatchingReMatched : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_23_1_20(); }
  public static SN_MatchingReMatched GetRootAsSN_MatchingReMatched(ByteBuffer _bb) { return GetRootAsSN_MatchingReMatched(_bb, new SN_MatchingReMatched()); }
  public static SN_MatchingReMatched GetRootAsSN_MatchingReMatched(ByteBuffer _bb, SN_MatchingReMatched obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public SN_MatchingReMatched __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public int MatchingTableID { get { int o = __p.__offset(4); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public string MatchTicket { get { int o = __p.__offset(6); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetMatchTicketBytes() { return __p.__vector_as_span<byte>(6, 1); }
#else
  public ArraySegment<byte>? GetMatchTicketBytes() { return __p.__vector_as_arraysegment(6); }
#endif
  public byte[] GetMatchTicketArray() { return __p.__vector_as_array<byte>(6); }

  public static Offset<Lobby.SN_MatchingReMatched> CreateSN_MatchingReMatched(FlatBufferBuilder builder,
      int MatchingTableID = 0,
      StringOffset MatchTicketOffset = default(StringOffset)) {
    builder.StartTable(2);
    SN_MatchingReMatched.AddMatchTicket(builder, MatchTicketOffset);
    SN_MatchingReMatched.AddMatchingTableID(builder, MatchingTableID);
    return SN_MatchingReMatched.EndSN_MatchingReMatched(builder);
  }

  public static void StartSN_MatchingReMatched(FlatBufferBuilder builder) { builder.StartTable(2); }
  public static void AddMatchingTableID(FlatBufferBuilder builder, int MatchingTableID) { builder.AddInt(0, MatchingTableID, 0); }
  public static void AddMatchTicket(FlatBufferBuilder builder, StringOffset MatchTicketOffset) { builder.AddOffset(1, MatchTicketOffset.Value, 0); }
  public static Offset<Lobby.SN_MatchingReMatched> EndSN_MatchingReMatched(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<Lobby.SN_MatchingReMatched>(o);
  }
  public SN_MatchingReMatchedT UnPack() {
    var _o = new SN_MatchingReMatchedT();
    this.UnPackTo(_o);
    return _o;
  }
  public void UnPackTo(SN_MatchingReMatchedT _o) {
    _o.MatchingTableID = this.MatchingTableID;
    _o.MatchTicket = this.MatchTicket;
  }
  public static Offset<Lobby.SN_MatchingReMatched> Pack(FlatBufferBuilder builder, SN_MatchingReMatchedT _o) {
    if (_o == null) return default(Offset<Lobby.SN_MatchingReMatched>);
    var _MatchTicket = _o.MatchTicket == null ? default(StringOffset) : builder.CreateString(_o.MatchTicket);
    return CreateSN_MatchingReMatched(
      builder,
      _o.MatchingTableID,
      _MatchTicket);
  }
}

public class SN_MatchingReMatchedT
{
  public int MatchingTableID { get; set; }
  public string MatchTicket { get; set; }

  public SN_MatchingReMatchedT() {
    this.MatchingTableID = 0;
    this.MatchTicket = null;
  }
}


}
