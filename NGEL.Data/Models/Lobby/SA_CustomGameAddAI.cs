// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Lobby
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct SA_CustomGameAddAI : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_23_1_20(); }
  public static SA_CustomGameAddAI GetRootAsSA_CustomGameAddAI(ByteBuffer _bb) { return GetRootAsSA_CustomGameAddAI(_bb, new SA_CustomGameAddAI()); }
  public static SA_CustomGameAddAI GetRootAsSA_CustomGameAddAI(ByteBuffer _bb, SA_CustomGameAddAI obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public SA_CustomGameAddAI __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }


  public static void StartSA_CustomGameAddAI(FlatBufferBuilder builder) { builder.StartTable(0); }
  public static Offset<Lobby.SA_CustomGameAddAI> EndSA_CustomGameAddAI(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<Lobby.SA_CustomGameAddAI>(o);
  }
  public SA_CustomGameAddAIT UnPack() {
    var _o = new SA_CustomGameAddAIT();
    this.UnPackTo(_o);
    return _o;
  }
  public void UnPackTo(SA_CustomGameAddAIT _o) {
  }
  public static Offset<Lobby.SA_CustomGameAddAI> Pack(FlatBufferBuilder builder, SA_CustomGameAddAIT _o) {
    if (_o == null) return default(Offset<Lobby.SA_CustomGameAddAI>);
    StartSA_CustomGameAddAI(builder);
    return EndSA_CustomGameAddAI(builder);
  }
}

public class SA_CustomGameAddAIT
{

  public SA_CustomGameAddAIT() {
  }
}


}
