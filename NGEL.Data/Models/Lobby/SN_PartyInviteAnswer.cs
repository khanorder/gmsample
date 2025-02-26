// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Lobby
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct SN_PartyInviteAnswer : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_23_1_20(); }
  public static SN_PartyInviteAnswer GetRootAsSN_PartyInviteAnswer(ByteBuffer _bb) { return GetRootAsSN_PartyInviteAnswer(_bb, new SN_PartyInviteAnswer()); }
  public static SN_PartyInviteAnswer GetRootAsSN_PartyInviteAnswer(ByteBuffer _bb, SN_PartyInviteAnswer obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public SN_PartyInviteAnswer __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public bool IsAccept { get { int o = __p.__offset(4); return o != 0 ? 0!=__p.bb.Get(o + __p.bb_pos) : (bool)false; } }
  public Lobby.EResultType Reason { get { int o = __p.__offset(6); return o != 0 ? (Lobby.EResultType)__p.bb.GetUshort(o + __p.bb_pos) : Lobby.EResultType.Success; } }
  public string InviteCode { get { int o = __p.__offset(8); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetInviteCodeBytes() { return __p.__vector_as_span<byte>(8, 1); }
#else
  public ArraySegment<byte>? GetInviteCodeBytes() { return __p.__vector_as_arraysegment(8); }
#endif
  public byte[] GetInviteCodeArray() { return __p.__vector_as_array<byte>(8); }
  public int GuestUID { get { int o = __p.__offset(10); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public string GuestNick { get { int o = __p.__offset(12); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetGuestNickBytes() { return __p.__vector_as_span<byte>(12, 1); }
#else
  public ArraySegment<byte>? GetGuestNickBytes() { return __p.__vector_as_arraysegment(12); }
#endif
  public byte[] GetGuestNickArray() { return __p.__vector_as_array<byte>(12); }
  public int GuestEntitlementID { get { int o = __p.__offset(14); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int GuestProfileIconID { get { int o = __p.__offset(16); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int GuestProfileBGID { get { int o = __p.__offset(18); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int GuestDodgeID { get { int o = __p.__offset(20); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }

  public static Offset<Lobby.SN_PartyInviteAnswer> CreateSN_PartyInviteAnswer(FlatBufferBuilder builder,
      bool IsAccept = false,
      Lobby.EResultType Reason = Lobby.EResultType.Success,
      StringOffset InviteCodeOffset = default(StringOffset),
      int GuestUID = 0,
      StringOffset GuestNickOffset = default(StringOffset),
      int GuestEntitlementID = 0,
      int GuestProfileIconID = 0,
      int GuestProfileBGID = 0,
      int GuestDodgeID = 0) {
    builder.StartTable(9);
    SN_PartyInviteAnswer.AddGuestDodgeID(builder, GuestDodgeID);
    SN_PartyInviteAnswer.AddGuestProfileBGID(builder, GuestProfileBGID);
    SN_PartyInviteAnswer.AddGuestProfileIconID(builder, GuestProfileIconID);
    SN_PartyInviteAnswer.AddGuestEntitlementID(builder, GuestEntitlementID);
    SN_PartyInviteAnswer.AddGuestNick(builder, GuestNickOffset);
    SN_PartyInviteAnswer.AddGuestUID(builder, GuestUID);
    SN_PartyInviteAnswer.AddInviteCode(builder, InviteCodeOffset);
    SN_PartyInviteAnswer.AddReason(builder, Reason);
    SN_PartyInviteAnswer.AddIsAccept(builder, IsAccept);
    return SN_PartyInviteAnswer.EndSN_PartyInviteAnswer(builder);
  }

  public static void StartSN_PartyInviteAnswer(FlatBufferBuilder builder) { builder.StartTable(9); }
  public static void AddIsAccept(FlatBufferBuilder builder, bool IsAccept) { builder.AddBool(0, IsAccept, false); }
  public static void AddReason(FlatBufferBuilder builder, Lobby.EResultType Reason) { builder.AddUshort(1, (ushort)Reason, 0); }
  public static void AddInviteCode(FlatBufferBuilder builder, StringOffset InviteCodeOffset) { builder.AddOffset(2, InviteCodeOffset.Value, 0); }
  public static void AddGuestUID(FlatBufferBuilder builder, int GuestUID) { builder.AddInt(3, GuestUID, 0); }
  public static void AddGuestNick(FlatBufferBuilder builder, StringOffset GuestNickOffset) { builder.AddOffset(4, GuestNickOffset.Value, 0); }
  public static void AddGuestEntitlementID(FlatBufferBuilder builder, int GuestEntitlementID) { builder.AddInt(5, GuestEntitlementID, 0); }
  public static void AddGuestProfileIconID(FlatBufferBuilder builder, int GuestProfileIconID) { builder.AddInt(6, GuestProfileIconID, 0); }
  public static void AddGuestProfileBGID(FlatBufferBuilder builder, int GuestProfileBGID) { builder.AddInt(7, GuestProfileBGID, 0); }
  public static void AddGuestDodgeID(FlatBufferBuilder builder, int GuestDodgeID) { builder.AddInt(8, GuestDodgeID, 0); }
  public static Offset<Lobby.SN_PartyInviteAnswer> EndSN_PartyInviteAnswer(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<Lobby.SN_PartyInviteAnswer>(o);
  }
  public SN_PartyInviteAnswerT UnPack() {
    var _o = new SN_PartyInviteAnswerT();
    this.UnPackTo(_o);
    return _o;
  }
  public void UnPackTo(SN_PartyInviteAnswerT _o) {
    _o.IsAccept = this.IsAccept;
    _o.Reason = this.Reason;
    _o.InviteCode = this.InviteCode;
    _o.GuestUID = this.GuestUID;
    _o.GuestNick = this.GuestNick;
    _o.GuestEntitlementID = this.GuestEntitlementID;
    _o.GuestProfileIconID = this.GuestProfileIconID;
    _o.GuestProfileBGID = this.GuestProfileBGID;
    _o.GuestDodgeID = this.GuestDodgeID;
  }
  public static Offset<Lobby.SN_PartyInviteAnswer> Pack(FlatBufferBuilder builder, SN_PartyInviteAnswerT _o) {
    if (_o == null) return default(Offset<Lobby.SN_PartyInviteAnswer>);
    var _InviteCode = _o.InviteCode == null ? default(StringOffset) : builder.CreateString(_o.InviteCode);
    var _GuestNick = _o.GuestNick == null ? default(StringOffset) : builder.CreateString(_o.GuestNick);
    return CreateSN_PartyInviteAnswer(
      builder,
      _o.IsAccept,
      _o.Reason,
      _InviteCode,
      _o.GuestUID,
      _GuestNick,
      _o.GuestEntitlementID,
      _o.GuestProfileIconID,
      _o.GuestProfileBGID,
      _o.GuestDodgeID);
  }
}

public class SN_PartyInviteAnswerT
{
  public bool IsAccept { get; set; }
  public Lobby.EResultType Reason { get; set; }
  public string InviteCode { get; set; }
  public int GuestUID { get; set; }
  public string GuestNick { get; set; }
  public int GuestEntitlementID { get; set; }
  public int GuestProfileIconID { get; set; }
  public int GuestProfileBGID { get; set; }
  public int GuestDodgeID { get; set; }

  public SN_PartyInviteAnswerT() {
    this.IsAccept = false;
    this.Reason = Lobby.EResultType.Success;
    this.InviteCode = null;
    this.GuestUID = 0;
    this.GuestNick = null;
    this.GuestEntitlementID = 0;
    this.GuestProfileIconID = 0;
    this.GuestProfileBGID = 0;
    this.GuestDodgeID = 0;
  }
}


}
