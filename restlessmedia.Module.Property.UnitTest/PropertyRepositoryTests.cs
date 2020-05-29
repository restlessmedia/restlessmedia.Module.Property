using FakeItEasy;
using System;
using System.Linq;
using Xunit;

namespace restlessmedia.UnitTest.Data.Repository
{
  public class PropertyRepositoryTests
  {
    public PropertyRepositoryTests()
    {
      _dataContext = A.Fake<IDataContext>();

      A.CallTo(() => _dataContext.ConnectionFactory).Returns(new IntegrationTestConnetionFactory());

      _context = new DatabaseContext(_dataContext);

      _repository = new PropertyRepository(_context);
    }

    [Fact(Skip="")]
    public void property_all()
    {
      var l = _repository.GetAll().ToList();
    }

    private readonly PropertyRepository _repository;

    private readonly DatabaseContext _context;

    private readonly IDataContext _dataContext;

    private readonly Guid _licenseKey = new Guid("BF812A79-9AE3-48F7-8210-FDA1DF411318");
  }
}
