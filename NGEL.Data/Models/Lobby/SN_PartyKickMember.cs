// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Lobby
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct SN_PartyKickMember : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_23_1_20(); }
  public static SN_PartyKickMember GetRootAsSN_PartyKickMember(ByteBuffer _bb) { return GetRootAsSN_PartyKickMember(_bb, new SN_PartyKickMember()); }
  public static SN_PartyKickMember GetRootAsSN_PartyKickMember(ByteBuffer _bb, SN_PartyKickMember obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public SN_PartyKickMember __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public string PartyID { get { int o = __p.__offset(4); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetPartyIDBytes() { return __p.__vector_as_span<byte>(4, 1); }
#else
  public ArraySegment<byte>? GetPartyIDBytes() { return __p.__vector_as_arraysegment(4); }
#endif
  public byte[] GetPartyIDArray() { return __p.__vector_as_array<byte>(4); }
  public int KickMemberUID { get { int o = __p.__offset(6); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int MasterUID { get { int o = __p.__offset(8); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public string MasterNick { get { int o = __p.__offset(10); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetMasterNickBytes() { return __p.__vector_as_span<byte>(10, 1); }
#else
  public ArraySegment<byte>? GetMasterNickBytes() { return __p.__vector_as_arraysegment(10); }
#endif
  public byte[] GetMasterNickArray() { return __p.__vector_as_array<byte>(10); }

  public static Offset<Lobby.SN_PartyKickMember> CreateSN_PartyKickMember(FlatBufferBuilder builder,
      StringOffset PartyIDOffset = default(StringOffset),
      int KickMemberUID = 0,
      int MasterUID = 0,
      StringOffset MasterNickOffset = default(StringOffset)) {
    builder.StartTable(4);
    SN_PartyKickMember.AddMasterNick(builder, MasterNickOffset);
    SN_PartyKickMember.AddMasterUID(builder, MasterUID);
    SN_PartyKickMember.AddKickMemberUID(builder, KickMemberUID);
    SN_PartyKickMember.AddPartyID(builder, PartyIDOffset);
    return SN_PartyKickMember.EndSN_PartyKickMember(builder);
  }

  public static void StartSN_PartyKickMember(FlatBufferBuilder builder) { builder.StartTable(4); }
  public static void AddPartyID(FlatBufferBuilder builder, StringOffset PartyIDOffset) { builder.AddOffset(0, PartyIDOffset.Value, 0); }
  public static void AddKickMemberUID(FlatBufferBuilder builder, int KickMemberUID) { builder.AddInt(1, KickMemberUID, 0); }
  public static void AddMasterUID(FlatBufferBuilder builder, int MasterUID) { builder.AddInt(2, MasterUID, 0); }
  public static void AddMasterNick(FlatBufferBuilder builder, StringOffset MasterNickOffset) { builder.AddOffset(3, MasterNickOffset.Value, 0); }
  public static Offset<Lobby.SN_PartyKickMember> EndSN_PartyKickMember(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<Lobby.SN_PartyKickMember>(o);
  }
  public SN_PartyKickMemberT UnPack() {
    var _o = new SN_PartyKickMemberT();
    this.UnPackTo(_o);
    return _o;
  }
  public void UnPackTo(SN_PartyKickMemberT _o) {
    _o.PartyID = this.PartyID;
    _o.KickMemberUID = this.KickMemberUID;
    _o.MasterUID = this.MasterUID;
    _o.MasterNick = this.MasterNick;
  }
  public static Offset<Lobby.SN_PartyKickMember> Pack(FlatBufferBuilder builder, SN_PartyKickMemberT _o) {
    if (_o == null) return default(Offset<Lobby.SN_PartyKickMember>);
    var _PartyID = _o.PartyID == null ? default(StringOffset) : builder.CreateString(_o.PartyID);
    var _MasterNick = _o.MasterNick == null ? default(StringOffset) : builder.CreateString(_o.MasterNick);
    return CreateSN_PartyKickMember(
      builder,
      _PartyID,
      _o.KickMemberUID,
      _o.MasterUID,
      _MasterNick);
  }
}

public class SN_PartyKickMemberT
{
  public string PartyID { get; set; }
  public int KickMemberUID { get; set; }
  public int MasterUID { get; set; }
  public string MasterNick { get; set; }

  public SN_PartyKickMemberT() {
    this.PartyID = null;
    this.KickMemberUID = 0;
    this.MasterUID = 0;
    this.MasterNick = null;
  }
}


}
