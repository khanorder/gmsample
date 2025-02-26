// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Lobby
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct SN_CustomGameWelcomeNewUser : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_23_1_20(); }
  public static SN_CustomGameWelcomeNewUser GetRootAsSN_CustomGameWelcomeNewUser(ByteBuffer _bb) { return GetRootAsSN_CustomGameWelcomeNewUser(_bb, new SN_CustomGameWelcomeNewUser()); }
  public static SN_CustomGameWelcomeNewUser GetRootAsSN_CustomGameWelcomeNewUser(ByteBuffer _bb, SN_CustomGameWelcomeNewUser obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public SN_CustomGameWelcomeNewUser __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public string RoomID { get { int o = __p.__offset(4); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetRoomIDBytes() { return __p.__vector_as_span<byte>(4, 1); }
#else
  public ArraySegment<byte>? GetRoomIDBytes() { return __p.__vector_as_arraysegment(4); }
#endif
  public byte[] GetRoomIDArray() { return __p.__vector_as_array<byte>(4); }
  public Lobby.CustomGameMemberInfo? NewMember { get { int o = __p.__offset(6); return o != 0 ? (Lobby.CustomGameMemberInfo?)(new Lobby.CustomGameMemberInfo()).__assign(__p.__indirect(o + __p.bb_pos), __p.bb) : null; } }

  public static Offset<Lobby.SN_CustomGameWelcomeNewUser> CreateSN_CustomGameWelcomeNewUser(FlatBufferBuilder builder,
      StringOffset RoomIDOffset = default(StringOffset),
      Offset<Lobby.CustomGameMemberInfo> NewMemberOffset = default(Offset<Lobby.CustomGameMemberInfo>)) {
    builder.StartTable(2);
    SN_CustomGameWelcomeNewUser.AddNewMember(builder, NewMemberOffset);
    SN_CustomGameWelcomeNewUser.AddRoomID(builder, RoomIDOffset);
    return SN_CustomGameWelcomeNewUser.EndSN_CustomGameWelcomeNewUser(builder);
  }

  public static void StartSN_CustomGameWelcomeNewUser(FlatBufferBuilder builder) { builder.StartTable(2); }
  public static void AddRoomID(FlatBufferBuilder builder, StringOffset RoomIDOffset) { builder.AddOffset(0, RoomIDOffset.Value, 0); }
  public static void AddNewMember(FlatBufferBuilder builder, Offset<Lobby.CustomGameMemberInfo> NewMemberOffset) { builder.AddOffset(1, NewMemberOffset.Value, 0); }
  public static Offset<Lobby.SN_CustomGameWelcomeNewUser> EndSN_CustomGameWelcomeNewUser(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<Lobby.SN_CustomGameWelcomeNewUser>(o);
  }
  public SN_CustomGameWelcomeNewUserT UnPack() {
    var _o = new SN_CustomGameWelcomeNewUserT();
    this.UnPackTo(_o);
    return _o;
  }
  public void UnPackTo(SN_CustomGameWelcomeNewUserT _o) {
    _o.RoomID = this.RoomID;
    _o.NewMember = this.NewMember.HasValue ? this.NewMember.Value.UnPack() : null;
  }
  public static Offset<Lobby.SN_CustomGameWelcomeNewUser> Pack(FlatBufferBuilder builder, SN_CustomGameWelcomeNewUserT _o) {
    if (_o == null) return default(Offset<Lobby.SN_CustomGameWelcomeNewUser>);
    var _RoomID = _o.RoomID == null ? default(StringOffset) : builder.CreateString(_o.RoomID);
    var _NewMember = _o.NewMember == null ? default(Offset<Lobby.CustomGameMemberInfo>) : Lobby.CustomGameMemberInfo.Pack(builder, _o.NewMember);
    return CreateSN_CustomGameWelcomeNewUser(
      builder,
      _RoomID,
      _NewMember);
  }
}

public class SN_CustomGameWelcomeNewUserT
{
  public string RoomID { get; set; }
  public Lobby.CustomGameMemberInfoT NewMember { get; set; }

  public SN_CustomGameWelcomeNewUserT() {
    this.RoomID = null;
    this.NewMember = null;
  }
}


}
