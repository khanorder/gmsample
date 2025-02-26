// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Lobby
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct MatchingAcceptanceUser : IFlatbufferObject
{
  private Struct __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public void __init(int _i, ByteBuffer _bb) { __p = new Struct(_i, _bb); }
  public MatchingAcceptanceUser __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public int UID { get { return __p.bb.GetInt(__p.bb_pos + 0); } }
  public int ProfileIconID { get { return __p.bb.GetInt(__p.bb_pos + 4); } }

  public static Offset<Lobby.MatchingAcceptanceUser> CreateMatchingAcceptanceUser(FlatBufferBuilder builder, int UID, int ProfileIconID) {
    builder.Prep(4, 8);
    builder.PutInt(ProfileIconID);
    builder.PutInt(UID);
    return new Offset<Lobby.MatchingAcceptanceUser>(builder.Offset);
  }
  public MatchingAcceptanceUserT UnPack() {
    var _o = new MatchingAcceptanceUserT();
    this.UnPackTo(_o);
    return _o;
  }
  public void UnPackTo(MatchingAcceptanceUserT _o) {
    _o.UID = this.UID;
    _o.ProfileIconID = this.ProfileIconID;
  }
  public static Offset<Lobby.MatchingAcceptanceUser> Pack(FlatBufferBuilder builder, MatchingAcceptanceUserT _o) {
    if (_o == null) return default(Offset<Lobby.MatchingAcceptanceUser>);
    return CreateMatchingAcceptanceUser(
      builder,
      _o.UID,
      _o.ProfileIconID);
  }
}

public class MatchingAcceptanceUserT
{
  public int UID { get; set; }
  public int ProfileIconID { get; set; }

  public MatchingAcceptanceUserT() {
    this.UID = 0;
    this.ProfileIconID = 0;
  }
}


}
