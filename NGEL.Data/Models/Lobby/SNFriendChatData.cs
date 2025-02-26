// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Lobby
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct SNFriendChatData : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_23_1_20(); }
  public static SNFriendChatData GetRootAsSNFriendChatData(ByteBuffer _bb) { return GetRootAsSNFriendChatData(_bb, new SNFriendChatData()); }
  public static SNFriendChatData GetRootAsSNFriendChatData(ByteBuffer _bb, SNFriendChatData obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public SNFriendChatData __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public int UID { get { int o = __p.__offset(4); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int FriendUID { get { int o = __p.__offset(6); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public long Serial { get { int o = __p.__offset(8); return o != 0 ? __p.bb.GetLong(o + __p.bb_pos) : (long)0; } }
  public string Message { get { int o = __p.__offset(10); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetMessageBytes() { return __p.__vector_as_span<byte>(10, 1); }
#else
  public ArraySegment<byte>? GetMessageBytes() { return __p.__vector_as_arraysegment(10); }
#endif
  public byte[] GetMessageArray() { return __p.__vector_as_array<byte>(10); }
  public int ChatAt { get { int o = __p.__offset(12); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }

  public static Offset<Lobby.SNFriendChatData> CreateSNFriendChatData(FlatBufferBuilder builder,
      int UID = 0,
      int FriendUID = 0,
      long Serial = 0,
      StringOffset MessageOffset = default(StringOffset),
      int ChatAt = 0) {
    builder.StartTable(5);
    SNFriendChatData.AddSerial(builder, Serial);
    SNFriendChatData.AddChatAt(builder, ChatAt);
    SNFriendChatData.AddMessage(builder, MessageOffset);
    SNFriendChatData.AddFriendUID(builder, FriendUID);
    SNFriendChatData.AddUID(builder, UID);
    return SNFriendChatData.EndSNFriendChatData(builder);
  }

  public static void StartSNFriendChatData(FlatBufferBuilder builder) { builder.StartTable(5); }
  public static void AddUID(FlatBufferBuilder builder, int UID) { builder.AddInt(0, UID, 0); }
  public static void AddFriendUID(FlatBufferBuilder builder, int FriendUID) { builder.AddInt(1, FriendUID, 0); }
  public static void AddSerial(FlatBufferBuilder builder, long Serial) { builder.AddLong(2, Serial, 0); }
  public static void AddMessage(FlatBufferBuilder builder, StringOffset MessageOffset) { builder.AddOffset(3, MessageOffset.Value, 0); }
  public static void AddChatAt(FlatBufferBuilder builder, int ChatAt) { builder.AddInt(4, ChatAt, 0); }
  public static Offset<Lobby.SNFriendChatData> EndSNFriendChatData(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<Lobby.SNFriendChatData>(o);
  }
  public SNFriendChatDataT UnPack() {
    var _o = new SNFriendChatDataT();
    this.UnPackTo(_o);
    return _o;
  }
  public void UnPackTo(SNFriendChatDataT _o) {
    _o.UID = this.UID;
    _o.FriendUID = this.FriendUID;
    _o.Serial = this.Serial;
    _o.Message = this.Message;
    _o.ChatAt = this.ChatAt;
  }
  public static Offset<Lobby.SNFriendChatData> Pack(FlatBufferBuilder builder, SNFriendChatDataT _o) {
    if (_o == null) return default(Offset<Lobby.SNFriendChatData>);
    var _Message = _o.Message == null ? default(StringOffset) : builder.CreateString(_o.Message);
    return CreateSNFriendChatData(
      builder,
      _o.UID,
      _o.FriendUID,
      _o.Serial,
      _Message,
      _o.ChatAt);
  }
}

public class SNFriendChatDataT
{
  public int UID { get; set; }
  public int FriendUID { get; set; }
  public long Serial { get; set; }
  public string Message { get; set; }
  public int ChatAt { get; set; }

  public SNFriendChatDataT() {
    this.UID = 0;
    this.FriendUID = 0;
    this.Serial = 0;
    this.Message = null;
    this.ChatAt = 0;
  }
}


}
