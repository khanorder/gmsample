// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Lobby
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct LobbyToDedi_VehicleRaceCheck : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_23_1_20(); }
  public static LobbyToDedi_VehicleRaceCheck GetRootAsLobbyToDedi_VehicleRaceCheck(ByteBuffer _bb) { return GetRootAsLobbyToDedi_VehicleRaceCheck(_bb, new LobbyToDedi_VehicleRaceCheck()); }
  public static LobbyToDedi_VehicleRaceCheck GetRootAsLobbyToDedi_VehicleRaceCheck(ByteBuffer _bb, LobbyToDedi_VehicleRaceCheck obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public LobbyToDedi_VehicleRaceCheck __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public int UID { get { int o = __p.__offset(4); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public bool IsBlock { get { int o = __p.__offset(6); return o != 0 ? 0!=__p.bb.Get(o + __p.bb_pos) : (bool)false; } }

  public static Offset<Lobby.LobbyToDedi_VehicleRaceCheck> CreateLobbyToDedi_VehicleRaceCheck(FlatBufferBuilder builder,
      int UID = 0,
      bool IsBlock = false) {
    builder.StartTable(2);
    LobbyToDedi_VehicleRaceCheck.AddUID(builder, UID);
    LobbyToDedi_VehicleRaceCheck.AddIsBlock(builder, IsBlock);
    return LobbyToDedi_VehicleRaceCheck.EndLobbyToDedi_VehicleRaceCheck(builder);
  }

  public static void StartLobbyToDedi_VehicleRaceCheck(FlatBufferBuilder builder) { builder.StartTable(2); }
  public static void AddUID(FlatBufferBuilder builder, int UID) { builder.AddInt(0, UID, 0); }
  public static void AddIsBlock(FlatBufferBuilder builder, bool IsBlock) { builder.AddBool(1, IsBlock, false); }
  public static Offset<Lobby.LobbyToDedi_VehicleRaceCheck> EndLobbyToDedi_VehicleRaceCheck(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<Lobby.LobbyToDedi_VehicleRaceCheck>(o);
  }
  public LobbyToDedi_VehicleRaceCheckT UnPack() {
    var _o = new LobbyToDedi_VehicleRaceCheckT();
    this.UnPackTo(_o);
    return _o;
  }
  public void UnPackTo(LobbyToDedi_VehicleRaceCheckT _o) {
    _o.UID = this.UID;
    _o.IsBlock = this.IsBlock;
  }
  public static Offset<Lobby.LobbyToDedi_VehicleRaceCheck> Pack(FlatBufferBuilder builder, LobbyToDedi_VehicleRaceCheckT _o) {
    if (_o == null) return default(Offset<Lobby.LobbyToDedi_VehicleRaceCheck>);
    return CreateLobbyToDedi_VehicleRaceCheck(
      builder,
      _o.UID,
      _o.IsBlock);
  }
}

public class LobbyToDedi_VehicleRaceCheckT
{
  public int UID { get; set; }
  public bool IsBlock { get; set; }

  public LobbyToDedi_VehicleRaceCheckT() {
    this.UID = 0;
    this.IsBlock = false;
  }
}


}
