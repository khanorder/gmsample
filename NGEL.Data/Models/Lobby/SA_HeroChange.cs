// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Lobby
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct SA_HeroChange : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_23_1_20(); }
  public static SA_HeroChange GetRootAsSA_HeroChange(ByteBuffer _bb) { return GetRootAsSA_HeroChange(_bb, new SA_HeroChange()); }
  public static SA_HeroChange GetRootAsSA_HeroChange(ByteBuffer _bb, SA_HeroChange obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public SA_HeroChange __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public int HeroID { get { int o = __p.__offset(4); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }

  public static Offset<Lobby.SA_HeroChange> CreateSA_HeroChange(FlatBufferBuilder builder,
      int HeroID = 0) {
    builder.StartTable(1);
    SA_HeroChange.AddHeroID(builder, HeroID);
    return SA_HeroChange.EndSA_HeroChange(builder);
  }

  public static void StartSA_HeroChange(FlatBufferBuilder builder) { builder.StartTable(1); }
  public static void AddHeroID(FlatBufferBuilder builder, int HeroID) { builder.AddInt(0, HeroID, 0); }
  public static Offset<Lobby.SA_HeroChange> EndSA_HeroChange(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<Lobby.SA_HeroChange>(o);
  }
  public SA_HeroChangeT UnPack() {
    var _o = new SA_HeroChangeT();
    this.UnPackTo(_o);
    return _o;
  }
  public void UnPackTo(SA_HeroChangeT _o) {
    _o.HeroID = this.HeroID;
  }
  public static Offset<Lobby.SA_HeroChange> Pack(FlatBufferBuilder builder, SA_HeroChangeT _o) {
    if (_o == null) return default(Offset<Lobby.SA_HeroChange>);
    return CreateSA_HeroChange(
      builder,
      _o.HeroID);
  }
}

public class SA_HeroChangeT
{
  public int HeroID { get; set; }

  public SA_HeroChangeT() {
    this.HeroID = 0;
  }
}


}
