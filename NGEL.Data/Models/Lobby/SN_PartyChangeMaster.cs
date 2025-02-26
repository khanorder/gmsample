// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Lobby
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct SN_PartyChangeMaster : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_23_1_20(); }
  public static SN_PartyChangeMaster GetRootAsSN_PartyChangeMaster(ByteBuffer _bb) { return GetRootAsSN_PartyChangeMaster(_bb, new SN_PartyChangeMaster()); }
  public static SN_PartyChangeMaster GetRootAsSN_PartyChangeMaster(ByteBuffer _bb, SN_PartyChangeMaster obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public SN_PartyChangeMaster __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public string PartyID { get { int o = __p.__offset(4); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetPartyIDBytes() { return __p.__vector_as_span<byte>(4, 1); }
#else
  public ArraySegment<byte>? GetPartyIDBytes() { return __p.__vector_as_arraysegment(4); }
#endif
  public byte[] GetPartyIDArray() { return __p.__vector_as_array<byte>(4); }
  public int OldMasterUID { get { int o = __p.__offset(6); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int NewMasterUID { get { int o = __p.__offset(8); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }

  public static Offset<Lobby.SN_PartyChangeMaster> CreateSN_PartyChangeMaster(FlatBufferBuilder builder,
      StringOffset PartyIDOffset = default(StringOffset),
      int OldMasterUID = 0,
      int NewMasterUID = 0) {
    builder.StartTable(3);
    SN_PartyChangeMaster.AddNewMasterUID(builder, NewMasterUID);
    SN_PartyChangeMaster.AddOldMasterUID(builder, OldMasterUID);
    SN_PartyChangeMaster.AddPartyID(builder, PartyIDOffset);
    return SN_PartyChangeMaster.EndSN_PartyChangeMaster(builder);
  }

  public static void StartSN_PartyChangeMaster(FlatBufferBuilder builder) { builder.StartTable(3); }
  public static void AddPartyID(FlatBufferBuilder builder, StringOffset PartyIDOffset) { builder.AddOffset(0, PartyIDOffset.Value, 0); }
  public static void AddOldMasterUID(FlatBufferBuilder builder, int OldMasterUID) { builder.AddInt(1, OldMasterUID, 0); }
  public static void AddNewMasterUID(FlatBufferBuilder builder, int NewMasterUID) { builder.AddInt(2, NewMasterUID, 0); }
  public static Offset<Lobby.SN_PartyChangeMaster> EndSN_PartyChangeMaster(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<Lobby.SN_PartyChangeMaster>(o);
  }
  public SN_PartyChangeMasterT UnPack() {
    var _o = new SN_PartyChangeMasterT();
    this.UnPackTo(_o);
    return _o;
  }
  public void UnPackTo(SN_PartyChangeMasterT _o) {
    _o.PartyID = this.PartyID;
    _o.OldMasterUID = this.OldMasterUID;
    _o.NewMasterUID = this.NewMasterUID;
  }
  public static Offset<Lobby.SN_PartyChangeMaster> Pack(FlatBufferBuilder builder, SN_PartyChangeMasterT _o) {
    if (_o == null) return default(Offset<Lobby.SN_PartyChangeMaster>);
    var _PartyID = _o.PartyID == null ? default(StringOffset) : builder.CreateString(_o.PartyID);
    return CreateSN_PartyChangeMaster(
      builder,
      _PartyID,
      _o.OldMasterUID,
      _o.NewMasterUID);
  }
}

public class SN_PartyChangeMasterT
{
  public string PartyID { get; set; }
  public int OldMasterUID { get; set; }
  public int NewMasterUID { get; set; }

  public SN_PartyChangeMasterT() {
    this.PartyID = null;
    this.OldMasterUID = 0;
    this.NewMasterUID = 0;
  }
}


}
