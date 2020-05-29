using System;

namespace restlessmedia.Module.Property
{
  [Flags]
  public enum PropertyStatus : byte
  {
    Available = 0,
    Unavailable = 1,
    UnderOffer = 2,
    Archived = 4,
    /// <summary>
    /// Available, Unavailable, UnderOffer
    /// </summary>
    NonArchived = 3,
    /// <summary>
    /// Available, Unavailable, UnderOffer, Archived
    /// </summary>
    All = 7,
  }
}