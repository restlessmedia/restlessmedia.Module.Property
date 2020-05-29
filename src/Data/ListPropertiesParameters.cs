using restlessmedia.Module.Data.Sql;
using restlessmedia.Module.Meta.Data;
using System;
using System.Data.SqlClient;
using System.Linq;

namespace restlessmedia.Module.Property.Data
{
  public class ListPropertiesParameters : ParametersBase
  {
    public ListPropertiesParameters(PropertyQuery query, bool includePaging = true)
    {
      _query = query ?? throw new ArgumentNullException(nameof(query));
      _includePaging = includePaging;
    }

    protected override void AddParameters(SqlCommand command)
    {
      if (_includePaging)
      {
        Add("@page", _query.Page);
        Add("@maxPerPage", _query.MaxPerPage);
      }

      AddCountParameters();
    }

    private void AddCountParameters()
    {
      Add("@listingType", (int)_query.ListingType);
      Add("@propertyType", (int)_query.PropertyType);
      Add("@excludeUnavailable", _query.ExcludeUnavailable);
      Add("@isCommercial", _query.IsCommercial);
      Add("@postCode", _query.Like(x => x.PostCode));
      Add("@minCost", _query.MinCost);
      Add("@maxCost", _query.MaxCost);
      Add("@minBedrooms", _query.MinBedrooms);
      Add("@maxBedrooms", _query.MaxBedrooms);
      Add("@orderFlags", (byte)_query.Order);
      Add("@keywords", _query.Like(x => x.Keywords));
      Add("@includeArchived", _query.Status.HasFlag(PropertyStatus.Archived));

      if (_query.Meta.Count > 0)
      {
        Add("@query", UDTMeta.TypeName, _query.Meta.Select(x => new UDTMeta(x.Key, x.Value)));
      }
    }

    private readonly PropertyQuery _query;

    private readonly bool _includePaging;
  }
}