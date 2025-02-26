// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Lobby
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct SA_WonderCubePointList : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_23_1_20(); }
  public static SA_WonderCubePointList GetRootAsSA_WonderCubePointList(ByteBuffer _bb) { return GetRootAsSA_WonderCubePointList(_bb, new SA_WonderCubePointList()); }
  public static SA_WonderCubePointList GetRootAsSA_WonderCubePointList(ByteBuffer _bb, SA_WonderCubePointList obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public SA_WonderCubePointList __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public Lobby.WonderCubePointInfo? WonderCubePoint(int j) { int o = __p.__offset(4); return o != 0 ? (Lobby.WonderCubePointInfo?)(new Lobby.WonderCubePointInfo()).__assign(__p.__vector(o) + j * 16, __p.bb) : null; }
  public int WonderCubePointLength { get { int o = __p.__offset(4); return o != 0 ? __p.__vector_len(o) : 0; } }

  public static Offset<Lobby.SA_WonderCubePointList> CreateSA_WonderCubePointList(FlatBufferBuilder builder,
      VectorOffset WonderCubePointOffset = default(VectorOffset)) {
    builder.StartTable(1);
    SA_WonderCubePointList.AddWonderCubePoint(builder, WonderCubePointOffset);
    return SA_WonderCubePointList.EndSA_WonderCubePointList(builder);
  }

  public static void StartSA_WonderCubePointList(FlatBufferBuilder builder) { builder.StartTable(1); }
  public static void AddWonderCubePoint(FlatBufferBuilder builder, VectorOffset WonderCubePointOffset) { builder.AddOffset(0, WonderCubePointOffset.Value, 0); }
  public static void StartWonderCubePointVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(16, numElems, 4); }
  public static Offset<Lobby.SA_WonderCubePointList> EndSA_WonderCubePointList(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<Lobby.SA_WonderCubePointList>(o);
  }
  public SA_WonderCubePointListT UnPack() {
    var _o = new SA_WonderCubePointListT();
    this.UnPackTo(_o);
    return _o;
  }
  public void UnPackTo(SA_WonderCubePointListT _o) {
    _o.WonderCubePoint = new List<Lobby.WonderCubePointInfoT>();
    for (var _j = 0; _j < this.WonderCubePointLength; ++_j) {_o.WonderCubePoint.Add(this.WonderCubePoint(_j).HasValue ? this.WonderCubePoint(_j).Value.UnPack() : null);}
  }
  public static Offset<Lobby.SA_WonderCubePointList> Pack(FlatBufferBuilder builder, SA_WonderCubePointListT _o) {
    if (_o == null) return default(Offset<Lobby.SA_WonderCubePointList>);
    var _WonderCubePoint = default(VectorOffset);
    if (_o.WonderCubePoint != null) {
      StartWonderCubePointVector(builder, _o.WonderCubePoint.Count);
      for (var _j = _o.WonderCubePoint.Count - 1; _j >= 0; --_j) { Lobby.WonderCubePointInfo.Pack(builder, _o.WonderCubePoint[_j]); }
      _WonderCubePoint = builder.EndVector();
    }
    return CreateSA_WonderCubePointList(
      builder,
      _WonderCubePoint);
  }
}

public class SA_WonderCubePointListT
{
  public List<Lobby.WonderCubePointInfoT> WonderCubePoint { get; set; }

  public SA_WonderCubePointListT() {
    this.WonderCubePoint = null;
  }
}


}
