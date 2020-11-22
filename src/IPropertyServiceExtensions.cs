using restlessmedia.Module.Address;

namespace restlessmedia.Module.Property
{
  public static class IPropertyServiceExtensions
  {
    public static ModelCollection<PropertyEntity> ListRelated(this IPropertyService propertyService, PropertyEntity property, int max = 10)
    {
      return property.PropertyId.HasValue ? propertyService.ListRelated(property.PropertyId.Value, max) : null;
    }

    public static ModelCollection<PropertyEntity> ListProperties(this IPropertyService propertyService, int page, int maxPerPage, ListingType listing, OrderFlags order, bool getCount = false)
    {
      return propertyService.ListProperties(new PropertyQuery
      {
        Page = page,
        MaxPerPage = maxPerPage,
        ListingType = listing,
        Order = order
      }, getCount);
    }

    public static ModelCollection<PropertyEntity> ListProperties(this IPropertyService propertyService, int developmentId, int page = 1, int maxPerPage = int.MaxValue)
    {
      return propertyService.ListProperties<PropertyEntity>(developmentId, page, maxPerPage);
    }

    public static ModelCollection<PropertyEntity> ListProperties(this IPropertyService propertyService, PropertyQuery query, bool getCount = false)
    {
      return propertyService.ListProperties<PropertyEntity>(query, getCount);
    }

    public static ModelCollection<PropertyEntity> ListRecentProperties(this IPropertyService propertyService, int page, int maxPerPage, ListingType listing, OrderFlags order = OrderFlags.AddedDateDesc, bool getCount = false)
    {
      return ListProperties(propertyService, page, maxPerPage, listing, order, getCount);
    }

    public static ModelCollection<PropertyContactEntity> ListPropertyContacts(this IPropertyService propertyService, int page, int maxPerPage, bool getCount)
    {
      return propertyService.ListPropertyContacts(new PropertyContactQuery
      {
        Page = page,
        MaxPerPage = maxPerPage
      }, getCount);
    }

    public static ModelCollection<PropertyEntity> ListFeatured(this IPropertyService propertyService)
    {
      return propertyService.ListFeatured<PropertyEntity>();
    }

    /// <summary>
    /// Reads a property contact
    /// </summary>
    /// <param name="contactId"></param>
    /// <returns></returns>
    public static PropertyContactEntity ReadPropertyContact(this IPropertyService propertyService, int contactId)
    {
      return propertyService.ReadPropertyContact<PropertyContactEntity, AddressEntity, Marker>(contactId);
    }
  }
}
