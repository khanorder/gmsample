// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Lobby
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct SA_MatchingChangeHero : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_23_1_20(); }
  public static SA_MatchingChangeHero GetRootAsSA_MatchingChangeHero(ByteBuffer _bb) { return GetRootAsSA_MatchingChangeHero(_bb, new SA_MatchingChangeHero()); }
  public static SA_MatchingChangeHero GetRootAsSA_MatchingChangeHero(ByteBuffer _bb, SA_MatchingChangeHero obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public SA_MatchingChangeHero __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }


  public static void StartSA_MatchingChangeHero(FlatBufferBuilder builder) { builder.StartTable(0); }
  public static Offset<Lobby.SA_MatchingChangeHero> EndSA_MatchingChangeHero(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<Lobby.SA_MatchingChangeHero>(o);
  }
  public SA_MatchingChangeHeroT UnPack() {
    var _o = new SA_MatchingChangeHeroT();
    this.UnPackTo(_o);
    return _o;
  }
  public void UnPackTo(SA_MatchingChangeHeroT _o) {
  }
  public static Offset<Lobby.SA_MatchingChangeHero> Pack(FlatBufferBuilder builder, SA_MatchingChangeHeroT _o) {
    if (_o == null) return default(Offset<Lobby.SA_MatchingChangeHero>);
    StartSA_MatchingChangeHero(builder);
    return EndSA_MatchingChangeHero(builder);
  }
}

public class SA_MatchingChangeHeroT
{

  public SA_MatchingChangeHeroT() {
  }
}


}
