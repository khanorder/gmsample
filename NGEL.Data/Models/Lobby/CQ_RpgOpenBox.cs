// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Lobby
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct CQ_RpgOpenBox : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_23_1_20(); }
  public static CQ_RpgOpenBox GetRootAsCQ_RpgOpenBox(ByteBuffer _bb) { return GetRootAsCQ_RpgOpenBox(_bb, new CQ_RpgOpenBox()); }
  public static CQ_RpgOpenBox GetRootAsCQ_RpgOpenBox(ByteBuffer _bb, CQ_RpgOpenBox obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public CQ_RpgOpenBox __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public int BoxID { get { int o = __p.__offset(4); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }

  public static Offset<Lobby.CQ_RpgOpenBox> CreateCQ_RpgOpenBox(FlatBufferBuilder builder,
      int BoxID = 0) {
    builder.StartTable(1);
    CQ_RpgOpenBox.AddBoxID(builder, BoxID);
    return CQ_RpgOpenBox.EndCQ_RpgOpenBox(builder);
  }

  public static void StartCQ_RpgOpenBox(FlatBufferBuilder builder) { builder.StartTable(1); }
  public static void AddBoxID(FlatBufferBuilder builder, int BoxID) { builder.AddInt(0, BoxID, 0); }
  public static Offset<Lobby.CQ_RpgOpenBox> EndCQ_RpgOpenBox(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<Lobby.CQ_RpgOpenBox>(o);
  }
  public CQ_RpgOpenBoxT UnPack() {
    var _o = new CQ_RpgOpenBoxT();
    this.UnPackTo(_o);
    return _o;
  }
  public void UnPackTo(CQ_RpgOpenBoxT _o) {
    _o.BoxID = this.BoxID;
  }
  public static Offset<Lobby.CQ_RpgOpenBox> Pack(FlatBufferBuilder builder, CQ_RpgOpenBoxT _o) {
    if (_o == null) return default(Offset<Lobby.CQ_RpgOpenBox>);
    return CreateCQ_RpgOpenBox(
      builder,
      _o.BoxID);
  }
}

public class CQ_RpgOpenBoxT
{
  public int BoxID { get; set; }

  public CQ_RpgOpenBoxT() {
    this.BoxID = 0;
  }
}


}
