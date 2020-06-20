using Autofac;
using FastMapper;
using restlessmedia.Module.Address;
using restlessmedia.Module.File;
using restlessmedia.Module.File.Data;
using restlessmedia.Module.Property.Data;
using SqlBuilder;

namespace restlessmedia.Module.Property
{
  public class Module : IModule
  {
    public void RegisterComponents(ContainerBuilder containerBuilder)
    {
      containerBuilder.RegisterType<PropertyDataProvider>().As<IPropertyDataProvider>().SingleInstance();
      containerBuilder.RegisterType<FileSystemStorageProvider>().As<IDiskStorageProvider>().SingleInstance();
      containerBuilder.RegisterType<PropertyService>().As<IPropertyService>().SingleInstance();
      containerBuilder.RegisterType<FileDataProvider>().As<IFileDataProvider>().SingleInstance();

      ModelFactory.RegisterTypes(GetType().Assembly);

      ObjectMapper.Init(config =>
      {
        config.LazyLoad = false;
        config.Add<PropertyEntity>(targetConfig =>
        {
          targetConfig.For(x => x.Address).ResolveWith<AddressEntity>();
          targetConfig.For(x => x.Branch).ResolveWith<PropertyBranch>();
          targetConfig.For(x => x.Development).ResolveWith<DevelopmentEntity>();
          targetConfig.ForEach(x => x.Files).ResolveWith<FileEntity>();
        });
      });
    }
  }
}