// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Lobby
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct LobbyToLobby_ConfirmManagerArtifactList_Res : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_23_1_20(); }
  public static LobbyToLobby_ConfirmManagerArtifactList_Res GetRootAsLobbyToLobby_ConfirmManagerArtifactList_Res(ByteBuffer _bb) { return GetRootAsLobbyToLobby_ConfirmManagerArtifactList_Res(_bb, new LobbyToLobby_ConfirmManagerArtifactList_Res()); }
  public static LobbyToLobby_ConfirmManagerArtifactList_Res GetRootAsLobbyToLobby_ConfirmManagerArtifactList_Res(ByteBuffer _bb, LobbyToLobby_ConfirmManagerArtifactList_Res obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public LobbyToLobby_ConfirmManagerArtifactList_Res __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public int UID { get { int o = __p.__offset(4); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int MatchingTableID { get { int o = __p.__offset(6); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public string MatchID { get { int o = __p.__offset(8); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetMatchIDBytes() { return __p.__vector_as_span<byte>(8, 1); }
#else
  public ArraySegment<byte>? GetMatchIDBytes() { return __p.__vector_as_arraysegment(8); }
#endif
  public byte[] GetMatchIDArray() { return __p.__vector_as_array<byte>(8); }
  public Lobby.MultiArtifactInfo? ArtifactList(int j) { int o = __p.__offset(10); return o != 0 ? (Lobby.MultiArtifactInfo?)(new Lobby.MultiArtifactInfo()).__assign(__p.__vector(o) + j * 8, __p.bb) : null; }
  public int ArtifactListLength { get { int o = __p.__offset(10); return o != 0 ? __p.__vector_len(o) : 0; } }

  public static Offset<Lobby.LobbyToLobby_ConfirmManagerArtifactList_Res> CreateLobbyToLobby_ConfirmManagerArtifactList_Res(FlatBufferBuilder builder,
      int UID = 0,
      int MatchingTableID = 0,
      StringOffset MatchIDOffset = default(StringOffset),
      VectorOffset ArtifactListOffset = default(VectorOffset)) {
    builder.StartTable(4);
    LobbyToLobby_ConfirmManagerArtifactList_Res.AddArtifactList(builder, ArtifactListOffset);
    LobbyToLobby_ConfirmManagerArtifactList_Res.AddMatchID(builder, MatchIDOffset);
    LobbyToLobby_ConfirmManagerArtifactList_Res.AddMatchingTableID(builder, MatchingTableID);
    LobbyToLobby_ConfirmManagerArtifactList_Res.AddUID(builder, UID);
    return LobbyToLobby_ConfirmManagerArtifactList_Res.EndLobbyToLobby_ConfirmManagerArtifactList_Res(builder);
  }

  public static void StartLobbyToLobby_ConfirmManagerArtifactList_Res(FlatBufferBuilder builder) { builder.StartTable(4); }
  public static void AddUID(FlatBufferBuilder builder, int UID) { builder.AddInt(0, UID, 0); }
  public static void AddMatchingTableID(FlatBufferBuilder builder, int MatchingTableID) { builder.AddInt(1, MatchingTableID, 0); }
  public static void AddMatchID(FlatBufferBuilder builder, StringOffset MatchIDOffset) { builder.AddOffset(2, MatchIDOffset.Value, 0); }
  public static void AddArtifactList(FlatBufferBuilder builder, VectorOffset ArtifactListOffset) { builder.AddOffset(3, ArtifactListOffset.Value, 0); }
  public static void StartArtifactListVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(8, numElems, 4); }
  public static Offset<Lobby.LobbyToLobby_ConfirmManagerArtifactList_Res> EndLobbyToLobby_ConfirmManagerArtifactList_Res(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<Lobby.LobbyToLobby_ConfirmManagerArtifactList_Res>(o);
  }
  public LobbyToLobby_ConfirmManagerArtifactList_ResT UnPack() {
    var _o = new LobbyToLobby_ConfirmManagerArtifactList_ResT();
    this.UnPackTo(_o);
    return _o;
  }
  public void UnPackTo(LobbyToLobby_ConfirmManagerArtifactList_ResT _o) {
    _o.UID = this.UID;
    _o.MatchingTableID = this.MatchingTableID;
    _o.MatchID = this.MatchID;
    _o.ArtifactList = new List<Lobby.MultiArtifactInfoT>();
    for (var _j = 0; _j < this.ArtifactListLength; ++_j) {_o.ArtifactList.Add(this.ArtifactList(_j).HasValue ? this.ArtifactList(_j).Value.UnPack() : null);}
  }
  public static Offset<Lobby.LobbyToLobby_ConfirmManagerArtifactList_Res> Pack(FlatBufferBuilder builder, LobbyToLobby_ConfirmManagerArtifactList_ResT _o) {
    if (_o == null) return default(Offset<Lobby.LobbyToLobby_ConfirmManagerArtifactList_Res>);
    var _MatchID = _o.MatchID == null ? default(StringOffset) : builder.CreateString(_o.MatchID);
    var _ArtifactList = default(VectorOffset);
    if (_o.ArtifactList != null) {
      StartArtifactListVector(builder, _o.ArtifactList.Count);
      for (var _j = _o.ArtifactList.Count - 1; _j >= 0; --_j) { Lobby.MultiArtifactInfo.Pack(builder, _o.ArtifactList[_j]); }
      _ArtifactList = builder.EndVector();
    }
    return CreateLobbyToLobby_ConfirmManagerArtifactList_Res(
      builder,
      _o.UID,
      _o.MatchingTableID,
      _MatchID,
      _ArtifactList);
  }
}

public class LobbyToLobby_ConfirmManagerArtifactList_ResT
{
  public int UID { get; set; }
  public int MatchingTableID { get; set; }
  public string MatchID { get; set; }
  public List<Lobby.MultiArtifactInfoT> ArtifactList { get; set; }

  public LobbyToLobby_ConfirmManagerArtifactList_ResT() {
    this.UID = 0;
    this.MatchingTableID = 0;
    this.MatchID = null;
    this.ArtifactList = null;
  }
}


}
