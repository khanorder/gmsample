// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Lobby
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct MatchingTeamInfo : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_23_1_20(); }
  public static MatchingTeamInfo GetRootAsMatchingTeamInfo(ByteBuffer _bb) { return GetRootAsMatchingTeamInfo(_bb, new MatchingTeamInfo()); }
  public static MatchingTeamInfo GetRootAsMatchingTeamInfo(ByteBuffer _bb, MatchingTeamInfo obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public MatchingTeamInfo __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public string TeamID { get { int o = __p.__offset(4); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetTeamIDBytes() { return __p.__vector_as_span<byte>(4, 1); }
#else
  public ArraySegment<byte>? GetTeamIDBytes() { return __p.__vector_as_arraysegment(4); }
#endif
  public byte[] GetTeamIDArray() { return __p.__vector_as_array<byte>(4); }
  public int UID { get { int o = __p.__offset(6); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public string Nick { get { int o = __p.__offset(8); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetNickBytes() { return __p.__vector_as_span<byte>(8, 1); }
#else
  public ArraySegment<byte>? GetNickBytes() { return __p.__vector_as_arraysegment(8); }
#endif
  public byte[] GetNickArray() { return __p.__vector_as_array<byte>(8); }
  public int ProfileIconID { get { int o = __p.__offset(10); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public string LobbyID { get { int o = __p.__offset(12); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetLobbyIDBytes() { return __p.__vector_as_span<byte>(12, 1); }
#else
  public ArraySegment<byte>? GetLobbyIDBytes() { return __p.__vector_as_arraysegment(12); }
#endif
  public byte[] GetLobbyIDArray() { return __p.__vector_as_array<byte>(12); }

  public static Offset<Lobby.MatchingTeamInfo> CreateMatchingTeamInfo(FlatBufferBuilder builder,
      StringOffset TeamIDOffset = default(StringOffset),
      int UID = 0,
      StringOffset NickOffset = default(StringOffset),
      int ProfileIconID = 0,
      StringOffset LobbyIDOffset = default(StringOffset)) {
    builder.StartTable(5);
    MatchingTeamInfo.AddLobbyID(builder, LobbyIDOffset);
    MatchingTeamInfo.AddProfileIconID(builder, ProfileIconID);
    MatchingTeamInfo.AddNick(builder, NickOffset);
    MatchingTeamInfo.AddUID(builder, UID);
    MatchingTeamInfo.AddTeamID(builder, TeamIDOffset);
    return MatchingTeamInfo.EndMatchingTeamInfo(builder);
  }

  public static void StartMatchingTeamInfo(FlatBufferBuilder builder) { builder.StartTable(5); }
  public static void AddTeamID(FlatBufferBuilder builder, StringOffset TeamIDOffset) { builder.AddOffset(0, TeamIDOffset.Value, 0); }
  public static void AddUID(FlatBufferBuilder builder, int UID) { builder.AddInt(1, UID, 0); }
  public static void AddNick(FlatBufferBuilder builder, StringOffset NickOffset) { builder.AddOffset(2, NickOffset.Value, 0); }
  public static void AddProfileIconID(FlatBufferBuilder builder, int ProfileIconID) { builder.AddInt(3, ProfileIconID, 0); }
  public static void AddLobbyID(FlatBufferBuilder builder, StringOffset LobbyIDOffset) { builder.AddOffset(4, LobbyIDOffset.Value, 0); }
  public static Offset<Lobby.MatchingTeamInfo> EndMatchingTeamInfo(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<Lobby.MatchingTeamInfo>(o);
  }
  public MatchingTeamInfoT UnPack() {
    var _o = new MatchingTeamInfoT();
    this.UnPackTo(_o);
    return _o;
  }
  public void UnPackTo(MatchingTeamInfoT _o) {
    _o.TeamID = this.TeamID;
    _o.UID = this.UID;
    _o.Nick = this.Nick;
    _o.ProfileIconID = this.ProfileIconID;
    _o.LobbyID = this.LobbyID;
  }
  public static Offset<Lobby.MatchingTeamInfo> Pack(FlatBufferBuilder builder, MatchingTeamInfoT _o) {
    if (_o == null) return default(Offset<Lobby.MatchingTeamInfo>);
    var _TeamID = _o.TeamID == null ? default(StringOffset) : builder.CreateString(_o.TeamID);
    var _Nick = _o.Nick == null ? default(StringOffset) : builder.CreateString(_o.Nick);
    var _LobbyID = _o.LobbyID == null ? default(StringOffset) : builder.CreateString(_o.LobbyID);
    return CreateMatchingTeamInfo(
      builder,
      _TeamID,
      _o.UID,
      _Nick,
      _o.ProfileIconID,
      _LobbyID);
  }
}

public class MatchingTeamInfoT
{
  public string TeamID { get; set; }
  public int UID { get; set; }
  public string Nick { get; set; }
  public int ProfileIconID { get; set; }
  public string LobbyID { get; set; }

  public MatchingTeamInfoT() {
    this.TeamID = null;
    this.UID = 0;
    this.Nick = null;
    this.ProfileIconID = 0;
    this.LobbyID = null;
  }
}


}
