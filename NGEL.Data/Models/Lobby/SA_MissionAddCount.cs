// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Lobby
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct SA_MissionAddCount : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_23_1_20(); }
  public static SA_MissionAddCount GetRootAsSA_MissionAddCount(ByteBuffer _bb) { return GetRootAsSA_MissionAddCount(_bb, new SA_MissionAddCount()); }
  public static SA_MissionAddCount GetRootAsSA_MissionAddCount(ByteBuffer _bb, SA_MissionAddCount obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public SA_MissionAddCount __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }


  public static void StartSA_MissionAddCount(FlatBufferBuilder builder) { builder.StartTable(0); }
  public static Offset<Lobby.SA_MissionAddCount> EndSA_MissionAddCount(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<Lobby.SA_MissionAddCount>(o);
  }
  public SA_MissionAddCountT UnPack() {
    var _o = new SA_MissionAddCountT();
    this.UnPackTo(_o);
    return _o;
  }
  public void UnPackTo(SA_MissionAddCountT _o) {
  }
  public static Offset<Lobby.SA_MissionAddCount> Pack(FlatBufferBuilder builder, SA_MissionAddCountT _o) {
    if (_o == null) return default(Offset<Lobby.SA_MissionAddCount>);
    StartSA_MissionAddCount(builder);
    return EndSA_MissionAddCount(builder);
  }
}

public class SA_MissionAddCountT
{

  public SA_MissionAddCountT() {
  }
}


}
