using System;

namespace restlessmedia.Module.Property
{
  [Flags]
  public enum OrderFlags
  {
    AddedDateAsc = 1,
    AddedDateDesc = 2,
    CostAsc = 4,
    CostDesc = 8,
    BedroomsAsc = 16,
    BedroomsDesc = 32,
    FeaturedAsc = 64,
    PostCodeAsc = 128,
    PostCodeDesc = 256,
  }
}