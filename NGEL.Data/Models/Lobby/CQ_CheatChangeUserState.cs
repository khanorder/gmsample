// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Lobby
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct CQ_CheatChangeUserState : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_23_1_20(); }
  public static CQ_CheatChangeUserState GetRootAsCQ_CheatChangeUserState(ByteBuffer _bb) { return GetRootAsCQ_CheatChangeUserState(_bb, new CQ_CheatChangeUserState()); }
  public static CQ_CheatChangeUserState GetRootAsCQ_CheatChangeUserState(ByteBuffer _bb, CQ_CheatChangeUserState obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public CQ_CheatChangeUserState __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public Lobby.EUserState UserState { get { int o = __p.__offset(4); return o != 0 ? (Lobby.EUserState)__p.bb.Get(o + __p.bb_pos) : Lobby.EUserState.None; } }
  public bool IsSetRedis { get { int o = __p.__offset(6); return o != 0 ? 0!=__p.bb.Get(o + __p.bb_pos) : (bool)false; } }
  public bool IsSendToFriends { get { int o = __p.__offset(8); return o != 0 ? 0!=__p.bb.Get(o + __p.bb_pos) : (bool)false; } }

  public static Offset<Lobby.CQ_CheatChangeUserState> CreateCQ_CheatChangeUserState(FlatBufferBuilder builder,
      Lobby.EUserState UserState = Lobby.EUserState.None,
      bool IsSetRedis = false,
      bool IsSendToFriends = false) {
    builder.StartTable(3);
    CQ_CheatChangeUserState.AddIsSendToFriends(builder, IsSendToFriends);
    CQ_CheatChangeUserState.AddIsSetRedis(builder, IsSetRedis);
    CQ_CheatChangeUserState.AddUserState(builder, UserState);
    return CQ_CheatChangeUserState.EndCQ_CheatChangeUserState(builder);
  }

  public static void StartCQ_CheatChangeUserState(FlatBufferBuilder builder) { builder.StartTable(3); }
  public static void AddUserState(FlatBufferBuilder builder, Lobby.EUserState UserState) { builder.AddByte(0, (byte)UserState, 0); }
  public static void AddIsSetRedis(FlatBufferBuilder builder, bool IsSetRedis) { builder.AddBool(1, IsSetRedis, false); }
  public static void AddIsSendToFriends(FlatBufferBuilder builder, bool IsSendToFriends) { builder.AddBool(2, IsSendToFriends, false); }
  public static Offset<Lobby.CQ_CheatChangeUserState> EndCQ_CheatChangeUserState(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<Lobby.CQ_CheatChangeUserState>(o);
  }
  public CQ_CheatChangeUserStateT UnPack() {
    var _o = new CQ_CheatChangeUserStateT();
    this.UnPackTo(_o);
    return _o;
  }
  public void UnPackTo(CQ_CheatChangeUserStateT _o) {
    _o.UserState = this.UserState;
    _o.IsSetRedis = this.IsSetRedis;
    _o.IsSendToFriends = this.IsSendToFriends;
  }
  public static Offset<Lobby.CQ_CheatChangeUserState> Pack(FlatBufferBuilder builder, CQ_CheatChangeUserStateT _o) {
    if (_o == null) return default(Offset<Lobby.CQ_CheatChangeUserState>);
    return CreateCQ_CheatChangeUserState(
      builder,
      _o.UserState,
      _o.IsSetRedis,
      _o.IsSendToFriends);
  }
}

public class CQ_CheatChangeUserStateT
{
  public Lobby.EUserState UserState { get; set; }
  public bool IsSetRedis { get; set; }
  public bool IsSendToFriends { get; set; }

  public CQ_CheatChangeUserStateT() {
    this.UserState = Lobby.EUserState.None;
    this.IsSetRedis = false;
    this.IsSendToFriends = false;
  }
}


}
