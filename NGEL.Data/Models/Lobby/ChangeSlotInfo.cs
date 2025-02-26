// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Lobby
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct ChangeSlotInfo : IFlatbufferObject
{
  private Struct __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public void __init(int _i, ByteBuffer _bb) { __p = new Struct(_i, _bb); }
  public ChangeSlotInfo __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public sbyte SlotNo { get { return __p.bb.GetSbyte(__p.bb_pos + 0); } }
  public int UID { get { return __p.bb.GetInt(__p.bb_pos + 4); } }

  public static Offset<Lobby.ChangeSlotInfo> CreateChangeSlotInfo(FlatBufferBuilder builder, sbyte SlotNo, int UID) {
    builder.Prep(4, 8);
    builder.PutInt(UID);
    builder.Pad(3);
    builder.PutSbyte(SlotNo);
    return new Offset<Lobby.ChangeSlotInfo>(builder.Offset);
  }
  public ChangeSlotInfoT UnPack() {
    var _o = new ChangeSlotInfoT();
    this.UnPackTo(_o);
    return _o;
  }
  public void UnPackTo(ChangeSlotInfoT _o) {
    _o.SlotNo = this.SlotNo;
    _o.UID = this.UID;
  }
  public static Offset<Lobby.ChangeSlotInfo> Pack(FlatBufferBuilder builder, ChangeSlotInfoT _o) {
    if (_o == null) return default(Offset<Lobby.ChangeSlotInfo>);
    return CreateChangeSlotInfo(
      builder,
      _o.SlotNo,
      _o.UID);
  }
}

public class ChangeSlotInfoT
{
  public sbyte SlotNo { get; set; }
  public int UID { get; set; }

  public ChangeSlotInfoT() {
    this.SlotNo = 0;
    this.UID = 0;
  }
}


}
