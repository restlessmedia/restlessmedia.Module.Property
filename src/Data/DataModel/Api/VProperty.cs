using SqlBuilder;
using SqlBuilder.Attributes;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace restlessmedia.Module.Property.Data.DataModel.Api
{
  [Table("VProperty", Schema = "Api")]
  public class VProperty : DataModel<VProperty, PropertyEntity>
  {
    [Key]
    public int? PropertyId { get; set; }

    public string Title { get; set; }

    public string ShortDescription { get; set; }

    public string LongDescription { get; set; }

    public decimal Cost { get; set; }

    public PropertyType? PropertyType { get; set; }

    public OwnershipType OwnershipType { get; set; }

    public bool IsCommercial { get; set; }

    public PropertyStatus Status { get; set; }

    public bool HasGarden { get; set; }

    public bool HasParking { get; set; }

    public PropertyPosition Position { get; set; }

    public bool IsStudio { get; set; }

    public bool IsFurnished { get; set; }

    [OrderBy]
    public DateTime AddedDate { get; set; }

    public byte BedroomCount { get; set; }

    public byte BathroomCount { get; set; }

    public byte ReceptionCount { get; set; }

    public bool Featured { get; set; }

    public double? SquareFootage { get; set; }

    #region License

    public int? EntityGuid { get; set; }

    public int? LicenseId { get; set; }

    #endregion

    #region Address

    public int? AddressId { get; set; }

    public string KnownAs { get; set; }

    public string Address01 { get; set; }

    public string Address02 { get; set; }

    public string Town { get; set; }

    public string City { get; set; }

    public string PostCode { get; set; }

    public string CountryCode { get; set; }

    public string NameNumber { get; set; }

    #endregion

    #region Marker

    public double? Latitude { get; set; }

    public double? Longitude { get; set; }

    #endregion

    #region Branch

    public int BranchGuid { get; set; }

    public int BranchId { get; set; }

    public byte Type { get; set; }

    #endregion

    #region Development

    public int? DevelopmentId { get; set; }

    public string Name { get; set; }

    #endregion

    public string SystemFileName { get; set; }

    public string FileName { get; set; }
  }
}