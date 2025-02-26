// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Lobby
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct SN_MatchingSetConfirm : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_23_1_20(); }
  public static SN_MatchingSetConfirm GetRootAsSN_MatchingSetConfirm(ByteBuffer _bb) { return GetRootAsSN_MatchingSetConfirm(_bb, new SN_MatchingSetConfirm()); }
  public static SN_MatchingSetConfirm GetRootAsSN_MatchingSetConfirm(ByteBuffer _bb, SN_MatchingSetConfirm obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public SN_MatchingSetConfirm __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public int UID { get { int o = __p.__offset(4); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public Lobby.HeroData? Hero { get { int o = __p.__offset(6); return o != 0 ? (Lobby.HeroData?)(new Lobby.HeroData()).__assign(o + __p.bb_pos, __p.bb) : null; } }
  public Lobby.HeroSkinData? HeroSkin { get { int o = __p.__offset(8); return o != 0 ? (Lobby.HeroSkinData?)(new Lobby.HeroSkinData()).__assign(o + __p.bb_pos, __p.bb) : null; } }

  public static Offset<Lobby.SN_MatchingSetConfirm> CreateSN_MatchingSetConfirm(FlatBufferBuilder builder,
      int UID = 0,
      Lobby.HeroDataT Hero = null,
      Lobby.HeroSkinDataT HeroSkin = null) {
    builder.StartTable(3);
    SN_MatchingSetConfirm.AddHeroSkin(builder, Lobby.HeroSkinData.Pack(builder, HeroSkin));
    SN_MatchingSetConfirm.AddHero(builder, Lobby.HeroData.Pack(builder, Hero));
    SN_MatchingSetConfirm.AddUID(builder, UID);
    return SN_MatchingSetConfirm.EndSN_MatchingSetConfirm(builder);
  }

  public static void StartSN_MatchingSetConfirm(FlatBufferBuilder builder) { builder.StartTable(3); }
  public static void AddUID(FlatBufferBuilder builder, int UID) { builder.AddInt(0, UID, 0); }
  public static void AddHero(FlatBufferBuilder builder, Offset<Lobby.HeroData> HeroOffset) { builder.AddStruct(1, HeroOffset.Value, 0); }
  public static void AddHeroSkin(FlatBufferBuilder builder, Offset<Lobby.HeroSkinData> HeroSkinOffset) { builder.AddStruct(2, HeroSkinOffset.Value, 0); }
  public static Offset<Lobby.SN_MatchingSetConfirm> EndSN_MatchingSetConfirm(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<Lobby.SN_MatchingSetConfirm>(o);
  }
  public SN_MatchingSetConfirmT UnPack() {
    var _o = new SN_MatchingSetConfirmT();
    this.UnPackTo(_o);
    return _o;
  }
  public void UnPackTo(SN_MatchingSetConfirmT _o) {
    _o.UID = this.UID;
    _o.Hero = this.Hero.HasValue ? this.Hero.Value.UnPack() : null;
    _o.HeroSkin = this.HeroSkin.HasValue ? this.HeroSkin.Value.UnPack() : null;
  }
  public static Offset<Lobby.SN_MatchingSetConfirm> Pack(FlatBufferBuilder builder, SN_MatchingSetConfirmT _o) {
    if (_o == null) return default(Offset<Lobby.SN_MatchingSetConfirm>);
    return CreateSN_MatchingSetConfirm(
      builder,
      _o.UID,
      _o.Hero,
      _o.HeroSkin);
  }
}

public class SN_MatchingSetConfirmT
{
  public int UID { get; set; }
  public Lobby.HeroDataT Hero { get; set; }
  public Lobby.HeroSkinDataT HeroSkin { get; set; }

  public SN_MatchingSetConfirmT() {
    this.UID = 0;
    this.Hero = new Lobby.HeroDataT();
    this.HeroSkin = new Lobby.HeroSkinDataT();
  }
}


}
