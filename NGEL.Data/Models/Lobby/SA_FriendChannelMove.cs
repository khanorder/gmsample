// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Lobby
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct SA_FriendChannelMove : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_23_1_20(); }
  public static SA_FriendChannelMove GetRootAsSA_FriendChannelMove(ByteBuffer _bb) { return GetRootAsSA_FriendChannelMove(_bb, new SA_FriendChannelMove()); }
  public static SA_FriendChannelMove GetRootAsSA_FriendChannelMove(ByteBuffer _bb, SA_FriendChannelMove obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public SA_FriendChannelMove __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }


  public static void StartSA_FriendChannelMove(FlatBufferBuilder builder) { builder.StartTable(0); }
  public static Offset<Lobby.SA_FriendChannelMove> EndSA_FriendChannelMove(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<Lobby.SA_FriendChannelMove>(o);
  }
  public SA_FriendChannelMoveT UnPack() {
    var _o = new SA_FriendChannelMoveT();
    this.UnPackTo(_o);
    return _o;
  }
  public void UnPackTo(SA_FriendChannelMoveT _o) {
  }
  public static Offset<Lobby.SA_FriendChannelMove> Pack(FlatBufferBuilder builder, SA_FriendChannelMoveT _o) {
    if (_o == null) return default(Offset<Lobby.SA_FriendChannelMove>);
    StartSA_FriendChannelMove(builder);
    return EndSA_FriendChannelMove(builder);
  }
}

public class SA_FriendChannelMoveT
{

  public SA_FriendChannelMoveT() {
  }
}


}
