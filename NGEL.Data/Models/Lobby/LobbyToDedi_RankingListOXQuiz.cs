// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Lobby
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct LobbyToDedi_RankingListOXQuiz : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_23_1_20(); }
  public static LobbyToDedi_RankingListOXQuiz GetRootAsLobbyToDedi_RankingListOXQuiz(ByteBuffer _bb) { return GetRootAsLobbyToDedi_RankingListOXQuiz(_bb, new LobbyToDedi_RankingListOXQuiz()); }
  public static LobbyToDedi_RankingListOXQuiz GetRootAsLobbyToDedi_RankingListOXQuiz(ByteBuffer _bb, LobbyToDedi_RankingListOXQuiz obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public LobbyToDedi_RankingListOXQuiz __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public Lobby.OXQuizRankingInfo? RankingList(int j) { int o = __p.__offset(4); return o != 0 ? (Lobby.OXQuizRankingInfo?)(new Lobby.OXQuizRankingInfo()).__assign(__p.__indirect(__p.__vector(o) + j * 4), __p.bb) : null; }
  public int RankingListLength { get { int o = __p.__offset(4); return o != 0 ? __p.__vector_len(o) : 0; } }

  public static Offset<Lobby.LobbyToDedi_RankingListOXQuiz> CreateLobbyToDedi_RankingListOXQuiz(FlatBufferBuilder builder,
      VectorOffset RankingListOffset = default(VectorOffset)) {
    builder.StartTable(1);
    LobbyToDedi_RankingListOXQuiz.AddRankingList(builder, RankingListOffset);
    return LobbyToDedi_RankingListOXQuiz.EndLobbyToDedi_RankingListOXQuiz(builder);
  }

  public static void StartLobbyToDedi_RankingListOXQuiz(FlatBufferBuilder builder) { builder.StartTable(1); }
  public static void AddRankingList(FlatBufferBuilder builder, VectorOffset RankingListOffset) { builder.AddOffset(0, RankingListOffset.Value, 0); }
  public static VectorOffset CreateRankingListVector(FlatBufferBuilder builder, Offset<Lobby.OXQuizRankingInfo>[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddOffset(data[i].Value); return builder.EndVector(); }
  public static VectorOffset CreateRankingListVectorBlock(FlatBufferBuilder builder, Offset<Lobby.OXQuizRankingInfo>[] data) { builder.StartVector(4, data.Length, 4); builder.Add(data); return builder.EndVector(); }
  public static VectorOffset CreateRankingListVectorBlock(FlatBufferBuilder builder, ArraySegment<Offset<Lobby.OXQuizRankingInfo>> data) { builder.StartVector(4, data.Count, 4); builder.Add(data); return builder.EndVector(); }
  public static VectorOffset CreateRankingListVectorBlock(FlatBufferBuilder builder, IntPtr dataPtr, int sizeInBytes) { builder.StartVector(1, sizeInBytes, 1); builder.Add<Offset<Lobby.OXQuizRankingInfo>>(dataPtr, sizeInBytes); return builder.EndVector(); }
  public static void StartRankingListVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static Offset<Lobby.LobbyToDedi_RankingListOXQuiz> EndLobbyToDedi_RankingListOXQuiz(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<Lobby.LobbyToDedi_RankingListOXQuiz>(o);
  }
  public LobbyToDedi_RankingListOXQuizT UnPack() {
    var _o = new LobbyToDedi_RankingListOXQuizT();
    this.UnPackTo(_o);
    return _o;
  }
  public void UnPackTo(LobbyToDedi_RankingListOXQuizT _o) {
    _o.RankingList = new List<Lobby.OXQuizRankingInfoT>();
    for (var _j = 0; _j < this.RankingListLength; ++_j) {_o.RankingList.Add(this.RankingList(_j).HasValue ? this.RankingList(_j).Value.UnPack() : null);}
  }
  public static Offset<Lobby.LobbyToDedi_RankingListOXQuiz> Pack(FlatBufferBuilder builder, LobbyToDedi_RankingListOXQuizT _o) {
    if (_o == null) return default(Offset<Lobby.LobbyToDedi_RankingListOXQuiz>);
    var _RankingList = default(VectorOffset);
    if (_o.RankingList != null) {
      var __RankingList = new Offset<Lobby.OXQuizRankingInfo>[_o.RankingList.Count];
      for (var _j = 0; _j < __RankingList.Length; ++_j) { __RankingList[_j] = Lobby.OXQuizRankingInfo.Pack(builder, _o.RankingList[_j]); }
      _RankingList = CreateRankingListVector(builder, __RankingList);
    }
    return CreateLobbyToDedi_RankingListOXQuiz(
      builder,
      _RankingList);
  }
}

public class LobbyToDedi_RankingListOXQuizT
{
  public List<Lobby.OXQuizRankingInfoT> RankingList { get; set; }

  public LobbyToDedi_RankingListOXQuizT() {
    this.RankingList = null;
  }
}


}
