using restlessmedia.Module.File;
using System.Collections.Generic;

namespace restlessmedia.Module.Property
{
  public class DevelopmentEntity : Entity, IDevelopment
  {
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

    public int? DevelopmentId { get; set; }

    public virtual string Name
    {
      get
      {
        return Development;
      }
      set
      {
        Development = value;
      }
    }

    public virtual string Description { get; set; }

    public IEnumerable<IFile> Files { get; set; }

    public int Rank { get; set; }

    #region Dapper Mappings

    private string Development
    {
      get
      {
        return base.Title;
      }
      set
      {
        base.Title = value;
      }
    }

    #endregion
  }
}