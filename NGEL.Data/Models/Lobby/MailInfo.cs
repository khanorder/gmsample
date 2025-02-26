// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Lobby
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct MailInfo : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_23_1_20(); }
  public static MailInfo GetRootAsMailInfo(ByteBuffer _bb) { return GetRootAsMailInfo(_bb, new MailInfo()); }
  public static MailInfo GetRootAsMailInfo(ByteBuffer _bb, MailInfo obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public MailInfo __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public long MailID { get { int o = __p.__offset(4); return o != 0 ? __p.bb.GetLong(o + __p.bb_pos) : (long)0; } }
  public Lobby.EMailType MailType { get { int o = __p.__offset(6); return o != 0 ? (Lobby.EMailType)__p.bb.Get(o + __p.bb_pos) : Lobby.EMailType.None; } }
  public Lobby.EMailStateType State { get { int o = __p.__offset(8); return o != 0 ? (Lobby.EMailStateType)__p.bb.Get(o + __p.bb_pos) : Lobby.EMailStateType.None; } }
  public bool IsBM { get { int o = __p.__offset(10); return o != 0 ? 0!=__p.bb.Get(o + __p.bb_pos) : (bool)false; } }
  public string Title { get { int o = __p.__offset(12); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetTitleBytes() { return __p.__vector_as_span<byte>(12, 1); }
#else
  public ArraySegment<byte>? GetTitleBytes() { return __p.__vector_as_arraysegment(12); }
#endif
  public byte[] GetTitleArray() { return __p.__vector_as_array<byte>(12); }
  public string Message { get { int o = __p.__offset(14); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetMessageBytes() { return __p.__vector_as_span<byte>(14, 1); }
#else
  public ArraySegment<byte>? GetMessageBytes() { return __p.__vector_as_arraysegment(14); }
#endif
  public byte[] GetMessageArray() { return __p.__vector_as_array<byte>(14); }
  public Lobby.MailRewardInfo? RewardList(int j) { int o = __p.__offset(16); return o != 0 ? (Lobby.MailRewardInfo?)(new Lobby.MailRewardInfo()).__assign(__p.__vector(o) + j * 12, __p.bb) : null; }
  public int RewardListLength { get { int o = __p.__offset(16); return o != 0 ? __p.__vector_len(o) : 0; } }
  public int ExpireAt { get { int o = __p.__offset(18); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int ArriveAt { get { int o = __p.__offset(20); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int ReceiveAt { get { int o = __p.__offset(22); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }

  public static Offset<Lobby.MailInfo> CreateMailInfo(FlatBufferBuilder builder,
      long MailID = 0,
      Lobby.EMailType MailType = Lobby.EMailType.None,
      Lobby.EMailStateType State = Lobby.EMailStateType.None,
      bool IsBM = false,
      StringOffset TitleOffset = default(StringOffset),
      StringOffset MessageOffset = default(StringOffset),
      VectorOffset RewardListOffset = default(VectorOffset),
      int ExpireAt = 0,
      int ArriveAt = 0,
      int ReceiveAt = 0) {
    builder.StartTable(10);
    MailInfo.AddMailID(builder, MailID);
    MailInfo.AddReceiveAt(builder, ReceiveAt);
    MailInfo.AddArriveAt(builder, ArriveAt);
    MailInfo.AddExpireAt(builder, ExpireAt);
    MailInfo.AddRewardList(builder, RewardListOffset);
    MailInfo.AddMessage(builder, MessageOffset);
    MailInfo.AddTitle(builder, TitleOffset);
    MailInfo.AddIsBM(builder, IsBM);
    MailInfo.AddState(builder, State);
    MailInfo.AddMailType(builder, MailType);
    return MailInfo.EndMailInfo(builder);
  }

  public static void StartMailInfo(FlatBufferBuilder builder) { builder.StartTable(10); }
  public static void AddMailID(FlatBufferBuilder builder, long MailID) { builder.AddLong(0, MailID, 0); }
  public static void AddMailType(FlatBufferBuilder builder, Lobby.EMailType MailType) { builder.AddByte(1, (byte)MailType, 0); }
  public static void AddState(FlatBufferBuilder builder, Lobby.EMailStateType State) { builder.AddByte(2, (byte)State, 0); }
  public static void AddIsBM(FlatBufferBuilder builder, bool IsBM) { builder.AddBool(3, IsBM, false); }
  public static void AddTitle(FlatBufferBuilder builder, StringOffset TitleOffset) { builder.AddOffset(4, TitleOffset.Value, 0); }
  public static void AddMessage(FlatBufferBuilder builder, StringOffset MessageOffset) { builder.AddOffset(5, MessageOffset.Value, 0); }
  public static void AddRewardList(FlatBufferBuilder builder, VectorOffset RewardListOffset) { builder.AddOffset(6, RewardListOffset.Value, 0); }
  public static void StartRewardListVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(12, numElems, 4); }
  public static void AddExpireAt(FlatBufferBuilder builder, int ExpireAt) { builder.AddInt(7, ExpireAt, 0); }
  public static void AddArriveAt(FlatBufferBuilder builder, int ArriveAt) { builder.AddInt(8, ArriveAt, 0); }
  public static void AddReceiveAt(FlatBufferBuilder builder, int ReceiveAt) { builder.AddInt(9, ReceiveAt, 0); }
  public static Offset<Lobby.MailInfo> EndMailInfo(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<Lobby.MailInfo>(o);
  }
  public MailInfoT UnPack() {
    var _o = new MailInfoT();
    this.UnPackTo(_o);
    return _o;
  }
  public void UnPackTo(MailInfoT _o) {
    _o.MailID = this.MailID;
    _o.MailType = this.MailType;
    _o.State = this.State;
    _o.IsBM = this.IsBM;
    _o.Title = this.Title;
    _o.Message = this.Message;
    _o.RewardList = new List<Lobby.MailRewardInfoT>();
    for (var _j = 0; _j < this.RewardListLength; ++_j) {_o.RewardList.Add(this.RewardList(_j).HasValue ? this.RewardList(_j).Value.UnPack() : null);}
    _o.ExpireAt = this.ExpireAt;
    _o.ArriveAt = this.ArriveAt;
    _o.ReceiveAt = this.ReceiveAt;
  }
  public static Offset<Lobby.MailInfo> Pack(FlatBufferBuilder builder, MailInfoT _o) {
    if (_o == null) return default(Offset<Lobby.MailInfo>);
    var _Title = _o.Title == null ? default(StringOffset) : builder.CreateString(_o.Title);
    var _Message = _o.Message == null ? default(StringOffset) : builder.CreateString(_o.Message);
    var _RewardList = default(VectorOffset);
    if (_o.RewardList != null) {
      StartRewardListVector(builder, _o.RewardList.Count);
      for (var _j = _o.RewardList.Count - 1; _j >= 0; --_j) { Lobby.MailRewardInfo.Pack(builder, _o.RewardList[_j]); }
      _RewardList = builder.EndVector();
    }
    return CreateMailInfo(
      builder,
      _o.MailID,
      _o.MailType,
      _o.State,
      _o.IsBM,
      _Title,
      _Message,
      _RewardList,
      _o.ExpireAt,
      _o.ArriveAt,
      _o.ReceiveAt);
  }
}

public class MailInfoT
{
  public long MailID { get; set; }
  public Lobby.EMailType MailType { get; set; }
  public Lobby.EMailStateType State { get; set; }
  public bool IsBM { get; set; }
  public string Title { get; set; }
  public string Message { get; set; }
  public List<Lobby.MailRewardInfoT> RewardList { get; set; }
  public int ExpireAt { get; set; }
  public int ArriveAt { get; set; }
  public int ReceiveAt { get; set; }

  public MailInfoT() {
    this.MailID = 0;
    this.MailType = Lobby.EMailType.None;
    this.State = Lobby.EMailStateType.None;
    this.IsBM = false;
    this.Title = null;
    this.Message = null;
    this.RewardList = null;
    this.ExpireAt = 0;
    this.ArriveAt = 0;
    this.ReceiveAt = 0;
  }
}


}
