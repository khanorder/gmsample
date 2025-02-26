// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Lobby
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct SA_FriendRequestList : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_23_1_20(); }
  public static SA_FriendRequestList GetRootAsSA_FriendRequestList(ByteBuffer _bb) { return GetRootAsSA_FriendRequestList(_bb, new SA_FriendRequestList()); }
  public static SA_FriendRequestList GetRootAsSA_FriendRequestList(ByteBuffer _bb, SA_FriendRequestList obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public SA_FriendRequestList __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public Lobby.FriendRequestData? FriendRequestList(int j) { int o = __p.__offset(4); return o != 0 ? (Lobby.FriendRequestData?)(new Lobby.FriendRequestData()).__assign(__p.__indirect(__p.__vector(o) + j * 4), __p.bb) : null; }
  public int FriendRequestListLength { get { int o = __p.__offset(4); return o != 0 ? __p.__vector_len(o) : 0; } }

  public static Offset<Lobby.SA_FriendRequestList> CreateSA_FriendRequestList(FlatBufferBuilder builder,
      VectorOffset FriendRequestListOffset = default(VectorOffset)) {
    builder.StartTable(1);
    SA_FriendRequestList.AddFriendRequestList(builder, FriendRequestListOffset);
    return SA_FriendRequestList.EndSA_FriendRequestList(builder);
  }

  public static void StartSA_FriendRequestList(FlatBufferBuilder builder) { builder.StartTable(1); }
  public static void AddFriendRequestList(FlatBufferBuilder builder, VectorOffset FriendRequestListOffset) { builder.AddOffset(0, FriendRequestListOffset.Value, 0); }
  public static VectorOffset CreateFriendRequestListVector(FlatBufferBuilder builder, Offset<Lobby.FriendRequestData>[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddOffset(data[i].Value); return builder.EndVector(); }
  public static VectorOffset CreateFriendRequestListVectorBlock(FlatBufferBuilder builder, Offset<Lobby.FriendRequestData>[] data) { builder.StartVector(4, data.Length, 4); builder.Add(data); return builder.EndVector(); }
  public static VectorOffset CreateFriendRequestListVectorBlock(FlatBufferBuilder builder, ArraySegment<Offset<Lobby.FriendRequestData>> data) { builder.StartVector(4, data.Count, 4); builder.Add(data); return builder.EndVector(); }
  public static VectorOffset CreateFriendRequestListVectorBlock(FlatBufferBuilder builder, IntPtr dataPtr, int sizeInBytes) { builder.StartVector(1, sizeInBytes, 1); builder.Add<Offset<Lobby.FriendRequestData>>(dataPtr, sizeInBytes); return builder.EndVector(); }
  public static void StartFriendRequestListVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static Offset<Lobby.SA_FriendRequestList> EndSA_FriendRequestList(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<Lobby.SA_FriendRequestList>(o);
  }
  public SA_FriendRequestListT UnPack() {
    var _o = new SA_FriendRequestListT();
    this.UnPackTo(_o);
    return _o;
  }
  public void UnPackTo(SA_FriendRequestListT _o) {
    _o.FriendRequestList = new List<Lobby.FriendRequestDataT>();
    for (var _j = 0; _j < this.FriendRequestListLength; ++_j) {_o.FriendRequestList.Add(this.FriendRequestList(_j).HasValue ? this.FriendRequestList(_j).Value.UnPack() : null);}
  }
  public static Offset<Lobby.SA_FriendRequestList> Pack(FlatBufferBuilder builder, SA_FriendRequestListT _o) {
    if (_o == null) return default(Offset<Lobby.SA_FriendRequestList>);
    var _FriendRequestList = default(VectorOffset);
    if (_o.FriendRequestList != null) {
      var __FriendRequestList = new Offset<Lobby.FriendRequestData>[_o.FriendRequestList.Count];
      for (var _j = 0; _j < __FriendRequestList.Length; ++_j) { __FriendRequestList[_j] = Lobby.FriendRequestData.Pack(builder, _o.FriendRequestList[_j]); }
      _FriendRequestList = CreateFriendRequestListVector(builder, __FriendRequestList);
    }
    return CreateSA_FriendRequestList(
      builder,
      _FriendRequestList);
  }
}

public class SA_FriendRequestListT
{
  public List<Lobby.FriendRequestDataT> FriendRequestList { get; set; }

  public SA_FriendRequestListT() {
    this.FriendRequestList = null;
  }
}


}
