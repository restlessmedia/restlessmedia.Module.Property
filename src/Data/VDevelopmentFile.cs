using restlessmedia.Module.File;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace restlessmedia.Module.Property.Data
{
  [Table("VDevelopmentFile")]
  public class VDevelopmentFile : IFile
  {
    public VDevelopmentFile() { }

    public VDevelopmentFile(VDevelopment development, IFile file)
    {
      if (file == null)
      {
        return;
      }

      DevelopmentId = development.DevelopmentId.GetValueOrDefault(0);
      FileId = file.FileId;
      FileName = file.FileName;
      SystemFileName = file.SystemFileName;
      MimeType = file.MimeType;
      Flags = file.Flags;
      LastUpdated = file.LastUpdated;
    }

    [Key, Column(Order = 0)]
    public int DevelopmentId { get; set; }

    [Key, Column(Order = 1)]
    public int? FileId { get; set; }

    public string FileName { get; set; }

    public string SystemFileName { get; set; }

    public string MimeType { get; set; }

    public int? Flags { get; set; }

    public bool IsDefault { get; set; }

    public int? Rank { get; set; }

    public DateTime? LastUpdated { get; set; }
  }
}