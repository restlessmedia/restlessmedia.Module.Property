using restlessmedia.Module.Data.EF;
using restlessmedia.Module.Property.Data.DataModel;
using System.Linq;

namespace restlessmedia.Module.Property.Data
{
  public class BranchRepository : Repository<TBranch, DatabaseContext>
  {
    public BranchRepository(DatabaseContext context)
      : base(context) { }

    public TBranch GetBranch(int branchId, byte? type)
    {
      return Set().SingleOrDefault(x => x.BranchId == branchId && x.Type == type);
    }

    public bool Exists(IBranch branch, out TBranch dataModel)
    {
      dataModel = GetBranch(branch.BranchId, branch.Type);
      return dataModel != null;
    }

    public TBranch AddOrAttach(IBranch branch)
    {
      if (Exists(branch, out TBranch dataModel))
      {
        Context.Branch.Attach(dataModel);
      }
      else
      {
        dataModel = new TBranch(branch);
        Context.Branch.Add(dataModel);
      }

      return dataModel;
    }
  }
}