// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Lobby
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct SA_RpgRoomEnd : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_23_1_20(); }
  public static SA_RpgRoomEnd GetRootAsSA_RpgRoomEnd(ByteBuffer _bb) { return GetRootAsSA_RpgRoomEnd(_bb, new SA_RpgRoomEnd()); }
  public static SA_RpgRoomEnd GetRootAsSA_RpgRoomEnd(ByteBuffer _bb, SA_RpgRoomEnd obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public SA_RpgRoomEnd __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }


  public static void StartSA_RpgRoomEnd(FlatBufferBuilder builder) { builder.StartTable(0); }
  public static Offset<Lobby.SA_RpgRoomEnd> EndSA_RpgRoomEnd(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<Lobby.SA_RpgRoomEnd>(o);
  }
  public SA_RpgRoomEndT UnPack() {
    var _o = new SA_RpgRoomEndT();
    this.UnPackTo(_o);
    return _o;
  }
  public void UnPackTo(SA_RpgRoomEndT _o) {
  }
  public static Offset<Lobby.SA_RpgRoomEnd> Pack(FlatBufferBuilder builder, SA_RpgRoomEndT _o) {
    if (_o == null) return default(Offset<Lobby.SA_RpgRoomEnd>);
    StartSA_RpgRoomEnd(builder);
    return EndSA_RpgRoomEnd(builder);
  }
}

public class SA_RpgRoomEndT
{

  public SA_RpgRoomEndT() {
  }
}


}
