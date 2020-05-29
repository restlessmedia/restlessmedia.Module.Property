using restlessmedia.Module.Extensions;
using restlessmedia.Module.File;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace restlessmedia.Module.Property
{
  public static class IPropertyExtensions
  {
    /// <summary>
    /// Returns the files for a gallewry (excluding the floorplan if one is available)
    /// </summary>
    public static IEnumerable<IFile> GalleryFiles(this IProperty property)
    {
      return property.Files.Where(x => !x.Flags.HasValue || (x.Flags.HasValue && !((FileFlags)x.Flags).HasFlag(FileFlags.FloorPlan)));
    }

    public static IFile FloorPlan(this IProperty property)
    {
      return property.Files.FirstOrDefault(x => x.Flags.HasValue && ((FileFlags)x.Flags).HasFlag(FileFlags.FloorPlan));
    }

    public static string EncodedShortDescription(this IProperty property)
    {
      return string.IsNullOrEmpty(property.ShortDescription) ? property.ShortDescription : HttpUtility.HtmlEncode(property.ShortDescription);
    }

    public static string DecodedShortDescription(this IProperty property)
    {
      return string.IsNullOrEmpty(property.ShortDescription) ? property.ShortDescription : HttpUtility.HtmlDecode(property.ShortDescription);
    }

    /// <summary>
    /// If true, the property is either under offer, sold or let
    /// </summary>
    public static bool CanRequestViewing(this IProperty property)
    {
      return property.Status == PropertyStatus.Available;
    }

    /// <summary>
    /// Returns state summary of property i.e sold, added today, under offer etc based on current properties
    /// </summary>
    public static string State(this IProperty property)
    {
      if (property.Status == PropertyStatus.Archived)
      {
        return "Archived";
      }

      if (property.Status == PropertyStatus.Unavailable)
      {
        return property.Branch.ListingType == ListingType.Letting ? "Let agreed" : "Sold";
      }

      if (property.Status == PropertyStatus.UnderOffer && property.Branch.ListingType == ListingType.Sale)
      {
        return "Under offer";
      }

      if (AddedRecently(property))
      {
        return "Just added";
      }

      return null;
    }

    /// <summary>
    /// Returns a shorterend version of the description if the length exceeds trimAt size.
    /// </summary>
    /// <param name="trimAt"></param>
    /// <returns></returns>
    public static string GetShortDescription(this IProperty property, int trimAt)
    {
      int shortDescriptionLen = property.ShortDescription.Length;

      if (shortDescriptionLen == 0 || shortDescriptionLen < trimAt)
      {
        return property.ShortDescription;
      }

      return property.ShortDescription.Substring(0, trimAt);
    }

    /// <summary>
    /// Returns the cost and term (if any) i.e. £210,000 Leasehold or £450 per week
    /// </summary>
    public static string CostTerm(this IProperty property)
    {
      if (property.Cost == 0)
      {
        return "£PoA";
      }

      string suffix = null;

      if (property.Branch.ListingType == ListingType.Sale)
      {
        switch (property.Ownership)
        {
          case OwnershipType.Leasehold:
            {
              suffix = " Leasehold";
              break;
            }
          case OwnershipType.ShareOfFreehold:
            {
              suffix = " s.o Freehold";
              break;
            }
          case OwnershipType.Freehold:
            {
              suffix = " Freehold";
              break;
            }
        }
      }
      else if (property.Branch.ListingType == ListingType.Letting)
      {
        suffix = " per week";
      }

      return string.Concat(property.Cost.ToCurrency(true), suffix);
    }

    public static bool AddedRecently(this IProperty property)
    {
      return property.AddedDate.IsWithinDays(2);
    }
  }
}