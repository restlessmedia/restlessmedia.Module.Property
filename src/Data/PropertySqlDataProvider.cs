using Dapper;
using FastMapper;
using restlessmedia.Module.Address;
using restlessmedia.Module.Address.Data;
using restlessmedia.Module.Contact;
using restlessmedia.Module.Data;
using restlessmedia.Module.Data.Sql;
using restlessmedia.Module.File;
using restlessmedia.Module.File.Data;
using restlessmedia.Module.Meta;
using SqlBuilder;
using SqlBuilder.DataServices;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace restlessmedia.Module.Property.Data
{
  public class PropertySqlDataProvider : SqlDataProviderBase
  {
    public PropertySqlDataProvider(IDataContext context, IAddressDataProvider addressDataProvider, IFileDataProvider fileDataProvider, IModelDataProvider<DataModel.VProperty> modelDataProvider)
      : base(context)
    {
      _addressDataProvider = addressDataProvider ?? throw new ArgumentNullException(nameof(addressDataProvider));
      _fileDataProvider = fileDataProvider ?? throw new ArgumentNullException(nameof(fileDataProvider));
      _modelDataProvider = modelDataProvider ?? throw new ArgumentNullException(nameof(modelDataProvider));
    }

    public IDevelopment ReadDevelopment(int developmentId)
    {
      using (DatabaseContext context = CreateDatabaseContext())
      {
        PropertyRepository propertyRepository = context.Repository<PropertyRepository>();
        return propertyRepository.GetDevelopment(developmentId);
      }
    }

    public ModelCollection<IDevelopment> ListDevelopments(int page = 1, int maxPerPage = int.MaxValue)
    {
      using (DatabaseContext context = CreateDatabaseContext())
      {
        PropertyRepository propertyRepository = new PropertyRepository(context);
        return propertyRepository.ListDevelopments(page, maxPerPage);
      }
    }

    public void SaveDevelopment<TDevelopment>(TDevelopment development)
      where TDevelopment : DevelopmentEntity
    {
      using (DatabaseContext context = CreateDatabaseContext())
      {
        PropertyRepository propertyRepository = context.Repository<PropertyRepository>();
        VDevelopment vDevelopment = propertyRepository.Save(development);
        context.SaveChanges();
        development.DevelopmentId = vDevelopment.DevelopmentId;
      }
    }

    public void DeleteDevelopment(int developmentId)
    {
      const string commandName = "dbo.SPDeleteDevelopment";
      Execute(commandName, new { developmentId });
    }

    public TProperty Read<TProperty, TAddress, TMarker>(int propertyId)
      where TProperty : IProperty
      where TAddress : AddressEntity, new()
      where TMarker : Marker
    {
      const string commandName = "dbo.SPReadProperty";

      using (IGridReader reader = QueryMultiple(commandName, new { propertyId }))
      {
        return Read<TProperty, TAddress, TMarker>(reader);
      }
    }

    public ModelCollection<T> ListProperties<T>(int developmentId, int page = 1, int maxPerPage = int.MaxValue)
      where T : PropertyEntity
    {
      const string commandText = "dbo.SPListDevelopmentProperty";
      return Query((connection) =>
      {
        return new ModelCollection<T>(connection.Query<T, AddressEntity, Marker, FileEntity, PropertyBranch, T>(commandText, Map, new { developmentId, page, maxPerPage }, commandType: CommandType.StoredProcedure, splitOn: "AddressId,Latitude,SystemFileName,BranchGuid"), GetPropertyCount(developmentId), page);
      });
    }

    public IWhere CreateWhereFromQuery(PropertyQuery propertyQuery)
    {
      return CreateSelect(propertyQuery).Where();
    }

    public Select CreateSelect(PropertyQuery query, bool getCount = false)
    {
      Select<DataModel.VProperty> select = _modelDataProvider.NewSelect();

      IClauseCollection<DataModel.VProperty> where = select
        .Where(x => x.Type, (byte)query.ListingType)
        .And(x => x.PropertyType, query.PropertyType, x => x != PropertyType.All)
        .And(x => x.BedroomCount, SqlOperator.GreaterThanOrEqual, query.MinBedrooms, x => x.HasValue)
        .And(x => x.BedroomCount, SqlOperator.LessThanOrEqual, query.MaxBedrooms, x => x.HasValue)
        .And(x => x.Cost, SqlOperator.GreaterThanOrEqual, query.MinCost, x => x.HasValue)
        .And(x => x.Cost, SqlOperator.LessThanOrEqual, query.MaxCost, x => x.HasValue)
        .And(x => x.PostCode, SqlOperator.Like, query.PostCode, x => !string.IsNullOrEmpty(x))
        .And(x => x.IsCommercial, query.IsCommercial, x => x.GetValueOrDefault(false));


      if (!query.Status.HasFlag(PropertyStatus.Archived))
      {
        where.And(x => x.Status, SqlOperator.NotEqual, PropertyStatus.Archived);
      }

      where.And(x => x.Status, PropertyStatus.Available, x => query.ExcludeUnavailable);

      if (!string.IsNullOrEmpty(query.Keywords))
      {
        where.And()
          .Or(x => x.Address01, SqlOperator.Like, query.Keywords)
          .Or(x => x.Address02, SqlOperator.Like, query.Keywords)
          .Or(x => x.PostCode, SqlOperator.Like, query.Keywords)
          .Or(x => x.Title, SqlOperator.Like, query.Keywords);
      }

      select.IncludeCount(getCount);
      select.Paging(query.Page, query.MaxPerPage);

      return select;
    }

    public ModelCollection<T> ListPropertiesInline<T>(PropertyQuery query, bool getCount = false)
      where T : PropertyEntity
    {
      Select select = CreateSelect(query, getCount);
      DataPage<dynamic> dataPage = _modelDataProvider.QueryPage<dynamic>(select);
      return new ModelCollection<T>(ObjectMapper.MapAll<dynamic, T>(dataPage.Data, config =>
      {
        config.For(x => x.Address).ResolveWith<AddressEntity>();
        config.ForEach(x => x.Files).ResolveWith<FileEntity>();
        config.For(x => x.Branch).ResolveWith<PropertyBranch>();
      }), dataPage.Count);
    }

    public ModelCollection<T> ListProperties<T>(PropertyQuery query, bool getCount = false)
      where T : PropertyEntity
    {
      //if(query.Meta.Count == 0)
      //{
      //  return ListPropertiesInline<T>(query, getCount);
      //}

      //string command = "dbo.SPListPropertyQuery";

      string command = query.Meta.Count == 0 ? "dbo.SPListProperty" : "dbo.SPListPropertyQuery";

      return Query((connection) =>
      {
        IEnumerable<T> data = connection.Query<T, AddressEntity, Marker, FileEntity, PropertyBranch, T>(command, Map, new ListPropertiesParameters(query), commandType: CommandType.StoredProcedure, splitOn: "AddressId,Latitude,SystemFileName,BranchGuid");

        if (getCount)
        {
          return new ModelCollection<T>(data, GetPropertyCount(query));
        }

        return new ModelCollection<T>(data);
      });
    }

    public ModelCollection<PropertyEntity> ListRelated(int propertyId)
    {
      return Query((connection) =>
      {
        return new ModelCollection<PropertyEntity>(connection.Query<PropertyEntity, AddressEntity, Marker, FileEntity, PropertyBranch, PropertyEntity>("dbo.SPListRelatedProperty", Map, new { propertyId }, commandType: CommandType.StoredProcedure, splitOn: "AddressId,Latitude,SystemFileName,BranchGuid"));
      });
    }

    public ModelCollection<PropertyContactEntity> ListPropertyContacts(PropertyContactQuery query, bool getCount = false)
    {
      DynamicParameters parameters = new DynamicParameters();
      DynamicParameters countParameters = new DynamicParameters();

      parameters.Add(query);
      countParameters.AddCountParameters(query);

      // use xml query - if we don't have any xml then just use the normal search - if we do this check here, the proc plan should be better
      string xmlQuery = query.GetQuery().GetXmlString();
      bool useQuery = !string.IsNullOrEmpty(xmlQuery);
      string command = string.Concat("dbo.SPListPropertyContacts", useQuery ? "Query" : string.Empty);
      string countCommand = string.Concat(command, "_Count");

      if (useQuery)
      {
        parameters.Add("@query", xmlQuery);
      }

      return Query((connection) =>
      {
        IEnumerable<PropertyContactEntity> data = connection.Query<PropertyContactEntity, AddressEntity, Marker, PropertyContactEntity>(command, (p, a, m) =>
       {
         p.Address = a ?? new AddressEntity();
         p.Address.Marker = m;
         return p;
       }, parameters, commandType: CommandType.StoredProcedure, splitOn: "AddressId,Latitude");

        if (getCount)
        {
          return new ModelCollection<PropertyContactEntity>(data, connection.Query<int>(countCommand, countParameters, commandType: CommandType.StoredProcedure).FirstOrDefault());
        }

        return new ModelCollection<PropertyContactEntity>(data);
      });
    }

    public void Save(IProperty property)
    {
      using (DatabaseContext context = CreateDatabaseContext())
      {
        PropertyRepository propertyRepository = new PropertyRepository(context);
        _addressDataProvider.Save(property.Address);
        VProperty vProperty = propertyRepository.Save(property);
        context.SaveChanges();
        property.PropertyId = vProperty.PropertyId;
      }
    }

    public void Save(PropertyContactEntity propertyContact)
    {
      DynamicParameters parameters = new DynamicParameters();
      const string commandName = "dbo.SPSavePropertyContact";
      parameters.AddId("@contactId", propertyContact.ContactId);
      parameters.Add("@applicationName", DataContext.LicenseSettings.ApplicationName);
      parameters.Add((ContactEntity)propertyContact);
      parameters.Add(propertyContact);
      parameters.Add(propertyContact.Address);

      Execute(commandName, parameters);

      propertyContact.ContactId = parameters.Get<int>("@contactId");
    }

    public IEnumerable<string> Delete(int propertyId)
    {
      const string commandName = "dbo.SPDeleteProperty";
      return QueryWithTransaction<string>(commandName, new { propertyId });
    }

    public void DeletePropertyContact(int contactId)
    {
      const string commandName = "dbo.SPDeleteContact";
      Execute(commandName, new { contactId });
    }

    public TPropertyContact ReadPropertyContact<TPropertyContact, TAddress, TMarker>(int contactId)
      where TPropertyContact : PropertyContactEntity
      where TAddress : AddressEntity, new()
      where TMarker : Marker
    {
      const string commandName = "dbo.SPReadPropertyContact";

      using (IGridReader reader = QueryMultiple(commandName, new { contactId }))
      {
        TPropertyContact propertyContact = reader.Read<TPropertyContact, TAddress, TMarker, TPropertyContact>((p, a, m) =>
        {
          p.Address = a ?? new TAddress();
          p.Address.Marker = m;
          return p;
        }, splitOn: "AddressId,Latitude").SingleOrDefault();

        propertyContact.MetaData = new MetaCollection(reader.Read<MetaEntity>());

        return propertyContact;
      }
    }

    public TProperty ReadFeatured<TProperty, TAddress, TMarker>()
      where TProperty : PropertyEntity
      where TAddress : AddressEntity, new()
      where TMarker : Marker
    {
      const string commandName = "dbo.SPReadFeaturedProperty";

      using (IGridReader reader = QueryMultiple(commandName))
      {
        return Read<TProperty, TAddress, TMarker>(reader);
      }
    }

    public void SetFeatured(int propertyId, bool featured)
    {
      using (DatabaseContext context = CreateDatabaseContext())
      {
        PropertyRepository propertyRepository = new PropertyRepository(context);
        propertyRepository.SetFeatured(propertyId, featured);
        context.SaveChanges();
      }
    }

    public void SetStatus(int propertyId, PropertyStatus status)
    {
      using (DatabaseContext context = CreateDatabaseContext())
      {
        PropertyRepository propertyRepository = new PropertyRepository(context);
        propertyRepository.SetStatus(propertyId, status);
        context.SaveChanges();
      }
    }

    public ModelCollection<T> ListFeatured<T>()
       where T : PropertyEntity
    {
      return Query((connection) =>
      {
        // total query should always return a value so no need for firstOrDefaut
        return new ModelCollection<T>(connection.Query<T, AddressEntity, Marker, FileEntity, PropertyBranch, T>("dbo.SPListFeaturedProperties", Map, commandType: CommandType.StoredProcedure, splitOn: "AddressId,Latitude,SystemFileName,BranchGuid"));
      });
    }

    public void MoveDevelopment(int developmentId, MoveDirection direction)
    {
      const string commandName = "dbo.SPMoveDevelopment";
      Execute(commandName, new { developmentId, directionFlag = (byte)direction });
    }

    public void RemoveArchived(DateTime before)
    {
      const string commandName = "dbo.SPRemovedArchivedProperty";
      Execute(commandName, new { before });
    }

    public IEnumerable<IPropertyBranch> ListBranches()
    {
      using (DatabaseContext context = CreateDatabaseContext())
      {
        return context
          .Branch
          .AsNoTracking()
          .OrderBy(x => x.Name)
          .ToList();
      }
    }

    public void AddAlert(int propertyId, string email)
    {
      const string sql = "INSERT INTO dbo.TPropertyAlert(PropertyId, Email) VALUES(@propertyId, @email)";
      Execute(sql, new { propertyId, email }, commandType: CommandType.Text);
    }

    public PropertyEntity GetRandomFeatured()
    {
      Select<DataModel.VProperty> select = _modelDataProvider.NewSelect();
      select
        .Paging(1, 1)
        .Randomise();

      select
        .Where(x => x.Featured, true);

      dynamic result = _modelDataProvider.QueryDynamic(select, connection => select.WithLicenseId(connection, DataContext.LicenseSettings)).FirstOrDefault();

      return ObjectMapper.Map<PropertyEntity>(result);
    }

    public IEnumerable<int> GetAvailablePropertyTypeIds()
    {
      Select<DataModel.VProperty> select = _modelDataProvider.NewSelect();
      select
        .Column(x => x.PropertyType)
        .GroupBy(x => x.PropertyType, null)
        .Where(x => x.PropertyType, SqlOperator.IsNot, null)
          .And(x => x.Status, SqlOperator.NotEqual, PropertyStatus.Archived);
      return _modelDataProvider.Query<int>(select);
    }

    public IEnumerable<Marker> ListRelatedPoints(int propertyId)
    {
      Select<DataModel.Api.VProperty> select = new Select<DataModel.Api.VProperty>(alias: "RP");
      select
        .Column(x => x.Title)
        .Column(x => x.Latitude)
        .Column(x => x.Longitude)
        .LeftJoin("dbo.TProperty P", "P.PropertyId = @propertyId") // note @p0 comes from the parameter added below
        .LeftJoin("dbo.TAddress PA", "PA.AddressId = P.AddressId")
        .OrderBy("RP.Cost")
        .Where("RP.PropertyId != P.PropertyId")
        .And("RP.PostCodeFirstPart = PA.PostCodeFirstPart")
        .And("RP.BedroomCount = P.BedroomCount")
        .And("RP.Cost between ROUND(P.Cost - ((P.Cost/100) * 5), -5) AND ROUND(P.Cost + ((P.Cost/100) * 5), +5)")
        .And("RP.[Status] != 4")
        .Parameters.Add("propertyId", propertyId);

      select.WithLicenseId(DataContext);

      return _modelDataProvider.Query<Marker>(select);
    }

    private TProperty Read<TProperty, TAddress, TMarker>(IGridReader reader)
      where TProperty : IProperty
      where TAddress : AddressEntity, new()
      where TMarker : Marker
    {
      const string splitOn = "AddressId,Latitude,BranchGuid,DevelopmentId";

      TProperty property = reader.Read<TProperty, TAddress, TMarker, PropertyBranch, DevelopmentEntity, TProperty>((p, a, m, b, d) =>
      {
        p.Address = a ?? new TAddress();
        p.Address.Marker = m;
        p.Branch = b;
        p.Development = d;
        return p;
      }, splitOn: splitOn).SingleOrDefault();

      if (property != null)
      {
        property.Files = reader.Read<FileEntity>().ToList();

        // nasty hack here.  we have confusion over whether to use the iproperty.branchguid property or the nested one.  this makes sure they are both in sync
        // need to fix :!

        if (property.Branch != null && property.Branch.BranchGuid != property.BranchGuid)
        {
          property.BranchGuid = property.Branch.BranchGuid;
        }

        if (property.Development != null && property.Development.DevelopmentId != property.DevelopmentId)
        {
          property.DevelopmentId = property.Development.DevelopmentId;
        }
      }

      return property;
    }

    /// <summary>
    /// Generic mapping for property lists
    /// </summary>
    /// <param name="property"></param>
    /// <param name="address"></param>
    /// <param name="marker"></param>
    /// <param name="file"></param>
    /// <returns></returns>
    private static T Map<T>(T property, AddressEntity address, Marker marker, FileEntity file, PropertyBranch branch)
      where T : PropertyEntity
    {
      property.Address = address;
      property.Address.Marker = marker;
      property.Files = new[] { file };
      property.Branch = branch;
      return property;
    }

    private static PropertyEntity Map(PropertyEntity property, AddressEntity address, Marker marker, FileEntity file, PropertyBranch branch)
    {
      return Map<PropertyEntity>(property, address, marker, file, branch);
    }

    private int GetPropertyCount(PropertyQuery query)
    {
      string command = query.Meta.Count == 0 ? "dbo.SPListProperty_Count" : "dbo.SPListPropertyQuery_Count";
      return Query<int>(command, new ListPropertiesParameters(query, false)).Single();
    }

    private int GetPropertyCount(int developmentId)
    {
      const string commandText = "dbo.SPListDevelopmentProperty_Count";
      return Query<int>(commandText, new { developmentId }).Single();
    }

    private DatabaseContext CreateDatabaseContext(bool autoDetectChanges = false)
    {
      return new DatabaseContext(DataContext, autoDetectChanges);
    }

    private readonly IAddressDataProvider _addressDataProvider;

    private readonly IFileDataProvider _fileDataProvider;

    private readonly IModelDataProvider<DataModel.VProperty> _modelDataProvider;
  }
}