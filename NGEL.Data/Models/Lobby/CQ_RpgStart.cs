// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Lobby
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct CQ_RpgStart : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_23_1_20(); }
  public static CQ_RpgStart GetRootAsCQ_RpgStart(ByteBuffer _bb) { return GetRootAsCQ_RpgStart(_bb, new CQ_RpgStart()); }
  public static CQ_RpgStart GetRootAsCQ_RpgStart(ByteBuffer _bb, CQ_RpgStart obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public CQ_RpgStart __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public int ChapterID { get { int o = __p.__offset(4); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }

  public static Offset<Lobby.CQ_RpgStart> CreateCQ_RpgStart(FlatBufferBuilder builder,
      int ChapterID = 0) {
    builder.StartTable(1);
    CQ_RpgStart.AddChapterID(builder, ChapterID);
    return CQ_RpgStart.EndCQ_RpgStart(builder);
  }

  public static void StartCQ_RpgStart(FlatBufferBuilder builder) { builder.StartTable(1); }
  public static void AddChapterID(FlatBufferBuilder builder, int ChapterID) { builder.AddInt(0, ChapterID, 0); }
  public static Offset<Lobby.CQ_RpgStart> EndCQ_RpgStart(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<Lobby.CQ_RpgStart>(o);
  }
  public CQ_RpgStartT UnPack() {
    var _o = new CQ_RpgStartT();
    this.UnPackTo(_o);
    return _o;
  }
  public void UnPackTo(CQ_RpgStartT _o) {
    _o.ChapterID = this.ChapterID;
  }
  public static Offset<Lobby.CQ_RpgStart> Pack(FlatBufferBuilder builder, CQ_RpgStartT _o) {
    if (_o == null) return default(Offset<Lobby.CQ_RpgStart>);
    return CreateCQ_RpgStart(
      builder,
      _o.ChapterID);
  }
}

public class CQ_RpgStartT
{
  public int ChapterID { get; set; }

  public CQ_RpgStartT() {
    this.ChapterID = 0;
  }
}


}
