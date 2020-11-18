﻿using restlessmedia.Module.Property.Data;

namespace restlessmedia.Module.Property.Configuration
{
  internal class VPropertyConfiguration : PropertyConfiguration<VProperty>
  {
    public VPropertyConfiguration()
      : base()
    {
      HasMany(x => x.VPropertyFiles)
       .WithOptional()
       .HasForeignKey(x => x.PropertyId);
    }
  }
}