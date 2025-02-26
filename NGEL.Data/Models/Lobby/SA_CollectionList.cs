// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Lobby
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct SA_CollectionList : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_23_1_20(); }
  public static SA_CollectionList GetRootAsSA_CollectionList(ByteBuffer _bb) { return GetRootAsSA_CollectionList(_bb, new SA_CollectionList()); }
  public static SA_CollectionList GetRootAsSA_CollectionList(ByteBuffer _bb, SA_CollectionList obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public SA_CollectionList __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public Lobby.CollectionData? CollectionList(int j) { int o = __p.__offset(4); return o != 0 ? (Lobby.CollectionData?)(new Lobby.CollectionData()).__assign(__p.__vector(o) + j * 8, __p.bb) : null; }
  public int CollectionListLength { get { int o = __p.__offset(4); return o != 0 ? __p.__vector_len(o) : 0; } }

  public static Offset<Lobby.SA_CollectionList> CreateSA_CollectionList(FlatBufferBuilder builder,
      VectorOffset CollectionListOffset = default(VectorOffset)) {
    builder.StartTable(1);
    SA_CollectionList.AddCollectionList(builder, CollectionListOffset);
    return SA_CollectionList.EndSA_CollectionList(builder);
  }

  public static void StartSA_CollectionList(FlatBufferBuilder builder) { builder.StartTable(1); }
  public static void AddCollectionList(FlatBufferBuilder builder, VectorOffset CollectionListOffset) { builder.AddOffset(0, CollectionListOffset.Value, 0); }
  public static void StartCollectionListVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(8, numElems, 4); }
  public static Offset<Lobby.SA_CollectionList> EndSA_CollectionList(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<Lobby.SA_CollectionList>(o);
  }
  public SA_CollectionListT UnPack() {
    var _o = new SA_CollectionListT();
    this.UnPackTo(_o);
    return _o;
  }
  public void UnPackTo(SA_CollectionListT _o) {
    _o.CollectionList = new List<Lobby.CollectionDataT>();
    for (var _j = 0; _j < this.CollectionListLength; ++_j) {_o.CollectionList.Add(this.CollectionList(_j).HasValue ? this.CollectionList(_j).Value.UnPack() : null);}
  }
  public static Offset<Lobby.SA_CollectionList> Pack(FlatBufferBuilder builder, SA_CollectionListT _o) {
    if (_o == null) return default(Offset<Lobby.SA_CollectionList>);
    var _CollectionList = default(VectorOffset);
    if (_o.CollectionList != null) {
      StartCollectionListVector(builder, _o.CollectionList.Count);
      for (var _j = _o.CollectionList.Count - 1; _j >= 0; --_j) { Lobby.CollectionData.Pack(builder, _o.CollectionList[_j]); }
      _CollectionList = builder.EndVector();
    }
    return CreateSA_CollectionList(
      builder,
      _CollectionList);
  }
}

public class SA_CollectionListT
{
  public List<Lobby.CollectionDataT> CollectionList { get; set; }

  public SA_CollectionListT() {
    this.CollectionList = null;
  }
}


}
