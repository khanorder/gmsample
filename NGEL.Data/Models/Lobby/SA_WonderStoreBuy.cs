// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Lobby
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct SA_WonderStoreBuy : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_23_1_20(); }
  public static SA_WonderStoreBuy GetRootAsSA_WonderStoreBuy(ByteBuffer _bb) { return GetRootAsSA_WonderStoreBuy(_bb, new SA_WonderStoreBuy()); }
  public static SA_WonderStoreBuy GetRootAsSA_WonderStoreBuy(ByteBuffer _bb, SA_WonderStoreBuy obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public SA_WonderStoreBuy __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public Lobby.WonderStoreData? ChangeWonderStoreList(int j) { int o = __p.__offset(4); return o != 0 ? (Lobby.WonderStoreData?)(new Lobby.WonderStoreData()).__assign(__p.__indirect(__p.__vector(o) + j * 4), __p.bb) : null; }
  public int ChangeWonderStoreListLength { get { int o = __p.__offset(4); return o != 0 ? __p.__vector_len(o) : 0; } }
  public Lobby.RewardData? RewardInfo { get { int o = __p.__offset(6); return o != 0 ? (Lobby.RewardData?)(new Lobby.RewardData()).__assign(__p.__indirect(o + __p.bb_pos), __p.bb) : null; } }
  public int BuyStoreID { get { int o = __p.__offset(8); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }

  public static Offset<Lobby.SA_WonderStoreBuy> CreateSA_WonderStoreBuy(FlatBufferBuilder builder,
      VectorOffset ChangeWonderStoreListOffset = default(VectorOffset),
      Offset<Lobby.RewardData> RewardInfoOffset = default(Offset<Lobby.RewardData>),
      int BuyStoreID = 0) {
    builder.StartTable(3);
    SA_WonderStoreBuy.AddBuyStoreID(builder, BuyStoreID);
    SA_WonderStoreBuy.AddRewardInfo(builder, RewardInfoOffset);
    SA_WonderStoreBuy.AddChangeWonderStoreList(builder, ChangeWonderStoreListOffset);
    return SA_WonderStoreBuy.EndSA_WonderStoreBuy(builder);
  }

  public static void StartSA_WonderStoreBuy(FlatBufferBuilder builder) { builder.StartTable(3); }
  public static void AddChangeWonderStoreList(FlatBufferBuilder builder, VectorOffset ChangeWonderStoreListOffset) { builder.AddOffset(0, ChangeWonderStoreListOffset.Value, 0); }
  public static VectorOffset CreateChangeWonderStoreListVector(FlatBufferBuilder builder, Offset<Lobby.WonderStoreData>[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddOffset(data[i].Value); return builder.EndVector(); }
  public static VectorOffset CreateChangeWonderStoreListVectorBlock(FlatBufferBuilder builder, Offset<Lobby.WonderStoreData>[] data) { builder.StartVector(4, data.Length, 4); builder.Add(data); return builder.EndVector(); }
  public static VectorOffset CreateChangeWonderStoreListVectorBlock(FlatBufferBuilder builder, ArraySegment<Offset<Lobby.WonderStoreData>> data) { builder.StartVector(4, data.Count, 4); builder.Add(data); return builder.EndVector(); }
  public static VectorOffset CreateChangeWonderStoreListVectorBlock(FlatBufferBuilder builder, IntPtr dataPtr, int sizeInBytes) { builder.StartVector(1, sizeInBytes, 1); builder.Add<Offset<Lobby.WonderStoreData>>(dataPtr, sizeInBytes); return builder.EndVector(); }
  public static void StartChangeWonderStoreListVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static void AddRewardInfo(FlatBufferBuilder builder, Offset<Lobby.RewardData> RewardInfoOffset) { builder.AddOffset(1, RewardInfoOffset.Value, 0); }
  public static void AddBuyStoreID(FlatBufferBuilder builder, int BuyStoreID) { builder.AddInt(2, BuyStoreID, 0); }
  public static Offset<Lobby.SA_WonderStoreBuy> EndSA_WonderStoreBuy(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<Lobby.SA_WonderStoreBuy>(o);
  }
  public SA_WonderStoreBuyT UnPack() {
    var _o = new SA_WonderStoreBuyT();
    this.UnPackTo(_o);
    return _o;
  }
  public void UnPackTo(SA_WonderStoreBuyT _o) {
    _o.ChangeWonderStoreList = new List<Lobby.WonderStoreDataT>();
    for (var _j = 0; _j < this.ChangeWonderStoreListLength; ++_j) {_o.ChangeWonderStoreList.Add(this.ChangeWonderStoreList(_j).HasValue ? this.ChangeWonderStoreList(_j).Value.UnPack() : null);}
    _o.RewardInfo = this.RewardInfo.HasValue ? this.RewardInfo.Value.UnPack() : null;
    _o.BuyStoreID = this.BuyStoreID;
  }
  public static Offset<Lobby.SA_WonderStoreBuy> Pack(FlatBufferBuilder builder, SA_WonderStoreBuyT _o) {
    if (_o == null) return default(Offset<Lobby.SA_WonderStoreBuy>);
    var _ChangeWonderStoreList = default(VectorOffset);
    if (_o.ChangeWonderStoreList != null) {
      var __ChangeWonderStoreList = new Offset<Lobby.WonderStoreData>[_o.ChangeWonderStoreList.Count];
      for (var _j = 0; _j < __ChangeWonderStoreList.Length; ++_j) { __ChangeWonderStoreList[_j] = Lobby.WonderStoreData.Pack(builder, _o.ChangeWonderStoreList[_j]); }
      _ChangeWonderStoreList = CreateChangeWonderStoreListVector(builder, __ChangeWonderStoreList);
    }
    var _RewardInfo = _o.RewardInfo == null ? default(Offset<Lobby.RewardData>) : Lobby.RewardData.Pack(builder, _o.RewardInfo);
    return CreateSA_WonderStoreBuy(
      builder,
      _ChangeWonderStoreList,
      _RewardInfo,
      _o.BuyStoreID);
  }
}

public class SA_WonderStoreBuyT
{
  public List<Lobby.WonderStoreDataT> ChangeWonderStoreList { get; set; }
  public Lobby.RewardDataT RewardInfo { get; set; }
  public int BuyStoreID { get; set; }

  public SA_WonderStoreBuyT() {
    this.ChangeWonderStoreList = null;
    this.RewardInfo = null;
    this.BuyStoreID = 0;
  }
}


}
