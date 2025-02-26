// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Lobby
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct SA_HeroExperienceTicketUse : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_23_1_20(); }
  public static SA_HeroExperienceTicketUse GetRootAsSA_HeroExperienceTicketUse(ByteBuffer _bb) { return GetRootAsSA_HeroExperienceTicketUse(_bb, new SA_HeroExperienceTicketUse()); }
  public static SA_HeroExperienceTicketUse GetRootAsSA_HeroExperienceTicketUse(ByteBuffer _bb, SA_HeroExperienceTicketUse obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public SA_HeroExperienceTicketUse __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public Lobby.NewHeroData? NewHero { get { int o = __p.__offset(4); return o != 0 ? (Lobby.NewHeroData?)(new Lobby.NewHeroData()).__assign(__p.__indirect(o + __p.bb_pos), __p.bb) : null; } }
  public Lobby.ItemData? ChangeItemList(int j) { int o = __p.__offset(6); return o != 0 ? (Lobby.ItemData?)(new Lobby.ItemData()).__assign(__p.__vector(o) + j * 8, __p.bb) : null; }
  public int ChangeItemListLength { get { int o = __p.__offset(6); return o != 0 ? __p.__vector_len(o) : 0; } }

  public static Offset<Lobby.SA_HeroExperienceTicketUse> CreateSA_HeroExperienceTicketUse(FlatBufferBuilder builder,
      Offset<Lobby.NewHeroData> newHeroOffset = default(Offset<Lobby.NewHeroData>),
      VectorOffset ChangeItemListOffset = default(VectorOffset)) {
    builder.StartTable(2);
    SA_HeroExperienceTicketUse.AddChangeItemList(builder, ChangeItemListOffset);
    SA_HeroExperienceTicketUse.AddNewHero(builder, newHeroOffset);
    return SA_HeroExperienceTicketUse.EndSA_HeroExperienceTicketUse(builder);
  }

  public static void StartSA_HeroExperienceTicketUse(FlatBufferBuilder builder) { builder.StartTable(2); }
  public static void AddNewHero(FlatBufferBuilder builder, Offset<Lobby.NewHeroData> newHeroOffset) { builder.AddOffset(0, newHeroOffset.Value, 0); }
  public static void AddChangeItemList(FlatBufferBuilder builder, VectorOffset ChangeItemListOffset) { builder.AddOffset(1, ChangeItemListOffset.Value, 0); }
  public static void StartChangeItemListVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(8, numElems, 4); }
  public static Offset<Lobby.SA_HeroExperienceTicketUse> EndSA_HeroExperienceTicketUse(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<Lobby.SA_HeroExperienceTicketUse>(o);
  }
  public SA_HeroExperienceTicketUseT UnPack() {
    var _o = new SA_HeroExperienceTicketUseT();
    this.UnPackTo(_o);
    return _o;
  }
  public void UnPackTo(SA_HeroExperienceTicketUseT _o) {
    _o.NewHero = this.NewHero.HasValue ? this.NewHero.Value.UnPack() : null;
    _o.ChangeItemList = new List<Lobby.ItemDataT>();
    for (var _j = 0; _j < this.ChangeItemListLength; ++_j) {_o.ChangeItemList.Add(this.ChangeItemList(_j).HasValue ? this.ChangeItemList(_j).Value.UnPack() : null);}
  }
  public static Offset<Lobby.SA_HeroExperienceTicketUse> Pack(FlatBufferBuilder builder, SA_HeroExperienceTicketUseT _o) {
    if (_o == null) return default(Offset<Lobby.SA_HeroExperienceTicketUse>);
    var _newHero = _o.NewHero == null ? default(Offset<Lobby.NewHeroData>) : Lobby.NewHeroData.Pack(builder, _o.NewHero);
    var _ChangeItemList = default(VectorOffset);
    if (_o.ChangeItemList != null) {
      StartChangeItemListVector(builder, _o.ChangeItemList.Count);
      for (var _j = _o.ChangeItemList.Count - 1; _j >= 0; --_j) { Lobby.ItemData.Pack(builder, _o.ChangeItemList[_j]); }
      _ChangeItemList = builder.EndVector();
    }
    return CreateSA_HeroExperienceTicketUse(
      builder,
      _newHero,
      _ChangeItemList);
  }
}

public class SA_HeroExperienceTicketUseT
{
  public Lobby.NewHeroDataT NewHero { get; set; }
  public List<Lobby.ItemDataT> ChangeItemList { get; set; }

  public SA_HeroExperienceTicketUseT() {
    this.NewHero = null;
    this.ChangeItemList = null;
  }
}


}
