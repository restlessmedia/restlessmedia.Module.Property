using Autofac;
using restlessmedia.Module.File;
using restlessmedia.Module.File.Data;
using restlessmedia.Module.Property.Configuration;

namespace restlessmedia.Module.Property
{
  public class Module : IModule
  {
    public void RegisterComponents(ContainerBuilder containerBuilder)
    {
      containerBuilder.RegisterType<FileSystemStorageProvider>().As<IDiskStorageProvider>().SingleInstance();
      containerBuilder.RegisterType<PropertyService>().As<IPropertyService>().SingleInstance();
      containerBuilder.RegisterType<FileDataProvider>().As<IFileDataProvider>().SingleInstance();
    }
  }
}