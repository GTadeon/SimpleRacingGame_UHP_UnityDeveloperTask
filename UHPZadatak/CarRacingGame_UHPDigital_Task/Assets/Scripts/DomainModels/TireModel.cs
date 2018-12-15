
public class TireModel
{
    public enum TireType
    {
        BasicRoadTires = 0,
        WinterTires = 1,
        UltraBigTires = 2,
        NoTires = 3
    }

    public TireType TireTypeLevel { get; set; }

    public float FrictionBenefit
    {
        get
        {
            var friction = DataCenter.NoTireFrictionBenefit;
            switch (TireTypeLevel)
            {
                case TireType.BasicRoadTires:
                    friction = DataCenter.BasicRoadTireFrictionBenefit;
                    break;
                case TireType.WinterTires:
                    friction = DataCenter.WinterTireFrictionBenefit;
                    break;
                case TireType.UltraBigTires:
                    friction = DataCenter.UltraBigTireFrictionBenefit;
                    break;
                case TireType.NoTires:
                    friction = DataCenter.NoTireFrictionBenefit;
                    break;
                default:
                    break;
            }
            return friction;
        }
    }

    public TireModel(TireType tireType)
    {
        this.TireTypeLevel = tireType;
    }
}
