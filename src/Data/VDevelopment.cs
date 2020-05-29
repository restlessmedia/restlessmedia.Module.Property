using restlessmedia.Module.Data.EF;
using restlessmedia.Module.File;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace restlessmedia.Module.Property.Data
{
  [Table("VDevelopment")]
  public class VDevelopment : LicensedEntity, IDevelopment
  {
    public VDevelopment() { }

    public VDevelopment(IDevelopment development)
    {
      if (development == null)
      {
        return;
      }

      DevelopmentId = development.DevelopmentId;
      Name = development.Name;
      Description = development.Description;
      Rank = development.Rank;
    }

    [Key]
    public int? DevelopmentId { get; set; }

    [Varchar]
    [StringLength(50)]
    public string Name { get; set; }

    [Varchar]
    [StringLength(8000)]
    public string Description { get; set; }

    public int Rank { get; set; }

    public override EntityType EntityType
    {
      get
      {
        return EntityType.Development;
      }
    }

    public override int? EntityId
    {
      get
      {
        return DevelopmentId;
      }
    }

    public IEnumerable<IFile> Files
    {
      get
      {
        return VDevelopmentFiles;
      }
      set
      {
        VDevelopmentFiles = value != null ? new List<VDevelopmentFile>(value.Select(x => new VDevelopmentFile(this, x))) : null;
      }
    }

    public ICollection<VDevelopmentFile> VDevelopmentFiles { get; set; }

    [NotMapped]
    public override string Title
    {
      get
      {
        return Name;
      }
      set
      {
        Name = value;
      }
    }
  }
}