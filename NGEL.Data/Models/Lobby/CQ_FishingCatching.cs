// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Lobby
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct CQ_FishingCatching : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_23_1_20(); }
  public static CQ_FishingCatching GetRootAsCQ_FishingCatching(ByteBuffer _bb) { return GetRootAsCQ_FishingCatching(_bb, new CQ_FishingCatching()); }
  public static CQ_FishingCatching GetRootAsCQ_FishingCatching(ByteBuffer _bb, CQ_FishingCatching obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public CQ_FishingCatching __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public bool CatchingResult { get { int o = __p.__offset(4); return o != 0 ? 0!=__p.bb.Get(o + __p.bb_pos) : (bool)false; } }

  public static Offset<Lobby.CQ_FishingCatching> CreateCQ_FishingCatching(FlatBufferBuilder builder,
      bool CatchingResult = false) {
    builder.StartTable(1);
    CQ_FishingCatching.AddCatchingResult(builder, CatchingResult);
    return CQ_FishingCatching.EndCQ_FishingCatching(builder);
  }

  public static void StartCQ_FishingCatching(FlatBufferBuilder builder) { builder.StartTable(1); }
  public static void AddCatchingResult(FlatBufferBuilder builder, bool CatchingResult) { builder.AddBool(0, CatchingResult, false); }
  public static Offset<Lobby.CQ_FishingCatching> EndCQ_FishingCatching(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<Lobby.CQ_FishingCatching>(o);
  }
  public CQ_FishingCatchingT UnPack() {
    var _o = new CQ_FishingCatchingT();
    this.UnPackTo(_o);
    return _o;
  }
  public void UnPackTo(CQ_FishingCatchingT _o) {
    _o.CatchingResult = this.CatchingResult;
  }
  public static Offset<Lobby.CQ_FishingCatching> Pack(FlatBufferBuilder builder, CQ_FishingCatchingT _o) {
    if (_o == null) return default(Offset<Lobby.CQ_FishingCatching>);
    return CreateCQ_FishingCatching(
      builder,
      _o.CatchingResult);
  }
}

public class CQ_FishingCatchingT
{
  public bool CatchingResult { get; set; }

  public CQ_FishingCatchingT() {
    this.CatchingResult = false;
  }
}


}
