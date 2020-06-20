using Dapper;
using FakeItEasy;
using restlessmedia.Module.Address.Data;
using restlessmedia.Module.Data;
using restlessmedia.Module.File.Data;
using restlessmedia.Module.Property;
using restlessmedia.Module.Property.Data;
using restlessmedia.Test;
using SqlBuilder;
using SqlBuilder.DataServices;
using System;
using System.Data.Common;
using System.Data.SQLite;
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
    }

    [Fact]
    public void CreateWhereFromQuery()
    {
      // set-up
      PropertyQuery propertyQuery = new PropertyQuery();
      UseFakeConnection();
      A.CallTo(() => _modelDataProvider.NewSelect()).Returns(new Select<Module.Property.Data.DataModel.VProperty>());

      // call
      IWhere where = _dataProvider.CreateWhereFromQuery(propertyQuery);

      // assert
      where.Sql().MustBe("where (Type=@p0 And Status!=@p1)");
      where.Parameters["@p0"].MustBe((byte)0);
    }

    [Fact]
    public void CreateSelect()
    {
      // set-up
      PropertyQuery propertyQuery = new PropertyQuery();
      UseFakeConnection();
      A.CallTo(() => _modelDataProvider.NewSelect()).Returns(new Select<Module.Property.Data.DataModel.VProperty>());

      // call
      Select select = _dataProvider.CreateSelect(propertyQuery, false);

      // assert
      select.Sql().MustBe("select top 10 PropertyId,Title,ShortDescription,LongDescription,Cost,PropertyType,OwnershipType,IsCommercial,Status,HasGarden,HasParking,Position,IsStudio,IsFurnished,AddedDate,BedroomCount,BathroomCount,ReceptionCount,Featured,SquareFootage,EntityGuid,LicenseId,AddressId,KnownAs,Address01,Address02,Town,City,PostCode,CountryCode,NameNumber,Latitude,Longitude,BranchGuid,BranchId,Type,DevelopmentId,Name,SystemFileName,FileName from Api.VProperty where (Type=@p0 And Status!=@p1) order by AddedDate");
    }

    private void UseFakeConnection()
    {
      A.CallTo(() => _dataContext.ConnectionFactory.CreateConnection(A<bool>.Ignored)).Returns(A.Fake<DbConnection>());
    }

    private void UseSqliteConnection(Action<SQLiteConnection> action = null)
    {
      SQLiteConnectionStringBuilder connectionStringBuilder = new SQLiteConnectionStringBuilder { DataSource = ":memory:" };
      string connectionString = connectionStringBuilder.ToString();
      SQLiteConnection connection = new SQLiteConnection(connectionString);
      action?.Invoke(connection);
      A.CallTo(() => _dataContext.ConnectionFactory.CreateConnection(A<bool>.Ignored)).Returns(connection);
    }

    private readonly IDataContext _dataContext;

    private readonly IAddressDataProvider _addressDataProvider;

    private readonly IFileDataProvider _fileDataProvider; 

    private readonly PropertySqlDataProvider _dataProvider;

    private readonly IModelDataProvider<Module.Property.Data.DataModel.VProperty> _modelDataProvider;
  }
}