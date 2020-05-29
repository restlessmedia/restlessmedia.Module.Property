using restlessmedia.Module.Address;
using restlessmedia.Module.Attributes;
using restlessmedia.Module.File;
using System;
using System.Collections.Generic;

namespace restlessmedia.Module.Property
{
  /// <summary>
  /// Base class for properties, usually used for listings - should extend full entity.
  /// </summary>
  public class PropertyEntity : Entity, IProperty
  {
    public PropertyEntity()
    {
      AddedDate = DateTime.Today;
      PropertyType = Property.PropertyType.None;
      Automated = false;
    }

    public override EntityType EntityType
    {
      get
      {
        return EntityType.Property;
      }
    }

    public override int? EntityId
    {
      get
      {
        return PropertyId;
      }
    }

    [BindAs(typeof(AddressEntity))]
    public virtual IAddress Address
    {
      get
      {
        return _address = _address ?? new AddressEntity();
      }
      set
      {
        _address = value;
      }
    }

    public virtual string LongDescription { get; set; }

    /// <summary>
    /// If true, the property is automatically updated and managed via an external api (i.e apiProperty) and should not be edited locally.
    /// </summary>
    public bool Automated { get; set; }

    /// <summary>
    /// Property entity id when used with apiProperty
    /// </summary>
    public virtual int? PropertyId { get; set; }

    public virtual PropertyType? PropertyType { get; set; }

    public virtual PropertyStatus Status { get; set; }

    public virtual byte BedroomCount { get; set; }

    public virtual byte BathroomCount { get; set; }

    public virtual byte ReceptionCount { get; set; }

    public virtual double? SquareFootage { get; set; }

    public virtual bool IsCommercial { get; set; }

    public virtual bool IsStudio { get; set; }

    public virtual bool IsFurnished { get; set; }

    public virtual bool HasGarden { get; set; }

    public virtual bool HasParking { get; set; }

    public virtual OwnershipType Ownership { get; set; }

    public virtual PropertyPosition Position { get; set; }

    public virtual DateTime AddedDate { get; set; }

    public virtual string ShortDescription { get; set; }

    public virtual decimal Cost { get; set; }

    public virtual bool Featured { get; set; }

    public virtual int? DevelopmentId { get; set; }

    private int _BranchGuid;

    public virtual int BranchGuid
    {
      get
      {
        return _BranchGuid;
      }
      set
      {
        _BranchGuid = value;
      }
    }

    [BindAs(typeof(PropertyBranch))]
    public virtual IPropertyBranch Branch
    {
      get
      {
        return _branch = _branch ?? new PropertyBranch();
      }
      set
      {
        _branch = value;
      }
    }

    [BindAs(typeof(DevelopmentEntity))]
    public IDevelopment Development { get; set; }

    [BindAs(typeof(List<FileEntity>))]
    public IEnumerable<IFile> Files { get; set; }

    private IPropertyBranch _branch;

    private IAddress _address;
  }
}