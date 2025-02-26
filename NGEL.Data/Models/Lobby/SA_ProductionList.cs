// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Lobby
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct SA_ProductionList : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_23_1_20(); }
  public static SA_ProductionList GetRootAsSA_ProductionList(ByteBuffer _bb) { return GetRootAsSA_ProductionList(_bb, new SA_ProductionList()); }
  public static SA_ProductionList GetRootAsSA_ProductionList(ByteBuffer _bb, SA_ProductionList obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public SA_ProductionList __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public Lobby.ProductionData? ProductionList(int j) { int o = __p.__offset(4); return o != 0 ? (Lobby.ProductionData?)(new Lobby.ProductionData()).__assign(__p.__vector(o) + j * 12, __p.bb) : null; }
  public int ProductionListLength { get { int o = __p.__offset(4); return o != 0 ? __p.__vector_len(o) : 0; } }

  public static Offset<Lobby.SA_ProductionList> CreateSA_ProductionList(FlatBufferBuilder builder,
      VectorOffset ProductionListOffset = default(VectorOffset)) {
    builder.StartTable(1);
    SA_ProductionList.AddProductionList(builder, ProductionListOffset);
    return SA_ProductionList.EndSA_ProductionList(builder);
  }

  public static void StartSA_ProductionList(FlatBufferBuilder builder) { builder.StartTable(1); }
  public static void AddProductionList(FlatBufferBuilder builder, VectorOffset ProductionListOffset) { builder.AddOffset(0, ProductionListOffset.Value, 0); }
  public static void StartProductionListVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(12, numElems, 4); }
  public static Offset<Lobby.SA_ProductionList> EndSA_ProductionList(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<Lobby.SA_ProductionList>(o);
  }
  public SA_ProductionListT UnPack() {
    var _o = new SA_ProductionListT();
    this.UnPackTo(_o);
    return _o;
  }
  public void UnPackTo(SA_ProductionListT _o) {
    _o.ProductionList = new List<Lobby.ProductionDataT>();
    for (var _j = 0; _j < this.ProductionListLength; ++_j) {_o.ProductionList.Add(this.ProductionList(_j).HasValue ? this.ProductionList(_j).Value.UnPack() : null);}
  }
  public static Offset<Lobby.SA_ProductionList> Pack(FlatBufferBuilder builder, SA_ProductionListT _o) {
    if (_o == null) return default(Offset<Lobby.SA_ProductionList>);
    var _ProductionList = default(VectorOffset);
    if (_o.ProductionList != null) {
      StartProductionListVector(builder, _o.ProductionList.Count);
      for (var _j = _o.ProductionList.Count - 1; _j >= 0; --_j) { Lobby.ProductionData.Pack(builder, _o.ProductionList[_j]); }
      _ProductionList = builder.EndVector();
    }
    return CreateSA_ProductionList(
      builder,
      _ProductionList);
  }
}

public class SA_ProductionListT
{
  public List<Lobby.ProductionDataT> ProductionList { get; set; }

  public SA_ProductionListT() {
    this.ProductionList = null;
  }
}


}
