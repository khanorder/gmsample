// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Lobby
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct CQ_CheatAddAllMissionList : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_23_1_20(); }
  public static CQ_CheatAddAllMissionList GetRootAsCQ_CheatAddAllMissionList(ByteBuffer _bb) { return GetRootAsCQ_CheatAddAllMissionList(_bb, new CQ_CheatAddAllMissionList()); }
  public static CQ_CheatAddAllMissionList GetRootAsCQ_CheatAddAllMissionList(ByteBuffer _bb, CQ_CheatAddAllMissionList obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public CQ_CheatAddAllMissionList __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }


  public static void StartCQ_CheatAddAllMissionList(FlatBufferBuilder builder) { builder.StartTable(0); }
  public static Offset<Lobby.CQ_CheatAddAllMissionList> EndCQ_CheatAddAllMissionList(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<Lobby.CQ_CheatAddAllMissionList>(o);
  }
  public CQ_CheatAddAllMissionListT UnPack() {
    var _o = new CQ_CheatAddAllMissionListT();
    this.UnPackTo(_o);
    return _o;
  }
  public void UnPackTo(CQ_CheatAddAllMissionListT _o) {
  }
  public static Offset<Lobby.CQ_CheatAddAllMissionList> Pack(FlatBufferBuilder builder, CQ_CheatAddAllMissionListT _o) {
    if (_o == null) return default(Offset<Lobby.CQ_CheatAddAllMissionList>);
    StartCQ_CheatAddAllMissionList(builder);
    return EndCQ_CheatAddAllMissionList(builder);
  }
}

public class CQ_CheatAddAllMissionListT
{

  public CQ_CheatAddAllMissionListT() {
  }
}


}
