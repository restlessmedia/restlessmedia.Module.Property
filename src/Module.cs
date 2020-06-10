using Autofac;
using restlessmedia.Module.File;
using restlessmedia.Module.File.Data;
using restlessmedia.Module.Property.Data;

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
    }
  }
}