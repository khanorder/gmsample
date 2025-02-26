// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Lobby
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct CQ_IncubationStart : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_23_1_20(); }
  public static CQ_IncubationStart GetRootAsCQ_IncubationStart(ByteBuffer _bb) { return GetRootAsCQ_IncubationStart(_bb, new CQ_IncubationStart()); }
  public static CQ_IncubationStart GetRootAsCQ_IncubationStart(ByteBuffer _bb, CQ_IncubationStart obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public CQ_IncubationStart __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public int IncubatorID { get { int o = __p.__offset(4); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int PetEggID { get { int o = __p.__offset(6); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }

  public static Offset<Lobby.CQ_IncubationStart> CreateCQ_IncubationStart(FlatBufferBuilder builder,
      int IncubatorID = 0,
      int PetEggID = 0) {
    builder.StartTable(2);
    CQ_IncubationStart.AddPetEggID(builder, PetEggID);
    CQ_IncubationStart.AddIncubatorID(builder, IncubatorID);
    return CQ_IncubationStart.EndCQ_IncubationStart(builder);
  }

  public static void StartCQ_IncubationStart(FlatBufferBuilder builder) { builder.StartTable(2); }
  public static void AddIncubatorID(FlatBufferBuilder builder, int IncubatorID) { builder.AddInt(0, IncubatorID, 0); }
  public static void AddPetEggID(FlatBufferBuilder builder, int PetEggID) { builder.AddInt(1, PetEggID, 0); }
  public static Offset<Lobby.CQ_IncubationStart> EndCQ_IncubationStart(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<Lobby.CQ_IncubationStart>(o);
  }
  public CQ_IncubationStartT UnPack() {
    var _o = new CQ_IncubationStartT();
    this.UnPackTo(_o);
    return _o;
  }
  public void UnPackTo(CQ_IncubationStartT _o) {
    _o.IncubatorID = this.IncubatorID;
    _o.PetEggID = this.PetEggID;
  }
  public static Offset<Lobby.CQ_IncubationStart> Pack(FlatBufferBuilder builder, CQ_IncubationStartT _o) {
    if (_o == null) return default(Offset<Lobby.CQ_IncubationStart>);
    return CreateCQ_IncubationStart(
      builder,
      _o.IncubatorID,
      _o.PetEggID);
  }
}

public class CQ_IncubationStartT
{
  public int IncubatorID { get; set; }
  public int PetEggID { get; set; }

  public CQ_IncubationStartT() {
    this.IncubatorID = 0;
    this.PetEggID = 0;
  }
}


}
