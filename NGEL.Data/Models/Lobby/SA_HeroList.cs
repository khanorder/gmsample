// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Lobby
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct SA_HeroList : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_23_1_20(); }
  public static SA_HeroList GetRootAsSA_HeroList(ByteBuffer _bb) { return GetRootAsSA_HeroList(_bb, new SA_HeroList()); }
  public static SA_HeroList GetRootAsSA_HeroList(ByteBuffer _bb, SA_HeroList obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public SA_HeroList __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public Lobby.HeroData? HeroList(int j) { int o = __p.__offset(4); return o != 0 ? (Lobby.HeroData?)(new Lobby.HeroData()).__assign(__p.__indirect(__p.__vector(o) + j * 4), __p.bb) : null; }
  public int HeroListLength { get { int o = __p.__offset(4); return o != 0 ? __p.__vector_len(o) : 0; } }

  public static Offset<Lobby.SA_HeroList> CreateSA_HeroList(FlatBufferBuilder builder,
      VectorOffset HeroListOffset = default(VectorOffset)) {
    builder.StartTable(1);
    SA_HeroList.AddHeroList(builder, HeroListOffset);
    return SA_HeroList.EndSA_HeroList(builder);
  }

  public static void StartSA_HeroList(FlatBufferBuilder builder) { builder.StartTable(1); }
  public static void AddHeroList(FlatBufferBuilder builder, VectorOffset HeroListOffset) { builder.AddOffset(0, HeroListOffset.Value, 0); }
  public static VectorOffset CreateHeroListVector(FlatBufferBuilder builder, Offset<Lobby.HeroData>[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddOffset(data[i].Value); return builder.EndVector(); }
  public static VectorOffset CreateHeroListVectorBlock(FlatBufferBuilder builder, Offset<Lobby.HeroData>[] data) { builder.StartVector(4, data.Length, 4); builder.Add(data); return builder.EndVector(); }
  public static VectorOffset CreateHeroListVectorBlock(FlatBufferBuilder builder, ArraySegment<Offset<Lobby.HeroData>> data) { builder.StartVector(4, data.Count, 4); builder.Add(data); return builder.EndVector(); }
  public static VectorOffset CreateHeroListVectorBlock(FlatBufferBuilder builder, IntPtr dataPtr, int sizeInBytes) { builder.StartVector(1, sizeInBytes, 1); builder.Add<Offset<Lobby.HeroData>>(dataPtr, sizeInBytes); return builder.EndVector(); }
  public static void StartHeroListVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static Offset<Lobby.SA_HeroList> EndSA_HeroList(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<Lobby.SA_HeroList>(o);
  }
  public SA_HeroListT UnPack() {
    var _o = new SA_HeroListT();
    this.UnPackTo(_o);
    return _o;
  }
  public void UnPackTo(SA_HeroListT _o) {
    _o.HeroList = new List<Lobby.HeroDataT>();
    for (var _j = 0; _j < this.HeroListLength; ++_j) {_o.HeroList.Add(this.HeroList(_j).HasValue ? this.HeroList(_j).Value.UnPack() : null);}
  }
  public static Offset<Lobby.SA_HeroList> Pack(FlatBufferBuilder builder, SA_HeroListT _o) {
    if (_o == null) return default(Offset<Lobby.SA_HeroList>);
    var _HeroList = default(VectorOffset);
    if (_o.HeroList != null) {
      var __HeroList = new Offset<Lobby.HeroData>[_o.HeroList.Count];
      for (var _j = 0; _j < __HeroList.Length; ++_j) { __HeroList[_j] = Lobby.HeroData.Pack(builder, _o.HeroList[_j]); }
      _HeroList = CreateHeroListVector(builder, __HeroList);
    }
    return CreateSA_HeroList(
      builder,
      _HeroList);
  }
}

public class SA_HeroListT
{
  public List<Lobby.HeroDataT> HeroList { get; set; }

  public SA_HeroListT() {
    this.HeroList = null;
  }
}


}
