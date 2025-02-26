// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Lobby
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct LobbyToDedi_OpenWorldUserData : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_23_1_20(); }
  public static LobbyToDedi_OpenWorldUserData GetRootAsLobbyToDedi_OpenWorldUserData(ByteBuffer _bb) { return GetRootAsLobbyToDedi_OpenWorldUserData(_bb, new LobbyToDedi_OpenWorldUserData()); }
  public static LobbyToDedi_OpenWorldUserData GetRootAsLobbyToDedi_OpenWorldUserData(ByteBuffer _bb, LobbyToDedi_OpenWorldUserData obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public LobbyToDedi_OpenWorldUserData __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public int UID { get { int o = __p.__offset(4); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public string Nick { get { int o = __p.__offset(6); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetNickBytes() { return __p.__vector_as_span<byte>(6, 1); }
#else
  public ArraySegment<byte>? GetNickBytes() { return __p.__vector_as_arraysegment(6); }
#endif
  public byte[] GetNickArray() { return __p.__vector_as_array<byte>(6); }
  public Lobby.HeroData? Hero { get { int o = __p.__offset(8); return o != 0 ? (Lobby.HeroData?)(new Lobby.HeroData()).__assign(__p.__indirect(o + __p.bb_pos), __p.bb) : null; } }
  public Lobby.HeroSkinData? HairSkin { get { int o = __p.__offset(10); return o != 0 ? (Lobby.HeroSkinData?)(new Lobby.HeroSkinData()).__assign(__p.__indirect(o + __p.bb_pos), __p.bb) : null; } }
  public Lobby.HeroSkinData? BodySkin { get { int o = __p.__offset(12); return o != 0 ? (Lobby.HeroSkinData?)(new Lobby.HeroSkinData()).__assign(__p.__indirect(o + __p.bb_pos), __p.bb) : null; } }
  public int EquipPetID { get { int o = __p.__offset(14); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int VehicleID { get { int o = __p.__offset(16); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int EntitlementID { get { int o = __p.__offset(18); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public string LastPosition { get { int o = __p.__offset(20); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetLastPositionBytes() { return __p.__vector_as_span<byte>(20, 1); }
#else
  public ArraySegment<byte>? GetLastPositionBytes() { return __p.__vector_as_arraysegment(20); }
#endif
  public byte[] GetLastPositionArray() { return __p.__vector_as_array<byte>(20); }
  public bool NotExistsUser { get { int o = __p.__offset(22); return o != 0 ? 0!=__p.bb.Get(o + __p.bb_pos) : (bool)false; } }

  public static Offset<Lobby.LobbyToDedi_OpenWorldUserData> CreateLobbyToDedi_OpenWorldUserData(FlatBufferBuilder builder,
      int UID = 0,
      StringOffset NickOffset = default(StringOffset),
      Offset<Lobby.HeroData> HeroOffset = default(Offset<Lobby.HeroData>),
      Offset<Lobby.HeroSkinData> HairSkinOffset = default(Offset<Lobby.HeroSkinData>),
      Offset<Lobby.HeroSkinData> BodySkinOffset = default(Offset<Lobby.HeroSkinData>),
      int EquipPetID = 0,
      int VehicleID = 0,
      int EntitlementID = 0,
      StringOffset LastPositionOffset = default(StringOffset),
      bool NotExistsUser = false) {
    builder.StartTable(10);
    LobbyToDedi_OpenWorldUserData.AddLastPosition(builder, LastPositionOffset);
    LobbyToDedi_OpenWorldUserData.AddEntitlementID(builder, EntitlementID);
    LobbyToDedi_OpenWorldUserData.AddVehicleID(builder, VehicleID);
    LobbyToDedi_OpenWorldUserData.AddEquipPetID(builder, EquipPetID);
    LobbyToDedi_OpenWorldUserData.AddBodySkin(builder, BodySkinOffset);
    LobbyToDedi_OpenWorldUserData.AddHairSkin(builder, HairSkinOffset);
    LobbyToDedi_OpenWorldUserData.AddHero(builder, HeroOffset);
    LobbyToDedi_OpenWorldUserData.AddNick(builder, NickOffset);
    LobbyToDedi_OpenWorldUserData.AddUID(builder, UID);
    LobbyToDedi_OpenWorldUserData.AddNotExistsUser(builder, NotExistsUser);
    return LobbyToDedi_OpenWorldUserData.EndLobbyToDedi_OpenWorldUserData(builder);
  }

  public static void StartLobbyToDedi_OpenWorldUserData(FlatBufferBuilder builder) { builder.StartTable(10); }
  public static void AddUID(FlatBufferBuilder builder, int UID) { builder.AddInt(0, UID, 0); }
  public static void AddNick(FlatBufferBuilder builder, StringOffset NickOffset) { builder.AddOffset(1, NickOffset.Value, 0); }
  public static void AddHero(FlatBufferBuilder builder, Offset<Lobby.HeroData> HeroOffset) { builder.AddOffset(2, HeroOffset.Value, 0); }
  public static void AddHairSkin(FlatBufferBuilder builder, Offset<Lobby.HeroSkinData> HairSkinOffset) { builder.AddOffset(3, HairSkinOffset.Value, 0); }
  public static void AddBodySkin(FlatBufferBuilder builder, Offset<Lobby.HeroSkinData> BodySkinOffset) { builder.AddOffset(4, BodySkinOffset.Value, 0); }
  public static void AddEquipPetID(FlatBufferBuilder builder, int EquipPetID) { builder.AddInt(5, EquipPetID, 0); }
  public static void AddVehicleID(FlatBufferBuilder builder, int VehicleID) { builder.AddInt(6, VehicleID, 0); }
  public static void AddEntitlementID(FlatBufferBuilder builder, int EntitlementID) { builder.AddInt(7, EntitlementID, 0); }
  public static void AddLastPosition(FlatBufferBuilder builder, StringOffset LastPositionOffset) { builder.AddOffset(8, LastPositionOffset.Value, 0); }
  public static void AddNotExistsUser(FlatBufferBuilder builder, bool NotExistsUser) { builder.AddBool(9, NotExistsUser, false); }
  public static Offset<Lobby.LobbyToDedi_OpenWorldUserData> EndLobbyToDedi_OpenWorldUserData(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<Lobby.LobbyToDedi_OpenWorldUserData>(o);
  }
  public LobbyToDedi_OpenWorldUserDataT UnPack() {
    var _o = new LobbyToDedi_OpenWorldUserDataT();
    this.UnPackTo(_o);
    return _o;
  }
  public void UnPackTo(LobbyToDedi_OpenWorldUserDataT _o) {
    _o.UID = this.UID;
    _o.Nick = this.Nick;
    _o.Hero = this.Hero.HasValue ? this.Hero.Value.UnPack() : null;
    _o.HairSkin = this.HairSkin.HasValue ? this.HairSkin.Value.UnPack() : null;
    _o.BodySkin = this.BodySkin.HasValue ? this.BodySkin.Value.UnPack() : null;
    _o.EquipPetID = this.EquipPetID;
    _o.VehicleID = this.VehicleID;
    _o.EntitlementID = this.EntitlementID;
    _o.LastPosition = this.LastPosition;
    _o.NotExistsUser = this.NotExistsUser;
  }
  public static Offset<Lobby.LobbyToDedi_OpenWorldUserData> Pack(FlatBufferBuilder builder, LobbyToDedi_OpenWorldUserDataT _o) {
    if (_o == null) return default(Offset<Lobby.LobbyToDedi_OpenWorldUserData>);
    var _Nick = _o.Nick == null ? default(StringOffset) : builder.CreateString(_o.Nick);
    var _Hero = _o.Hero == null ? default(Offset<Lobby.HeroData>) : Lobby.HeroData.Pack(builder, _o.Hero);
    var _HairSkin = _o.HairSkin == null ? default(Offset<Lobby.HeroSkinData>) : Lobby.HeroSkinData.Pack(builder, _o.HairSkin);
    var _BodySkin = _o.BodySkin == null ? default(Offset<Lobby.HeroSkinData>) : Lobby.HeroSkinData.Pack(builder, _o.BodySkin);
    var _LastPosition = _o.LastPosition == null ? default(StringOffset) : builder.CreateString(_o.LastPosition);
    return CreateLobbyToDedi_OpenWorldUserData(
      builder,
      _o.UID,
      _Nick,
      _Hero,
      _HairSkin,
      _BodySkin,
      _o.EquipPetID,
      _o.VehicleID,
      _o.EntitlementID,
      _LastPosition,
      _o.NotExistsUser);
  }
}

public class LobbyToDedi_OpenWorldUserDataT
{
  public int UID { get; set; }
  public string Nick { get; set; }
  public Lobby.HeroDataT Hero { get; set; }
  public Lobby.HeroSkinDataT HairSkin { get; set; }
  public Lobby.HeroSkinDataT BodySkin { get; set; }
  public int EquipPetID { get; set; }
  public int VehicleID { get; set; }
  public int EntitlementID { get; set; }
  public string LastPosition { get; set; }
  public bool NotExistsUser { get; set; }

  public LobbyToDedi_OpenWorldUserDataT() {
    this.UID = 0;
    this.Nick = null;
    this.Hero = null;
    this.HairSkin = null;
    this.BodySkin = null;
    this.EquipPetID = 0;
    this.VehicleID = 0;
    this.EntitlementID = 0;
    this.LastPosition = null;
    this.NotExistsUser = false;
  }
}


}
