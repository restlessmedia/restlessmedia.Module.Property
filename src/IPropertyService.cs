using restlessmedia.Module.Address;
using System.Collections.Generic;

namespace restlessmedia.Module.Property
{
  public interface IPropertyService : IService
  {
    ModelCollection<T> ListProperties<T>(int developmentId, int page = 1, int maxPerPage = int.MaxValue)
      where T : PropertyEntity;

    ModelCollection<T> ListProperties<T>(PropertyQuery query, bool getCount = false)
      where T : PropertyEntity;

    ModelCollection<PropertyEntity> ListRelated(int propertyId, int max = 10);

    /// <summary>
    /// List property contacts
    /// </summary>
    /// <param name="page"></param>
    /// <param name="maxPerPage"></param>
    /// <param name="metaQuery">A meta category query</param>
    /// <returns></returns>
    ModelCollection<PropertyContactEntity> ListPropertyContacts(PropertyContactQuery query, bool getCount);

    ModelCollection<IDevelopment> ListDevelopments(int page = 1, int maxPerPage = int.MaxValue);

    IDevelopment ReadDevelopment(int developmentId);

    void MoveDevelopment(int developmentId, MoveDirection direction);

    PropertyEntity Read(int propertyId);

    /// <summary>
    /// Reads a property
    /// </summary>
    /// <typeparam name="TProperty"></typeparam>
    /// <typeparam name="TAddress"></typeparam>
    /// <typeparam name="TMarker"></typeparam>
    /// <param name="propertyId"></param>
    /// <param name="employCache"></param>
    /// <returns></returns>
    TProperty Read<TProperty, TAddress, TMarker>(int propertyId)
      where TProperty : PropertyEntity
      where TAddress : AddressEntity, new()
      where TMarker : Marker;

    /// <summary>
    /// Returns all featured properties
    /// </summary>
    /// <returns></returns>
    ModelCollection<T> ListFeatured<T>()
      where T : PropertyEntity;

    PropertyEntity GetRandomFeatured();

    /// <summary>
    /// Reads a property contact
    /// </summary>
    /// <typeparam name="TPropertyContact"></typeparam>
    /// <typeparam name="TAddress"></typeparam>
    /// <typeparam name="TMarker"></typeparam>
    /// <param name="contactId"></param>
    /// <returns></returns>
    TPropertyContact ReadPropertyContact<TPropertyContact, TAddress, TMarker>(int contactId)
      where TPropertyContact : PropertyContactEntity
      where TAddress : AddressEntity, new()
      where TMarker : Marker;

    /// <summary>
    /// Creates or updates a property contact based on primary key
    /// </summary>
    /// <param name="propertyContact"></param>
    void Save(PropertyContactEntity propertyContact);

    /// <summary>
    /// Creates or updates a property
    /// </summary>
    /// <param name="property"></param>
    void Save(PropertyEntity property);

    void SaveDevelopment<TDevelopment>(TDevelopment development)
      where TDevelopment : DevelopmentEntity;

    void DeleteDevelopment(int developmentId);

    /// <summary>
    /// Deletes a property
    /// </summary>
    /// <param name="propertyId"></param>
    /// <param name="path"></param>
    void Delete(int propertyId, string path);

    /// <summary>
    /// Deletes a property contact
    /// </summary>
    /// <param name="contactId"></param>
    void DeletePropertyContact(int contactId);

    /// <summary>
    /// Sets a property to featured
    /// </summary>
    /// <param name="propertyId"></param>
    /// <param name="featured"></param>
    void SetFeatured(int propertyId, bool featured);

    /// <summary>
    /// Sets property status
    /// </summary>
    /// <param name="propertyId"></param>
    /// <param name="status"></param>
    void SetStatus(int propertyId, PropertyStatus status);

    IEnumerable<IPropertyBranch> ListBranches();

    /// <summary>
    /// Sets up an alert for a user based on a particular property
    /// </summary>
    /// <param name="propertyId"></param>
    /// <param name="email"></param>
    void AddAlert(int propertyId, string email);

    /// <summary>
    /// Returns a list of available types
    /// </summary>
    /// <returns></returns>
    IDictionary<int, string> GetAvailablePropertyTypes();

    /// <summary>
    /// Returns the related points for a property
    /// </summary>
    /// <param name="propertyId"></param>
    /// <returns></returns>
    IEnumerable<Marker> ListRelatedPoints(int propertyId);
  }
}