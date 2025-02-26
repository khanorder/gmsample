// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Lobby
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct SA_ADVList : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_23_1_20(); }
  public static SA_ADVList GetRootAsSA_ADVList(ByteBuffer _bb) { return GetRootAsSA_ADVList(_bb, new SA_ADVList()); }
  public static SA_ADVList GetRootAsSA_ADVList(ByteBuffer _bb, SA_ADVList obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public SA_ADVList __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public int NextResetAt { get { int o = __p.__offset(4); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int ReceivedExtraRewardID { get { int o = __p.__offset(6); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public Lobby.AdvertisementData? ADVList(int j) { int o = __p.__offset(8); return o != 0 ? (Lobby.AdvertisementData?)(new Lobby.AdvertisementData()).__assign(__p.__vector(o) + j * 8, __p.bb) : null; }
  public int ADVListLength { get { int o = __p.__offset(8); return o != 0 ? __p.__vector_len(o) : 0; } }

  public static Offset<Lobby.SA_ADVList> CreateSA_ADVList(FlatBufferBuilder builder,
      int NextResetAt = 0,
      int ReceivedExtraRewardID = 0,
      VectorOffset ADVListOffset = default(VectorOffset)) {
    builder.StartTable(3);
    SA_ADVList.AddADVList(builder, ADVListOffset);
    SA_ADVList.AddReceivedExtraRewardID(builder, ReceivedExtraRewardID);
    SA_ADVList.AddNextResetAt(builder, NextResetAt);
    return SA_ADVList.EndSA_ADVList(builder);
  }

  public static void StartSA_ADVList(FlatBufferBuilder builder) { builder.StartTable(3); }
  public static void AddNextResetAt(FlatBufferBuilder builder, int NextResetAt) { builder.AddInt(0, NextResetAt, 0); }
  public static void AddReceivedExtraRewardID(FlatBufferBuilder builder, int ReceivedExtraRewardID) { builder.AddInt(1, ReceivedExtraRewardID, 0); }
  public static void AddADVList(FlatBufferBuilder builder, VectorOffset ADVListOffset) { builder.AddOffset(2, ADVListOffset.Value, 0); }
  public static void StartADVListVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(8, numElems, 4); }
  public static Offset<Lobby.SA_ADVList> EndSA_ADVList(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<Lobby.SA_ADVList>(o);
  }
  public SA_ADVListT UnPack() {
    var _o = new SA_ADVListT();
    this.UnPackTo(_o);
    return _o;
  }
  public void UnPackTo(SA_ADVListT _o) {
    _o.NextResetAt = this.NextResetAt;
    _o.ReceivedExtraRewardID = this.ReceivedExtraRewardID;
    _o.ADVList = new List<Lobby.AdvertisementDataT>();
    for (var _j = 0; _j < this.ADVListLength; ++_j) {_o.ADVList.Add(this.ADVList(_j).HasValue ? this.ADVList(_j).Value.UnPack() : null);}
  }
  public static Offset<Lobby.SA_ADVList> Pack(FlatBufferBuilder builder, SA_ADVListT _o) {
    if (_o == null) return default(Offset<Lobby.SA_ADVList>);
    var _ADVList = default(VectorOffset);
    if (_o.ADVList != null) {
      StartADVListVector(builder, _o.ADVList.Count);
      for (var _j = _o.ADVList.Count - 1; _j >= 0; --_j) { Lobby.AdvertisementData.Pack(builder, _o.ADVList[_j]); }
      _ADVList = builder.EndVector();
    }
    return CreateSA_ADVList(
      builder,
      _o.NextResetAt,
      _o.ReceivedExtraRewardID,
      _ADVList);
  }
}

public class SA_ADVListT
{
  public int NextResetAt { get; set; }
  public int ReceivedExtraRewardID { get; set; }
  public List<Lobby.AdvertisementDataT> ADVList { get; set; }

  public SA_ADVListT() {
    this.NextResetAt = 0;
    this.ReceivedExtraRewardID = 0;
    this.ADVList = null;
  }
}


}
