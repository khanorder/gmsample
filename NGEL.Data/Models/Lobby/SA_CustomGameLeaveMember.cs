// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Lobby
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct SA_CustomGameLeaveMember : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_23_1_20(); }
  public static SA_CustomGameLeaveMember GetRootAsSA_CustomGameLeaveMember(ByteBuffer _bb) { return GetRootAsSA_CustomGameLeaveMember(_bb, new SA_CustomGameLeaveMember()); }
  public static SA_CustomGameLeaveMember GetRootAsSA_CustomGameLeaveMember(ByteBuffer _bb, SA_CustomGameLeaveMember obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public SA_CustomGameLeaveMember __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }


  public static void StartSA_CustomGameLeaveMember(FlatBufferBuilder builder) { builder.StartTable(0); }
  public static Offset<Lobby.SA_CustomGameLeaveMember> EndSA_CustomGameLeaveMember(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<Lobby.SA_CustomGameLeaveMember>(o);
  }
  public SA_CustomGameLeaveMemberT UnPack() {
    var _o = new SA_CustomGameLeaveMemberT();
    this.UnPackTo(_o);
    return _o;
  }
  public void UnPackTo(SA_CustomGameLeaveMemberT _o) {
  }
  public static Offset<Lobby.SA_CustomGameLeaveMember> Pack(FlatBufferBuilder builder, SA_CustomGameLeaveMemberT _o) {
    if (_o == null) return default(Offset<Lobby.SA_CustomGameLeaveMember>);
    StartSA_CustomGameLeaveMember(builder);
    return EndSA_CustomGameLeaveMember(builder);
  }
}

public class SA_CustomGameLeaveMemberT
{

  public SA_CustomGameLeaveMemberT() {
  }
}


}
