// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Lobby
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct WonderCubePointInfo : IFlatbufferObject
{
  private Struct __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public void __init(int _i, ByteBuffer _bb) { __p = new Struct(_i, _bb); }
  public WonderCubePointInfo __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public int WonderCubePointID { get { return __p.bb.GetInt(__p.bb_pos + 0); } }
  public int Point { get { return __p.bb.GetInt(__p.bb_pos + 4); } }
  public sbyte RewardLevel { get { return __p.bb.GetSbyte(__p.bb_pos + 8); } }
  public int NextResetAt { get { return __p.bb.GetInt(__p.bb_pos + 12); } }

  public static Offset<Lobby.WonderCubePointInfo> CreateWonderCubePointInfo(FlatBufferBuilder builder, int WonderCubePointID, int Point, sbyte RewardLevel, int NextResetAt) {
    builder.Prep(4, 16);
    builder.PutInt(NextResetAt);
    builder.Pad(3);
    builder.PutSbyte(RewardLevel);
    builder.PutInt(Point);
    builder.PutInt(WonderCubePointID);
    return new Offset<Lobby.WonderCubePointInfo>(builder.Offset);
  }
  public WonderCubePointInfoT UnPack() {
    var _o = new WonderCubePointInfoT();
    this.UnPackTo(_o);
    return _o;
  }
  public void UnPackTo(WonderCubePointInfoT _o) {
    _o.WonderCubePointID = this.WonderCubePointID;
    _o.Point = this.Point;
    _o.RewardLevel = this.RewardLevel;
    _o.NextResetAt = this.NextResetAt;
  }
  public static Offset<Lobby.WonderCubePointInfo> Pack(FlatBufferBuilder builder, WonderCubePointInfoT _o) {
    if (_o == null) return default(Offset<Lobby.WonderCubePointInfo>);
    return CreateWonderCubePointInfo(
      builder,
      _o.WonderCubePointID,
      _o.Point,
      _o.RewardLevel,
      _o.NextResetAt);
  }
}

public class WonderCubePointInfoT
{
  public int WonderCubePointID { get; set; }
  public int Point { get; set; }
  public sbyte RewardLevel { get; set; }
  public int NextResetAt { get; set; }

  public WonderCubePointInfoT() {
    this.WonderCubePointID = 0;
    this.Point = 0;
    this.RewardLevel = 0;
    this.NextResetAt = 0;
  }
}


}
