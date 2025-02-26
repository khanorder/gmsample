// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Lobby
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct SN_CustomGameFollowMaster : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_23_1_20(); }
  public static SN_CustomGameFollowMaster GetRootAsSN_CustomGameFollowMaster(ByteBuffer _bb) { return GetRootAsSN_CustomGameFollowMaster(_bb, new SN_CustomGameFollowMaster()); }
  public static SN_CustomGameFollowMaster GetRootAsSN_CustomGameFollowMaster(ByteBuffer _bb, SN_CustomGameFollowMaster obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public SN_CustomGameFollowMaster __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public int GuestUID { get { int o = __p.__offset(4); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public string GuestNick { get { int o = __p.__offset(6); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetGuestNickBytes() { return __p.__vector_as_span<byte>(6, 1); }
#else
  public ArraySegment<byte>? GetGuestNickBytes() { return __p.__vector_as_arraysegment(6); }
#endif
  public byte[] GetGuestNickArray() { return __p.__vector_as_array<byte>(6); }
  public int GuestEntitlementID { get { int o = __p.__offset(8); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int GuestProfileIconID { get { int o = __p.__offset(10); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int GuestProfileBGID { get { int o = __p.__offset(12); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }

  public static Offset<Lobby.SN_CustomGameFollowMaster> CreateSN_CustomGameFollowMaster(FlatBufferBuilder builder,
      int GuestUID = 0,
      StringOffset GuestNickOffset = default(StringOffset),
      int GuestEntitlementID = 0,
      int GuestProfileIconID = 0,
      int GuestProfileBGID = 0) {
    builder.StartTable(5);
    SN_CustomGameFollowMaster.AddGuestProfileBGID(builder, GuestProfileBGID);
    SN_CustomGameFollowMaster.AddGuestProfileIconID(builder, GuestProfileIconID);
    SN_CustomGameFollowMaster.AddGuestEntitlementID(builder, GuestEntitlementID);
    SN_CustomGameFollowMaster.AddGuestNick(builder, GuestNickOffset);
    SN_CustomGameFollowMaster.AddGuestUID(builder, GuestUID);
    return SN_CustomGameFollowMaster.EndSN_CustomGameFollowMaster(builder);
  }

  public static void StartSN_CustomGameFollowMaster(FlatBufferBuilder builder) { builder.StartTable(5); }
  public static void AddGuestUID(FlatBufferBuilder builder, int GuestUID) { builder.AddInt(0, GuestUID, 0); }
  public static void AddGuestNick(FlatBufferBuilder builder, StringOffset GuestNickOffset) { builder.AddOffset(1, GuestNickOffset.Value, 0); }
  public static void AddGuestEntitlementID(FlatBufferBuilder builder, int GuestEntitlementID) { builder.AddInt(2, GuestEntitlementID, 0); }
  public static void AddGuestProfileIconID(FlatBufferBuilder builder, int GuestProfileIconID) { builder.AddInt(3, GuestProfileIconID, 0); }
  public static void AddGuestProfileBGID(FlatBufferBuilder builder, int GuestProfileBGID) { builder.AddInt(4, GuestProfileBGID, 0); }
  public static Offset<Lobby.SN_CustomGameFollowMaster> EndSN_CustomGameFollowMaster(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<Lobby.SN_CustomGameFollowMaster>(o);
  }
  public SN_CustomGameFollowMasterT UnPack() {
    var _o = new SN_CustomGameFollowMasterT();
    this.UnPackTo(_o);
    return _o;
  }
  public void UnPackTo(SN_CustomGameFollowMasterT _o) {
    _o.GuestUID = this.GuestUID;
    _o.GuestNick = this.GuestNick;
    _o.GuestEntitlementID = this.GuestEntitlementID;
    _o.GuestProfileIconID = this.GuestProfileIconID;
    _o.GuestProfileBGID = this.GuestProfileBGID;
  }
  public static Offset<Lobby.SN_CustomGameFollowMaster> Pack(FlatBufferBuilder builder, SN_CustomGameFollowMasterT _o) {
    if (_o == null) return default(Offset<Lobby.SN_CustomGameFollowMaster>);
    var _GuestNick = _o.GuestNick == null ? default(StringOffset) : builder.CreateString(_o.GuestNick);
    return CreateSN_CustomGameFollowMaster(
      builder,
      _o.GuestUID,
      _GuestNick,
      _o.GuestEntitlementID,
      _o.GuestProfileIconID,
      _o.GuestProfileBGID);
  }
}

public class SN_CustomGameFollowMasterT
{
  public int GuestUID { get; set; }
  public string GuestNick { get; set; }
  public int GuestEntitlementID { get; set; }
  public int GuestProfileIconID { get; set; }
  public int GuestProfileBGID { get; set; }

  public SN_CustomGameFollowMasterT() {
    this.GuestUID = 0;
    this.GuestNick = null;
    this.GuestEntitlementID = 0;
    this.GuestProfileIconID = 0;
    this.GuestProfileBGID = 0;
  }
}


}
