// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Lobby
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct SN_MatchingCancelMatch : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_23_1_20(); }
  public static SN_MatchingCancelMatch GetRootAsSN_MatchingCancelMatch(ByteBuffer _bb) { return GetRootAsSN_MatchingCancelMatch(_bb, new SN_MatchingCancelMatch()); }
  public static SN_MatchingCancelMatch GetRootAsSN_MatchingCancelMatch(ByteBuffer _bb, SN_MatchingCancelMatch obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public SN_MatchingCancelMatch __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public string MatchTicket { get { int o = __p.__offset(4); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetMatchTicketBytes() { return __p.__vector_as_span<byte>(4, 1); }
#else
  public ArraySegment<byte>? GetMatchTicketBytes() { return __p.__vector_as_arraysegment(4); }
#endif
  public byte[] GetMatchTicketArray() { return __p.__vector_as_array<byte>(4); }
  public Lobby.EResultType Reason { get { int o = __p.__offset(6); return o != 0 ? (Lobby.EResultType)__p.bb.GetUshort(o + __p.bb_pos) : Lobby.EResultType.Success; } }
  public bool IsCustomGame { get { int o = __p.__offset(8); return o != 0 ? 0!=__p.bb.Get(o + __p.bb_pos) : (bool)false; } }

  public static Offset<Lobby.SN_MatchingCancelMatch> CreateSN_MatchingCancelMatch(FlatBufferBuilder builder,
      StringOffset MatchTicketOffset = default(StringOffset),
      Lobby.EResultType Reason = Lobby.EResultType.Success,
      bool IsCustomGame = false) {
    builder.StartTable(3);
    SN_MatchingCancelMatch.AddMatchTicket(builder, MatchTicketOffset);
    SN_MatchingCancelMatch.AddReason(builder, Reason);
    SN_MatchingCancelMatch.AddIsCustomGame(builder, IsCustomGame);
    return SN_MatchingCancelMatch.EndSN_MatchingCancelMatch(builder);
  }

  public static void StartSN_MatchingCancelMatch(FlatBufferBuilder builder) { builder.StartTable(3); }
  public static void AddMatchTicket(FlatBufferBuilder builder, StringOffset MatchTicketOffset) { builder.AddOffset(0, MatchTicketOffset.Value, 0); }
  public static void AddReason(FlatBufferBuilder builder, Lobby.EResultType Reason) { builder.AddUshort(1, (ushort)Reason, 0); }
  public static void AddIsCustomGame(FlatBufferBuilder builder, bool IsCustomGame) { builder.AddBool(2, IsCustomGame, false); }
  public static Offset<Lobby.SN_MatchingCancelMatch> EndSN_MatchingCancelMatch(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<Lobby.SN_MatchingCancelMatch>(o);
  }
  public SN_MatchingCancelMatchT UnPack() {
    var _o = new SN_MatchingCancelMatchT();
    this.UnPackTo(_o);
    return _o;
  }
  public void UnPackTo(SN_MatchingCancelMatchT _o) {
    _o.MatchTicket = this.MatchTicket;
    _o.Reason = this.Reason;
    _o.IsCustomGame = this.IsCustomGame;
  }
  public static Offset<Lobby.SN_MatchingCancelMatch> Pack(FlatBufferBuilder builder, SN_MatchingCancelMatchT _o) {
    if (_o == null) return default(Offset<Lobby.SN_MatchingCancelMatch>);
    var _MatchTicket = _o.MatchTicket == null ? default(StringOffset) : builder.CreateString(_o.MatchTicket);
    return CreateSN_MatchingCancelMatch(
      builder,
      _MatchTicket,
      _o.Reason,
      _o.IsCustomGame);
  }
}

public class SN_MatchingCancelMatchT
{
  public string MatchTicket { get; set; }
  public Lobby.EResultType Reason { get; set; }
  public bool IsCustomGame { get; set; }

  public SN_MatchingCancelMatchT() {
    this.MatchTicket = null;
    this.Reason = Lobby.EResultType.Success;
    this.IsCustomGame = false;
  }
}


}
