// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Lobby
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct SA_ModifyNickName : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_23_1_20(); }
  public static SA_ModifyNickName GetRootAsSA_ModifyNickName(ByteBuffer _bb) { return GetRootAsSA_ModifyNickName(_bb, new SA_ModifyNickName()); }
  public static SA_ModifyNickName GetRootAsSA_ModifyNickName(ByteBuffer _bb, SA_ModifyNickName obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public SA_ModifyNickName __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public string NickName { get { int o = __p.__offset(4); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetNickNameBytes() { return __p.__vector_as_span<byte>(4, 1); }
#else
  public ArraySegment<byte>? GetNickNameBytes() { return __p.__vector_as_arraysegment(4); }
#endif
  public byte[] GetNickNameArray() { return __p.__vector_as_array<byte>(4); }
  public Lobby.ItemData? ChangeItemList(int j) { int o = __p.__offset(6); return o != 0 ? (Lobby.ItemData?)(new Lobby.ItemData()).__assign(__p.__vector(o) + j * 8, __p.bb) : null; }
  public int ChangeItemListLength { get { int o = __p.__offset(6); return o != 0 ? __p.__vector_len(o) : 0; } }

  public static Offset<Lobby.SA_ModifyNickName> CreateSA_ModifyNickName(FlatBufferBuilder builder,
      StringOffset NickNameOffset = default(StringOffset),
      VectorOffset ChangeItemListOffset = default(VectorOffset)) {
    builder.StartTable(2);
    SA_ModifyNickName.AddChangeItemList(builder, ChangeItemListOffset);
    SA_ModifyNickName.AddNickName(builder, NickNameOffset);
    return SA_ModifyNickName.EndSA_ModifyNickName(builder);
  }

  public static void StartSA_ModifyNickName(FlatBufferBuilder builder) { builder.StartTable(2); }
  public static void AddNickName(FlatBufferBuilder builder, StringOffset NickNameOffset) { builder.AddOffset(0, NickNameOffset.Value, 0); }
  public static void AddChangeItemList(FlatBufferBuilder builder, VectorOffset ChangeItemListOffset) { builder.AddOffset(1, ChangeItemListOffset.Value, 0); }
  public static void StartChangeItemListVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(8, numElems, 4); }
  public static Offset<Lobby.SA_ModifyNickName> EndSA_ModifyNickName(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<Lobby.SA_ModifyNickName>(o);
  }
  public SA_ModifyNickNameT UnPack() {
    var _o = new SA_ModifyNickNameT();
    this.UnPackTo(_o);
    return _o;
  }
  public void UnPackTo(SA_ModifyNickNameT _o) {
    _o.NickName = this.NickName;
    _o.ChangeItemList = new List<Lobby.ItemDataT>();
    for (var _j = 0; _j < this.ChangeItemListLength; ++_j) {_o.ChangeItemList.Add(this.ChangeItemList(_j).HasValue ? this.ChangeItemList(_j).Value.UnPack() : null);}
  }
  public static Offset<Lobby.SA_ModifyNickName> Pack(FlatBufferBuilder builder, SA_ModifyNickNameT _o) {
    if (_o == null) return default(Offset<Lobby.SA_ModifyNickName>);
    var _NickName = _o.NickName == null ? default(StringOffset) : builder.CreateString(_o.NickName);
    var _ChangeItemList = default(VectorOffset);
    if (_o.ChangeItemList != null) {
      StartChangeItemListVector(builder, _o.ChangeItemList.Count);
      for (var _j = _o.ChangeItemList.Count - 1; _j >= 0; --_j) { Lobby.ItemData.Pack(builder, _o.ChangeItemList[_j]); }
      _ChangeItemList = builder.EndVector();
    }
    return CreateSA_ModifyNickName(
      builder,
      _NickName,
      _ChangeItemList);
  }
}

public class SA_ModifyNickNameT
{
  public string NickName { get; set; }
  public List<Lobby.ItemDataT> ChangeItemList { get; set; }

  public SA_ModifyNickNameT() {
    this.NickName = null;
    this.ChangeItemList = null;
  }
}


}
