// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Lobby
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct CQ_VehicleRaceExit : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_23_1_20(); }
  public static CQ_VehicleRaceExit GetRootAsCQ_VehicleRaceExit(ByteBuffer _bb) { return GetRootAsCQ_VehicleRaceExit(_bb, new CQ_VehicleRaceExit()); }
  public static CQ_VehicleRaceExit GetRootAsCQ_VehicleRaceExit(ByteBuffer _bb, CQ_VehicleRaceExit obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public CQ_VehicleRaceExit __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }


  public static void StartCQ_VehicleRaceExit(FlatBufferBuilder builder) { builder.StartTable(0); }
  public static Offset<Lobby.CQ_VehicleRaceExit> EndCQ_VehicleRaceExit(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<Lobby.CQ_VehicleRaceExit>(o);
  }
  public CQ_VehicleRaceExitT UnPack() {
    var _o = new CQ_VehicleRaceExitT();
    this.UnPackTo(_o);
    return _o;
  }
  public void UnPackTo(CQ_VehicleRaceExitT _o) {
  }
  public static Offset<Lobby.CQ_VehicleRaceExit> Pack(FlatBufferBuilder builder, CQ_VehicleRaceExitT _o) {
    if (_o == null) return default(Offset<Lobby.CQ_VehicleRaceExit>);
    StartCQ_VehicleRaceExit(builder);
    return EndCQ_VehicleRaceExit(builder);
  }
}

public class CQ_VehicleRaceExitT
{

  public CQ_VehicleRaceExitT() {
  }
}


}
