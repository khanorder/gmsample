// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Lobby
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct SA_DiceGameMatchingStart : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_23_1_20(); }
  public static SA_DiceGameMatchingStart GetRootAsSA_DiceGameMatchingStart(ByteBuffer _bb) { return GetRootAsSA_DiceGameMatchingStart(_bb, new SA_DiceGameMatchingStart()); }
  public static SA_DiceGameMatchingStart GetRootAsSA_DiceGameMatchingStart(ByteBuffer _bb, SA_DiceGameMatchingStart obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public SA_DiceGameMatchingStart __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }


  public static void StartSA_DiceGameMatchingStart(FlatBufferBuilder builder) { builder.StartTable(0); }
  public static Offset<Lobby.SA_DiceGameMatchingStart> EndSA_DiceGameMatchingStart(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<Lobby.SA_DiceGameMatchingStart>(o);
  }
  public SA_DiceGameMatchingStartT UnPack() {
    var _o = new SA_DiceGameMatchingStartT();
    this.UnPackTo(_o);
    return _o;
  }
  public void UnPackTo(SA_DiceGameMatchingStartT _o) {
  }
  public static Offset<Lobby.SA_DiceGameMatchingStart> Pack(FlatBufferBuilder builder, SA_DiceGameMatchingStartT _o) {
    if (_o == null) return default(Offset<Lobby.SA_DiceGameMatchingStart>);
    StartSA_DiceGameMatchingStart(builder);
    return EndSA_DiceGameMatchingStart(builder);
  }
}

public class SA_DiceGameMatchingStartT
{

  public SA_DiceGameMatchingStartT() {
  }
}


}
