// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Lobby
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct SA_ExpressionPresetSave : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_23_1_20(); }
  public static SA_ExpressionPresetSave GetRootAsSA_ExpressionPresetSave(ByteBuffer _bb) { return GetRootAsSA_ExpressionPresetSave(_bb, new SA_ExpressionPresetSave()); }
  public static SA_ExpressionPresetSave GetRootAsSA_ExpressionPresetSave(ByteBuffer _bb, SA_ExpressionPresetSave obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public SA_ExpressionPresetSave __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public Lobby.ExpressionPresetData? ExpressionPreset { get { int o = __p.__offset(4); return o != 0 ? (Lobby.ExpressionPresetData?)(new Lobby.ExpressionPresetData()).__assign(__p.__indirect(o + __p.bb_pos), __p.bb) : null; } }

  public static Offset<Lobby.SA_ExpressionPresetSave> CreateSA_ExpressionPresetSave(FlatBufferBuilder builder,
      Offset<Lobby.ExpressionPresetData> ExpressionPresetOffset = default(Offset<Lobby.ExpressionPresetData>)) {
    builder.StartTable(1);
    SA_ExpressionPresetSave.AddExpressionPreset(builder, ExpressionPresetOffset);
    return SA_ExpressionPresetSave.EndSA_ExpressionPresetSave(builder);
  }

  public static void StartSA_ExpressionPresetSave(FlatBufferBuilder builder) { builder.StartTable(1); }
  public static void AddExpressionPreset(FlatBufferBuilder builder, Offset<Lobby.ExpressionPresetData> ExpressionPresetOffset) { builder.AddOffset(0, ExpressionPresetOffset.Value, 0); }
  public static Offset<Lobby.SA_ExpressionPresetSave> EndSA_ExpressionPresetSave(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<Lobby.SA_ExpressionPresetSave>(o);
  }
  public SA_ExpressionPresetSaveT UnPack() {
    var _o = new SA_ExpressionPresetSaveT();
    this.UnPackTo(_o);
    return _o;
  }
  public void UnPackTo(SA_ExpressionPresetSaveT _o) {
    _o.ExpressionPreset = this.ExpressionPreset.HasValue ? this.ExpressionPreset.Value.UnPack() : null;
  }
  public static Offset<Lobby.SA_ExpressionPresetSave> Pack(FlatBufferBuilder builder, SA_ExpressionPresetSaveT _o) {
    if (_o == null) return default(Offset<Lobby.SA_ExpressionPresetSave>);
    var _ExpressionPreset = _o.ExpressionPreset == null ? default(Offset<Lobby.ExpressionPresetData>) : Lobby.ExpressionPresetData.Pack(builder, _o.ExpressionPreset);
    return CreateSA_ExpressionPresetSave(
      builder,
      _ExpressionPreset);
  }
}

public class SA_ExpressionPresetSaveT
{
  public Lobby.ExpressionPresetDataT ExpressionPreset { get; set; }

  public SA_ExpressionPresetSaveT() {
    this.ExpressionPreset = null;
  }
}


}
