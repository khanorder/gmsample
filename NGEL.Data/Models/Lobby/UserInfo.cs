// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Lobby
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct UserInfo : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_23_1_20(); }
  public static UserInfo GetRootAsUserInfo(ByteBuffer _bb) { return GetRootAsUserInfo(_bb, new UserInfo()); }
  public static UserInfo GetRootAsUserInfo(ByteBuffer _bb, UserInfo obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public UserInfo __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public int UID { get { int o = __p.__offset(4); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public string Nick { get { int o = __p.__offset(6); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetNickBytes() { return __p.__vector_as_span<byte>(6, 1); }
#else
  public ArraySegment<byte>? GetNickBytes() { return __p.__vector_as_arraysegment(6); }
#endif
  public byte[] GetNickArray() { return __p.__vector_as_array<byte>(6); }
  public int HeroID { get { int o = __p.__offset(8); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int MemberNo { get { int o = __p.__offset(10); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int LogoutAt { get { int o = __p.__offset(12); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int ProfileIconID { get { int o = __p.__offset(14); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int ProfileBGID { get { int o = __p.__offset(16); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int EntitlementID { get { int o = __p.__offset(18); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int GuideClass { get { int o = __p.__offset(20); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int IntroduceID { get { int o = __p.__offset(22); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int AchievementCount { get { int o = __p.__offset(24); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int CollectionCount { get { int o = __p.__offset(26); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }

  public static Offset<Lobby.UserInfo> CreateUserInfo(FlatBufferBuilder builder,
      int UID = 0,
      StringOffset NickOffset = default(StringOffset),
      int HeroID = 0,
      int MemberNo = 0,
      int LogoutAt = 0,
      int ProfileIconID = 0,
      int ProfileBGID = 0,
      int EntitlementID = 0,
      int GuideClass = 0,
      int IntroduceID = 0,
      int AchievementCount = 0,
      int CollectionCount = 0) {
    builder.StartTable(12);
    UserInfo.AddCollectionCount(builder, CollectionCount);
    UserInfo.AddAchievementCount(builder, AchievementCount);
    UserInfo.AddIntroduceID(builder, IntroduceID);
    UserInfo.AddGuideClass(builder, GuideClass);
    UserInfo.AddEntitlementID(builder, EntitlementID);
    UserInfo.AddProfileBGID(builder, ProfileBGID);
    UserInfo.AddProfileIconID(builder, ProfileIconID);
    UserInfo.AddLogoutAt(builder, LogoutAt);
    UserInfo.AddMemberNo(builder, MemberNo);
    UserInfo.AddHeroID(builder, HeroID);
    UserInfo.AddNick(builder, NickOffset);
    UserInfo.AddUID(builder, UID);
    return UserInfo.EndUserInfo(builder);
  }

  public static void StartUserInfo(FlatBufferBuilder builder) { builder.StartTable(12); }
  public static void AddUID(FlatBufferBuilder builder, int UID) { builder.AddInt(0, UID, 0); }
  public static void AddNick(FlatBufferBuilder builder, StringOffset NickOffset) { builder.AddOffset(1, NickOffset.Value, 0); }
  public static void AddHeroID(FlatBufferBuilder builder, int HeroID) { builder.AddInt(2, HeroID, 0); }
  public static void AddMemberNo(FlatBufferBuilder builder, int MemberNo) { builder.AddInt(3, MemberNo, 0); }
  public static void AddLogoutAt(FlatBufferBuilder builder, int LogoutAt) { builder.AddInt(4, LogoutAt, 0); }
  public static void AddProfileIconID(FlatBufferBuilder builder, int ProfileIconID) { builder.AddInt(5, ProfileIconID, 0); }
  public static void AddProfileBGID(FlatBufferBuilder builder, int ProfileBGID) { builder.AddInt(6, ProfileBGID, 0); }
  public static void AddEntitlementID(FlatBufferBuilder builder, int EntitlementID) { builder.AddInt(7, EntitlementID, 0); }
  public static void AddGuideClass(FlatBufferBuilder builder, int GuideClass) { builder.AddInt(8, GuideClass, 0); }
  public static void AddIntroduceID(FlatBufferBuilder builder, int IntroduceID) { builder.AddInt(9, IntroduceID, 0); }
  public static void AddAchievementCount(FlatBufferBuilder builder, int AchievementCount) { builder.AddInt(10, AchievementCount, 0); }
  public static void AddCollectionCount(FlatBufferBuilder builder, int CollectionCount) { builder.AddInt(11, CollectionCount, 0); }
  public static Offset<Lobby.UserInfo> EndUserInfo(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<Lobby.UserInfo>(o);
  }
  public UserInfoT UnPack() {
    var _o = new UserInfoT();
    this.UnPackTo(_o);
    return _o;
  }
  public void UnPackTo(UserInfoT _o) {
    _o.UID = this.UID;
    _o.Nick = this.Nick;
    _o.HeroID = this.HeroID;
    _o.MemberNo = this.MemberNo;
    _o.LogoutAt = this.LogoutAt;
    _o.ProfileIconID = this.ProfileIconID;
    _o.ProfileBGID = this.ProfileBGID;
    _o.EntitlementID = this.EntitlementID;
    _o.GuideClass = this.GuideClass;
    _o.IntroduceID = this.IntroduceID;
    _o.AchievementCount = this.AchievementCount;
    _o.CollectionCount = this.CollectionCount;
  }
  public static Offset<Lobby.UserInfo> Pack(FlatBufferBuilder builder, UserInfoT _o) {
    if (_o == null) return default(Offset<Lobby.UserInfo>);
    var _Nick = _o.Nick == null ? default(StringOffset) : builder.CreateString(_o.Nick);
    return CreateUserInfo(
      builder,
      _o.UID,
      _Nick,
      _o.HeroID,
      _o.MemberNo,
      _o.LogoutAt,
      _o.ProfileIconID,
      _o.ProfileBGID,
      _o.EntitlementID,
      _o.GuideClass,
      _o.IntroduceID,
      _o.AchievementCount,
      _o.CollectionCount);
  }
}

public class UserInfoT
{
  public int UID { get; set; }
  public string Nick { get; set; }
  public int HeroID { get; set; }
  public int MemberNo { get; set; }
  public int LogoutAt { get; set; }
  public int ProfileIconID { get; set; }
  public int ProfileBGID { get; set; }
  public int EntitlementID { get; set; }
  public int GuideClass { get; set; }
  public int IntroduceID { get; set; }
  public int AchievementCount { get; set; }
  public int CollectionCount { get; set; }

  public UserInfoT() {
    this.UID = 0;
    this.Nick = null;
    this.HeroID = 0;
    this.MemberNo = 0;
    this.LogoutAt = 0;
    this.ProfileIconID = 0;
    this.ProfileBGID = 0;
    this.EntitlementID = 0;
    this.GuideClass = 0;
    this.IntroduceID = 0;
    this.AchievementCount = 0;
    this.CollectionCount = 0;
  }
}


}
