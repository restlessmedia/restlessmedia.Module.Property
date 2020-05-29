using restlessmedia.Module.Address.Data;
using restlessmedia.Module.Data;
using restlessmedia.Module.File.Data;
using SqlBuilder.DataServices;

namespace restlessmedia.Module.Property.Data
{
  public class PropertyDataProvider : PropertySqlDataProvider, IPropertyDataProvider
  {
    public PropertyDataProvider(IDataContext context, IAddressDataProvider addressDataProvider, IFileDataProvider fileDataProvider, IModelDataProvider<DataModel.VProperty> modelDataProvider)
      : base(context, addressDataProvider, fileDataProvider, modelDataProvider) { }
  }
}