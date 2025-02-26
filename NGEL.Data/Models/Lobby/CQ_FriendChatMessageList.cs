// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Lobby
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct CQ_FriendChatMessageList : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_23_1_20(); }
  public static CQ_FriendChatMessageList GetRootAsCQ_FriendChatMessageList(ByteBuffer _bb) { return GetRootAsCQ_FriendChatMessageList(_bb, new CQ_FriendChatMessageList()); }
  public static CQ_FriendChatMessageList GetRootAsCQ_FriendChatMessageList(ByteBuffer _bb, CQ_FriendChatMessageList obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public CQ_FriendChatMessageList __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public int FriendUID { get { int o = __p.__offset(4); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }

  public static Offset<Lobby.CQ_FriendChatMessageList> CreateCQ_FriendChatMessageList(FlatBufferBuilder builder,
      int FriendUID = 0) {
    builder.StartTable(1);
    CQ_FriendChatMessageList.AddFriendUID(builder, FriendUID);
    return CQ_FriendChatMessageList.EndCQ_FriendChatMessageList(builder);
  }

  public static void StartCQ_FriendChatMessageList(FlatBufferBuilder builder) { builder.StartTable(1); }
  public static void AddFriendUID(FlatBufferBuilder builder, int FriendUID) { builder.AddInt(0, FriendUID, 0); }
  public static Offset<Lobby.CQ_FriendChatMessageList> EndCQ_FriendChatMessageList(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<Lobby.CQ_FriendChatMessageList>(o);
  }
  public CQ_FriendChatMessageListT UnPack() {
    var _o = new CQ_FriendChatMessageListT();
    this.UnPackTo(_o);
    return _o;
  }
  public void UnPackTo(CQ_FriendChatMessageListT _o) {
    _o.FriendUID = this.FriendUID;
  }
  public static Offset<Lobby.CQ_FriendChatMessageList> Pack(FlatBufferBuilder builder, CQ_FriendChatMessageListT _o) {
    if (_o == null) return default(Offset<Lobby.CQ_FriendChatMessageList>);
    return CreateCQ_FriendChatMessageList(
      builder,
      _o.FriendUID);
  }
}

public class CQ_FriendChatMessageListT
{
  public int FriendUID { get; set; }

  public CQ_FriendChatMessageListT() {
    this.FriendUID = 0;
  }
}


}
