// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Lobby
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct SA_CustomGameChangeFollow : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_23_1_20(); }
  public static SA_CustomGameChangeFollow GetRootAsSA_CustomGameChangeFollow(ByteBuffer _bb) { return GetRootAsSA_CustomGameChangeFollow(_bb, new SA_CustomGameChangeFollow()); }
  public static SA_CustomGameChangeFollow GetRootAsSA_CustomGameChangeFollow(ByteBuffer _bb, SA_CustomGameChangeFollow obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public SA_CustomGameChangeFollow __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public bool IsFollowOn { get { int o = __p.__offset(4); return o != 0 ? 0!=__p.bb.Get(o + __p.bb_pos) : (bool)false; } }

  public static Offset<Lobby.SA_CustomGameChangeFollow> CreateSA_CustomGameChangeFollow(FlatBufferBuilder builder,
      bool IsFollowOn = false) {
    builder.StartTable(1);
    SA_CustomGameChangeFollow.AddIsFollowOn(builder, IsFollowOn);
    return SA_CustomGameChangeFollow.EndSA_CustomGameChangeFollow(builder);
  }

  public static void StartSA_CustomGameChangeFollow(FlatBufferBuilder builder) { builder.StartTable(1); }
  public static void AddIsFollowOn(FlatBufferBuilder builder, bool IsFollowOn) { builder.AddBool(0, IsFollowOn, false); }
  public static Offset<Lobby.SA_CustomGameChangeFollow> EndSA_CustomGameChangeFollow(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<Lobby.SA_CustomGameChangeFollow>(o);
  }
  public SA_CustomGameChangeFollowT UnPack() {
    var _o = new SA_CustomGameChangeFollowT();
    this.UnPackTo(_o);
    return _o;
  }
  public void UnPackTo(SA_CustomGameChangeFollowT _o) {
    _o.IsFollowOn = this.IsFollowOn;
  }
  public static Offset<Lobby.SA_CustomGameChangeFollow> Pack(FlatBufferBuilder builder, SA_CustomGameChangeFollowT _o) {
    if (_o == null) return default(Offset<Lobby.SA_CustomGameChangeFollow>);
    return CreateSA_CustomGameChangeFollow(
      builder,
      _o.IsFollowOn);
  }
}

public class SA_CustomGameChangeFollowT
{
  public bool IsFollowOn { get; set; }

  public SA_CustomGameChangeFollowT() {
    this.IsFollowOn = false;
  }
}


}
