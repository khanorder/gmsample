// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Lobby
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct CQ_PetChangeAbility : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_23_1_20(); }
  public static CQ_PetChangeAbility GetRootAsCQ_PetChangeAbility(ByteBuffer _bb) { return GetRootAsCQ_PetChangeAbility(_bb, new CQ_PetChangeAbility()); }
  public static CQ_PetChangeAbility GetRootAsCQ_PetChangeAbility(ByteBuffer _bb, CQ_PetChangeAbility obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public CQ_PetChangeAbility __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public long PetUniqueID { get { int o = __p.__offset(4); return o != 0 ? __p.bb.GetLong(o + __p.bb_pos) : (long)0; } }
  public long MaterialUniqueID { get { int o = __p.__offset(6); return o != 0 ? __p.bb.GetLong(o + __p.bb_pos) : (long)0; } }
  public int AbilityIndex { get { int o = __p.__offset(8); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }

  public static Offset<Lobby.CQ_PetChangeAbility> CreateCQ_PetChangeAbility(FlatBufferBuilder builder,
      long PetUniqueID = 0,
      long MaterialUniqueID = 0,
      int AbilityIndex = 0) {
    builder.StartTable(3);
    CQ_PetChangeAbility.AddMaterialUniqueID(builder, MaterialUniqueID);
    CQ_PetChangeAbility.AddPetUniqueID(builder, PetUniqueID);
    CQ_PetChangeAbility.AddAbilityIndex(builder, AbilityIndex);
    return CQ_PetChangeAbility.EndCQ_PetChangeAbility(builder);
  }

  public static void StartCQ_PetChangeAbility(FlatBufferBuilder builder) { builder.StartTable(3); }
  public static void AddPetUniqueID(FlatBufferBuilder builder, long PetUniqueID) { builder.AddLong(0, PetUniqueID, 0); }
  public static void AddMaterialUniqueID(FlatBufferBuilder builder, long MaterialUniqueID) { builder.AddLong(1, MaterialUniqueID, 0); }
  public static void AddAbilityIndex(FlatBufferBuilder builder, int AbilityIndex) { builder.AddInt(2, AbilityIndex, 0); }
  public static Offset<Lobby.CQ_PetChangeAbility> EndCQ_PetChangeAbility(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<Lobby.CQ_PetChangeAbility>(o);
  }
  public CQ_PetChangeAbilityT UnPack() {
    var _o = new CQ_PetChangeAbilityT();
    this.UnPackTo(_o);
    return _o;
  }
  public void UnPackTo(CQ_PetChangeAbilityT _o) {
    _o.PetUniqueID = this.PetUniqueID;
    _o.MaterialUniqueID = this.MaterialUniqueID;
    _o.AbilityIndex = this.AbilityIndex;
  }
  public static Offset<Lobby.CQ_PetChangeAbility> Pack(FlatBufferBuilder builder, CQ_PetChangeAbilityT _o) {
    if (_o == null) return default(Offset<Lobby.CQ_PetChangeAbility>);
    return CreateCQ_PetChangeAbility(
      builder,
      _o.PetUniqueID,
      _o.MaterialUniqueID,
      _o.AbilityIndex);
  }
}

public class CQ_PetChangeAbilityT
{
  public long PetUniqueID { get; set; }
  public long MaterialUniqueID { get; set; }
  public int AbilityIndex { get; set; }

  public CQ_PetChangeAbilityT() {
    this.PetUniqueID = 0;
    this.MaterialUniqueID = 0;
    this.AbilityIndex = 0;
  }
}


}
