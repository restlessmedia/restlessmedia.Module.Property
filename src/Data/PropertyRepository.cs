using restlessmedia.Module.Data.EF;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace restlessmedia.Module.Property.Data
{
  public class PropertyRepository : LicensedEntityRepository<VProperty>
  {
    public PropertyRepository(DatabaseContext context)
      : base(context)
    {
      // _branchRepository = new BranchRepository(context);
      _context = context;
    }

    public IEnumerable<VProperty> GetAll()
    {
      return Set();
    }

    public VProperty Save(IProperty property)
    {
      VProperty dataModel = new VProperty(property);

      //dataModel.TBranch = _branchRepository.AddOrAttach(dataModel.Branch);

      if (_context.Property.Any(x => x.PropertyId == property.PropertyId))
      {
        Update(dataModel,
          x => x.PropertyId,
          x => x.Cost,
          x => x.Ownership,
          x => x.IsCommercial,
          x => x.Status,
          x => x.HasGarden,
          x => x.HasParking,
          x => x.Position,
          x => x.IsStudio,
          x => x.IsFurnished,
          x => x.BedroomCount,
          x => x.BathroomCount,
          x => x.ReceptionCount,
          x => x.Automated,
          x => x.Title,
          x => x.ShortDescription,
          x => x.LongDescription,
          x => x.Featured,
          x => x.SquareFootage,
          x => x.DevelopmentId,
          x => x.BranchGuid,
          x => x.AddressId,
          x => x.PropertyType
      );
      }
      else
      {
        Add(dataModel);
      }

      return dataModel;
    }

    public VDevelopment Add(VDevelopment development)
    {
      development.Rank = _context.Development.Max(x => x.Rank) + 1;
      return Add<VDevelopment>(development);
    }

    public VDevelopment Save(IDevelopment development)
    {
      VDevelopment dataModel = new VDevelopment(development);

      if (_context.Development.Any(x => x.DevelopmentId == development.DevelopmentId))
      {
        Update(dataModel,
          x => x.DevelopmentId,
          x => x.Name,
          x => x.Description
       );
      }
      else
      {
        Add(dataModel);
      }

      return dataModel;
    }

    public void SetFeatured(int propertyId, bool featured)
    {
      // TODO: this needs to be license aware
      VProperty dataModel = new VProperty { PropertyId = propertyId, Featured = featured };
      Update(dataModel, x => x.Featured);
    }

    public IDevelopment GetDevelopment(int developmentId)
    {
      return _context.Development
        .Include(x => x.VDevelopmentFiles)
        .FirstOrDefault(x => x.DevelopmentId == developmentId);
    }

    public void SetStatus(int propertyId, PropertyStatus status)
    {
      // TODO: this needs to be license aware
      VProperty dataModel = new VProperty { PropertyId = propertyId, Status = status };
      Update(dataModel, x => x.Status);
    }

    public ModelCollection<IDevelopment> ListDevelopments(int page = 1, int maxPerPage = int.MaxValue)
    {
      var query = _context.Development
        .AsNoTracking()
        .Select(x => new
        {
          Development = x,
          Files = x.VDevelopmentFiles
                .OrderBy(y => y.Rank)
                .ThenBy(y => y.FileName)
                .Take(1)
        })
        .Where(x => x.Development.LicenseId == Context.LicenseId)
        .OrderBy(x => x.Development.Rank);

      return new ModelCollection<IDevelopment>(query.Page(page, maxPerPage).ToList().Select(x => { x.Development.Files = x.Files; return x.Development; }), query.Count(), page);
    }

    // private readonly BranchRepository _branchRepository;

    private readonly DatabaseContext _context;
  }
}