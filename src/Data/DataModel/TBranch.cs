using restlessmedia.Module.Data.EF;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace restlessmedia.Module.Property.Data.DataModel
{
  [Table("TBranch")]
  public class TBranch : IPropertyBranch
  {
    public TBranch() { }

    public TBranch(IBranch branch)
    {
      if (branch == null)
      {
        return;
      }

      Name = branch.Name;
      BranchGuid = branch.BranchGuid;
      BranchId = branch.BranchId;
      Type = branch.Type;
      Reference = branch.Reference;
    }

    public ListingType ListingType
    {
      get
      {
        return (ListingType)Type;
      }
    }

    [Key]
    public int BranchGuid { get; set; }

    public int BranchId { get; set; }

    [Varchar]
    public string Name { get; set; }

    public byte Type { get; set; }

    [Varchar]
    public string Reference { get; set; }
  }
}