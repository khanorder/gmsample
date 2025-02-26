// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Lobby
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct LobbyToLobby_MatchingWaitConfirm : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_23_1_20(); }
  public static LobbyToLobby_MatchingWaitConfirm GetRootAsLobbyToLobby_MatchingWaitConfirm(ByteBuffer _bb) { return GetRootAsLobbyToLobby_MatchingWaitConfirm(_bb, new LobbyToLobby_MatchingWaitConfirm()); }
  public static LobbyToLobby_MatchingWaitConfirm GetRootAsLobbyToLobby_MatchingWaitConfirm(ByteBuffer _bb, LobbyToLobby_MatchingWaitConfirm obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public LobbyToLobby_MatchingWaitConfirm __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public string MatchID { get { int o = __p.__offset(4); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetMatchIDBytes() { return __p.__vector_as_span<byte>(4, 1); }
#else
  public ArraySegment<byte>? GetMatchIDBytes() { return __p.__vector_as_arraysegment(4); }
#endif
  public byte[] GetMatchIDArray() { return __p.__vector_as_array<byte>(4); }
  public Lobby.MatchingTeamInfo? TeamList(int j) { int o = __p.__offset(6); return o != 0 ? (Lobby.MatchingTeamInfo?)(new Lobby.MatchingTeamInfo()).__assign(__p.__indirect(__p.__vector(o) + j * 4), __p.bb) : null; }
  public int TeamListLength { get { int o = __p.__offset(6); return o != 0 ? __p.__vector_len(o) : 0; } }
  public string TeamID { get { int o = __p.__offset(8); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetTeamIDBytes() { return __p.__vector_as_span<byte>(8, 1); }
#else
  public ArraySegment<byte>? GetTeamIDBytes() { return __p.__vector_as_arraysegment(8); }
#endif
  public byte[] GetTeamIDArray() { return __p.__vector_as_array<byte>(8); }
  public string DedicatedID { get { int o = __p.__offset(10); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetDedicatedIDBytes() { return __p.__vector_as_span<byte>(10, 1); }
#else
  public ArraySegment<byte>? GetDedicatedIDBytes() { return __p.__vector_as_arraysegment(10); }
#endif
  public byte[] GetDedicatedIDArray() { return __p.__vector_as_array<byte>(10); }
  public string ServerUrl { get { int o = __p.__offset(12); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetServerUrlBytes() { return __p.__vector_as_span<byte>(12, 1); }
#else
  public ArraySegment<byte>? GetServerUrlBytes() { return __p.__vector_as_arraysegment(12); }
#endif
  public byte[] GetServerUrlArray() { return __p.__vector_as_array<byte>(12); }
  public string OTP { get { int o = __p.__offset(14); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetOTPBytes() { return __p.__vector_as_span<byte>(14, 1); }
#else
  public ArraySegment<byte>? GetOTPBytes() { return __p.__vector_as_arraysegment(14); }
#endif
  public byte[] GetOTPArray() { return __p.__vector_as_array<byte>(14); }
  public string GameSessionID { get { int o = __p.__offset(16); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetGameSessionIDBytes() { return __p.__vector_as_span<byte>(16, 1); }
#else
  public ArraySegment<byte>? GetGameSessionIDBytes() { return __p.__vector_as_arraysegment(16); }
#endif
  public byte[] GetGameSessionIDArray() { return __p.__vector_as_array<byte>(16); }
  public bool HasAI { get { int o = __p.__offset(18); return o != 0 ? 0!=__p.bb.Get(o + __p.bb_pos) : (bool)false; } }
  public int MatchingTableID { get { int o = __p.__offset(20); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }

  public static Offset<Lobby.LobbyToLobby_MatchingWaitConfirm> CreateLobbyToLobby_MatchingWaitConfirm(FlatBufferBuilder builder,
      StringOffset MatchIDOffset = default(StringOffset),
      VectorOffset TeamListOffset = default(VectorOffset),
      StringOffset TeamIDOffset = default(StringOffset),
      StringOffset DedicatedIDOffset = default(StringOffset),
      StringOffset ServerUrlOffset = default(StringOffset),
      StringOffset OTPOffset = default(StringOffset),
      StringOffset GameSessionIDOffset = default(StringOffset),
      bool HasAI = false,
      int MatchingTableID = 0) {
    builder.StartTable(9);
    LobbyToLobby_MatchingWaitConfirm.AddMatchingTableID(builder, MatchingTableID);
    LobbyToLobby_MatchingWaitConfirm.AddGameSessionID(builder, GameSessionIDOffset);
    LobbyToLobby_MatchingWaitConfirm.AddOTP(builder, OTPOffset);
    LobbyToLobby_MatchingWaitConfirm.AddServerUrl(builder, ServerUrlOffset);
    LobbyToLobby_MatchingWaitConfirm.AddDedicatedID(builder, DedicatedIDOffset);
    LobbyToLobby_MatchingWaitConfirm.AddTeamID(builder, TeamIDOffset);
    LobbyToLobby_MatchingWaitConfirm.AddTeamList(builder, TeamListOffset);
    LobbyToLobby_MatchingWaitConfirm.AddMatchID(builder, MatchIDOffset);
    LobbyToLobby_MatchingWaitConfirm.AddHasAI(builder, HasAI);
    return LobbyToLobby_MatchingWaitConfirm.EndLobbyToLobby_MatchingWaitConfirm(builder);
  }

  public static void StartLobbyToLobby_MatchingWaitConfirm(FlatBufferBuilder builder) { builder.StartTable(9); }
  public static void AddMatchID(FlatBufferBuilder builder, StringOffset MatchIDOffset) { builder.AddOffset(0, MatchIDOffset.Value, 0); }
  public static void AddTeamList(FlatBufferBuilder builder, VectorOffset TeamListOffset) { builder.AddOffset(1, TeamListOffset.Value, 0); }
  public static VectorOffset CreateTeamListVector(FlatBufferBuilder builder, Offset<Lobby.MatchingTeamInfo>[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddOffset(data[i].Value); return builder.EndVector(); }
  public static VectorOffset CreateTeamListVectorBlock(FlatBufferBuilder builder, Offset<Lobby.MatchingTeamInfo>[] data) { builder.StartVector(4, data.Length, 4); builder.Add(data); return builder.EndVector(); }
  public static VectorOffset CreateTeamListVectorBlock(FlatBufferBuilder builder, ArraySegment<Offset<Lobby.MatchingTeamInfo>> data) { builder.StartVector(4, data.Count, 4); builder.Add(data); return builder.EndVector(); }
  public static VectorOffset CreateTeamListVectorBlock(FlatBufferBuilder builder, IntPtr dataPtr, int sizeInBytes) { builder.StartVector(1, sizeInBytes, 1); builder.Add<Offset<Lobby.MatchingTeamInfo>>(dataPtr, sizeInBytes); return builder.EndVector(); }
  public static void StartTeamListVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static void AddTeamID(FlatBufferBuilder builder, StringOffset TeamIDOffset) { builder.AddOffset(2, TeamIDOffset.Value, 0); }
  public static void AddDedicatedID(FlatBufferBuilder builder, StringOffset DedicatedIDOffset) { builder.AddOffset(3, DedicatedIDOffset.Value, 0); }
  public static void AddServerUrl(FlatBufferBuilder builder, StringOffset ServerUrlOffset) { builder.AddOffset(4, ServerUrlOffset.Value, 0); }
  public static void AddOTP(FlatBufferBuilder builder, StringOffset OTPOffset) { builder.AddOffset(5, OTPOffset.Value, 0); }
  public static void AddGameSessionID(FlatBufferBuilder builder, StringOffset GameSessionIDOffset) { builder.AddOffset(6, GameSessionIDOffset.Value, 0); }
  public static void AddHasAI(FlatBufferBuilder builder, bool HasAI) { builder.AddBool(7, HasAI, false); }
  public static void AddMatchingTableID(FlatBufferBuilder builder, int MatchingTableID) { builder.AddInt(8, MatchingTableID, 0); }
  public static Offset<Lobby.LobbyToLobby_MatchingWaitConfirm> EndLobbyToLobby_MatchingWaitConfirm(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<Lobby.LobbyToLobby_MatchingWaitConfirm>(o);
  }
  public LobbyToLobby_MatchingWaitConfirmT UnPack() {
    var _o = new LobbyToLobby_MatchingWaitConfirmT();
    this.UnPackTo(_o);
    return _o;
  }
  public void UnPackTo(LobbyToLobby_MatchingWaitConfirmT _o) {
    _o.MatchID = this.MatchID;
    _o.TeamList = new List<Lobby.MatchingTeamInfoT>();
    for (var _j = 0; _j < this.TeamListLength; ++_j) {_o.TeamList.Add(this.TeamList(_j).HasValue ? this.TeamList(_j).Value.UnPack() : null);}
    _o.TeamID = this.TeamID;
    _o.DedicatedID = this.DedicatedID;
    _o.ServerUrl = this.ServerUrl;
    _o.OTP = this.OTP;
    _o.GameSessionID = this.GameSessionID;
    _o.HasAI = this.HasAI;
    _o.MatchingTableID = this.MatchingTableID;
  }
  public static Offset<Lobby.LobbyToLobby_MatchingWaitConfirm> Pack(FlatBufferBuilder builder, LobbyToLobby_MatchingWaitConfirmT _o) {
    if (_o == null) return default(Offset<Lobby.LobbyToLobby_MatchingWaitConfirm>);
    var _MatchID = _o.MatchID == null ? default(StringOffset) : builder.CreateString(_o.MatchID);
    var _TeamList = default(VectorOffset);
    if (_o.TeamList != null) {
      var __TeamList = new Offset<Lobby.MatchingTeamInfo>[_o.TeamList.Count];
      for (var _j = 0; _j < __TeamList.Length; ++_j) { __TeamList[_j] = Lobby.MatchingTeamInfo.Pack(builder, _o.TeamList[_j]); }
      _TeamList = CreateTeamListVector(builder, __TeamList);
    }
    var _TeamID = _o.TeamID == null ? default(StringOffset) : builder.CreateString(_o.TeamID);
    var _DedicatedID = _o.DedicatedID == null ? default(StringOffset) : builder.CreateString(_o.DedicatedID);
    var _ServerUrl = _o.ServerUrl == null ? default(StringOffset) : builder.CreateString(_o.ServerUrl);
    var _OTP = _o.OTP == null ? default(StringOffset) : builder.CreateString(_o.OTP);
    var _GameSessionID = _o.GameSessionID == null ? default(StringOffset) : builder.CreateString(_o.GameSessionID);
    return CreateLobbyToLobby_MatchingWaitConfirm(
      builder,
      _MatchID,
      _TeamList,
      _TeamID,
      _DedicatedID,
      _ServerUrl,
      _OTP,
      _GameSessionID,
      _o.HasAI,
      _o.MatchingTableID);
  }
}

public class LobbyToLobby_MatchingWaitConfirmT
{
  public string MatchID { get; set; }
  public List<Lobby.MatchingTeamInfoT> TeamList { get; set; }
  public string TeamID { get; set; }
  public string DedicatedID { get; set; }
  public string ServerUrl { get; set; }
  public string OTP { get; set; }
  public string GameSessionID { get; set; }
  public bool HasAI { get; set; }
  public int MatchingTableID { get; set; }

  public LobbyToLobby_MatchingWaitConfirmT() {
    this.MatchID = null;
    this.TeamList = null;
    this.TeamID = null;
    this.DedicatedID = null;
    this.ServerUrl = null;
    this.OTP = null;
    this.GameSessionID = null;
    this.HasAI = false;
    this.MatchingTableID = 0;
  }
}


}
