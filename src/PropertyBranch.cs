namespace restlessmedia.Module.Property
{
  public class PropertyBranch : Branch, IPropertyBranch
  {
    public PropertyBranch()
      : base() { }

    public PropertyBranch(Office office)
      : base(office) { }

    public ListingType ListingType
    {
      get
      {
        return (ListingType)Type;
      }
      set
      {
        Type = (byte)value;
      }
    }
  }
}