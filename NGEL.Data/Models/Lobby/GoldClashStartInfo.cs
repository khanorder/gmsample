// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Lobby
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct GoldClashStartInfo : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_23_1_20(); }
  public static GoldClashStartInfo GetRootAsGoldClashStartInfo(ByteBuffer _bb) { return GetRootAsGoldClashStartInfo(_bb, new GoldClashStartInfo()); }
  public static GoldClashStartInfo GetRootAsGoldClashStartInfo(ByteBuffer _bb, GoldClashStartInfo obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public GoldClashStartInfo __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public int UID { get { int o = __p.__offset(4); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int HeroID { get { int o = __p.__offset(6); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int AIJoinState { get { int o = __p.__offset(8); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int MyTeamAICount { get { int o = __p.__offset(10); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }

  public static Offset<Lobby.GoldClashStartInfo> CreateGoldClashStartInfo(FlatBufferBuilder builder,
      int UID = 0,
      int HeroID = 0,
      int AIJoinState = 0,
      int MyTeamAICount = 0) {
    builder.StartTable(4);
    GoldClashStartInfo.AddMyTeamAICount(builder, MyTeamAICount);
    GoldClashStartInfo.AddAIJoinState(builder, AIJoinState);
    GoldClashStartInfo.AddHeroID(builder, HeroID);
    GoldClashStartInfo.AddUID(builder, UID);
    return GoldClashStartInfo.EndGoldClashStartInfo(builder);
  }

  public static void StartGoldClashStartInfo(FlatBufferBuilder builder) { builder.StartTable(4); }
  public static void AddUID(FlatBufferBuilder builder, int UID) { builder.AddInt(0, UID, 0); }
  public static void AddHeroID(FlatBufferBuilder builder, int HeroID) { builder.AddInt(1, HeroID, 0); }
  public static void AddAIJoinState(FlatBufferBuilder builder, int AIJoinState) { builder.AddInt(2, AIJoinState, 0); }
  public static void AddMyTeamAICount(FlatBufferBuilder builder, int MyTeamAICount) { builder.AddInt(3, MyTeamAICount, 0); }
  public static Offset<Lobby.GoldClashStartInfo> EndGoldClashStartInfo(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<Lobby.GoldClashStartInfo>(o);
  }
  public GoldClashStartInfoT UnPack() {
    var _o = new GoldClashStartInfoT();
    this.UnPackTo(_o);
    return _o;
  }
  public void UnPackTo(GoldClashStartInfoT _o) {
    _o.UID = this.UID;
    _o.HeroID = this.HeroID;
    _o.AIJoinState = this.AIJoinState;
    _o.MyTeamAICount = this.MyTeamAICount;
  }
  public static Offset<Lobby.GoldClashStartInfo> Pack(FlatBufferBuilder builder, GoldClashStartInfoT _o) {
    if (_o == null) return default(Offset<Lobby.GoldClashStartInfo>);
    return CreateGoldClashStartInfo(
      builder,
      _o.UID,
      _o.HeroID,
      _o.AIJoinState,
      _o.MyTeamAICount);
  }
}

public class GoldClashStartInfoT
{
  public int UID { get; set; }
  public int HeroID { get; set; }
  public int AIJoinState { get; set; }
  public int MyTeamAICount { get; set; }

  public GoldClashStartInfoT() {
    this.UID = 0;
    this.HeroID = 0;
    this.AIJoinState = 0;
    this.MyTeamAICount = 0;
  }
}


}
