// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Lobby
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct WonderStoreData : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_23_1_20(); }
  public static WonderStoreData GetRootAsWonderStoreData(ByteBuffer _bb) { return GetRootAsWonderStoreData(_bb, new WonderStoreData()); }
  public static WonderStoreData GetRootAsWonderStoreData(ByteBuffer _bb, WonderStoreData obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public WonderStoreData __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public int StoreID { get { int o = __p.__offset(4); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int BuyCount { get { int o = __p.__offset(6); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int SeasonPassID { get { int o = __p.__offset(8); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public bool IsSubscription { get { int o = __p.__offset(10); return o != 0 ? 0!=__p.bb.Get(o + __p.bb_pos) : (bool)false; } }
  public int SubscriptionExpireAt { get { int o = __p.__offset(12); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int BuyAbleStartAt { get { int o = __p.__offset(14); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int BuyAbleEndAt { get { int o = __p.__offset(16); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int NextResetAt { get { int o = __p.__offset(18); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }

  public static Offset<Lobby.WonderStoreData> CreateWonderStoreData(FlatBufferBuilder builder,
      int StoreID = 0,
      int BuyCount = 0,
      int SeasonPassID = 0,
      bool IsSubscription = false,
      int SubscriptionExpireAt = 0,
      int BuyAbleStartAt = 0,
      int BuyAbleEndAt = 0,
      int NextResetAt = 0) {
    builder.StartTable(8);
    WonderStoreData.AddNextResetAt(builder, NextResetAt);
    WonderStoreData.AddBuyAbleEndAt(builder, BuyAbleEndAt);
    WonderStoreData.AddBuyAbleStartAt(builder, BuyAbleStartAt);
    WonderStoreData.AddSubscriptionExpireAt(builder, SubscriptionExpireAt);
    WonderStoreData.AddSeasonPassID(builder, SeasonPassID);
    WonderStoreData.AddBuyCount(builder, BuyCount);
    WonderStoreData.AddStoreID(builder, StoreID);
    WonderStoreData.AddIsSubscription(builder, IsSubscription);
    return WonderStoreData.EndWonderStoreData(builder);
  }

  public static void StartWonderStoreData(FlatBufferBuilder builder) { builder.StartTable(8); }
  public static void AddStoreID(FlatBufferBuilder builder, int StoreID) { builder.AddInt(0, StoreID, 0); }
  public static void AddBuyCount(FlatBufferBuilder builder, int BuyCount) { builder.AddInt(1, BuyCount, 0); }
  public static void AddSeasonPassID(FlatBufferBuilder builder, int SeasonPassID) { builder.AddInt(2, SeasonPassID, 0); }
  public static void AddIsSubscription(FlatBufferBuilder builder, bool IsSubscription) { builder.AddBool(3, IsSubscription, false); }
  public static void AddSubscriptionExpireAt(FlatBufferBuilder builder, int SubscriptionExpireAt) { builder.AddInt(4, SubscriptionExpireAt, 0); }
  public static void AddBuyAbleStartAt(FlatBufferBuilder builder, int BuyAbleStartAt) { builder.AddInt(5, BuyAbleStartAt, 0); }
  public static void AddBuyAbleEndAt(FlatBufferBuilder builder, int BuyAbleEndAt) { builder.AddInt(6, BuyAbleEndAt, 0); }
  public static void AddNextResetAt(FlatBufferBuilder builder, int NextResetAt) { builder.AddInt(7, NextResetAt, 0); }
  public static Offset<Lobby.WonderStoreData> EndWonderStoreData(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<Lobby.WonderStoreData>(o);
  }
  public WonderStoreDataT UnPack() {
    var _o = new WonderStoreDataT();
    this.UnPackTo(_o);
    return _o;
  }
  public void UnPackTo(WonderStoreDataT _o) {
    _o.StoreID = this.StoreID;
    _o.BuyCount = this.BuyCount;
    _o.SeasonPassID = this.SeasonPassID;
    _o.IsSubscription = this.IsSubscription;
    _o.SubscriptionExpireAt = this.SubscriptionExpireAt;
    _o.BuyAbleStartAt = this.BuyAbleStartAt;
    _o.BuyAbleEndAt = this.BuyAbleEndAt;
    _o.NextResetAt = this.NextResetAt;
  }
  public static Offset<Lobby.WonderStoreData> Pack(FlatBufferBuilder builder, WonderStoreDataT _o) {
    if (_o == null) return default(Offset<Lobby.WonderStoreData>);
    return CreateWonderStoreData(
      builder,
      _o.StoreID,
      _o.BuyCount,
      _o.SeasonPassID,
      _o.IsSubscription,
      _o.SubscriptionExpireAt,
      _o.BuyAbleStartAt,
      _o.BuyAbleEndAt,
      _o.NextResetAt);
  }
}

public class WonderStoreDataT
{
  public int StoreID { get; set; }
  public int BuyCount { get; set; }
  public int SeasonPassID { get; set; }
  public bool IsSubscription { get; set; }
  public int SubscriptionExpireAt { get; set; }
  public int BuyAbleStartAt { get; set; }
  public int BuyAbleEndAt { get; set; }
  public int NextResetAt { get; set; }

  public WonderStoreDataT() {
    this.StoreID = 0;
    this.BuyCount = 0;
    this.SeasonPassID = 0;
    this.IsSubscription = false;
    this.SubscriptionExpireAt = 0;
    this.BuyAbleStartAt = 0;
    this.BuyAbleEndAt = 0;
    this.NextResetAt = 0;
  }
}


}
