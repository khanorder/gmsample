// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Lobby
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct LobbyToLobby_ConfirmManagerSetConfirm : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_23_1_20(); }
  public static LobbyToLobby_ConfirmManagerSetConfirm GetRootAsLobbyToLobby_ConfirmManagerSetConfirm(ByteBuffer _bb) { return GetRootAsLobbyToLobby_ConfirmManagerSetConfirm(_bb, new LobbyToLobby_ConfirmManagerSetConfirm()); }
  public static LobbyToLobby_ConfirmManagerSetConfirm GetRootAsLobbyToLobby_ConfirmManagerSetConfirm(ByteBuffer _bb, LobbyToLobby_ConfirmManagerSetConfirm obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public LobbyToLobby_ConfirmManagerSetConfirm __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public int MatchingTableID { get { int o = __p.__offset(4); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public string MatchID { get { int o = __p.__offset(6); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetMatchIDBytes() { return __p.__vector_as_span<byte>(6, 1); }
#else
  public ArraySegment<byte>? GetMatchIDBytes() { return __p.__vector_as_arraysegment(6); }
#endif
  public byte[] GetMatchIDArray() { return __p.__vector_as_array<byte>(6); }
  public string TeamID { get { int o = __p.__offset(8); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetTeamIDBytes() { return __p.__vector_as_span<byte>(8, 1); }
#else
  public ArraySegment<byte>? GetTeamIDBytes() { return __p.__vector_as_arraysegment(8); }
#endif
  public byte[] GetTeamIDArray() { return __p.__vector_as_array<byte>(8); }
  public int UID { get { int o = __p.__offset(10); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public Lobby.HeroData? Hero { get { int o = __p.__offset(12); return o != 0 ? (Lobby.HeroData?)(new Lobby.HeroData()).__assign(o + __p.bb_pos, __p.bb) : null; } }
  public Lobby.HeroSkinData? HeroSkin { get { int o = __p.__offset(14); return o != 0 ? (Lobby.HeroSkinData?)(new Lobby.HeroSkinData()).__assign(o + __p.bb_pos, __p.bb) : null; } }
  public Lobby.MultiArtifactInfo? ArtifactList(int j) { int o = __p.__offset(16); return o != 0 ? (Lobby.MultiArtifactInfo?)(new Lobby.MultiArtifactInfo()).__assign(__p.__vector(o) + j * 8, __p.bb) : null; }
  public int ArtifactListLength { get { int o = __p.__offset(16); return o != 0 ? __p.__vector_len(o) : 0; } }

  public static Offset<Lobby.LobbyToLobby_ConfirmManagerSetConfirm> CreateLobbyToLobby_ConfirmManagerSetConfirm(FlatBufferBuilder builder,
      int MatchingTableID = 0,
      StringOffset MatchIDOffset = default(StringOffset),
      StringOffset TeamIDOffset = default(StringOffset),
      int UID = 0,
      Lobby.HeroDataT Hero = null,
      Lobby.HeroSkinDataT HeroSkin = null,
      VectorOffset ArtifactListOffset = default(VectorOffset)) {
    builder.StartTable(7);
    LobbyToLobby_ConfirmManagerSetConfirm.AddArtifactList(builder, ArtifactListOffset);
    LobbyToLobby_ConfirmManagerSetConfirm.AddHeroSkin(builder, Lobby.HeroSkinData.Pack(builder, HeroSkin));
    LobbyToLobby_ConfirmManagerSetConfirm.AddHero(builder, Lobby.HeroData.Pack(builder, Hero));
    LobbyToLobby_ConfirmManagerSetConfirm.AddUID(builder, UID);
    LobbyToLobby_ConfirmManagerSetConfirm.AddTeamID(builder, TeamIDOffset);
    LobbyToLobby_ConfirmManagerSetConfirm.AddMatchID(builder, MatchIDOffset);
    LobbyToLobby_ConfirmManagerSetConfirm.AddMatchingTableID(builder, MatchingTableID);
    return LobbyToLobby_ConfirmManagerSetConfirm.EndLobbyToLobby_ConfirmManagerSetConfirm(builder);
  }

  public static void StartLobbyToLobby_ConfirmManagerSetConfirm(FlatBufferBuilder builder) { builder.StartTable(7); }
  public static void AddMatchingTableID(FlatBufferBuilder builder, int MatchingTableID) { builder.AddInt(0, MatchingTableID, 0); }
  public static void AddMatchID(FlatBufferBuilder builder, StringOffset MatchIDOffset) { builder.AddOffset(1, MatchIDOffset.Value, 0); }
  public static void AddTeamID(FlatBufferBuilder builder, StringOffset TeamIDOffset) { builder.AddOffset(2, TeamIDOffset.Value, 0); }
  public static void AddUID(FlatBufferBuilder builder, int UID) { builder.AddInt(3, UID, 0); }
  public static void AddHero(FlatBufferBuilder builder, Offset<Lobby.HeroData> HeroOffset) { builder.AddStruct(4, HeroOffset.Value, 0); }
  public static void AddHeroSkin(FlatBufferBuilder builder, Offset<Lobby.HeroSkinData> HeroSkinOffset) { builder.AddStruct(5, HeroSkinOffset.Value, 0); }
  public static void AddArtifactList(FlatBufferBuilder builder, VectorOffset ArtifactListOffset) { builder.AddOffset(6, ArtifactListOffset.Value, 0); }
  public static void StartArtifactListVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(8, numElems, 4); }
  public static Offset<Lobby.LobbyToLobby_ConfirmManagerSetConfirm> EndLobbyToLobby_ConfirmManagerSetConfirm(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<Lobby.LobbyToLobby_ConfirmManagerSetConfirm>(o);
  }
  public LobbyToLobby_ConfirmManagerSetConfirmT UnPack() {
    var _o = new LobbyToLobby_ConfirmManagerSetConfirmT();
    this.UnPackTo(_o);
    return _o;
  }
  public void UnPackTo(LobbyToLobby_ConfirmManagerSetConfirmT _o) {
    _o.MatchingTableID = this.MatchingTableID;
    _o.MatchID = this.MatchID;
    _o.TeamID = this.TeamID;
    _o.UID = this.UID;
    _o.Hero = this.Hero.HasValue ? this.Hero.Value.UnPack() : null;
    _o.HeroSkin = this.HeroSkin.HasValue ? this.HeroSkin.Value.UnPack() : null;
    _o.ArtifactList = new List<Lobby.MultiArtifactInfoT>();
    for (var _j = 0; _j < this.ArtifactListLength; ++_j) {_o.ArtifactList.Add(this.ArtifactList(_j).HasValue ? this.ArtifactList(_j).Value.UnPack() : null);}
  }
  public static Offset<Lobby.LobbyToLobby_ConfirmManagerSetConfirm> Pack(FlatBufferBuilder builder, LobbyToLobby_ConfirmManagerSetConfirmT _o) {
    if (_o == null) return default(Offset<Lobby.LobbyToLobby_ConfirmManagerSetConfirm>);
    var _MatchID = _o.MatchID == null ? default(StringOffset) : builder.CreateString(_o.MatchID);
    var _TeamID = _o.TeamID == null ? default(StringOffset) : builder.CreateString(_o.TeamID);
    var _ArtifactList = default(VectorOffset);
    if (_o.ArtifactList != null) {
      StartArtifactListVector(builder, _o.ArtifactList.Count);
      for (var _j = _o.ArtifactList.Count - 1; _j >= 0; --_j) { Lobby.MultiArtifactInfo.Pack(builder, _o.ArtifactList[_j]); }
      _ArtifactList = builder.EndVector();
    }
    return CreateLobbyToLobby_ConfirmManagerSetConfirm(
      builder,
      _o.MatchingTableID,
      _MatchID,
      _TeamID,
      _o.UID,
      _o.Hero,
      _o.HeroSkin,
      _ArtifactList);
  }
}

public class LobbyToLobby_ConfirmManagerSetConfirmT
{
  public int MatchingTableID { get; set; }
  public string MatchID { get; set; }
  public string TeamID { get; set; }
  public int UID { get; set; }
  public Lobby.HeroDataT Hero { get; set; }
  public Lobby.HeroSkinDataT HeroSkin { get; set; }
  public List<Lobby.MultiArtifactInfoT> ArtifactList { get; set; }

  public LobbyToLobby_ConfirmManagerSetConfirmT() {
    this.MatchingTableID = 0;
    this.MatchID = null;
    this.TeamID = null;
    this.UID = 0;
    this.Hero = new Lobby.HeroDataT();
    this.HeroSkin = new Lobby.HeroSkinDataT();
    this.ArtifactList = null;
  }
}


}
