// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Lobby
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct CQ_ArtifactDeckList : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_23_1_20(); }
  public static CQ_ArtifactDeckList GetRootAsCQ_ArtifactDeckList(ByteBuffer _bb) { return GetRootAsCQ_ArtifactDeckList(_bb, new CQ_ArtifactDeckList()); }
  public static CQ_ArtifactDeckList GetRootAsCQ_ArtifactDeckList(ByteBuffer _bb, CQ_ArtifactDeckList obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public CQ_ArtifactDeckList __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }


  public static void StartCQ_ArtifactDeckList(FlatBufferBuilder builder) { builder.StartTable(0); }
  public static Offset<Lobby.CQ_ArtifactDeckList> EndCQ_ArtifactDeckList(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<Lobby.CQ_ArtifactDeckList>(o);
  }
  public CQ_ArtifactDeckListT UnPack() {
    var _o = new CQ_ArtifactDeckListT();
    this.UnPackTo(_o);
    return _o;
  }
  public void UnPackTo(CQ_ArtifactDeckListT _o) {
  }
  public static Offset<Lobby.CQ_ArtifactDeckList> Pack(FlatBufferBuilder builder, CQ_ArtifactDeckListT _o) {
    if (_o == null) return default(Offset<Lobby.CQ_ArtifactDeckList>);
    StartCQ_ArtifactDeckList(builder);
    return EndCQ_ArtifactDeckList(builder);
  }
}

public class CQ_ArtifactDeckListT
{

  public CQ_ArtifactDeckListT() {
  }
}


}
