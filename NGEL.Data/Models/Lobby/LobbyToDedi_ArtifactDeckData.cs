// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Lobby
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct LobbyToDedi_ArtifactDeckData : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_23_1_20(); }
  public static LobbyToDedi_ArtifactDeckData GetRootAsLobbyToDedi_ArtifactDeckData(ByteBuffer _bb) { return GetRootAsLobbyToDedi_ArtifactDeckData(_bb, new LobbyToDedi_ArtifactDeckData()); }
  public static LobbyToDedi_ArtifactDeckData GetRootAsLobbyToDedi_ArtifactDeckData(ByteBuffer _bb, LobbyToDedi_ArtifactDeckData obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public LobbyToDedi_ArtifactDeckData __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public int UID { get { int o = __p.__offset(4); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public byte SlotID { get { int o = __p.__offset(6); return o != 0 ? __p.bb.Get(o + __p.bb_pos) : (byte)0; } }
  public Lobby.MultiArtifactInfo? ArtifactList(int j) { int o = __p.__offset(8); return o != 0 ? (Lobby.MultiArtifactInfo?)(new Lobby.MultiArtifactInfo()).__assign(__p.__vector(o) + j * 8, __p.bb) : null; }
  public int ArtifactListLength { get { int o = __p.__offset(8); return o != 0 ? __p.__vector_len(o) : 0; } }

  public static Offset<Lobby.LobbyToDedi_ArtifactDeckData> CreateLobbyToDedi_ArtifactDeckData(FlatBufferBuilder builder,
      int UID = 0,
      byte SlotID = 0,
      VectorOffset ArtifactListOffset = default(VectorOffset)) {
    builder.StartTable(3);
    LobbyToDedi_ArtifactDeckData.AddArtifactList(builder, ArtifactListOffset);
    LobbyToDedi_ArtifactDeckData.AddUID(builder, UID);
    LobbyToDedi_ArtifactDeckData.AddSlotID(builder, SlotID);
    return LobbyToDedi_ArtifactDeckData.EndLobbyToDedi_ArtifactDeckData(builder);
  }

  public static void StartLobbyToDedi_ArtifactDeckData(FlatBufferBuilder builder) { builder.StartTable(3); }
  public static void AddUID(FlatBufferBuilder builder, int UID) { builder.AddInt(0, UID, 0); }
  public static void AddSlotID(FlatBufferBuilder builder, byte SlotID) { builder.AddByte(1, SlotID, 0); }
  public static void AddArtifactList(FlatBufferBuilder builder, VectorOffset ArtifactListOffset) { builder.AddOffset(2, ArtifactListOffset.Value, 0); }
  public static void StartArtifactListVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(8, numElems, 4); }
  public static Offset<Lobby.LobbyToDedi_ArtifactDeckData> EndLobbyToDedi_ArtifactDeckData(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<Lobby.LobbyToDedi_ArtifactDeckData>(o);
  }
  public LobbyToDedi_ArtifactDeckDataT UnPack() {
    var _o = new LobbyToDedi_ArtifactDeckDataT();
    this.UnPackTo(_o);
    return _o;
  }
  public void UnPackTo(LobbyToDedi_ArtifactDeckDataT _o) {
    _o.UID = this.UID;
    _o.SlotID = this.SlotID;
    _o.ArtifactList = new List<Lobby.MultiArtifactInfoT>();
    for (var _j = 0; _j < this.ArtifactListLength; ++_j) {_o.ArtifactList.Add(this.ArtifactList(_j).HasValue ? this.ArtifactList(_j).Value.UnPack() : null);}
  }
  public static Offset<Lobby.LobbyToDedi_ArtifactDeckData> Pack(FlatBufferBuilder builder, LobbyToDedi_ArtifactDeckDataT _o) {
    if (_o == null) return default(Offset<Lobby.LobbyToDedi_ArtifactDeckData>);
    var _ArtifactList = default(VectorOffset);
    if (_o.ArtifactList != null) {
      StartArtifactListVector(builder, _o.ArtifactList.Count);
      for (var _j = _o.ArtifactList.Count - 1; _j >= 0; --_j) { Lobby.MultiArtifactInfo.Pack(builder, _o.ArtifactList[_j]); }
      _ArtifactList = builder.EndVector();
    }
    return CreateLobbyToDedi_ArtifactDeckData(
      builder,
      _o.UID,
      _o.SlotID,
      _ArtifactList);
  }
}

public class LobbyToDedi_ArtifactDeckDataT
{
  public int UID { get; set; }
  public byte SlotID { get; set; }
  public List<Lobby.MultiArtifactInfoT> ArtifactList { get; set; }

  public LobbyToDedi_ArtifactDeckDataT() {
    this.UID = 0;
    this.SlotID = 0;
    this.ArtifactList = null;
  }
}


}
