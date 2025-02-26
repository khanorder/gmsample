// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Lobby
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct SA_PlayRecordRpgListOther : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_23_1_20(); }
  public static SA_PlayRecordRpgListOther GetRootAsSA_PlayRecordRpgListOther(ByteBuffer _bb) { return GetRootAsSA_PlayRecordRpgListOther(_bb, new SA_PlayRecordRpgListOther()); }
  public static SA_PlayRecordRpgListOther GetRootAsSA_PlayRecordRpgListOther(ByteBuffer _bb, SA_PlayRecordRpgListOther obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public SA_PlayRecordRpgListOther __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public int TargetUID { get { int o = __p.__offset(4); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int ChapterID { get { int o = __p.__offset(6); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public Lobby.PlayRecordRpgData? RecordList(int j) { int o = __p.__offset(8); return o != 0 ? (Lobby.PlayRecordRpgData?)(new Lobby.PlayRecordRpgData()).__assign(__p.__vector(o) + j * 32, __p.bb) : null; }
  public int RecordListLength { get { int o = __p.__offset(8); return o != 0 ? __p.__vector_len(o) : 0; } }

  public static Offset<Lobby.SA_PlayRecordRpgListOther> CreateSA_PlayRecordRpgListOther(FlatBufferBuilder builder,
      int TargetUID = 0,
      int ChapterID = 0,
      VectorOffset RecordListOffset = default(VectorOffset)) {
    builder.StartTable(3);
    SA_PlayRecordRpgListOther.AddRecordList(builder, RecordListOffset);
    SA_PlayRecordRpgListOther.AddChapterID(builder, ChapterID);
    SA_PlayRecordRpgListOther.AddTargetUID(builder, TargetUID);
    return SA_PlayRecordRpgListOther.EndSA_PlayRecordRpgListOther(builder);
  }

  public static void StartSA_PlayRecordRpgListOther(FlatBufferBuilder builder) { builder.StartTable(3); }
  public static void AddTargetUID(FlatBufferBuilder builder, int TargetUID) { builder.AddInt(0, TargetUID, 0); }
  public static void AddChapterID(FlatBufferBuilder builder, int ChapterID) { builder.AddInt(1, ChapterID, 0); }
  public static void AddRecordList(FlatBufferBuilder builder, VectorOffset RecordListOffset) { builder.AddOffset(2, RecordListOffset.Value, 0); }
  public static void StartRecordListVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(32, numElems, 4); }
  public static Offset<Lobby.SA_PlayRecordRpgListOther> EndSA_PlayRecordRpgListOther(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<Lobby.SA_PlayRecordRpgListOther>(o);
  }
  public SA_PlayRecordRpgListOtherT UnPack() {
    var _o = new SA_PlayRecordRpgListOtherT();
    this.UnPackTo(_o);
    return _o;
  }
  public void UnPackTo(SA_PlayRecordRpgListOtherT _o) {
    _o.TargetUID = this.TargetUID;
    _o.ChapterID = this.ChapterID;
    _o.RecordList = new List<Lobby.PlayRecordRpgDataT>();
    for (var _j = 0; _j < this.RecordListLength; ++_j) {_o.RecordList.Add(this.RecordList(_j).HasValue ? this.RecordList(_j).Value.UnPack() : null);}
  }
  public static Offset<Lobby.SA_PlayRecordRpgListOther> Pack(FlatBufferBuilder builder, SA_PlayRecordRpgListOtherT _o) {
    if (_o == null) return default(Offset<Lobby.SA_PlayRecordRpgListOther>);
    var _RecordList = default(VectorOffset);
    if (_o.RecordList != null) {
      StartRecordListVector(builder, _o.RecordList.Count);
      for (var _j = _o.RecordList.Count - 1; _j >= 0; --_j) { Lobby.PlayRecordRpgData.Pack(builder, _o.RecordList[_j]); }
      _RecordList = builder.EndVector();
    }
    return CreateSA_PlayRecordRpgListOther(
      builder,
      _o.TargetUID,
      _o.ChapterID,
      _RecordList);
  }
}

public class SA_PlayRecordRpgListOtherT
{
  public int TargetUID { get; set; }
  public int ChapterID { get; set; }
  public List<Lobby.PlayRecordRpgDataT> RecordList { get; set; }

  public SA_PlayRecordRpgListOtherT() {
    this.TargetUID = 0;
    this.ChapterID = 0;
    this.RecordList = null;
  }
}


}
