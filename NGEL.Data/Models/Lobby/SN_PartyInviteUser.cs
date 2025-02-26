// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Lobby
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct SN_PartyInviteUser : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_23_1_20(); }
  public static SN_PartyInviteUser GetRootAsSN_PartyInviteUser(ByteBuffer _bb) { return GetRootAsSN_PartyInviteUser(_bb, new SN_PartyInviteUser()); }
  public static SN_PartyInviteUser GetRootAsSN_PartyInviteUser(ByteBuffer _bb, SN_PartyInviteUser obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public SN_PartyInviteUser __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public int MatchingTableID { get { int o = __p.__offset(4); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public string InviteCode { get { int o = __p.__offset(6); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetInviteCodeBytes() { return __p.__vector_as_span<byte>(6, 1); }
#else
  public ArraySegment<byte>? GetInviteCodeBytes() { return __p.__vector_as_arraysegment(6); }
#endif
  public byte[] GetInviteCodeArray() { return __p.__vector_as_array<byte>(6); }
  public int ExpireAt { get { int o = __p.__offset(8); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int MasterUID { get { int o = __p.__offset(10); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public string MasterNick { get { int o = __p.__offset(12); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetMasterNickBytes() { return __p.__vector_as_span<byte>(12, 1); }
#else
  public ArraySegment<byte>? GetMasterNickBytes() { return __p.__vector_as_arraysegment(12); }
#endif
  public byte[] GetMasterNickArray() { return __p.__vector_as_array<byte>(12); }
  public string GuestNick { get { int o = __p.__offset(14); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetGuestNickBytes() { return __p.__vector_as_span<byte>(14, 1); }
#else
  public ArraySegment<byte>? GetGuestNickBytes() { return __p.__vector_as_arraysegment(14); }
#endif
  public byte[] GetGuestNickArray() { return __p.__vector_as_array<byte>(14); }
  public string PartyID { get { int o = __p.__offset(16); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetPartyIDBytes() { return __p.__vector_as_span<byte>(16, 1); }
#else
  public ArraySegment<byte>? GetPartyIDBytes() { return __p.__vector_as_arraysegment(16); }
#endif
  public byte[] GetPartyIDArray() { return __p.__vector_as_array<byte>(16); }

  public static Offset<Lobby.SN_PartyInviteUser> CreateSN_PartyInviteUser(FlatBufferBuilder builder,
      int MatchingTableID = 0,
      StringOffset InviteCodeOffset = default(StringOffset),
      int ExpireAt = 0,
      int MasterUID = 0,
      StringOffset MasterNickOffset = default(StringOffset),
      StringOffset GuestNickOffset = default(StringOffset),
      StringOffset PartyIDOffset = default(StringOffset)) {
    builder.StartTable(7);
    SN_PartyInviteUser.AddPartyID(builder, PartyIDOffset);
    SN_PartyInviteUser.AddGuestNick(builder, GuestNickOffset);
    SN_PartyInviteUser.AddMasterNick(builder, MasterNickOffset);
    SN_PartyInviteUser.AddMasterUID(builder, MasterUID);
    SN_PartyInviteUser.AddExpireAt(builder, ExpireAt);
    SN_PartyInviteUser.AddInviteCode(builder, InviteCodeOffset);
    SN_PartyInviteUser.AddMatchingTableID(builder, MatchingTableID);
    return SN_PartyInviteUser.EndSN_PartyInviteUser(builder);
  }

  public static void StartSN_PartyInviteUser(FlatBufferBuilder builder) { builder.StartTable(7); }
  public static void AddMatchingTableID(FlatBufferBuilder builder, int MatchingTableID) { builder.AddInt(0, MatchingTableID, 0); }
  public static void AddInviteCode(FlatBufferBuilder builder, StringOffset InviteCodeOffset) { builder.AddOffset(1, InviteCodeOffset.Value, 0); }
  public static void AddExpireAt(FlatBufferBuilder builder, int ExpireAt) { builder.AddInt(2, ExpireAt, 0); }
  public static void AddMasterUID(FlatBufferBuilder builder, int MasterUID) { builder.AddInt(3, MasterUID, 0); }
  public static void AddMasterNick(FlatBufferBuilder builder, StringOffset MasterNickOffset) { builder.AddOffset(4, MasterNickOffset.Value, 0); }
  public static void AddGuestNick(FlatBufferBuilder builder, StringOffset GuestNickOffset) { builder.AddOffset(5, GuestNickOffset.Value, 0); }
  public static void AddPartyID(FlatBufferBuilder builder, StringOffset PartyIDOffset) { builder.AddOffset(6, PartyIDOffset.Value, 0); }
  public static Offset<Lobby.SN_PartyInviteUser> EndSN_PartyInviteUser(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<Lobby.SN_PartyInviteUser>(o);
  }
  public SN_PartyInviteUserT UnPack() {
    var _o = new SN_PartyInviteUserT();
    this.UnPackTo(_o);
    return _o;
  }
  public void UnPackTo(SN_PartyInviteUserT _o) {
    _o.MatchingTableID = this.MatchingTableID;
    _o.InviteCode = this.InviteCode;
    _o.ExpireAt = this.ExpireAt;
    _o.MasterUID = this.MasterUID;
    _o.MasterNick = this.MasterNick;
    _o.GuestNick = this.GuestNick;
    _o.PartyID = this.PartyID;
  }
  public static Offset<Lobby.SN_PartyInviteUser> Pack(FlatBufferBuilder builder, SN_PartyInviteUserT _o) {
    if (_o == null) return default(Offset<Lobby.SN_PartyInviteUser>);
    var _InviteCode = _o.InviteCode == null ? default(StringOffset) : builder.CreateString(_o.InviteCode);
    var _MasterNick = _o.MasterNick == null ? default(StringOffset) : builder.CreateString(_o.MasterNick);
    var _GuestNick = _o.GuestNick == null ? default(StringOffset) : builder.CreateString(_o.GuestNick);
    var _PartyID = _o.PartyID == null ? default(StringOffset) : builder.CreateString(_o.PartyID);
    return CreateSN_PartyInviteUser(
      builder,
      _o.MatchingTableID,
      _InviteCode,
      _o.ExpireAt,
      _o.MasterUID,
      _MasterNick,
      _GuestNick,
      _PartyID);
  }
}

public class SN_PartyInviteUserT
{
  public int MatchingTableID { get; set; }
  public string InviteCode { get; set; }
  public int ExpireAt { get; set; }
  public int MasterUID { get; set; }
  public string MasterNick { get; set; }
  public string GuestNick { get; set; }
  public string PartyID { get; set; }

  public SN_PartyInviteUserT() {
    this.MatchingTableID = 0;
    this.InviteCode = null;
    this.ExpireAt = 0;
    this.MasterUID = 0;
    this.MasterNick = null;
    this.GuestNick = null;
    this.PartyID = null;
  }
}


}
