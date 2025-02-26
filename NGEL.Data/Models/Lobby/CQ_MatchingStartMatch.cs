// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Lobby
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct CQ_MatchingStartMatch : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_23_1_20(); }
  public static CQ_MatchingStartMatch GetRootAsCQ_MatchingStartMatch(ByteBuffer _bb) { return GetRootAsCQ_MatchingStartMatch(_bb, new CQ_MatchingStartMatch()); }
  public static CQ_MatchingStartMatch GetRootAsCQ_MatchingStartMatch(ByteBuffer _bb, CQ_MatchingStartMatch obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public CQ_MatchingStartMatch __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public int MatchingTableID { get { int o = __p.__offset(4); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public Lobby.MatchingTeamInfo? TeamList(int j) { int o = __p.__offset(6); return o != 0 ? (Lobby.MatchingTeamInfo?)(new Lobby.MatchingTeamInfo()).__assign(__p.__indirect(__p.__vector(o) + j * 4), __p.bb) : null; }
  public int TeamListLength { get { int o = __p.__offset(6); return o != 0 ? __p.__vector_len(o) : 0; } }
  public int Ping { get { int o = __p.__offset(8); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }

  public static Offset<Lobby.CQ_MatchingStartMatch> CreateCQ_MatchingStartMatch(FlatBufferBuilder builder,
      int MatchingTableID = 0,
      VectorOffset TeamListOffset = default(VectorOffset),
      int Ping = 0) {
    builder.StartTable(3);
    CQ_MatchingStartMatch.AddPing(builder, Ping);
    CQ_MatchingStartMatch.AddTeamList(builder, TeamListOffset);
    CQ_MatchingStartMatch.AddMatchingTableID(builder, MatchingTableID);
    return CQ_MatchingStartMatch.EndCQ_MatchingStartMatch(builder);
  }

  public static void StartCQ_MatchingStartMatch(FlatBufferBuilder builder) { builder.StartTable(3); }
  public static void AddMatchingTableID(FlatBufferBuilder builder, int MatchingTableID) { builder.AddInt(0, MatchingTableID, 0); }
  public static void AddTeamList(FlatBufferBuilder builder, VectorOffset TeamListOffset) { builder.AddOffset(1, TeamListOffset.Value, 0); }
  public static VectorOffset CreateTeamListVector(FlatBufferBuilder builder, Offset<Lobby.MatchingTeamInfo>[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddOffset(data[i].Value); return builder.EndVector(); }
  public static VectorOffset CreateTeamListVectorBlock(FlatBufferBuilder builder, Offset<Lobby.MatchingTeamInfo>[] data) { builder.StartVector(4, data.Length, 4); builder.Add(data); return builder.EndVector(); }
  public static VectorOffset CreateTeamListVectorBlock(FlatBufferBuilder builder, ArraySegment<Offset<Lobby.MatchingTeamInfo>> data) { builder.StartVector(4, data.Count, 4); builder.Add(data); return builder.EndVector(); }
  public static VectorOffset CreateTeamListVectorBlock(FlatBufferBuilder builder, IntPtr dataPtr, int sizeInBytes) { builder.StartVector(1, sizeInBytes, 1); builder.Add<Offset<Lobby.MatchingTeamInfo>>(dataPtr, sizeInBytes); return builder.EndVector(); }
  public static void StartTeamListVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static void AddPing(FlatBufferBuilder builder, int Ping) { builder.AddInt(2, Ping, 0); }
  public static Offset<Lobby.CQ_MatchingStartMatch> EndCQ_MatchingStartMatch(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<Lobby.CQ_MatchingStartMatch>(o);
  }
  public CQ_MatchingStartMatchT UnPack() {
    var _o = new CQ_MatchingStartMatchT();
    this.UnPackTo(_o);
    return _o;
  }
  public void UnPackTo(CQ_MatchingStartMatchT _o) {
    _o.MatchingTableID = this.MatchingTableID;
    _o.TeamList = new List<Lobby.MatchingTeamInfoT>();
    for (var _j = 0; _j < this.TeamListLength; ++_j) {_o.TeamList.Add(this.TeamList(_j).HasValue ? this.TeamList(_j).Value.UnPack() : null);}
    _o.Ping = this.Ping;
  }
  public static Offset<Lobby.CQ_MatchingStartMatch> Pack(FlatBufferBuilder builder, CQ_MatchingStartMatchT _o) {
    if (_o == null) return default(Offset<Lobby.CQ_MatchingStartMatch>);
    var _TeamList = default(VectorOffset);
    if (_o.TeamList != null) {
      var __TeamList = new Offset<Lobby.MatchingTeamInfo>[_o.TeamList.Count];
      for (var _j = 0; _j < __TeamList.Length; ++_j) { __TeamList[_j] = Lobby.MatchingTeamInfo.Pack(builder, _o.TeamList[_j]); }
      _TeamList = CreateTeamListVector(builder, __TeamList);
    }
    return CreateCQ_MatchingStartMatch(
      builder,
      _o.MatchingTableID,
      _TeamList,
      _o.Ping);
  }
}

public class CQ_MatchingStartMatchT
{
  public int MatchingTableID { get; set; }
  public List<Lobby.MatchingTeamInfoT> TeamList { get; set; }
  public int Ping { get; set; }

  public CQ_MatchingStartMatchT() {
    this.MatchingTableID = 0;
    this.TeamList = null;
    this.Ping = 0;
  }
}


}
