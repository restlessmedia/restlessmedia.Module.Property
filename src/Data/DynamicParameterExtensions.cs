using Dapper;
using restlessmedia.Module.Data.Sql;
using System.Data;

namespace restlessmedia.Module.Property.Data
{
  public static class DynamicParameterExtensions
  {
    public static void Add(this DynamicParameters parameters, PropertyContactQuery query)
    {
      parameters.Add("page", query.Page);
      parameters.Add("maxPerPage", query.MaxPerPage);

      AddCountParameters(parameters, query);
    }

    public static void Add(this DynamicParameters parameters, IProperty property)
    {
      AddId(parameters, "propertyId", property.PropertyId);
      parameters.Add("cost", property.Cost);
      parameters.Add("propertyType", property.PropertyType == PropertyType.All ? null : (int?)property.PropertyType);
      parameters.Add("ownershipType", (int)property.Ownership);
      parameters.Add("isCommercial", property.IsCommercial);
      parameters.Add("status", (int)property.Status);
      parameters.Add("hasGarden", property.HasGarden);
      parameters.Add("hasParking", property.HasParking);
      parameters.Add("position", (int)property.Position);
      parameters.Add("isStudio", property.IsStudio);
      parameters.Add("isFurnished", property.IsFurnished);
      parameters.Add("bedroomCount", property.BedroomCount);
      parameters.Add("bathroomCount", property.BathroomCount);
      parameters.Add("receptionCount", property.ReceptionCount);
      parameters.Add("automated", property.Automated);
      parameters.Add("branchGuid", property.Branch.BranchGuid);
      parameters.Add("title", property.Title);
      parameters.Add("shortDescription", property.EncodedShortDescription());
      parameters.Add("longDescription", property.LongDescription);
      parameters.Add("featured", property.Featured);
      parameters.Add("squareFootage", property.SquareFootage);
      parameters.Add("developmentId", property.Development != null ? property.Development.DevelopmentId : null);
      parameters.Add("addressId", property.Address.AddressId);
    }

    public static void Add(this DynamicParameters parameters, PropertyContactEntity propertyContact)
    {
      parameters.Add("notes", propertyContact.Notes);
      parameters.Add("listingType", (short)propertyContact.ListingType);
      parameters.Add("ownerUserKey", propertyContact.OwnerUserKey);
    }

    public static void AddCountParameters(this DynamicParameters parameters, PropertyContactQuery query)
    {
      parameters.Add("listingType", (int)query.Listing);
      parameters.Add("ownerUserKey", query.OwnerUserKey);
    }

    public static void AddCountParameters(this DynamicParameters parameters, PropertyQuery query)
    {
      parameters.Add("listingType", (int)query.ListingType);
      parameters.Add("propertyType", (int)query.PropertyType);
      parameters.Add("excludeUnavailable", query.ExcludeUnavailable);
      parameters.Add("isCommercial", query.IsCommercial);
      parameters.Add("postCode", query.Like(x => x.PostCode));
      parameters.Add("minCost", query.MinCost);
      parameters.Add("maxCost", query.MaxCost);
      parameters.Add("minBedrooms", query.MinBedrooms);
      parameters.Add("maxBedrooms", query.MaxBedrooms);
      parameters.Add("orderFlags", (byte)query.Order);
      parameters.Add("keywords", query.Like(x => x.Keywords));
    }

    public static void AddId(this DynamicParameters parameters, string name, int? value)
    {
      parameters.Add(name, value, DbType.Int32, ParameterDirection.InputOutput, 4);
    }
  }
}