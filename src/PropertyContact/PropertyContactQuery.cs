using restlessmedia.Module.Meta;
using System;

namespace restlessmedia.Module.Property
{
  public class PropertyContactQuery : Query
  {
    public PropertyContactQuery()
    {
      Listing = ListingType.None;
    } 

    /// <summary>
    /// A list of meta category ids used to filter on
    /// </summary>
    public int[] Categories { get; set; }

    public ListingType Listing { get; set; }

    public Guid? OwnerUserKey { get; set; }

    public MetaQuery GetQuery()
    {
      MetaQuery query = new MetaQuery();

      if (Categories != null && Categories.Length > 0)
      {
        for (int i = 0; i < Categories.Length; i++)
        {
          query.Add(Categories[i], MetaValues.True);
        }
      }

      return query;
    }
  }
}