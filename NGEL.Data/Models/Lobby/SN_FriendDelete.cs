// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Lobby
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct SN_FriendDelete : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_23_1_20(); }
  public static SN_FriendDelete GetRootAsSN_FriendDelete(ByteBuffer _bb) { return GetRootAsSN_FriendDelete(_bb, new SN_FriendDelete()); }
  public static SN_FriendDelete GetRootAsSN_FriendDelete(ByteBuffer _bb, SN_FriendDelete obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public SN_FriendDelete __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public int FriendUID { get { int o = __p.__offset(4); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }

  public static Offset<Lobby.SN_FriendDelete> CreateSN_FriendDelete(FlatBufferBuilder builder,
      int FriendUID = 0) {
    builder.StartTable(1);
    SN_FriendDelete.AddFriendUID(builder, FriendUID);
    return SN_FriendDelete.EndSN_FriendDelete(builder);
  }

  public static void StartSN_FriendDelete(FlatBufferBuilder builder) { builder.StartTable(1); }
  public static void AddFriendUID(FlatBufferBuilder builder, int FriendUID) { builder.AddInt(0, FriendUID, 0); }
  public static Offset<Lobby.SN_FriendDelete> EndSN_FriendDelete(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<Lobby.SN_FriendDelete>(o);
  }
  public SN_FriendDeleteT UnPack() {
    var _o = new SN_FriendDeleteT();
    this.UnPackTo(_o);
    return _o;
  }
  public void UnPackTo(SN_FriendDeleteT _o) {
    _o.FriendUID = this.FriendUID;
  }
  public static Offset<Lobby.SN_FriendDelete> Pack(FlatBufferBuilder builder, SN_FriendDeleteT _o) {
    if (_o == null) return default(Offset<Lobby.SN_FriendDelete>);
    return CreateSN_FriendDelete(
      builder,
      _o.FriendUID);
  }
}

public class SN_FriendDeleteT
{
  public int FriendUID { get; set; }

  public SN_FriendDeleteT() {
    this.FriendUID = 0;
  }
}


}
