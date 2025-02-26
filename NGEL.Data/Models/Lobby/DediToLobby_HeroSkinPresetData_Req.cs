// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Lobby
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct DediToLobby_HeroSkinPresetData_Req : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_23_1_20(); }
  public static DediToLobby_HeroSkinPresetData_Req GetRootAsDediToLobby_HeroSkinPresetData_Req(ByteBuffer _bb) { return GetRootAsDediToLobby_HeroSkinPresetData_Req(_bb, new DediToLobby_HeroSkinPresetData_Req()); }
  public static DediToLobby_HeroSkinPresetData_Req GetRootAsDediToLobby_HeroSkinPresetData_Req(ByteBuffer _bb, DediToLobby_HeroSkinPresetData_Req obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public DediToLobby_HeroSkinPresetData_Req __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public string Destination { get { int o = __p.__offset(4); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetDestinationBytes() { return __p.__vector_as_span<byte>(4, 1); }
#else
  public ArraySegment<byte>? GetDestinationBytes() { return __p.__vector_as_arraysegment(4); }
#endif
  public byte[] GetDestinationArray() { return __p.__vector_as_array<byte>(4); }
  public int UID { get { int o = __p.__offset(6); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int HeroID { get { int o = __p.__offset(8); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public byte HeroSkinPresetSlotID { get { int o = __p.__offset(10); return o != 0 ? __p.bb.Get(o + __p.bb_pos) : (byte)0; } }

  public static Offset<Lobby.DediToLobby_HeroSkinPresetData_Req> CreateDediToLobby_HeroSkinPresetData_Req(FlatBufferBuilder builder,
      StringOffset DestinationOffset = default(StringOffset),
      int UID = 0,
      int HeroID = 0,
      byte HeroSkinPresetSlotID = 0) {
    builder.StartTable(4);
    DediToLobby_HeroSkinPresetData_Req.AddHeroID(builder, HeroID);
    DediToLobby_HeroSkinPresetData_Req.AddUID(builder, UID);
    DediToLobby_HeroSkinPresetData_Req.AddDestination(builder, DestinationOffset);
    DediToLobby_HeroSkinPresetData_Req.AddHeroSkinPresetSlotID(builder, HeroSkinPresetSlotID);
    return DediToLobby_HeroSkinPresetData_Req.EndDediToLobby_HeroSkinPresetData_Req(builder);
  }

  public static void StartDediToLobby_HeroSkinPresetData_Req(FlatBufferBuilder builder) { builder.StartTable(4); }
  public static void AddDestination(FlatBufferBuilder builder, StringOffset DestinationOffset) { builder.AddOffset(0, DestinationOffset.Value, 0); }
  public static void AddUID(FlatBufferBuilder builder, int UID) { builder.AddInt(1, UID, 0); }
  public static void AddHeroID(FlatBufferBuilder builder, int HeroID) { builder.AddInt(2, HeroID, 0); }
  public static void AddHeroSkinPresetSlotID(FlatBufferBuilder builder, byte HeroSkinPresetSlotID) { builder.AddByte(3, HeroSkinPresetSlotID, 0); }
  public static Offset<Lobby.DediToLobby_HeroSkinPresetData_Req> EndDediToLobby_HeroSkinPresetData_Req(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<Lobby.DediToLobby_HeroSkinPresetData_Req>(o);
  }
  public DediToLobby_HeroSkinPresetData_ReqT UnPack() {
    var _o = new DediToLobby_HeroSkinPresetData_ReqT();
    this.UnPackTo(_o);
    return _o;
  }
  public void UnPackTo(DediToLobby_HeroSkinPresetData_ReqT _o) {
    _o.Destination = this.Destination;
    _o.UID = this.UID;
    _o.HeroID = this.HeroID;
    _o.HeroSkinPresetSlotID = this.HeroSkinPresetSlotID;
  }
  public static Offset<Lobby.DediToLobby_HeroSkinPresetData_Req> Pack(FlatBufferBuilder builder, DediToLobby_HeroSkinPresetData_ReqT _o) {
    if (_o == null) return default(Offset<Lobby.DediToLobby_HeroSkinPresetData_Req>);
    var _Destination = _o.Destination == null ? default(StringOffset) : builder.CreateString(_o.Destination);
    return CreateDediToLobby_HeroSkinPresetData_Req(
      builder,
      _Destination,
      _o.UID,
      _o.HeroID,
      _o.HeroSkinPresetSlotID);
  }
}

public class DediToLobby_HeroSkinPresetData_ReqT
{
  public string Destination { get; set; }
  public int UID { get; set; }
  public int HeroID { get; set; }
  public byte HeroSkinPresetSlotID { get; set; }

  public DediToLobby_HeroSkinPresetData_ReqT() {
    this.Destination = null;
    this.UID = 0;
    this.HeroID = 0;
    this.HeroSkinPresetSlotID = 0;
  }
}


}
