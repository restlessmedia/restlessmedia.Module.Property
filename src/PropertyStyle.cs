using System.Xml.Serialization;

namespace restlessmedia.Module.Property
{
  public enum PropertyStyle : byte
  {
    [XmlEnum(Name = "1")]
    BarnConversion = 1,

    [XmlEnum(Name = "2")]
    Cottage = 2,

    [XmlEnum(Name = "3")]
    Chalet = 3,

    [XmlEnum(Name = "4")]
    DetachedHouse = 4,

    [XmlEnum(Name = "5")]
    SemiDetachedHouse = 5,

    [XmlEnum(Name = "6")]
    FarmHouse = 6,

    [XmlEnum(Name = "7")]
    ManorHouse = 7,

    [XmlEnum(Name = "8")]
    Mews = 8,

    [XmlEnum(Name = "9")]
    MidTerracedHouse = 9,

    [XmlEnum(Name = "10")]
    EndTerracedHouse = 10,

    [XmlEnum(Name = "11")]
    TownHouse = 11,

    [XmlEnum(Name = "12")]
    Villa = 12,

    [XmlEnum(Name = "13")]
    Apartment = 13,

    [XmlEnum(Name = "14")]
    Bedsit = 14,

    [XmlEnum(Name = "15")]
    GroundFloorFlat = 15,

    [XmlEnum(Name = "16")]
    Flat = 16,

    [XmlEnum(Name = "17")]
    GroundFloorMaisonette = 17,

    [XmlEnum(Name = "18")]
    Maisonette = 18,

    [XmlEnum(Name = "19")]
    Penthouse = 19,

    [XmlEnum(Name = "20")]
    Studio = 20,

    [XmlEnum(Name = "21")]
    DetachedBungalow = 21,

    [XmlEnum(Name = "22")]
    SemiDetachedBungalow = 22,

    [XmlEnum(Name = "23")]
    BuildingPlotOrLand = 23,

    [XmlEnum(Name = "24")]
    Garage = 24,

    [XmlEnum(Name = "25")]
    HouseBoat = 25,

    [XmlEnum(Name = "26")]
    MobileHome = 26,

    [XmlEnum(Name = "27")]
    Parking = 27,

    [XmlEnum(Name = "28")]
    LinkDetached = 28,

    [XmlEnum(Name = "29")]
    SharedHouse = 29,

    [XmlEnum(Name = "30")]
    SharedFlat = 30,

    [XmlEnum(Name = "31")]
    ShelteredHousing = 31,

    [XmlEnum(Name = "32")]
    Equestrian = 32,

    [XmlEnum(Name = "33")]
    UnconvertedBarn = 33,

    [XmlEnum(Name = "34")]
    MidTerracedBungalow = 34,

    [XmlEnum(Name = "35")]
    EndTerracedBungalow = 35,

    [XmlEnum(Name = "37")]
    ExLA = 37,

    [XmlEnum(Name = "39")]
    ExLA2 = 39,

    [XmlEnum(Name = "41")]
    PeriodHouse = 41,

    [XmlEnum(Name = "43")]
    NewBuildHouse = 43,

    [XmlEnum(Name = "45")]
    PurposeBuiltHouse = 45,

    [XmlEnum(Name = "47")]
    ExLAHouse = 47,

    [XmlEnum(Name = "49")]
    MewsHouse = 49,

    [XmlEnum(Name = "51")]
    Townhouse = 51,

    [XmlEnum(Name = "53")]
    PeriodConversion = 53,

    [XmlEnum(Name = "55")]
    PeriodConversionWithGarden = 55,

    [XmlEnum(Name = "57")]
    PurposeBuiltFlat = 57,

    [XmlEnum(Name = "59")]
    PurposeBuiltFlatWithGarden = 59,

    [XmlEnum(Name = "61")]
    NewBuildFlat = 61,

    [XmlEnum(Name = "63")]
    NewBuildFlatWithGarden = 63,

    [XmlEnum(Name = "65")]
    Balcony = 65,

    [XmlEnum(Name = "67")]
    RoofTerrace = 67,

    [XmlEnum(Name = "69")]
    CommunalOutsideArea = 69,

    [XmlEnum(Name = "71")]
    ExLAFlat = 71,

    [XmlEnum(Name = "73")]
    Studio2 = 73,

    [XmlEnum(Name = "75")]
    Warehouse = 75,

    [XmlEnum(Name = "77")]
    PeriodMaisonette = 77,

    [XmlEnum(Name = "79")]
    PurposeBuiltMaisonette = 79,

    [XmlEnum(Name = "81")]
    NewBuildMaisonette = 81,

    [XmlEnum(Name = "83")]
    ExLAMaisonette = 83,

    [XmlEnum(Name = "85")]
    Commercial = 85,

    [XmlEnum(Name = "87")]
    DevelopmentOpportunity = 87
  }
}