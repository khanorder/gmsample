// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Lobby
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct DediToLobby_WaitRoomUserData_Req : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_23_1_20(); }
  public static DediToLobby_WaitRoomUserData_Req GetRootAsDediToLobby_WaitRoomUserData_Req(ByteBuffer _bb) { return GetRootAsDediToLobby_WaitRoomUserData_Req(_bb, new DediToLobby_WaitRoomUserData_Req()); }
  public static DediToLobby_WaitRoomUserData_Req GetRootAsDediToLobby_WaitRoomUserData_Req(ByteBuffer _bb, DediToLobby_WaitRoomUserData_Req obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public DediToLobby_WaitRoomUserData_Req __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public string Destination { get { int o = __p.__offset(4); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetDestinationBytes() { return __p.__vector_as_span<byte>(4, 1); }
#else
  public ArraySegment<byte>? GetDestinationBytes() { return __p.__vector_as_arraysegment(4); }
#endif
  public byte[] GetDestinationArray() { return __p.__vector_as_array<byte>(4); }
  public int UID { get { int o = __p.__offset(6); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }

  public static Offset<Lobby.DediToLobby_WaitRoomUserData_Req> CreateDediToLobby_WaitRoomUserData_Req(FlatBufferBuilder builder,
      StringOffset DestinationOffset = default(StringOffset),
      int UID = 0) {
    builder.StartTable(2);
    DediToLobby_WaitRoomUserData_Req.AddUID(builder, UID);
    DediToLobby_WaitRoomUserData_Req.AddDestination(builder, DestinationOffset);
    return DediToLobby_WaitRoomUserData_Req.EndDediToLobby_WaitRoomUserData_Req(builder);
  }

  public static void StartDediToLobby_WaitRoomUserData_Req(FlatBufferBuilder builder) { builder.StartTable(2); }
  public static void AddDestination(FlatBufferBuilder builder, StringOffset DestinationOffset) { builder.AddOffset(0, DestinationOffset.Value, 0); }
  public static void AddUID(FlatBufferBuilder builder, int UID) { builder.AddInt(1, UID, 0); }
  public static Offset<Lobby.DediToLobby_WaitRoomUserData_Req> EndDediToLobby_WaitRoomUserData_Req(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<Lobby.DediToLobby_WaitRoomUserData_Req>(o);
  }
  public DediToLobby_WaitRoomUserData_ReqT UnPack() {
    var _o = new DediToLobby_WaitRoomUserData_ReqT();
    this.UnPackTo(_o);
    return _o;
  }
  public void UnPackTo(DediToLobby_WaitRoomUserData_ReqT _o) {
    _o.Destination = this.Destination;
    _o.UID = this.UID;
  }
  public static Offset<Lobby.DediToLobby_WaitRoomUserData_Req> Pack(FlatBufferBuilder builder, DediToLobby_WaitRoomUserData_ReqT _o) {
    if (_o == null) return default(Offset<Lobby.DediToLobby_WaitRoomUserData_Req>);
    var _Destination = _o.Destination == null ? default(StringOffset) : builder.CreateString(_o.Destination);
    return CreateDediToLobby_WaitRoomUserData_Req(
      builder,
      _Destination,
      _o.UID);
  }
}

public class DediToLobby_WaitRoomUserData_ReqT
{
  public string Destination { get; set; }
  public int UID { get; set; }

  public DediToLobby_WaitRoomUserData_ReqT() {
    this.Destination = null;
    this.UID = 0;
  }
}


}
