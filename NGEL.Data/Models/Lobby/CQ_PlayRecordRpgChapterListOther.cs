// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Lobby
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct CQ_PlayRecordRpgChapterListOther : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_23_1_20(); }
  public static CQ_PlayRecordRpgChapterListOther GetRootAsCQ_PlayRecordRpgChapterListOther(ByteBuffer _bb) { return GetRootAsCQ_PlayRecordRpgChapterListOther(_bb, new CQ_PlayRecordRpgChapterListOther()); }
  public static CQ_PlayRecordRpgChapterListOther GetRootAsCQ_PlayRecordRpgChapterListOther(ByteBuffer _bb, CQ_PlayRecordRpgChapterListOther obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public CQ_PlayRecordRpgChapterListOther __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public int TargetUID { get { int o = __p.__offset(4); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }

  public static Offset<Lobby.CQ_PlayRecordRpgChapterListOther> CreateCQ_PlayRecordRpgChapterListOther(FlatBufferBuilder builder,
      int TargetUID = 0) {
    builder.StartTable(1);
    CQ_PlayRecordRpgChapterListOther.AddTargetUID(builder, TargetUID);
    return CQ_PlayRecordRpgChapterListOther.EndCQ_PlayRecordRpgChapterListOther(builder);
  }

  public static void StartCQ_PlayRecordRpgChapterListOther(FlatBufferBuilder builder) { builder.StartTable(1); }
  public static void AddTargetUID(FlatBufferBuilder builder, int TargetUID) { builder.AddInt(0, TargetUID, 0); }
  public static Offset<Lobby.CQ_PlayRecordRpgChapterListOther> EndCQ_PlayRecordRpgChapterListOther(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<Lobby.CQ_PlayRecordRpgChapterListOther>(o);
  }
  public CQ_PlayRecordRpgChapterListOtherT UnPack() {
    var _o = new CQ_PlayRecordRpgChapterListOtherT();
    this.UnPackTo(_o);
    return _o;
  }
  public void UnPackTo(CQ_PlayRecordRpgChapterListOtherT _o) {
    _o.TargetUID = this.TargetUID;
  }
  public static Offset<Lobby.CQ_PlayRecordRpgChapterListOther> Pack(FlatBufferBuilder builder, CQ_PlayRecordRpgChapterListOtherT _o) {
    if (_o == null) return default(Offset<Lobby.CQ_PlayRecordRpgChapterListOther>);
    return CreateCQ_PlayRecordRpgChapterListOther(
      builder,
      _o.TargetUID);
  }
}

public class CQ_PlayRecordRpgChapterListOtherT
{
  public int TargetUID { get; set; }

  public CQ_PlayRecordRpgChapterListOtherT() {
    this.TargetUID = 0;
  }
}


}
