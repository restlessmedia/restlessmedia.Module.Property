using System;

namespace restlessmedia.Module.Property
{
  public static class CacheProviderExtensions
  {
    public static TProperty GetProperty<TProperty>(this ICacheProvider cache, int propertyId, Func<TProperty> valueFactory)
      where TProperty : PropertyEntity
    {
      return cache.Get(cache.GetKey("property", propertyId), valueFactory);
    }

    public static void RemoveProperty(this ICacheProvider cache, int propertyId)
    {
      cache.Remove(cache.GetKey("property", propertyId));
    }
  }
}