using restlessmedia.Module.Contact;
using restlessmedia.Module.Meta;
using System;

namespace restlessmedia.Module.Property
{
  public class PropertyContactEntity : ContactEntity
  {
    public virtual ListingType ListingType { get; set; }

    public string Notes { get; set; }

    public virtual Guid? OwnerUserKey { get; set; }

    public virtual string OwnerUsername { get; set; }

    [Ignore]
    public virtual MetaCollection MetaData
    {
      get
      {
        return _metaData = _metaData ?? new MetaCollection(0);
      }
      set
      {
        _metaData = value;
      }
    }

    private MetaCollection _metaData;
  }
}