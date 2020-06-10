using restlessmedia.Module.Data;
using restlessmedia.Module.Data.EF;
using restlessmedia.Module.Property.Data.DataModel;
using System.Data.Entity;

namespace restlessmedia.Module.Property.Data
{
  public class DatabaseContext : restlessmedia.Module.Data.EF.DatabaseContext
  {
    public DatabaseContext(IDataContext dataContext, bool autoDetectChanges = false)
      : base(dataContext, autoDetectChanges) { }

    public T Repository<T>()
      where T : Repository
    {
      return (T)typeof(T).GetConstructor(new[] { typeof(DatabaseContext) }).Invoke(new[] { this });
    }

    public DbSet<TBranch> Branch
    {
      get
      {
        return Set<TBranch>();
      }
    }

    public DbSet<VDevelopment> Development
    {
      get
      {
        return Set<VDevelopment>();
      }
    }

    public DbSet<VProperty> Property
    {
      get
      {
        return Set<VProperty>();
      }
    }
  }
}