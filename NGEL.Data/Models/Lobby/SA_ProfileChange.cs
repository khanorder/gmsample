// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Lobby
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct SA_ProfileChange : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_23_1_20(); }
  public static SA_ProfileChange GetRootAsSA_ProfileChange(ByteBuffer _bb) { return GetRootAsSA_ProfileChange(_bb, new SA_ProfileChange()); }
  public static SA_ProfileChange GetRootAsSA_ProfileChange(ByteBuffer _bb, SA_ProfileChange obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public SA_ProfileChange __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public int EntitlementID { get { int o = __p.__offset(4); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int ProfileBGID { get { int o = __p.__offset(6); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int ProfileIconID { get { int o = __p.__offset(8); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }

  public static Offset<Lobby.SA_ProfileChange> CreateSA_ProfileChange(FlatBufferBuilder builder,
      int EntitlementID = 0,
      int ProfileBGID = 0,
      int ProfileIconID = 0) {
    builder.StartTable(3);
    SA_ProfileChange.AddProfileIconID(builder, ProfileIconID);
    SA_ProfileChange.AddProfileBGID(builder, ProfileBGID);
    SA_ProfileChange.AddEntitlementID(builder, EntitlementID);
    return SA_ProfileChange.EndSA_ProfileChange(builder);
  }

  public static void StartSA_ProfileChange(FlatBufferBuilder builder) { builder.StartTable(3); }
  public static void AddEntitlementID(FlatBufferBuilder builder, int EntitlementID) { builder.AddInt(0, EntitlementID, 0); }
  public static void AddProfileBGID(FlatBufferBuilder builder, int ProfileBGID) { builder.AddInt(1, ProfileBGID, 0); }
  public static void AddProfileIconID(FlatBufferBuilder builder, int ProfileIconID) { builder.AddInt(2, ProfileIconID, 0); }
  public static Offset<Lobby.SA_ProfileChange> EndSA_ProfileChange(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<Lobby.SA_ProfileChange>(o);
  }
  public SA_ProfileChangeT UnPack() {
    var _o = new SA_ProfileChangeT();
    this.UnPackTo(_o);
    return _o;
  }
  public void UnPackTo(SA_ProfileChangeT _o) {
    _o.EntitlementID = this.EntitlementID;
    _o.ProfileBGID = this.ProfileBGID;
    _o.ProfileIconID = this.ProfileIconID;
  }
  public static Offset<Lobby.SA_ProfileChange> Pack(FlatBufferBuilder builder, SA_ProfileChangeT _o) {
    if (_o == null) return default(Offset<Lobby.SA_ProfileChange>);
    return CreateSA_ProfileChange(
      builder,
      _o.EntitlementID,
      _o.ProfileBGID,
      _o.ProfileIconID);
  }
}

public class SA_ProfileChangeT
{
  public int EntitlementID { get; set; }
  public int ProfileBGID { get; set; }
  public int ProfileIconID { get; set; }

  public SA_ProfileChangeT() {
    this.EntitlementID = 0;
    this.ProfileBGID = 0;
    this.ProfileIconID = 0;
  }
}


}
