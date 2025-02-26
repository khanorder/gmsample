// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Lobby
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct AssetData : IFlatbufferObject
{
  private Struct __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public void __init(int _i, ByteBuffer _bb) { __p = new Struct(_i, _bb); }
  public AssetData __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public Lobby.EUserAssetType AssetID { get { return (Lobby.EUserAssetType)__p.bb.GetUint(__p.bb_pos + 0); } }
  public int Count { get { return __p.bb.GetInt(__p.bb_pos + 4); } }
  public int RecoverAt { get { return __p.bb.GetInt(__p.bb_pos + 8); } }

  public static Offset<Lobby.AssetData> CreateAssetData(FlatBufferBuilder builder, Lobby.EUserAssetType AssetID, int Count, int RecoverAt) {
    builder.Prep(4, 12);
    builder.PutInt(RecoverAt);
    builder.PutInt(Count);
    builder.PutUint((uint)AssetID);
    return new Offset<Lobby.AssetData>(builder.Offset);
  }
  public AssetDataT UnPack() {
    var _o = new AssetDataT();
    this.UnPackTo(_o);
    return _o;
  }
  public void UnPackTo(AssetDataT _o) {
    _o.AssetID = this.AssetID;
    _o.Count = this.Count;
    _o.RecoverAt = this.RecoverAt;
  }
  public static Offset<Lobby.AssetData> Pack(FlatBufferBuilder builder, AssetDataT _o) {
    if (_o == null) return default(Offset<Lobby.AssetData>);
    return CreateAssetData(
      builder,
      _o.AssetID,
      _o.Count,
      _o.RecoverAt);
  }
}

public class AssetDataT
{
  public Lobby.EUserAssetType AssetID { get; set; }
  public int Count { get; set; }
  public int RecoverAt { get; set; }

  public AssetDataT() {
    this.AssetID = Lobby.EUserAssetType.None;
    this.Count = 0;
    this.RecoverAt = 0;
  }
}


}
