using restlessmedia.Module.Address;
using restlessmedia.Module.Data;
using System;
using System.Collections.Generic;

namespace restlessmedia.Module.Property.Data
{
    public interface IPropertyDataProvider : IDataProvider
  {
    IDevelopment ReadDevelopment(int developmentId);

    ModelCollection<IDevelopment> ListDevelopments(int page = 1, int maxPerPage = int.MaxValue);

    ModelCollection<T> ListProperties<T>(int developmentId, int page = 1, int maxPerPage = int.MaxValue)
      where T : PropertyEntity;

    ModelCollection<T> ListProperties<T>(PropertyQuery query, bool getCount = false)
      where T : PropertyEntity;

    ModelCollection<PropertyEntity> ListRelated(int propertyId);

    ModelCollection<PropertyContactEntity> ListPropertyContacts(PropertyContactQuery query, bool getCount = false);

    void Save(IProperty property);

    void Save(PropertyContactEntity propertyContact);

    IEnumerable<string> Delete(int propertyId);

    void DeletePropertyContact(int contactId);

    TPropertyContact ReadPropertyContact<TPropertyContact, TAddress, TMarker>(int contactId)
      where TPropertyContact : PropertyContactEntity
      where TAddress : AddressEntity, new()
      where TMarker : Marker;

    TProperty Read<TProperty, TAddress, TMarker>(int propertyId)
      where TProperty : IProperty
      where TAddress : AddressEntity, new()
      where TMarker : Marker;

    TProperty ReadFeatured<TProperty, TAddress, TMarker>()
      where TProperty : PropertyEntity
      where TAddress : AddressEntity, new()
      where TMarker : Marker;

    void SetFeatured(int propertyId, bool featured);

    void SetStatus(int propertyId, PropertyStatus status);

    ModelCollection<T> ListFeatured<T>()
      where T : PropertyEntity;

    void SaveDevelopment<TDevelopment>(TDevelopment development)
      where TDevelopment : DevelopmentEntity;

    void DeleteDevelopment(int developmentId);

    void MoveDevelopment(int developmentId, MoveDirection direction);

    void RemoveArchived(DateTime before);

    IEnumerable<IPropertyBranch> ListBranches();

    void AddAlert(int propertyId, string email);

    PropertyEntity GetRandomFeatured();

    /// <summary>
    /// Returns a list of the available property type ids
    /// </summary>
    /// <returns></returns>
    IEnumerable<int> GetAvailablePropertyTypeIds();

    IEnumerable<Marker> ListRelatedPoints(int propertyId);
  }
}