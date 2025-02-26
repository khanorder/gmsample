// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Lobby
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct SA_PetChangeAbility : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_23_1_20(); }
  public static SA_PetChangeAbility GetRootAsSA_PetChangeAbility(ByteBuffer _bb) { return GetRootAsSA_PetChangeAbility(_bb, new SA_PetChangeAbility()); }
  public static SA_PetChangeAbility GetRootAsSA_PetChangeAbility(ByteBuffer _bb, SA_PetChangeAbility obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public SA_PetChangeAbility __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public Lobby.PetData? Pet { get { int o = __p.__offset(4); return o != 0 ? (Lobby.PetData?)(new Lobby.PetData()).__assign(__p.__indirect(o + __p.bb_pos), __p.bb) : null; } }
  public long DelPetUniqueID { get { int o = __p.__offset(6); return o != 0 ? __p.bb.GetLong(o + __p.bb_pos) : (long)0; } }

  public static Offset<Lobby.SA_PetChangeAbility> CreateSA_PetChangeAbility(FlatBufferBuilder builder,
      Offset<Lobby.PetData> PetOffset = default(Offset<Lobby.PetData>),
      long DelPetUniqueID = 0) {
    builder.StartTable(2);
    SA_PetChangeAbility.AddDelPetUniqueID(builder, DelPetUniqueID);
    SA_PetChangeAbility.AddPet(builder, PetOffset);
    return SA_PetChangeAbility.EndSA_PetChangeAbility(builder);
  }

  public static void StartSA_PetChangeAbility(FlatBufferBuilder builder) { builder.StartTable(2); }
  public static void AddPet(FlatBufferBuilder builder, Offset<Lobby.PetData> PetOffset) { builder.AddOffset(0, PetOffset.Value, 0); }
  public static void AddDelPetUniqueID(FlatBufferBuilder builder, long DelPetUniqueID) { builder.AddLong(1, DelPetUniqueID, 0); }
  public static Offset<Lobby.SA_PetChangeAbility> EndSA_PetChangeAbility(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<Lobby.SA_PetChangeAbility>(o);
  }
  public SA_PetChangeAbilityT UnPack() {
    var _o = new SA_PetChangeAbilityT();
    this.UnPackTo(_o);
    return _o;
  }
  public void UnPackTo(SA_PetChangeAbilityT _o) {
    _o.Pet = this.Pet.HasValue ? this.Pet.Value.UnPack() : null;
    _o.DelPetUniqueID = this.DelPetUniqueID;
  }
  public static Offset<Lobby.SA_PetChangeAbility> Pack(FlatBufferBuilder builder, SA_PetChangeAbilityT _o) {
    if (_o == null) return default(Offset<Lobby.SA_PetChangeAbility>);
    var _Pet = _o.Pet == null ? default(Offset<Lobby.PetData>) : Lobby.PetData.Pack(builder, _o.Pet);
    return CreateSA_PetChangeAbility(
      builder,
      _Pet,
      _o.DelPetUniqueID);
  }
}

public class SA_PetChangeAbilityT
{
  public Lobby.PetDataT Pet { get; set; }
  public long DelPetUniqueID { get; set; }

  public SA_PetChangeAbilityT() {
    this.Pet = null;
    this.DelPetUniqueID = 0;
  }
}


}
