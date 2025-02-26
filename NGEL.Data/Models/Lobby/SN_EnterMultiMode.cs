// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Lobby
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct SN_EnterMultiMode : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_23_1_20(); }
  public static SN_EnterMultiMode GetRootAsSN_EnterMultiMode(ByteBuffer _bb) { return GetRootAsSN_EnterMultiMode(_bb, new SN_EnterMultiMode()); }
  public static SN_EnterMultiMode GetRootAsSN_EnterMultiMode(ByteBuffer _bb, SN_EnterMultiMode obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public SN_EnterMultiMode __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public Lobby.EContentsType ContentsType { get { int o = __p.__offset(4); return o != 0 ? (Lobby.EContentsType)__p.bb.Get(o + __p.bb_pos) : Lobby.EContentsType.None; } }
  public string TeamID { get { int o = __p.__offset(6); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetTeamIDBytes() { return __p.__vector_as_span<byte>(6, 1); }
#else
  public ArraySegment<byte>? GetTeamIDBytes() { return __p.__vector_as_arraysegment(6); }
#endif
  public byte[] GetTeamIDArray() { return __p.__vector_as_array<byte>(6); }
  public string ServerUrl { get { int o = __p.__offset(8); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetServerUrlBytes() { return __p.__vector_as_span<byte>(8, 1); }
#else
  public ArraySegment<byte>? GetServerUrlBytes() { return __p.__vector_as_arraysegment(8); }
#endif
  public byte[] GetServerUrlArray() { return __p.__vector_as_array<byte>(8); }
  public string PlayerSessionID { get { int o = __p.__offset(10); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetPlayerSessionIDBytes() { return __p.__vector_as_span<byte>(10, 1); }
#else
  public ArraySegment<byte>? GetPlayerSessionIDBytes() { return __p.__vector_as_arraysegment(10); }
#endif
  public byte[] GetPlayerSessionIDArray() { return __p.__vector_as_array<byte>(10); }
  public string DedicatedID { get { int o = __p.__offset(12); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetDedicatedIDBytes() { return __p.__vector_as_span<byte>(12, 1); }
#else
  public ArraySegment<byte>? GetDedicatedIDBytes() { return __p.__vector_as_arraysegment(12); }
#endif
  public byte[] GetDedicatedIDArray() { return __p.__vector_as_array<byte>(12); }
  public string FriendUserName { get { int o = __p.__offset(14); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetFriendUserNameBytes() { return __p.__vector_as_span<byte>(14, 1); }
#else
  public ArraySegment<byte>? GetFriendUserNameBytes() { return __p.__vector_as_arraysegment(14); }
#endif
  public byte[] GetFriendUserNameArray() { return __p.__vector_as_array<byte>(14); }
  public string MatchID { get { int o = __p.__offset(16); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetMatchIDBytes() { return __p.__vector_as_span<byte>(16, 1); }
#else
  public ArraySegment<byte>? GetMatchIDBytes() { return __p.__vector_as_arraysegment(16); }
#endif
  public byte[] GetMatchIDArray() { return __p.__vector_as_array<byte>(16); }
  public bool IsCustomGame { get { int o = __p.__offset(18); return o != 0 ? 0!=__p.bb.Get(o + __p.bb_pos) : (bool)false; } }

  public static Offset<Lobby.SN_EnterMultiMode> CreateSN_EnterMultiMode(FlatBufferBuilder builder,
      Lobby.EContentsType ContentsType = Lobby.EContentsType.None,
      StringOffset TeamIDOffset = default(StringOffset),
      StringOffset ServerUrlOffset = default(StringOffset),
      StringOffset PlayerSessionIDOffset = default(StringOffset),
      StringOffset DedicatedIDOffset = default(StringOffset),
      StringOffset FriendUserNameOffset = default(StringOffset),
      StringOffset MatchIDOffset = default(StringOffset),
      bool IsCustomGame = false) {
    builder.StartTable(8);
    SN_EnterMultiMode.AddMatchID(builder, MatchIDOffset);
    SN_EnterMultiMode.AddFriendUserName(builder, FriendUserNameOffset);
    SN_EnterMultiMode.AddDedicatedID(builder, DedicatedIDOffset);
    SN_EnterMultiMode.AddPlayerSessionID(builder, PlayerSessionIDOffset);
    SN_EnterMultiMode.AddServerUrl(builder, ServerUrlOffset);
    SN_EnterMultiMode.AddTeamID(builder, TeamIDOffset);
    SN_EnterMultiMode.AddIsCustomGame(builder, IsCustomGame);
    SN_EnterMultiMode.AddContentsType(builder, ContentsType);
    return SN_EnterMultiMode.EndSN_EnterMultiMode(builder);
  }

  public static void StartSN_EnterMultiMode(FlatBufferBuilder builder) { builder.StartTable(8); }
  public static void AddContentsType(FlatBufferBuilder builder, Lobby.EContentsType ContentsType) { builder.AddByte(0, (byte)ContentsType, 0); }
  public static void AddTeamID(FlatBufferBuilder builder, StringOffset TeamIDOffset) { builder.AddOffset(1, TeamIDOffset.Value, 0); }
  public static void AddServerUrl(FlatBufferBuilder builder, StringOffset ServerUrlOffset) { builder.AddOffset(2, ServerUrlOffset.Value, 0); }
  public static void AddPlayerSessionID(FlatBufferBuilder builder, StringOffset PlayerSessionIDOffset) { builder.AddOffset(3, PlayerSessionIDOffset.Value, 0); }
  public static void AddDedicatedID(FlatBufferBuilder builder, StringOffset DedicatedIDOffset) { builder.AddOffset(4, DedicatedIDOffset.Value, 0); }
  public static void AddFriendUserName(FlatBufferBuilder builder, StringOffset FriendUserNameOffset) { builder.AddOffset(5, FriendUserNameOffset.Value, 0); }
  public static void AddMatchID(FlatBufferBuilder builder, StringOffset MatchIDOffset) { builder.AddOffset(6, MatchIDOffset.Value, 0); }
  public static void AddIsCustomGame(FlatBufferBuilder builder, bool IsCustomGame) { builder.AddBool(7, IsCustomGame, false); }
  public static Offset<Lobby.SN_EnterMultiMode> EndSN_EnterMultiMode(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<Lobby.SN_EnterMultiMode>(o);
  }
  public SN_EnterMultiModeT UnPack() {
    var _o = new SN_EnterMultiModeT();
    this.UnPackTo(_o);
    return _o;
  }
  public void UnPackTo(SN_EnterMultiModeT _o) {
    _o.ContentsType = this.ContentsType;
    _o.TeamID = this.TeamID;
    _o.ServerUrl = this.ServerUrl;
    _o.PlayerSessionID = this.PlayerSessionID;
    _o.DedicatedID = this.DedicatedID;
    _o.FriendUserName = this.FriendUserName;
    _o.MatchID = this.MatchID;
    _o.IsCustomGame = this.IsCustomGame;
  }
  public static Offset<Lobby.SN_EnterMultiMode> Pack(FlatBufferBuilder builder, SN_EnterMultiModeT _o) {
    if (_o == null) return default(Offset<Lobby.SN_EnterMultiMode>);
    var _TeamID = _o.TeamID == null ? default(StringOffset) : builder.CreateString(_o.TeamID);
    var _ServerUrl = _o.ServerUrl == null ? default(StringOffset) : builder.CreateString(_o.ServerUrl);
    var _PlayerSessionID = _o.PlayerSessionID == null ? default(StringOffset) : builder.CreateString(_o.PlayerSessionID);
    var _DedicatedID = _o.DedicatedID == null ? default(StringOffset) : builder.CreateString(_o.DedicatedID);
    var _FriendUserName = _o.FriendUserName == null ? default(StringOffset) : builder.CreateString(_o.FriendUserName);
    var _MatchID = _o.MatchID == null ? default(StringOffset) : builder.CreateString(_o.MatchID);
    return CreateSN_EnterMultiMode(
      builder,
      _o.ContentsType,
      _TeamID,
      _ServerUrl,
      _PlayerSessionID,
      _DedicatedID,
      _FriendUserName,
      _MatchID,
      _o.IsCustomGame);
  }
}

public class SN_EnterMultiModeT
{
  public Lobby.EContentsType ContentsType { get; set; }
  public string TeamID { get; set; }
  public string ServerUrl { get; set; }
  public string PlayerSessionID { get; set; }
  public string DedicatedID { get; set; }
  public string FriendUserName { get; set; }
  public string MatchID { get; set; }
  public bool IsCustomGame { get; set; }

  public SN_EnterMultiModeT() {
    this.ContentsType = Lobby.EContentsType.None;
    this.TeamID = null;
    this.ServerUrl = null;
    this.PlayerSessionID = null;
    this.DedicatedID = null;
    this.FriendUserName = null;
    this.MatchID = null;
    this.IsCustomGame = false;
  }
}


}
