using restlessmedia.Module;
using restlessmedia.Module.Address;
using restlessmedia.Module.Address.Data;
using restlessmedia.Module.Data.EF;
using restlessmedia.Module.File;
using restlessmedia.Module.Property;
using restlessmedia.Module.Property.Data.DataModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.RegularExpressions;

namespace restlessmedia.Module.Property.Data
{
  [Table("VProperty")]
  public class VProperty : LicensedEntity, IProperty
  {
    public VProperty()
    {
      AddedDate = DateTime.Now;
    }

    public VProperty(IProperty property)
      : this()
    {
      PropertyId = property.PropertyId;
      Cost = property.Cost;
      Ownership = property.Ownership;
      IsCommercial = property.IsCommercial;
      Status = property.Status;
      HasGarden = property.HasGarden;
      HasParking = property.HasParking;
      Position = property.Position;
      IsStudio = property.IsStudio;
      IsFurnished = property.IsFurnished;
      BedroomCount = property.BedroomCount;
      BathroomCount = property.BathroomCount;
      ReceptionCount = property.ReceptionCount;
      Automated = property.Automated;
      BranchGuid = property.BranchGuid;
      Title = property.Title;
      ShortDescription = ContainsInnerText(property.ShortDescription) ? property.ShortDescription : null;
      LongDescription = ContainsInnerText(property.LongDescription) ? property.LongDescription : null;
      Featured = property.Featured;
      SquareFootage = property.SquareFootage;
      DevelopmentId = property.DevelopmentId;
      AddressId = property.Address.AddressId;
      PropertyType = property.PropertyType;
    }

    [Key]
    public int? PropertyId { get; set; }

    public override int? EntityId
    {
      get
      {
        return PropertyId;
      }
    }

    public override EntityType EntityType
    {
      get
      {
        return EntityType.Property;
      }
    }

    public int? AddressId { get; set; }

    [Varchar]
    public override string Title { get; set; }

    [Varchar]
    public string ShortDescription { get; set; }

    [Varchar]
    public string LongDescription { get; set; }

    public decimal Cost { get; set; }

    public PropertyType? PropertyType { get; set; }

    public OwnershipType Ownership { get; set; }

    public bool IsCommercial { get; set; }

    public PropertyStatus Status { get; set; }

    public bool HasGarden { get; set; }

    public bool HasParking { get; set; }

    public PropertyPosition Position { get; set; }

    public bool IsStudio { get; set; }

    public bool IsFurnished { get; set; }

    public DateTime AddedDate { get; set; }

    public byte BedroomCount { get; set; }

    public byte BathroomCount { get; set; }

    public byte ReceptionCount { get; set; }

    public bool Automated { get; set; }

    public bool Featured { get; set; }

    public double? SquareFootage { get; set; }

    public int? DevelopmentId { get; set; }

    public int BranchGuid { get; set; }

    [ForeignKey("BranchGuid")]
    public TBranch TBranch { get; set; }

    [ForeignKey("AddressId")]
    public VAddress VAddress { get; set; }

    [ForeignKey("DevelopmentId")]
    public VDevelopment VDevelopment { get; set; }

    public IPropertyBranch Branch
    {
      get
      {
        return TBranch;
      }
      set
      {
        TBranch = new TBranch(value);
      }
    }

    public IDevelopment Development
    {
      get
      {
        return VDevelopment;
      }
      set
      {
        VDevelopment = new VDevelopment(value);
      }
    }

    public IAddress Address
    {
      get
      {
        return VAddress;
      }
      set
      {
        VAddress = new VAddress(value);
      }
    }

    public ICollection<VPropertyFile> VPropertyFiles { get; set; }

    public IEnumerable<IFile> Files
    {
      get
      {
        return VPropertyFiles;
      }
      set
      {
        VPropertyFiles = value != null ? new List<VPropertyFile>(value.Select(x => new VPropertyFile(this, x))) : null;
      }
    }

    /// <summary>
    /// Converts an html to text
    /// </summary>
    /// <param name="html"></param>
    /// <returns></returns>
    private static string ToText(string html)
    {
      if (string.IsNullOrWhiteSpace(html))
      {
        return html;
      }

      const string matchTagPattern = @"</?\w+((\s+\w+(\s*=\s*(?:"".*?""|'.*?'|[\^'"">\s]+))?)+\s*|\s*)/?>";
      return Regex.Replace(html, matchTagPattern, string.Empty).Trim();
    }

    /// <summary>
    /// If true, the html contains at least one tag with innerTEXT.
    /// </summary>
    /// <param name="html"></param>
    /// <returns></returns>
    private static bool ContainsInnerText(string html)
    {
      if (string.IsNullOrWhiteSpace(html))
      {
        return false;
      }

      // first strip all the tags then check the length of the remaining string
      return ToText(html).Length > 0;
    }
  }
}