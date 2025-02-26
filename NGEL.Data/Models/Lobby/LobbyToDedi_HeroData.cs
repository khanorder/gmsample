// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Lobby
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct LobbyToDedi_HeroData : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_23_1_20(); }
  public static LobbyToDedi_HeroData GetRootAsLobbyToDedi_HeroData(ByteBuffer _bb) { return GetRootAsLobbyToDedi_HeroData(_bb, new LobbyToDedi_HeroData()); }
  public static LobbyToDedi_HeroData GetRootAsLobbyToDedi_HeroData(ByteBuffer _bb, LobbyToDedi_HeroData obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public LobbyToDedi_HeroData __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public int UID { get { int o = __p.__offset(4); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public Lobby.HeroData? Hero { get { int o = __p.__offset(6); return o != 0 ? (Lobby.HeroData?)(new Lobby.HeroData()).__assign(__p.__indirect(o + __p.bb_pos), __p.bb) : null; } }
  public Lobby.HeroSkinData? HairSkin { get { int o = __p.__offset(8); return o != 0 ? (Lobby.HeroSkinData?)(new Lobby.HeroSkinData()).__assign(__p.__indirect(o + __p.bb_pos), __p.bb) : null; } }
  public Lobby.HeroSkinData? BodySkin { get { int o = __p.__offset(10); return o != 0 ? (Lobby.HeroSkinData?)(new Lobby.HeroSkinData()).__assign(__p.__indirect(o + __p.bb_pos), __p.bb) : null; } }

  public static Offset<Lobby.LobbyToDedi_HeroData> CreateLobbyToDedi_HeroData(FlatBufferBuilder builder,
      int UID = 0,
      Offset<Lobby.HeroData> HeroOffset = default(Offset<Lobby.HeroData>),
      Offset<Lobby.HeroSkinData> HairSkinOffset = default(Offset<Lobby.HeroSkinData>),
      Offset<Lobby.HeroSkinData> BodySkinOffset = default(Offset<Lobby.HeroSkinData>)) {
    builder.StartTable(4);
    LobbyToDedi_HeroData.AddBodySkin(builder, BodySkinOffset);
    LobbyToDedi_HeroData.AddHairSkin(builder, HairSkinOffset);
    LobbyToDedi_HeroData.AddHero(builder, HeroOffset);
    LobbyToDedi_HeroData.AddUID(builder, UID);
    return LobbyToDedi_HeroData.EndLobbyToDedi_HeroData(builder);
  }

  public static void StartLobbyToDedi_HeroData(FlatBufferBuilder builder) { builder.StartTable(4); }
  public static void AddUID(FlatBufferBuilder builder, int UID) { builder.AddInt(0, UID, 0); }
  public static void AddHero(FlatBufferBuilder builder, Offset<Lobby.HeroData> HeroOffset) { builder.AddOffset(1, HeroOffset.Value, 0); }
  public static void AddHairSkin(FlatBufferBuilder builder, Offset<Lobby.HeroSkinData> HairSkinOffset) { builder.AddOffset(2, HairSkinOffset.Value, 0); }
  public static void AddBodySkin(FlatBufferBuilder builder, Offset<Lobby.HeroSkinData> BodySkinOffset) { builder.AddOffset(3, BodySkinOffset.Value, 0); }
  public static Offset<Lobby.LobbyToDedi_HeroData> EndLobbyToDedi_HeroData(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<Lobby.LobbyToDedi_HeroData>(o);
  }
  public LobbyToDedi_HeroDataT UnPack() {
    var _o = new LobbyToDedi_HeroDataT();
    this.UnPackTo(_o);
    return _o;
  }
  public void UnPackTo(LobbyToDedi_HeroDataT _o) {
    _o.UID = this.UID;
    _o.Hero = this.Hero.HasValue ? this.Hero.Value.UnPack() : null;
    _o.HairSkin = this.HairSkin.HasValue ? this.HairSkin.Value.UnPack() : null;
    _o.BodySkin = this.BodySkin.HasValue ? this.BodySkin.Value.UnPack() : null;
  }
  public static Offset<Lobby.LobbyToDedi_HeroData> Pack(FlatBufferBuilder builder, LobbyToDedi_HeroDataT _o) {
    if (_o == null) return default(Offset<Lobby.LobbyToDedi_HeroData>);
    var _Hero = _o.Hero == null ? default(Offset<Lobby.HeroData>) : Lobby.HeroData.Pack(builder, _o.Hero);
    var _HairSkin = _o.HairSkin == null ? default(Offset<Lobby.HeroSkinData>) : Lobby.HeroSkinData.Pack(builder, _o.HairSkin);
    var _BodySkin = _o.BodySkin == null ? default(Offset<Lobby.HeroSkinData>) : Lobby.HeroSkinData.Pack(builder, _o.BodySkin);
    return CreateLobbyToDedi_HeroData(
      builder,
      _o.UID,
      _Hero,
      _HairSkin,
      _BodySkin);
  }
}

public class LobbyToDedi_HeroDataT
{
  public int UID { get; set; }
  public Lobby.HeroDataT Hero { get; set; }
  public Lobby.HeroSkinDataT HairSkin { get; set; }
  public Lobby.HeroSkinDataT BodySkin { get; set; }

  public LobbyToDedi_HeroDataT() {
    this.UID = 0;
    this.Hero = null;
    this.HairSkin = null;
    this.BodySkin = null;
  }
}


}
