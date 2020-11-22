using restlessmedia.Module.Address;
using restlessmedia.Module.File;
using restlessmedia.Module.Meta;
using restlessmedia.Module.Property.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace restlessmedia.Module.Property
{
  internal sealed class PropertyService : IPropertyService
  {
    public PropertyService(IFileService fileService, IMetaService metaService, IPropertyDataProvider propertyDataProvider, ICacheProvider cacheProvider)
      : base()
    {
      _fileService = fileService ?? throw new ArgumentNullException(nameof(fileService));
      _metaService = metaService ?? throw new ArgumentNullException(nameof(metaService));
      _propertyDataProvider = propertyDataProvider ?? throw new ArgumentNullException(nameof(propertyDataProvider));
      _cacheProvider = cacheProvider ?? throw new ArgumentNullException(nameof(cacheProvider));
    }

    public IDevelopment ReadDevelopment(int developmentId)
    {
      return _propertyDataProvider.ReadDevelopment(developmentId);
    }

    public void MoveDevelopment(int developmentId, MoveDirection direction)
    {
      _propertyDataProvider.MoveDevelopment(developmentId, direction);
    }

    public ModelCollection<IDevelopment> ListDevelopments(int page = 1, int maxPerPage = int.MaxValue)
    {
      return _propertyDataProvider.ListDevelopments(page, maxPerPage);
    }

    public void SaveDevelopment<TDevelopment>(TDevelopment development)
      where TDevelopment : DevelopmentEntity
    {
      _propertyDataProvider.SaveDevelopment(development);
    }

    public void DeleteDevelopment(int developmentId)
    {
      _propertyDataProvider.DeleteDevelopment(developmentId);
    }

    public PropertyEntity Read(int propertyId)
    {
      return Read<PropertyEntity, AddressEntity, Marker>(propertyId);
    }

    public TProperty Read<TProperty, TAddress, TMarker>(int propertyId)
      where TProperty : PropertyEntity
      where TAddress : AddressEntity, new()
      where TMarker : Marker
    {
      return _propertyDataProvider.Read<TProperty, TAddress, TMarker>(propertyId);
    }

    public TPropertyContact ReadPropertyContact<TPropertyContact, TAddress, TMarker>(int contactId)
      where TPropertyContact : PropertyContactEntity
      where TAddress : AddressEntity, new()
      where TMarker : Marker
    {
      return _propertyDataProvider.ReadPropertyContact<TPropertyContact, TAddress, TMarker>(contactId);
    }

    public ModelCollection<T> ListFeatured<T>()
      where T : PropertyEntity
    {
      return _propertyDataProvider.ListFeatured<T>();
    }

    public PropertyEntity GetRandomFeatured()
    {
      return _propertyDataProvider.GetRandomFeatured();
    }

    public ModelCollection<T> ListProperties<T>(int developmentId, int page = 1, int maxPerPage = int.MaxValue)
      where T : PropertyEntity
    {
      return _propertyDataProvider.ListProperties<T>(developmentId, page, maxPerPage);
    }

    public ModelCollection<T> ListProperties<T>(PropertyQuery query, bool getCount = false)
      where T : PropertyEntity
    {
      ModelCollection<T> list = _propertyDataProvider.ListProperties<T>(query, getCount);
      list.Paging.Page = query.Page;
      list.Paging.MaxPerPage = query.MaxPerPage;
      return list;
    }

    public ModelCollection<PropertyEntity> ListRelated(int propertyId, int max = 10)
    {
      return _propertyDataProvider.ListRelated(propertyId, max);
    }

    public ModelCollection<PropertyContactEntity> ListPropertyContacts(PropertyContactQuery query, bool getCount)
    {
      return _propertyDataProvider.ListPropertyContacts(query, getCount);
    }

    public void SetFeatured(int propertyId, bool featured)
    {
      _propertyDataProvider.SetFeatured(propertyId, featured);
    }

    public void SetStatus(int propertyId, PropertyStatus status)
    {
      _propertyDataProvider.SetStatus(propertyId, status);
    }

    public void Delete(int propertyId, string path)
    {
      IEnumerable<string> orphanFiles = _propertyDataProvider.Delete(propertyId);
      _cacheProvider.RemoveProperty(propertyId);

      // now do the file clean up
      foreach (string file in orphanFiles)
      {
        _fileService.Delete(path, file);
      }
    }

    public void DeletePropertyContact(int contactId)
    {
      _propertyDataProvider.DeletePropertyContact(contactId);
    }

    public void Save(PropertyContactEntity propertyContact)
    {
      _propertyDataProvider.Save(propertyContact);
      _metaService.Save(propertyContact, propertyContact.MetaData, true);
    }

    public void Save(PropertyEntity property)
    {
      if (property.PropertyId.HasValue)
      {
        _cacheProvider.RemoveProperty(property.PropertyId.Value);
      }

      _propertyDataProvider.Save(property);
    }

    public void RemoveArchived(DateTime before)
    {
      _propertyDataProvider.RemoveArchived(before);
    }

    public IEnumerable<IPropertyBranch> ListBranches()
    {
      return _propertyDataProvider.ListBranches();
    }

    /// <inheritDoc />
    public void AddAlert(int propertyId, string email)
    {
      _propertyDataProvider.AddAlert(propertyId, email);
    }

    /// <inheritDoc />
    public IDictionary<int, string> GetAvailablePropertyTypes()
    {
      return _propertyDataProvider.GetAvailablePropertyTypeIds().ToDictionary(x => x, GetPropertyTypeDescription);
    }

    public IEnumerable<Marker> ListRelatedPoints(int propertyId)
    {
      return _propertyDataProvider.ListRelatedPoints(propertyId);
    }

    private static string GetPropertyTypeDescription(int value)
    {
      PropertyType propertyType = (PropertyType)value;

      switch (propertyType)
      {
        case PropertyType.BarnConversion:
          {
            return "Barn Conversion";
          }
        case PropertyType.FarmHouse:
          {
            return "Farm House";
          }
        case PropertyType.ManorHouse:
          {
            return "Manor House";
          }
        case PropertyType.TownHouse:
          {
            return "Town House";
          }
        case PropertyType.SharedHouse:
          {
            return "Shared House";
          }
        case PropertyType.ShelteredHousing:
          {
            return "Sheltered Housing";
          }
        case PropertyType.GroundFloorFlat:
          {
            return "Ground Floor Flat";
          }
        case PropertyType.GroundFloorMaisonette:
          {
            return "Ground Floor Maisonette";
          }
        case PropertyType.SharedFlat:
          {
            return "Shared Flat";
          }
        case PropertyType.BuildingPlotOrLand:
          {
            return "Building Plot";
          }
        case PropertyType.HouseBoat:
          {
            return "House Boat";
          }
        case PropertyType.MobileHome:
          {
            return "Mobile Home";
          }
        case PropertyType.UnconvertedBarn:
          {
            return "Unconverted Barn";
          }
        default:
          {
            return propertyType.ToString();
          }
      }
    }
    
    private readonly IFileService _fileService;

    private readonly IMetaService _metaService;

    private readonly IPropertyDataProvider _propertyDataProvider;

    private readonly ICacheProvider _cacheProvider;
  }
}