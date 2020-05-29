using restlessmedia.Module.Data.EF;
using restlessmedia.Module.File.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace restlessmedia.Module.Property.Data
{
  [Table("VEntityFile")]
  public class VEntityFile
  {
    public VEntityFile() { }

    public VEntityFile(TEntity entity, VFile file)
    {
      Entity = entity;
      File = file;
    }

    public VEntityFile(int entityGuid, int fileId)
    {
      EntityGuid = entityGuid;
      FileId = FileId;
    }

    [Key, Column(Order = 0)]
    public int EntityGuid { get; set; }

    [Key, Column(Order = 1)]
    public int FileId { get; set; }

    public bool IsDefault { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public int? Rank { get; set; }

    [ForeignKey("FileId")]
    public VFile File { get; set; }

    [ForeignKey("EntityGuid")]
    public TEntity Entity { get; set; }
  }
}