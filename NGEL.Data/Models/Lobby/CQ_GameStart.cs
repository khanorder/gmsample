// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Lobby
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct CQ_GameStart : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_23_1_20(); }
  public static CQ_GameStart GetRootAsCQ_GameStart(ByteBuffer _bb) { return GetRootAsCQ_GameStart(_bb, new CQ_GameStart()); }
  public static CQ_GameStart GetRootAsCQ_GameStart(ByteBuffer _bb, CQ_GameStart obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public CQ_GameStart __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }


  public static void StartCQ_GameStart(FlatBufferBuilder builder) { builder.StartTable(0); }
  public static Offset<Lobby.CQ_GameStart> EndCQ_GameStart(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<Lobby.CQ_GameStart>(o);
  }
  public CQ_GameStartT UnPack() {
    var _o = new CQ_GameStartT();
    this.UnPackTo(_o);
    return _o;
  }
  public void UnPackTo(CQ_GameStartT _o) {
  }
  public static Offset<Lobby.CQ_GameStart> Pack(FlatBufferBuilder builder, CQ_GameStartT _o) {
    if (_o == null) return default(Offset<Lobby.CQ_GameStart>);
    StartCQ_GameStart(builder);
    return EndCQ_GameStart(builder);
  }
}

public class CQ_GameStartT
{

  public CQ_GameStartT() {
  }
}


}
