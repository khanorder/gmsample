// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Lobby
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct CQ_CheatAddPet : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_23_1_20(); }
  public static CQ_CheatAddPet GetRootAsCQ_CheatAddPet(ByteBuffer _bb) { return GetRootAsCQ_CheatAddPet(_bb, new CQ_CheatAddPet()); }
  public static CQ_CheatAddPet GetRootAsCQ_CheatAddPet(ByteBuffer _bb, CQ_CheatAddPet obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public CQ_CheatAddPet __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public int PetID { get { int o = __p.__offset(4); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }

  public static Offset<Lobby.CQ_CheatAddPet> CreateCQ_CheatAddPet(FlatBufferBuilder builder,
      int PetID = 0) {
    builder.StartTable(1);
    CQ_CheatAddPet.AddPetID(builder, PetID);
    return CQ_CheatAddPet.EndCQ_CheatAddPet(builder);
  }

  public static void StartCQ_CheatAddPet(FlatBufferBuilder builder) { builder.StartTable(1); }
  public static void AddPetID(FlatBufferBuilder builder, int PetID) { builder.AddInt(0, PetID, 0); }
  public static Offset<Lobby.CQ_CheatAddPet> EndCQ_CheatAddPet(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<Lobby.CQ_CheatAddPet>(o);
  }
  public CQ_CheatAddPetT UnPack() {
    var _o = new CQ_CheatAddPetT();
    this.UnPackTo(_o);
    return _o;
  }
  public void UnPackTo(CQ_CheatAddPetT _o) {
    _o.PetID = this.PetID;
  }
  public static Offset<Lobby.CQ_CheatAddPet> Pack(FlatBufferBuilder builder, CQ_CheatAddPetT _o) {
    if (_o == null) return default(Offset<Lobby.CQ_CheatAddPet>);
    return CreateCQ_CheatAddPet(
      builder,
      _o.PetID);
  }
}

public class CQ_CheatAddPetT
{
  public int PetID { get; set; }

  public CQ_CheatAddPetT() {
    this.PetID = 0;
  }
}


}
