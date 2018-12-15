public class BrandModel
{
    public enum BrandModelType
    {
        Police = 0,
        Ambulance = 1,
        Taxi = 2,
        Van = 3,
        CommonCityCar = 4
    }

    public BrandModelType BrandModelTypeLevel { get; set; }

    public BrandModel(BrandModelType brandModelType)
    {
        this.BrandModelTypeLevel = brandModelType;
    }
}



