// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Lobby
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct SN_MatchingRequireAcceptance : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_23_1_20(); }
  public static SN_MatchingRequireAcceptance GetRootAsSN_MatchingRequireAcceptance(ByteBuffer _bb) { return GetRootAsSN_MatchingRequireAcceptance(_bb, new SN_MatchingRequireAcceptance()); }
  public static SN_MatchingRequireAcceptance GetRootAsSN_MatchingRequireAcceptance(ByteBuffer _bb, SN_MatchingRequireAcceptance obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public SN_MatchingRequireAcceptance __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public int MatchingTableID { get { int o = __p.__offset(4); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public string MatchTicket { get { int o = __p.__offset(6); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetMatchTicketBytes() { return __p.__vector_as_span<byte>(6, 1); }
#else
  public ArraySegment<byte>? GetMatchTicketBytes() { return __p.__vector_as_arraysegment(6); }
#endif
  public byte[] GetMatchTicketArray() { return __p.__vector_as_array<byte>(6); }
  public string MatchID { get { int o = __p.__offset(8); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetMatchIDBytes() { return __p.__vector_as_span<byte>(8, 1); }
#else
  public ArraySegment<byte>? GetMatchIDBytes() { return __p.__vector_as_arraysegment(8); }
#endif
  public byte[] GetMatchIDArray() { return __p.__vector_as_array<byte>(8); }
  public int AICount { get { int o = __p.__offset(10); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public Lobby.MatchingAcceptanceUser? UserList(int j) { int o = __p.__offset(12); return o != 0 ? (Lobby.MatchingAcceptanceUser?)(new Lobby.MatchingAcceptanceUser()).__assign(__p.__vector(o) + j * 8, __p.bb) : null; }
  public int UserListLength { get { int o = __p.__offset(12); return o != 0 ? __p.__vector_len(o) : 0; } }

  public static Offset<Lobby.SN_MatchingRequireAcceptance> CreateSN_MatchingRequireAcceptance(FlatBufferBuilder builder,
      int MatchingTableID = 0,
      StringOffset MatchTicketOffset = default(StringOffset),
      StringOffset MatchIDOffset = default(StringOffset),
      int AICount = 0,
      VectorOffset UserListOffset = default(VectorOffset)) {
    builder.StartTable(5);
    SN_MatchingRequireAcceptance.AddUserList(builder, UserListOffset);
    SN_MatchingRequireAcceptance.AddAICount(builder, AICount);
    SN_MatchingRequireAcceptance.AddMatchID(builder, MatchIDOffset);
    SN_MatchingRequireAcceptance.AddMatchTicket(builder, MatchTicketOffset);
    SN_MatchingRequireAcceptance.AddMatchingTableID(builder, MatchingTableID);
    return SN_MatchingRequireAcceptance.EndSN_MatchingRequireAcceptance(builder);
  }

  public static void StartSN_MatchingRequireAcceptance(FlatBufferBuilder builder) { builder.StartTable(5); }
  public static void AddMatchingTableID(FlatBufferBuilder builder, int MatchingTableID) { builder.AddInt(0, MatchingTableID, 0); }
  public static void AddMatchTicket(FlatBufferBuilder builder, StringOffset MatchTicketOffset) { builder.AddOffset(1, MatchTicketOffset.Value, 0); }
  public static void AddMatchID(FlatBufferBuilder builder, StringOffset MatchIDOffset) { builder.AddOffset(2, MatchIDOffset.Value, 0); }
  public static void AddAICount(FlatBufferBuilder builder, int AICount) { builder.AddInt(3, AICount, 0); }
  public static void AddUserList(FlatBufferBuilder builder, VectorOffset UserListOffset) { builder.AddOffset(4, UserListOffset.Value, 0); }
  public static void StartUserListVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(8, numElems, 4); }
  public static Offset<Lobby.SN_MatchingRequireAcceptance> EndSN_MatchingRequireAcceptance(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<Lobby.SN_MatchingRequireAcceptance>(o);
  }
  public SN_MatchingRequireAcceptanceT UnPack() {
    var _o = new SN_MatchingRequireAcceptanceT();
    this.UnPackTo(_o);
    return _o;
  }
  public void UnPackTo(SN_MatchingRequireAcceptanceT _o) {
    _o.MatchingTableID = this.MatchingTableID;
    _o.MatchTicket = this.MatchTicket;
    _o.MatchID = this.MatchID;
    _o.AICount = this.AICount;
    _o.UserList = new List<Lobby.MatchingAcceptanceUserT>();
    for (var _j = 0; _j < this.UserListLength; ++_j) {_o.UserList.Add(this.UserList(_j).HasValue ? this.UserList(_j).Value.UnPack() : null);}
  }
  public static Offset<Lobby.SN_MatchingRequireAcceptance> Pack(FlatBufferBuilder builder, SN_MatchingRequireAcceptanceT _o) {
    if (_o == null) return default(Offset<Lobby.SN_MatchingRequireAcceptance>);
    var _MatchTicket = _o.MatchTicket == null ? default(StringOffset) : builder.CreateString(_o.MatchTicket);
    var _MatchID = _o.MatchID == null ? default(StringOffset) : builder.CreateString(_o.MatchID);
    var _UserList = default(VectorOffset);
    if (_o.UserList != null) {
      StartUserListVector(builder, _o.UserList.Count);
      for (var _j = _o.UserList.Count - 1; _j >= 0; --_j) { Lobby.MatchingAcceptanceUser.Pack(builder, _o.UserList[_j]); }
      _UserList = builder.EndVector();
    }
    return CreateSN_MatchingRequireAcceptance(
      builder,
      _o.MatchingTableID,
      _MatchTicket,
      _MatchID,
      _o.AICount,
      _UserList);
  }
}

public class SN_MatchingRequireAcceptanceT
{
  public int MatchingTableID { get; set; }
  public string MatchTicket { get; set; }
  public string MatchID { get; set; }
  public int AICount { get; set; }
  public List<Lobby.MatchingAcceptanceUserT> UserList { get; set; }

  public SN_MatchingRequireAcceptanceT() {
    this.MatchingTableID = 0;
    this.MatchTicket = null;
    this.MatchID = null;
    this.AICount = 0;
    this.UserList = null;
  }
}


}
