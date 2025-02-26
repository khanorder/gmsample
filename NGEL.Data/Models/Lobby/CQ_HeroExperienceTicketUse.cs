// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Lobby
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct CQ_HeroExperienceTicketUse : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_23_1_20(); }
  public static CQ_HeroExperienceTicketUse GetRootAsCQ_HeroExperienceTicketUse(ByteBuffer _bb) { return GetRootAsCQ_HeroExperienceTicketUse(_bb, new CQ_HeroExperienceTicketUse()); }
  public static CQ_HeroExperienceTicketUse GetRootAsCQ_HeroExperienceTicketUse(ByteBuffer _bb, CQ_HeroExperienceTicketUse obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public CQ_HeroExperienceTicketUse __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public Lobby.EHeroType HeroID { get { int o = __p.__offset(4); return o != 0 ? (Lobby.EHeroType)__p.bb.Get(o + __p.bb_pos) : Lobby.EHeroType.None; } }

  public static Offset<Lobby.CQ_HeroExperienceTicketUse> CreateCQ_HeroExperienceTicketUse(FlatBufferBuilder builder,
      Lobby.EHeroType HeroID = Lobby.EHeroType.None) {
    builder.StartTable(1);
    CQ_HeroExperienceTicketUse.AddHeroID(builder, HeroID);
    return CQ_HeroExperienceTicketUse.EndCQ_HeroExperienceTicketUse(builder);
  }

  public static void StartCQ_HeroExperienceTicketUse(FlatBufferBuilder builder) { builder.StartTable(1); }
  public static void AddHeroID(FlatBufferBuilder builder, Lobby.EHeroType HeroID) { builder.AddByte(0, (byte)HeroID, 0); }
  public static Offset<Lobby.CQ_HeroExperienceTicketUse> EndCQ_HeroExperienceTicketUse(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<Lobby.CQ_HeroExperienceTicketUse>(o);
  }
  public CQ_HeroExperienceTicketUseT UnPack() {
    var _o = new CQ_HeroExperienceTicketUseT();
    this.UnPackTo(_o);
    return _o;
  }
  public void UnPackTo(CQ_HeroExperienceTicketUseT _o) {
    _o.HeroID = this.HeroID;
  }
  public static Offset<Lobby.CQ_HeroExperienceTicketUse> Pack(FlatBufferBuilder builder, CQ_HeroExperienceTicketUseT _o) {
    if (_o == null) return default(Offset<Lobby.CQ_HeroExperienceTicketUse>);
    return CreateCQ_HeroExperienceTicketUse(
      builder,
      _o.HeroID);
  }
}

public class CQ_HeroExperienceTicketUseT
{
  public Lobby.EHeroType HeroID { get; set; }

  public CQ_HeroExperienceTicketUseT() {
    this.HeroID = Lobby.EHeroType.None;
  }
}


}
