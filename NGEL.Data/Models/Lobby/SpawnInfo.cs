// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Lobby
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct SpawnInfo : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_23_1_20(); }
  public static SpawnInfo GetRootAsSpawnInfo(ByteBuffer _bb) { return GetRootAsSpawnInfo(_bb, new SpawnInfo()); }
  public static SpawnInfo GetRootAsSpawnInfo(ByteBuffer _bb, SpawnInfo obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public SpawnInfo __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public string TeamID { get { int o = __p.__offset(4); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetTeamIDBytes() { return __p.__vector_as_span<byte>(4, 1); }
#else
  public ArraySegment<byte>? GetTeamIDBytes() { return __p.__vector_as_arraysegment(4); }
#endif
  public byte[] GetTeamIDArray() { return __p.__vector_as_array<byte>(4); }
  public int HeroAITableID { get { int o = __p.__offset(6); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }

  public static Offset<Lobby.SpawnInfo> CreateSpawnInfo(FlatBufferBuilder builder,
      StringOffset TeamIDOffset = default(StringOffset),
      int HeroAITableID = 0) {
    builder.StartTable(2);
    SpawnInfo.AddHeroAITableID(builder, HeroAITableID);
    SpawnInfo.AddTeamID(builder, TeamIDOffset);
    return SpawnInfo.EndSpawnInfo(builder);
  }

  public static void StartSpawnInfo(FlatBufferBuilder builder) { builder.StartTable(2); }
  public static void AddTeamID(FlatBufferBuilder builder, StringOffset TeamIDOffset) { builder.AddOffset(0, TeamIDOffset.Value, 0); }
  public static void AddHeroAITableID(FlatBufferBuilder builder, int HeroAITableID) { builder.AddInt(1, HeroAITableID, 0); }
  public static Offset<Lobby.SpawnInfo> EndSpawnInfo(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<Lobby.SpawnInfo>(o);
  }
  public SpawnInfoT UnPack() {
    var _o = new SpawnInfoT();
    this.UnPackTo(_o);
    return _o;
  }
  public void UnPackTo(SpawnInfoT _o) {
    _o.TeamID = this.TeamID;
    _o.HeroAITableID = this.HeroAITableID;
  }
  public static Offset<Lobby.SpawnInfo> Pack(FlatBufferBuilder builder, SpawnInfoT _o) {
    if (_o == null) return default(Offset<Lobby.SpawnInfo>);
    var _TeamID = _o.TeamID == null ? default(StringOffset) : builder.CreateString(_o.TeamID);
    return CreateSpawnInfo(
      builder,
      _TeamID,
      _o.HeroAITableID);
  }
}

public class SpawnInfoT
{
  public string TeamID { get; set; }
  public int HeroAITableID { get; set; }

  public SpawnInfoT() {
    this.TeamID = null;
    this.HeroAITableID = 0;
  }
}


}
