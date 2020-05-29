using restlessmedia.Module.Meta;

namespace restlessmedia.Module.Property
{
  public class PropertyQuery : Query
  {
    public PropertyQuery()
    {
      Order = OrderFlags.CostDesc;
      IsCommercial = false;
      PropertyType = PropertyType.All;
      Status = PropertyStatus.NonArchived;
    }

    public string PostCode { get; set; }

    public decimal? MinCost { get; set; }

    public decimal? MaxCost { get; set; }

    public byte? MinBedrooms { get; set; }

    public byte? MaxBedrooms { get; set; }

    public PropertyType PropertyType { get; set; }

    public ListingType ListingType { get; set; }

    public OrderFlags Order { get; set; }

    public PropertyStatus Status { get; set; }

    [Ignore]
    public MetaQuery Meta
    {
      get
      {
        return _meta = _meta ?? new MetaQuery();
      }
      set
      {
        _meta = value;
      }
    }

    public string Keywords { get; set; }

    public bool? IsCommercial { get; set; }

    public bool ExcludeUnavailable { get; set; }

    private MetaQuery _meta;
  }
}