// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Lobby
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct CQ_UserIntroduceChange : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_23_1_20(); }
  public static CQ_UserIntroduceChange GetRootAsCQ_UserIntroduceChange(ByteBuffer _bb) { return GetRootAsCQ_UserIntroduceChange(_bb, new CQ_UserIntroduceChange()); }
  public static CQ_UserIntroduceChange GetRootAsCQ_UserIntroduceChange(ByteBuffer _bb, CQ_UserIntroduceChange obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public CQ_UserIntroduceChange __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public int IntroduceID { get { int o = __p.__offset(4); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }

  public static Offset<Lobby.CQ_UserIntroduceChange> CreateCQ_UserIntroduceChange(FlatBufferBuilder builder,
      int IntroduceID = 0) {
    builder.StartTable(1);
    CQ_UserIntroduceChange.AddIntroduceID(builder, IntroduceID);
    return CQ_UserIntroduceChange.EndCQ_UserIntroduceChange(builder);
  }

  public static void StartCQ_UserIntroduceChange(FlatBufferBuilder builder) { builder.StartTable(1); }
  public static void AddIntroduceID(FlatBufferBuilder builder, int IntroduceID) { builder.AddInt(0, IntroduceID, 0); }
  public static Offset<Lobby.CQ_UserIntroduceChange> EndCQ_UserIntroduceChange(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<Lobby.CQ_UserIntroduceChange>(o);
  }
  public CQ_UserIntroduceChangeT UnPack() {
    var _o = new CQ_UserIntroduceChangeT();
    this.UnPackTo(_o);
    return _o;
  }
  public void UnPackTo(CQ_UserIntroduceChangeT _o) {
    _o.IntroduceID = this.IntroduceID;
  }
  public static Offset<Lobby.CQ_UserIntroduceChange> Pack(FlatBufferBuilder builder, CQ_UserIntroduceChangeT _o) {
    if (_o == null) return default(Offset<Lobby.CQ_UserIntroduceChange>);
    return CreateCQ_UserIntroduceChange(
      builder,
      _o.IntroduceID);
  }
}

public class CQ_UserIntroduceChangeT
{
  public int IntroduceID { get; set; }

  public CQ_UserIntroduceChangeT() {
    this.IntroduceID = 0;
  }
}


}
