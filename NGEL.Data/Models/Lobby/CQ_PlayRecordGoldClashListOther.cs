// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Lobby
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct CQ_PlayRecordGoldClashListOther : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_23_1_20(); }
  public static CQ_PlayRecordGoldClashListOther GetRootAsCQ_PlayRecordGoldClashListOther(ByteBuffer _bb) { return GetRootAsCQ_PlayRecordGoldClashListOther(_bb, new CQ_PlayRecordGoldClashListOther()); }
  public static CQ_PlayRecordGoldClashListOther GetRootAsCQ_PlayRecordGoldClashListOther(ByteBuffer _bb, CQ_PlayRecordGoldClashListOther obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public CQ_PlayRecordGoldClashListOther __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public int TargetUID { get { int o = __p.__offset(4); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int SeasonID { get { int o = __p.__offset(6); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }

  public static Offset<Lobby.CQ_PlayRecordGoldClashListOther> CreateCQ_PlayRecordGoldClashListOther(FlatBufferBuilder builder,
      int TargetUID = 0,
      int SeasonID = 0) {
    builder.StartTable(2);
    CQ_PlayRecordGoldClashListOther.AddSeasonID(builder, SeasonID);
    CQ_PlayRecordGoldClashListOther.AddTargetUID(builder, TargetUID);
    return CQ_PlayRecordGoldClashListOther.EndCQ_PlayRecordGoldClashListOther(builder);
  }

  public static void StartCQ_PlayRecordGoldClashListOther(FlatBufferBuilder builder) { builder.StartTable(2); }
  public static void AddTargetUID(FlatBufferBuilder builder, int TargetUID) { builder.AddInt(0, TargetUID, 0); }
  public static void AddSeasonID(FlatBufferBuilder builder, int SeasonID) { builder.AddInt(1, SeasonID, 0); }
  public static Offset<Lobby.CQ_PlayRecordGoldClashListOther> EndCQ_PlayRecordGoldClashListOther(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<Lobby.CQ_PlayRecordGoldClashListOther>(o);
  }
  public CQ_PlayRecordGoldClashListOtherT UnPack() {
    var _o = new CQ_PlayRecordGoldClashListOtherT();
    this.UnPackTo(_o);
    return _o;
  }
  public void UnPackTo(CQ_PlayRecordGoldClashListOtherT _o) {
    _o.TargetUID = this.TargetUID;
    _o.SeasonID = this.SeasonID;
  }
  public static Offset<Lobby.CQ_PlayRecordGoldClashListOther> Pack(FlatBufferBuilder builder, CQ_PlayRecordGoldClashListOtherT _o) {
    if (_o == null) return default(Offset<Lobby.CQ_PlayRecordGoldClashListOther>);
    return CreateCQ_PlayRecordGoldClashListOther(
      builder,
      _o.TargetUID,
      _o.SeasonID);
  }
}

public class CQ_PlayRecordGoldClashListOtherT
{
  public int TargetUID { get; set; }
  public int SeasonID { get; set; }

  public CQ_PlayRecordGoldClashListOtherT() {
    this.TargetUID = 0;
    this.SeasonID = 0;
  }
}


}
