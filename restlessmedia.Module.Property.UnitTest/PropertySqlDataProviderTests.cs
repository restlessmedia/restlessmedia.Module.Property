using FakeItEasy;
using SqlBuilder;

namespace restlessmedia.UnitTest.Data.Provider.Sql
{
  public class PropertySqlDataProviderTests
  {
    public PropertySqlDataProviderTests()
    {
      _dataContext = A.Fake<IDataContext>();
      _addressDataProvider = A.Fake<IAddressDataProvider>();
      _fileDataProvider = A.Fake<IFileDataProvider>();
      _modelDataProvider = A.Fake<IModelDataProvider<restlessmedia.Data.DataModel.Api.VProperty>>();
      _dataProvider = new PropertySqlDataProvider(_dataContext, _addressDataProvider, _fileDataProvider, _modelDataProvider);
      A.CallTo(() => _dataContext.ConnectionFactory.CreateConnection(A<bool>.Ignored)).Returns(A.Fake< DbConnection>());
    }

    public void CreateWhereFromQuery()
    {
      PropertyQuery propertyQuery = new PropertyQuery();

      A.CallTo(() => _modelDataProvider.NewSelect()).Returns(new Select<restlessmedia.Data.DataModel.Api.VProperty>());

      IWhere where = _dataProvider.CreateWhereFromQuery(propertyQuery);

      where.Sql().MustEqual("where (Type=@p0 And PropertyType=@p1 And Status=@p2 And (Address01 Like @p3 Or Address02 Like @p4 Or PostCode Like @p5 Or Title Like @p6))");
      where.Parameters["@p0"].MustEqual("");
    }

    public void CreateSelect()
    {
      PropertyQuery propertyQuery = new PropertyQuery();

      A.CallTo(() => _modelDataProvider.NewSelect()).Returns(new Select<restlessmedia.Data.DataModel.Api.VProperty>());

      Select select = _dataProvider.CreateSelect(propertyQuery, false);

      select.Sql().MustEqual("where (Type=@p0 And PropertyType=@p1 And Status=@p2 And (Address01 Like @p3 Or Address02 Like @p4 Or PostCode Like @p5 Or Title Like @p6))");
    }

    private readonly IDataContext _dataContext;

    private readonly IAddressDataProvider _addressDataProvider;

    private readonly IFileDataProvider _fileDataProvider; 

    private readonly PropertySqlDataProvider _dataProvider;

    private readonly IModelDataProvider<restlessmedia.Data.DataModel.Api.VProperty> _modelDataProvider;
  }
}
