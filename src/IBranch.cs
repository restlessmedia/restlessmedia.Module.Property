namespace restlessmedia.Module.Property
{
  public interface IBranch
  {
    int BranchGuid { get; set; }

    int BranchId { get; set; }

    string Name { get; set; }

    byte Type { get; set; }

    string Reference { get; set; }
  }
}