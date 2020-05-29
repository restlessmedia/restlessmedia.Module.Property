using restlessmedia.Module.Address;
using restlessmedia.Module.File;
using System;
using System.Collections.Generic;

namespace restlessmedia.Module.Property
{
  /// <summary>
  /// Property information.  Doesn't include the ids because of the api property and standard property.
  /// </summary>
  public interface IProperty
  {
    int? PropertyId { get; set; }

    string Title { get; set; }

    string ShortDescription { get; set; }

    string LongDescription { get; set; }

    decimal Cost { get; set; }

    PropertyType? PropertyType { get; set; }

    OwnershipType Ownership { get; set; }

    bool IsCommercial { get; set; }

    PropertyStatus Status { get; set; }

    bool HasGarden { get; set; }

    bool HasParking { get; set; }

    PropertyPosition Position { get; set; }

    bool IsStudio { get; set; }

    bool IsFurnished { get; set; }

    DateTime AddedDate { get; set; }

    byte BedroomCount { get; set; }

    byte BathroomCount { get; set; }

    byte ReceptionCount { get; set; }

    bool Automated { get; set; }

    bool Featured { get; set; }

    double? SquareFootage { get; set; }

    int? DevelopmentId { get; set; }

    int BranchGuid { get; set; }

    IPropertyBranch Branch { get; set; }

    IDevelopment Development { get; set; }

    IAddress Address { get; set; }

    IEnumerable<IFile> Files { get; set; }
  }
}