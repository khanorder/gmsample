// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Lobby
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct SA_ADVWatchingStart : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_23_1_20(); }
  public static SA_ADVWatchingStart GetRootAsSA_ADVWatchingStart(ByteBuffer _bb) { return GetRootAsSA_ADVWatchingStart(_bb, new SA_ADVWatchingStart()); }
  public static SA_ADVWatchingStart GetRootAsSA_ADVWatchingStart(ByteBuffer _bb, SA_ADVWatchingStart obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public SA_ADVWatchingStart __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public int ADVID { get { int o = __p.__offset(4); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }

  public static Offset<Lobby.SA_ADVWatchingStart> CreateSA_ADVWatchingStart(FlatBufferBuilder builder,
      int ADVID = 0) {
    builder.StartTable(1);
    SA_ADVWatchingStart.AddADVID(builder, ADVID);
    return SA_ADVWatchingStart.EndSA_ADVWatchingStart(builder);
  }

  public static void StartSA_ADVWatchingStart(FlatBufferBuilder builder) { builder.StartTable(1); }
  public static void AddADVID(FlatBufferBuilder builder, int ADVID) { builder.AddInt(0, ADVID, 0); }
  public static Offset<Lobby.SA_ADVWatchingStart> EndSA_ADVWatchingStart(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<Lobby.SA_ADVWatchingStart>(o);
  }
  public SA_ADVWatchingStartT UnPack() {
    var _o = new SA_ADVWatchingStartT();
    this.UnPackTo(_o);
    return _o;
  }
  public void UnPackTo(SA_ADVWatchingStartT _o) {
    _o.ADVID = this.ADVID;
  }
  public static Offset<Lobby.SA_ADVWatchingStart> Pack(FlatBufferBuilder builder, SA_ADVWatchingStartT _o) {
    if (_o == null) return default(Offset<Lobby.SA_ADVWatchingStart>);
    return CreateSA_ADVWatchingStart(
      builder,
      _o.ADVID);
  }
}

public class SA_ADVWatchingStartT
{
  public int ADVID { get; set; }

  public SA_ADVWatchingStartT() {
    this.ADVID = 0;
  }
}


}
