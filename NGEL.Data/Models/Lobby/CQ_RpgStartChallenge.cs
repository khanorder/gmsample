// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Lobby
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct CQ_RpgStartChallenge : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_23_1_20(); }
  public static CQ_RpgStartChallenge GetRootAsCQ_RpgStartChallenge(ByteBuffer _bb) { return GetRootAsCQ_RpgStartChallenge(_bb, new CQ_RpgStartChallenge()); }
  public static CQ_RpgStartChallenge GetRootAsCQ_RpgStartChallenge(ByteBuffer _bb, CQ_RpgStartChallenge obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public CQ_RpgStartChallenge __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public int ChapterID { get { int o = __p.__offset(4); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public Lobby.RpgChallengeParameter? ParameterList(int j) { int o = __p.__offset(6); return o != 0 ? (Lobby.RpgChallengeParameter?)(new Lobby.RpgChallengeParameter()).__assign(__p.__vector(o) + j * 8, __p.bb) : null; }
  public int ParameterListLength { get { int o = __p.__offset(6); return o != 0 ? __p.__vector_len(o) : 0; } }

  public static Offset<Lobby.CQ_RpgStartChallenge> CreateCQ_RpgStartChallenge(FlatBufferBuilder builder,
      int ChapterID = 0,
      VectorOffset ParameterListOffset = default(VectorOffset)) {
    builder.StartTable(2);
    CQ_RpgStartChallenge.AddParameterList(builder, ParameterListOffset);
    CQ_RpgStartChallenge.AddChapterID(builder, ChapterID);
    return CQ_RpgStartChallenge.EndCQ_RpgStartChallenge(builder);
  }

  public static void StartCQ_RpgStartChallenge(FlatBufferBuilder builder) { builder.StartTable(2); }
  public static void AddChapterID(FlatBufferBuilder builder, int ChapterID) { builder.AddInt(0, ChapterID, 0); }
  public static void AddParameterList(FlatBufferBuilder builder, VectorOffset ParameterListOffset) { builder.AddOffset(1, ParameterListOffset.Value, 0); }
  public static void StartParameterListVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(8, numElems, 4); }
  public static Offset<Lobby.CQ_RpgStartChallenge> EndCQ_RpgStartChallenge(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<Lobby.CQ_RpgStartChallenge>(o);
  }
  public CQ_RpgStartChallengeT UnPack() {
    var _o = new CQ_RpgStartChallengeT();
    this.UnPackTo(_o);
    return _o;
  }
  public void UnPackTo(CQ_RpgStartChallengeT _o) {
    _o.ChapterID = this.ChapterID;
    _o.ParameterList = new List<Lobby.RpgChallengeParameterT>();
    for (var _j = 0; _j < this.ParameterListLength; ++_j) {_o.ParameterList.Add(this.ParameterList(_j).HasValue ? this.ParameterList(_j).Value.UnPack() : null);}
  }
  public static Offset<Lobby.CQ_RpgStartChallenge> Pack(FlatBufferBuilder builder, CQ_RpgStartChallengeT _o) {
    if (_o == null) return default(Offset<Lobby.CQ_RpgStartChallenge>);
    var _ParameterList = default(VectorOffset);
    if (_o.ParameterList != null) {
      StartParameterListVector(builder, _o.ParameterList.Count);
      for (var _j = _o.ParameterList.Count - 1; _j >= 0; --_j) { Lobby.RpgChallengeParameter.Pack(builder, _o.ParameterList[_j]); }
      _ParameterList = builder.EndVector();
    }
    return CreateCQ_RpgStartChallenge(
      builder,
      _o.ChapterID,
      _ParameterList);
  }
}

public class CQ_RpgStartChallengeT
{
  public int ChapterID { get; set; }
  public List<Lobby.RpgChallengeParameterT> ParameterList { get; set; }

  public CQ_RpgStartChallengeT() {
    this.ChapterID = 0;
    this.ParameterList = null;
  }
}


}
