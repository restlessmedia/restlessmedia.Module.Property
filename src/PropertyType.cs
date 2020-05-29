using System;

namespace restlessmedia.Module.Property
{
  [Flags]
  public enum PropertyType : byte
  {
    None = 0,
    House = 1,
    Flat = 2,
    All = 3,
    BarnConversion = 4,
    Cottage = 5,
    Chalet = 6,
    FarmHouse = 7,
    ManorHouse = 8,
    Mews = 9,
    TownHouse = 10,
    Villa = 11,
    SharedHouse = 12,
    ShelteredHousing = 13,
    Apartment = 14,
    Bedsit = 15,
    GroundFloorFlat = 16,
    GroundFloorMaisonette = 17,
    Maisonette = 18,
    Penthouse = 19,
    Studio = 20,
    SharedFlat = 21,
    Bungalow = 22,
    BuildingPlotOrLand = 24,
    Garage = 25,
    HouseBoat = 26,
    MobileHome = 27,
    Parking = 28,
    Equestrian = 29,
    UnconvertedBarn = 33
  }
}