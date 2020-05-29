using restlessmedia.Module.File;

namespace restlessmedia.Module.Property
{
  public interface IPropertyFile : IFile
  {
    FileFlags FileFlags { get; set; }
  }
}