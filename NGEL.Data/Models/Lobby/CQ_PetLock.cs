// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Lobby
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct CQ_PetLock : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_23_1_20(); }
  public static CQ_PetLock GetRootAsCQ_PetLock(ByteBuffer _bb) { return GetRootAsCQ_PetLock(_bb, new CQ_PetLock()); }
  public static CQ_PetLock GetRootAsCQ_PetLock(ByteBuffer _bb, CQ_PetLock obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public CQ_PetLock __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public long PetUniqueID { get { int o = __p.__offset(4); return o != 0 ? __p.bb.GetLong(o + __p.bb_pos) : (long)0; } }

  public static Offset<Lobby.CQ_PetLock> CreateCQ_PetLock(FlatBufferBuilder builder,
      long PetUniqueID = 0) {
    builder.StartTable(1);
    CQ_PetLock.AddPetUniqueID(builder, PetUniqueID);
    return CQ_PetLock.EndCQ_PetLock(builder);
  }

  public static void StartCQ_PetLock(FlatBufferBuilder builder) { builder.StartTable(1); }
  public static void AddPetUniqueID(FlatBufferBuilder builder, long PetUniqueID) { builder.AddLong(0, PetUniqueID, 0); }
  public static Offset<Lobby.CQ_PetLock> EndCQ_PetLock(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<Lobby.CQ_PetLock>(o);
  }
  public CQ_PetLockT UnPack() {
    var _o = new CQ_PetLockT();
    this.UnPackTo(_o);
    return _o;
  }
  public void UnPackTo(CQ_PetLockT _o) {
    _o.PetUniqueID = this.PetUniqueID;
  }
  public static Offset<Lobby.CQ_PetLock> Pack(FlatBufferBuilder builder, CQ_PetLockT _o) {
    if (_o == null) return default(Offset<Lobby.CQ_PetLock>);
    return CreateCQ_PetLock(
      builder,
      _o.PetUniqueID);
  }
}

public class CQ_PetLockT
{
  public long PetUniqueID { get; set; }

  public CQ_PetLockT() {
    this.PetUniqueID = 0;
  }
}


}
