// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Lobby
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct SA_PartyLeaveMember : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_23_1_20(); }
  public static SA_PartyLeaveMember GetRootAsSA_PartyLeaveMember(ByteBuffer _bb) { return GetRootAsSA_PartyLeaveMember(_bb, new SA_PartyLeaveMember()); }
  public static SA_PartyLeaveMember GetRootAsSA_PartyLeaveMember(ByteBuffer _bb, SA_PartyLeaveMember obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public SA_PartyLeaveMember __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }


  public static void StartSA_PartyLeaveMember(FlatBufferBuilder builder) { builder.StartTable(0); }
  public static Offset<Lobby.SA_PartyLeaveMember> EndSA_PartyLeaveMember(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<Lobby.SA_PartyLeaveMember>(o);
  }
  public SA_PartyLeaveMemberT UnPack() {
    var _o = new SA_PartyLeaveMemberT();
    this.UnPackTo(_o);
    return _o;
  }
  public void UnPackTo(SA_PartyLeaveMemberT _o) {
  }
  public static Offset<Lobby.SA_PartyLeaveMember> Pack(FlatBufferBuilder builder, SA_PartyLeaveMemberT _o) {
    if (_o == null) return default(Offset<Lobby.SA_PartyLeaveMember>);
    StartSA_PartyLeaveMember(builder);
    return EndSA_PartyLeaveMember(builder);
  }
}

public class SA_PartyLeaveMemberT
{

  public SA_PartyLeaveMemberT() {
  }
}


}
