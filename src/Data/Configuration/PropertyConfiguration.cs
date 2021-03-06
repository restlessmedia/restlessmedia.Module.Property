﻿using restlessmedia.Module.Data.EF;

namespace restlessmedia.Module.Property.Data.Configuration
{
  internal class PropertyConfiguration<T> : LicensedEntityConfiguration<T>
    where T : LicensedEntity, IProperty
  {
    public PropertyConfiguration()
      : base()
    {
      Property(x => x.Ownership).HasColumnName("OwnershipType");
    }
  }
}