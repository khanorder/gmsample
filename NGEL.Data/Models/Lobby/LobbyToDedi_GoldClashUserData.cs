// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Lobby
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct LobbyToDedi_GoldClashUserData : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_23_1_20(); }
  public static LobbyToDedi_GoldClashUserData GetRootAsLobbyToDedi_GoldClashUserData(ByteBuffer _bb) { return GetRootAsLobbyToDedi_GoldClashUserData(_bb, new LobbyToDedi_GoldClashUserData()); }
  public static LobbyToDedi_GoldClashUserData GetRootAsLobbyToDedi_GoldClashUserData(ByteBuffer _bb, LobbyToDedi_GoldClashUserData obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public LobbyToDedi_GoldClashUserData __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public int UID { get { int o = __p.__offset(4); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public string Nick { get { int o = __p.__offset(6); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetNickBytes() { return __p.__vector_as_span<byte>(6, 1); }
#else
  public ArraySegment<byte>? GetNickBytes() { return __p.__vector_as_arraysegment(6); }
#endif
  public byte[] GetNickArray() { return __p.__vector_as_array<byte>(6); }
  public string TeamID { get { int o = __p.__offset(8); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetTeamIDBytes() { return __p.__vector_as_span<byte>(8, 1); }
#else
  public ArraySegment<byte>? GetTeamIDBytes() { return __p.__vector_as_arraysegment(8); }
#endif
  public byte[] GetTeamIDArray() { return __p.__vector_as_array<byte>(8); }
  public Lobby.HeroData? Hero { get { int o = __p.__offset(10); return o != 0 ? (Lobby.HeroData?)(new Lobby.HeroData()).__assign(__p.__indirect(o + __p.bb_pos), __p.bb) : null; } }
  public Lobby.HeroSkinData? HairSkin { get { int o = __p.__offset(12); return o != 0 ? (Lobby.HeroSkinData?)(new Lobby.HeroSkinData()).__assign(__p.__indirect(o + __p.bb_pos), __p.bb) : null; } }
  public Lobby.HeroSkinData? BodySkin { get { int o = __p.__offset(14); return o != 0 ? (Lobby.HeroSkinData?)(new Lobby.HeroSkinData()).__assign(__p.__indirect(o + __p.bb_pos), __p.bb) : null; } }
  public Lobby.ArtifactData? ArtifactList(int j) { int o = __p.__offset(16); return o != 0 ? (Lobby.ArtifactData?)(new Lobby.ArtifactData()).__assign(__p.__vector(o) + j * 16, __p.bb) : null; }
  public int ArtifactListLength { get { int o = __p.__offset(16); return o != 0 ? __p.__vector_len(o) : 0; } }
  public Lobby.ArtifactData? DeckArtifactList(int j) { int o = __p.__offset(18); return o != 0 ? (Lobby.ArtifactData?)(new Lobby.ArtifactData()).__assign(__p.__vector(o) + j * 16, __p.bb) : null; }
  public int DeckArtifactListLength { get { int o = __p.__offset(18); return o != 0 ? __p.__vector_len(o) : 0; } }
  public Lobby.PetData? Pet { get { int o = __p.__offset(20); return o != 0 ? (Lobby.PetData?)(new Lobby.PetData()).__assign(__p.__indirect(o + __p.bb_pos), __p.bb) : null; } }
  public int EntitlementID { get { int o = __p.__offset(22); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int WinningStreak { get { int o = __p.__offset(24); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int RankingPoint { get { int o = __p.__offset(26); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int ExpressionID(int j) { int o = __p.__offset(28); return o != 0 ? __p.bb.GetInt(__p.__vector(o) + j * 4) : (int)0; }
  public int ExpressionIDLength { get { int o = __p.__offset(28); return o != 0 ? __p.__vector_len(o) : 0; } }
#if ENABLE_SPAN_T
  public Span<int> GetExpressionIDBytes() { return __p.__vector_as_span<int>(28, 4); }
#else
  public ArraySegment<byte>? GetExpressionIDBytes() { return __p.__vector_as_arraysegment(28); }
#endif
  public int[] GetExpressionIDArray() { return __p.__vector_as_array<int>(28); }
  public bool NotExistsUser { get { int o = __p.__offset(30); return o != 0 ? 0!=__p.bb.Get(o + __p.bb_pos) : (bool)false; } }

  public static Offset<Lobby.LobbyToDedi_GoldClashUserData> CreateLobbyToDedi_GoldClashUserData(FlatBufferBuilder builder,
      int UID = 0,
      StringOffset NickOffset = default(StringOffset),
      StringOffset TeamIDOffset = default(StringOffset),
      Offset<Lobby.HeroData> HeroOffset = default(Offset<Lobby.HeroData>),
      Offset<Lobby.HeroSkinData> HairSkinOffset = default(Offset<Lobby.HeroSkinData>),
      Offset<Lobby.HeroSkinData> BodySkinOffset = default(Offset<Lobby.HeroSkinData>),
      VectorOffset ArtifactListOffset = default(VectorOffset),
      VectorOffset DeckArtifactListOffset = default(VectorOffset),
      Offset<Lobby.PetData> PetOffset = default(Offset<Lobby.PetData>),
      int EntitlementID = 0,
      int WinningStreak = 0,
      int RankingPoint = 0,
      VectorOffset ExpressionIDOffset = default(VectorOffset),
      bool NotExistsUser = false) {
    builder.StartTable(14);
    LobbyToDedi_GoldClashUserData.AddExpressionID(builder, ExpressionIDOffset);
    LobbyToDedi_GoldClashUserData.AddRankingPoint(builder, RankingPoint);
    LobbyToDedi_GoldClashUserData.AddWinningStreak(builder, WinningStreak);
    LobbyToDedi_GoldClashUserData.AddEntitlementID(builder, EntitlementID);
    LobbyToDedi_GoldClashUserData.AddPet(builder, PetOffset);
    LobbyToDedi_GoldClashUserData.AddDeckArtifactList(builder, DeckArtifactListOffset);
    LobbyToDedi_GoldClashUserData.AddArtifactList(builder, ArtifactListOffset);
    LobbyToDedi_GoldClashUserData.AddBodySkin(builder, BodySkinOffset);
    LobbyToDedi_GoldClashUserData.AddHairSkin(builder, HairSkinOffset);
    LobbyToDedi_GoldClashUserData.AddHero(builder, HeroOffset);
    LobbyToDedi_GoldClashUserData.AddTeamID(builder, TeamIDOffset);
    LobbyToDedi_GoldClashUserData.AddNick(builder, NickOffset);
    LobbyToDedi_GoldClashUserData.AddUID(builder, UID);
    LobbyToDedi_GoldClashUserData.AddNotExistsUser(builder, NotExistsUser);
    return LobbyToDedi_GoldClashUserData.EndLobbyToDedi_GoldClashUserData(builder);
  }

  public static void StartLobbyToDedi_GoldClashUserData(FlatBufferBuilder builder) { builder.StartTable(14); }
  public static void AddUID(FlatBufferBuilder builder, int UID) { builder.AddInt(0, UID, 0); }
  public static void AddNick(FlatBufferBuilder builder, StringOffset NickOffset) { builder.AddOffset(1, NickOffset.Value, 0); }
  public static void AddTeamID(FlatBufferBuilder builder, StringOffset TeamIDOffset) { builder.AddOffset(2, TeamIDOffset.Value, 0); }
  public static void AddHero(FlatBufferBuilder builder, Offset<Lobby.HeroData> HeroOffset) { builder.AddOffset(3, HeroOffset.Value, 0); }
  public static void AddHairSkin(FlatBufferBuilder builder, Offset<Lobby.HeroSkinData> HairSkinOffset) { builder.AddOffset(4, HairSkinOffset.Value, 0); }
  public static void AddBodySkin(FlatBufferBuilder builder, Offset<Lobby.HeroSkinData> BodySkinOffset) { builder.AddOffset(5, BodySkinOffset.Value, 0); }
  public static void AddArtifactList(FlatBufferBuilder builder, VectorOffset ArtifactListOffset) { builder.AddOffset(6, ArtifactListOffset.Value, 0); }
  public static void StartArtifactListVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(16, numElems, 4); }
  public static void AddDeckArtifactList(FlatBufferBuilder builder, VectorOffset DeckArtifactListOffset) { builder.AddOffset(7, DeckArtifactListOffset.Value, 0); }
  public static void StartDeckArtifactListVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(16, numElems, 4); }
  public static void AddPet(FlatBufferBuilder builder, Offset<Lobby.PetData> PetOffset) { builder.AddOffset(8, PetOffset.Value, 0); }
  public static void AddEntitlementID(FlatBufferBuilder builder, int EntitlementID) { builder.AddInt(9, EntitlementID, 0); }
  public static void AddWinningStreak(FlatBufferBuilder builder, int WinningStreak) { builder.AddInt(10, WinningStreak, 0); }
  public static void AddRankingPoint(FlatBufferBuilder builder, int RankingPoint) { builder.AddInt(11, RankingPoint, 0); }
  public static void AddExpressionID(FlatBufferBuilder builder, VectorOffset ExpressionIDOffset) { builder.AddOffset(12, ExpressionIDOffset.Value, 0); }
  public static VectorOffset CreateExpressionIDVector(FlatBufferBuilder builder, int[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddInt(data[i]); return builder.EndVector(); }
  public static VectorOffset CreateExpressionIDVectorBlock(FlatBufferBuilder builder, int[] data) { builder.StartVector(4, data.Length, 4); builder.Add(data); return builder.EndVector(); }
  public static VectorOffset CreateExpressionIDVectorBlock(FlatBufferBuilder builder, ArraySegment<int> data) { builder.StartVector(4, data.Count, 4); builder.Add(data); return builder.EndVector(); }
  public static VectorOffset CreateExpressionIDVectorBlock(FlatBufferBuilder builder, IntPtr dataPtr, int sizeInBytes) { builder.StartVector(1, sizeInBytes, 1); builder.Add<int>(dataPtr, sizeInBytes); return builder.EndVector(); }
  public static void StartExpressionIDVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static void AddNotExistsUser(FlatBufferBuilder builder, bool NotExistsUser) { builder.AddBool(13, NotExistsUser, false); }
  public static Offset<Lobby.LobbyToDedi_GoldClashUserData> EndLobbyToDedi_GoldClashUserData(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<Lobby.LobbyToDedi_GoldClashUserData>(o);
  }
  public LobbyToDedi_GoldClashUserDataT UnPack() {
    var _o = new LobbyToDedi_GoldClashUserDataT();
    this.UnPackTo(_o);
    return _o;
  }
  public void UnPackTo(LobbyToDedi_GoldClashUserDataT _o) {
    _o.UID = this.UID;
    _o.Nick = this.Nick;
    _o.TeamID = this.TeamID;
    _o.Hero = this.Hero.HasValue ? this.Hero.Value.UnPack() : null;
    _o.HairSkin = this.HairSkin.HasValue ? this.HairSkin.Value.UnPack() : null;
    _o.BodySkin = this.BodySkin.HasValue ? this.BodySkin.Value.UnPack() : null;
    _o.ArtifactList = new List<Lobby.ArtifactDataT>();
    for (var _j = 0; _j < this.ArtifactListLength; ++_j) {_o.ArtifactList.Add(this.ArtifactList(_j).HasValue ? this.ArtifactList(_j).Value.UnPack() : null);}
    _o.DeckArtifactList = new List<Lobby.ArtifactDataT>();
    for (var _j = 0; _j < this.DeckArtifactListLength; ++_j) {_o.DeckArtifactList.Add(this.DeckArtifactList(_j).HasValue ? this.DeckArtifactList(_j).Value.UnPack() : null);}
    _o.Pet = this.Pet.HasValue ? this.Pet.Value.UnPack() : null;
    _o.EntitlementID = this.EntitlementID;
    _o.WinningStreak = this.WinningStreak;
    _o.RankingPoint = this.RankingPoint;
    _o.ExpressionID = new List<int>();
    for (var _j = 0; _j < this.ExpressionIDLength; ++_j) {_o.ExpressionID.Add(this.ExpressionID(_j));}
    _o.NotExistsUser = this.NotExistsUser;
  }
  public static Offset<Lobby.LobbyToDedi_GoldClashUserData> Pack(FlatBufferBuilder builder, LobbyToDedi_GoldClashUserDataT _o) {
    if (_o == null) return default(Offset<Lobby.LobbyToDedi_GoldClashUserData>);
    var _Nick = _o.Nick == null ? default(StringOffset) : builder.CreateString(_o.Nick);
    var _TeamID = _o.TeamID == null ? default(StringOffset) : builder.CreateString(_o.TeamID);
    var _Hero = _o.Hero == null ? default(Offset<Lobby.HeroData>) : Lobby.HeroData.Pack(builder, _o.Hero);
    var _HairSkin = _o.HairSkin == null ? default(Offset<Lobby.HeroSkinData>) : Lobby.HeroSkinData.Pack(builder, _o.HairSkin);
    var _BodySkin = _o.BodySkin == null ? default(Offset<Lobby.HeroSkinData>) : Lobby.HeroSkinData.Pack(builder, _o.BodySkin);
    var _ArtifactList = default(VectorOffset);
    if (_o.ArtifactList != null) {
      StartArtifactListVector(builder, _o.ArtifactList.Count);
      for (var _j = _o.ArtifactList.Count - 1; _j >= 0; --_j) { Lobby.ArtifactData.Pack(builder, _o.ArtifactList[_j]); }
      _ArtifactList = builder.EndVector();
    }
    var _DeckArtifactList = default(VectorOffset);
    if (_o.DeckArtifactList != null) {
      StartDeckArtifactListVector(builder, _o.DeckArtifactList.Count);
      for (var _j = _o.DeckArtifactList.Count - 1; _j >= 0; --_j) { Lobby.ArtifactData.Pack(builder, _o.DeckArtifactList[_j]); }
      _DeckArtifactList = builder.EndVector();
    }
    var _Pet = _o.Pet == null ? default(Offset<Lobby.PetData>) : Lobby.PetData.Pack(builder, _o.Pet);
    var _ExpressionID = default(VectorOffset);
    if (_o.ExpressionID != null) {
      var __ExpressionID = _o.ExpressionID.ToArray();
      _ExpressionID = CreateExpressionIDVector(builder, __ExpressionID);
    }
    return CreateLobbyToDedi_GoldClashUserData(
      builder,
      _o.UID,
      _Nick,
      _TeamID,
      _Hero,
      _HairSkin,
      _BodySkin,
      _ArtifactList,
      _DeckArtifactList,
      _Pet,
      _o.EntitlementID,
      _o.WinningStreak,
      _o.RankingPoint,
      _ExpressionID,
      _o.NotExistsUser);
  }
}

public class LobbyToDedi_GoldClashUserDataT
{
  public int UID { get; set; }
  public string Nick { get; set; }
  public string TeamID { get; set; }
  public Lobby.HeroDataT Hero { get; set; }
  public Lobby.HeroSkinDataT HairSkin { get; set; }
  public Lobby.HeroSkinDataT BodySkin { get; set; }
  public List<Lobby.ArtifactDataT> ArtifactList { get; set; }
  public List<Lobby.ArtifactDataT> DeckArtifactList { get; set; }
  public Lobby.PetDataT Pet { get; set; }
  public int EntitlementID { get; set; }
  public int WinningStreak { get; set; }
  public int RankingPoint { get; set; }
  public List<int> ExpressionID { get; set; }
  public bool NotExistsUser { get; set; }

  public LobbyToDedi_GoldClashUserDataT() {
    this.UID = 0;
    this.Nick = null;
    this.TeamID = null;
    this.Hero = null;
    this.HairSkin = null;
    this.BodySkin = null;
    this.ArtifactList = null;
    this.DeckArtifactList = null;
    this.Pet = null;
    this.EntitlementID = 0;
    this.WinningStreak = 0;
    this.RankingPoint = 0;
    this.ExpressionID = null;
    this.NotExistsUser = false;
  }
}


}
