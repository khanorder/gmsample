// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Lobby
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct GuideMissionData : IFlatbufferObject
{
  private Struct __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public void __init(int _i, ByteBuffer _bb) { __p = new Struct(_i, _bb); }
  public GuideMissionData __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public Lobby.EGuideMissionCategory GuideMissionCategory { get { return (Lobby.EGuideMissionCategory)__p.bb.Get(__p.bb_pos + 0); } }
  public int MissionID { get { return __p.bb.GetInt(__p.bb_pos + 4); } }
  public bool IsCompleted { get { return 0!=__p.bb.Get(__p.bb_pos + 8); } }
  public bool IsRewarded { get { return 0!=__p.bb.Get(__p.bb_pos + 9); } }

  public static Offset<Lobby.GuideMissionData> CreateGuideMissionData(FlatBufferBuilder builder, Lobby.EGuideMissionCategory GuideMissionCategory, int MissionID, bool IsCompleted, bool IsRewarded) {
    builder.Prep(4, 12);
    builder.Pad(2);
    builder.PutBool(IsRewarded);
    builder.PutBool(IsCompleted);
    builder.PutInt(MissionID);
    builder.Pad(3);
    builder.PutByte((byte)GuideMissionCategory);
    return new Offset<Lobby.GuideMissionData>(builder.Offset);
  }
  public GuideMissionDataT UnPack() {
    var _o = new GuideMissionDataT();
    this.UnPackTo(_o);
    return _o;
  }
  public void UnPackTo(GuideMissionDataT _o) {
    _o.GuideMissionCategory = this.GuideMissionCategory;
    _o.MissionID = this.MissionID;
    _o.IsCompleted = this.IsCompleted;
    _o.IsRewarded = this.IsRewarded;
  }
  public static Offset<Lobby.GuideMissionData> Pack(FlatBufferBuilder builder, GuideMissionDataT _o) {
    if (_o == null) return default(Offset<Lobby.GuideMissionData>);
    return CreateGuideMissionData(
      builder,
      _o.GuideMissionCategory,
      _o.MissionID,
      _o.IsCompleted,
      _o.IsRewarded);
  }
}

public class GuideMissionDataT
{
  public Lobby.EGuideMissionCategory GuideMissionCategory { get; set; }
  public int MissionID { get; set; }
  public bool IsCompleted { get; set; }
  public bool IsRewarded { get; set; }

  public GuideMissionDataT() {
    this.GuideMissionCategory = Lobby.EGuideMissionCategory.None;
    this.MissionID = 0;
    this.IsCompleted = false;
    this.IsRewarded = false;
  }
}


}
