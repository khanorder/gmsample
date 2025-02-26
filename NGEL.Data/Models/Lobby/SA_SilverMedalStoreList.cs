// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Lobby
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct SA_SilverMedalStoreList : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_23_1_20(); }
  public static SA_SilverMedalStoreList GetRootAsSA_SilverMedalStoreList(ByteBuffer _bb) { return GetRootAsSA_SilverMedalStoreList(_bb, new SA_SilverMedalStoreList()); }
  public static SA_SilverMedalStoreList GetRootAsSA_SilverMedalStoreList(ByteBuffer _bb, SA_SilverMedalStoreList obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public SA_SilverMedalStoreList __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public Lobby.SilverMedalStoreData? SilverMedalStoreList(int j) { int o = __p.__offset(4); return o != 0 ? (Lobby.SilverMedalStoreData?)(new Lobby.SilverMedalStoreData()).__assign(__p.__vector(o) + j * 8, __p.bb) : null; }
  public int SilverMedalStoreListLength { get { int o = __p.__offset(4); return o != 0 ? __p.__vector_len(o) : 0; } }

  public static Offset<Lobby.SA_SilverMedalStoreList> CreateSA_SilverMedalStoreList(FlatBufferBuilder builder,
      VectorOffset SilverMedalStoreListOffset = default(VectorOffset)) {
    builder.StartTable(1);
    SA_SilverMedalStoreList.AddSilverMedalStoreList(builder, SilverMedalStoreListOffset);
    return SA_SilverMedalStoreList.EndSA_SilverMedalStoreList(builder);
  }

  public static void StartSA_SilverMedalStoreList(FlatBufferBuilder builder) { builder.StartTable(1); }
  public static void AddSilverMedalStoreList(FlatBufferBuilder builder, VectorOffset SilverMedalStoreListOffset) { builder.AddOffset(0, SilverMedalStoreListOffset.Value, 0); }
  public static void StartSilverMedalStoreListVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(8, numElems, 4); }
  public static Offset<Lobby.SA_SilverMedalStoreList> EndSA_SilverMedalStoreList(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<Lobby.SA_SilverMedalStoreList>(o);
  }
  public SA_SilverMedalStoreListT UnPack() {
    var _o = new SA_SilverMedalStoreListT();
    this.UnPackTo(_o);
    return _o;
  }
  public void UnPackTo(SA_SilverMedalStoreListT _o) {
    _o.SilverMedalStoreList = new List<Lobby.SilverMedalStoreDataT>();
    for (var _j = 0; _j < this.SilverMedalStoreListLength; ++_j) {_o.SilverMedalStoreList.Add(this.SilverMedalStoreList(_j).HasValue ? this.SilverMedalStoreList(_j).Value.UnPack() : null);}
  }
  public static Offset<Lobby.SA_SilverMedalStoreList> Pack(FlatBufferBuilder builder, SA_SilverMedalStoreListT _o) {
    if (_o == null) return default(Offset<Lobby.SA_SilverMedalStoreList>);
    var _SilverMedalStoreList = default(VectorOffset);
    if (_o.SilverMedalStoreList != null) {
      StartSilverMedalStoreListVector(builder, _o.SilverMedalStoreList.Count);
      for (var _j = _o.SilverMedalStoreList.Count - 1; _j >= 0; --_j) { Lobby.SilverMedalStoreData.Pack(builder, _o.SilverMedalStoreList[_j]); }
      _SilverMedalStoreList = builder.EndVector();
    }
    return CreateSA_SilverMedalStoreList(
      builder,
      _SilverMedalStoreList);
  }
}

public class SA_SilverMedalStoreListT
{
  public List<Lobby.SilverMedalStoreDataT> SilverMedalStoreList { get; set; }

  public SA_SilverMedalStoreListT() {
    this.SilverMedalStoreList = null;
  }
}


}
