// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Lobby
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct RecommendUserInfo : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_23_1_20(); }
  public static RecommendUserInfo GetRootAsRecommendUserInfo(ByteBuffer _bb) { return GetRootAsRecommendUserInfo(_bb, new RecommendUserInfo()); }
  public static RecommendUserInfo GetRootAsRecommendUserInfo(ByteBuffer _bb, RecommendUserInfo obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public RecommendUserInfo __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public int UID { get { int o = __p.__offset(4); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public string Nick { get { int o = __p.__offset(6); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetNickBytes() { return __p.__vector_as_span<byte>(6, 1); }
#else
  public ArraySegment<byte>? GetNickBytes() { return __p.__vector_as_arraysegment(6); }
#endif
  public byte[] GetNickArray() { return __p.__vector_as_array<byte>(6); }
  public int EntitlementID { get { int o = __p.__offset(8); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int ProfileIconID { get { int o = __p.__offset(10); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int ProfileBGID { get { int o = __p.__offset(12); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }

  public static Offset<Lobby.RecommendUserInfo> CreateRecommendUserInfo(FlatBufferBuilder builder,
      int UID = 0,
      StringOffset NickOffset = default(StringOffset),
      int EntitlementID = 0,
      int ProfileIconID = 0,
      int ProfileBGID = 0) {
    builder.StartTable(5);
    RecommendUserInfo.AddProfileBGID(builder, ProfileBGID);
    RecommendUserInfo.AddProfileIconID(builder, ProfileIconID);
    RecommendUserInfo.AddEntitlementID(builder, EntitlementID);
    RecommendUserInfo.AddNick(builder, NickOffset);
    RecommendUserInfo.AddUID(builder, UID);
    return RecommendUserInfo.EndRecommendUserInfo(builder);
  }

  public static void StartRecommendUserInfo(FlatBufferBuilder builder) { builder.StartTable(5); }
  public static void AddUID(FlatBufferBuilder builder, int UID) { builder.AddInt(0, UID, 0); }
  public static void AddNick(FlatBufferBuilder builder, StringOffset NickOffset) { builder.AddOffset(1, NickOffset.Value, 0); }
  public static void AddEntitlementID(FlatBufferBuilder builder, int EntitlementID) { builder.AddInt(2, EntitlementID, 0); }
  public static void AddProfileIconID(FlatBufferBuilder builder, int ProfileIconID) { builder.AddInt(3, ProfileIconID, 0); }
  public static void AddProfileBGID(FlatBufferBuilder builder, int ProfileBGID) { builder.AddInt(4, ProfileBGID, 0); }
  public static Offset<Lobby.RecommendUserInfo> EndRecommendUserInfo(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<Lobby.RecommendUserInfo>(o);
  }
  public RecommendUserInfoT UnPack() {
    var _o = new RecommendUserInfoT();
    this.UnPackTo(_o);
    return _o;
  }
  public void UnPackTo(RecommendUserInfoT _o) {
    _o.UID = this.UID;
    _o.Nick = this.Nick;
    _o.EntitlementID = this.EntitlementID;
    _o.ProfileIconID = this.ProfileIconID;
    _o.ProfileBGID = this.ProfileBGID;
  }
  public static Offset<Lobby.RecommendUserInfo> Pack(FlatBufferBuilder builder, RecommendUserInfoT _o) {
    if (_o == null) return default(Offset<Lobby.RecommendUserInfo>);
    var _Nick = _o.Nick == null ? default(StringOffset) : builder.CreateString(_o.Nick);
    return CreateRecommendUserInfo(
      builder,
      _o.UID,
      _Nick,
      _o.EntitlementID,
      _o.ProfileIconID,
      _o.ProfileBGID);
  }
}

public class RecommendUserInfoT
{
  public int UID { get; set; }
  public string Nick { get; set; }
  public int EntitlementID { get; set; }
  public int ProfileIconID { get; set; }
  public int ProfileBGID { get; set; }

  public RecommendUserInfoT() {
    this.UID = 0;
    this.Nick = null;
    this.EntitlementID = 0;
    this.ProfileIconID = 0;
    this.ProfileBGID = 0;
  }
}


}
