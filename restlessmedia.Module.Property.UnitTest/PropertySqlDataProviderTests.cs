using FakeItEasy;
using restlessmedia.Module.Address.Data;
using restlessmedia.Module.Data;
using restlessmedia.Module.File.Data;
using restlessmedia.Module.Property;
using restlessmedia.Module.Property.Data;
using restlessmedia.Test;
using SqlBuilder;
using SqlBuilder.DataServices;
using System.Data.Common;
using Xunit;

namespace restlessmedia.UnitTest.Data.Provider.Sql
{
  public class PropertySqlDataProviderTests
  {
    public PropertySqlDataProviderTests()
    {
      _dataContext = A.Fake<IDataContext>();
      _addressDataProvider = A.Fake<IAddressDataProvider>();
      _fileDataProvider = A.Fake<IFileDataProvider>();
      _modelDataProvider = A.Fake<IModelDataProvider<Module.Property.Data.DataModel.VProperty>>();
      _dataProvider = new PropertySqlDataProvider(_dataContext, _addressDataProvider, _fileDataProvider, _modelDataProvider);
      A.CallTo(() => _dataContext.ConnectionFactory.CreateConnection(A<bool>.Ignored)).Returns(A.Fake< DbConnection>());
    }

    [Fact]
    public void CreateWhereFromQuery()
    {
      PropertyQuery propertyQuery = new PropertyQuery();

      A.CallTo(() => _modelDataProvider.NewSelect()).Returns(new Select<Module.Property.Data.DataModel.VProperty>());

      IWhere where = _dataProvider.CreateWhereFromQuery(propertyQuery);

      where.Sql().MustBe("where (Type=@p0 And Status!=@p1)");
      where.Parameters["@p0"].MustBe((byte)0);
    }

    [Fact]
    public void CreateSelect()
    {
      PropertyQuery propertyQuery = new PropertyQuery();

      A.CallTo(() => _modelDataProvider.NewSelect()).Returns(new Select<Module.Property.Data.DataModel.VProperty>());

      Select select = _dataProvider.CreateSelect(propertyQuery, false);

      select.Sql().MustBe("select top 10 PropertyId,Title,ShortDescription,LongDescription,Cost,PropertyType,OwnershipType,IsCommercial,Status,HasGarden,HasParking,Position,IsStudio,IsFurnished,AddedDate,BedroomCount,BathroomCount,ReceptionCount,Featured,SquareFootage,EntityGuid,LicenseId,AddressId,KnownAs,Address01,Address02,Town,City,PostCode,CountryCode,NameNumber,Latitude,Longitude,BranchGuid,BranchId,Type,DevelopmentId,Name,SystemFileName,FileName from Api.VProperty where (Type=@p0 And Status!=@p1) order by AddedDate");
    }

    private readonly IDataContext _dataContext;

    private readonly IAddressDataProvider _addressDataProvider;

    private readonly IFileDataProvider _fileDataProvider; 

    private readonly PropertySqlDataProvider _dataProvider;

    private readonly IModelDataProvider<Module.Property.Data.DataModel.VProperty> _modelDataProvider;
  }
}
