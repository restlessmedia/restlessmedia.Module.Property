using restlessmedia.Module.File;
using System.Collections.Generic;

namespace restlessmedia.Module.Property
{
  public interface IDevelopment
  {
    int? DevelopmentId { get; set; }

    string Name { get; set; }

    string Description { get; set; }

    int Rank { get; set; }

    IEnumerable<IFile> Files { get; set; }
  }
}