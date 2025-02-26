// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Lobby
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct SA_GlitchStoreBuy : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_23_1_20(); }
  public static SA_GlitchStoreBuy GetRootAsSA_GlitchStoreBuy(ByteBuffer _bb) { return GetRootAsSA_GlitchStoreBuy(_bb, new SA_GlitchStoreBuy()); }
  public static SA_GlitchStoreBuy GetRootAsSA_GlitchStoreBuy(ByteBuffer _bb, SA_GlitchStoreBuy obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public SA_GlitchStoreBuy __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public int StoreID { get { int o = __p.__offset(4); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public Lobby.AssetData? ChangeAsset { get { int o = __p.__offset(6); return o != 0 ? (Lobby.AssetData?)(new Lobby.AssetData()).__assign(o + __p.bb_pos, __p.bb) : null; } }
  public Lobby.ArtifactData? ChangeArtifact { get { int o = __p.__offset(8); return o != 0 ? (Lobby.ArtifactData?)(new Lobby.ArtifactData()).__assign(o + __p.bb_pos, __p.bb) : null; } }
  public Lobby.PetData? ChangePet { get { int o = __p.__offset(10); return o != 0 ? (Lobby.PetData?)(new Lobby.PetData()).__assign(__p.__indirect(o + __p.bb_pos), __p.bb) : null; } }

  public static Offset<Lobby.SA_GlitchStoreBuy> CreateSA_GlitchStoreBuy(FlatBufferBuilder builder,
      int StoreID = 0,
      Lobby.AssetDataT ChangeAsset = null,
      Lobby.ArtifactDataT ChangeArtifact = null,
      Offset<Lobby.PetData> ChangePetOffset = default(Offset<Lobby.PetData>)) {
    builder.StartTable(4);
    SA_GlitchStoreBuy.AddChangePet(builder, ChangePetOffset);
    SA_GlitchStoreBuy.AddChangeArtifact(builder, Lobby.ArtifactData.Pack(builder, ChangeArtifact));
    SA_GlitchStoreBuy.AddChangeAsset(builder, Lobby.AssetData.Pack(builder, ChangeAsset));
    SA_GlitchStoreBuy.AddStoreID(builder, StoreID);
    return SA_GlitchStoreBuy.EndSA_GlitchStoreBuy(builder);
  }

  public static void StartSA_GlitchStoreBuy(FlatBufferBuilder builder) { builder.StartTable(4); }
  public static void AddStoreID(FlatBufferBuilder builder, int StoreID) { builder.AddInt(0, StoreID, 0); }
  public static void AddChangeAsset(FlatBufferBuilder builder, Offset<Lobby.AssetData> ChangeAssetOffset) { builder.AddStruct(1, ChangeAssetOffset.Value, 0); }
  public static void AddChangeArtifact(FlatBufferBuilder builder, Offset<Lobby.ArtifactData> ChangeArtifactOffset) { builder.AddStruct(2, ChangeArtifactOffset.Value, 0); }
  public static void AddChangePet(FlatBufferBuilder builder, Offset<Lobby.PetData> ChangePetOffset) { builder.AddOffset(3, ChangePetOffset.Value, 0); }
  public static Offset<Lobby.SA_GlitchStoreBuy> EndSA_GlitchStoreBuy(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<Lobby.SA_GlitchStoreBuy>(o);
  }
  public SA_GlitchStoreBuyT UnPack() {
    var _o = new SA_GlitchStoreBuyT();
    this.UnPackTo(_o);
    return _o;
  }
  public void UnPackTo(SA_GlitchStoreBuyT _o) {
    _o.StoreID = this.StoreID;
    _o.ChangeAsset = this.ChangeAsset.HasValue ? this.ChangeAsset.Value.UnPack() : null;
    _o.ChangeArtifact = this.ChangeArtifact.HasValue ? this.ChangeArtifact.Value.UnPack() : null;
    _o.ChangePet = this.ChangePet.HasValue ? this.ChangePet.Value.UnPack() : null;
  }
  public static Offset<Lobby.SA_GlitchStoreBuy> Pack(FlatBufferBuilder builder, SA_GlitchStoreBuyT _o) {
    if (_o == null) return default(Offset<Lobby.SA_GlitchStoreBuy>);
    var _ChangePet = _o.ChangePet == null ? default(Offset<Lobby.PetData>) : Lobby.PetData.Pack(builder, _o.ChangePet);
    return CreateSA_GlitchStoreBuy(
      builder,
      _o.StoreID,
      _o.ChangeAsset,
      _o.ChangeArtifact,
      _ChangePet);
  }
}

public class SA_GlitchStoreBuyT
{
  public int StoreID { get; set; }
  public Lobby.AssetDataT ChangeAsset { get; set; }
  public Lobby.ArtifactDataT ChangeArtifact { get; set; }
  public Lobby.PetDataT ChangePet { get; set; }

  public SA_GlitchStoreBuyT() {
    this.StoreID = 0;
    this.ChangeAsset = new Lobby.AssetDataT();
    this.ChangeArtifact = new Lobby.ArtifactDataT();
    this.ChangePet = null;
  }
}


}
