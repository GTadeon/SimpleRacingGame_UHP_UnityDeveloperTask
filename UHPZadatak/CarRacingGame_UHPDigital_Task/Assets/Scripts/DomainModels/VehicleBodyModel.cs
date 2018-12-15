public class VehicleBodyModel
{
    public enum VehicleType
    {
        BasicCarBody = 0,
        BodyWithMachineGun = 1,
        BodyWithSpeakers = 2,
        WithParachute = 3
    }

    public VehicleType VehicleTypeLevel { get; set; }

    public float MassBenefit
    {
        get
        {
            var massBenefit = DataCenter.BasicCarBodyMassBenefit;
            switch (VehicleTypeLevel)
            {
                case VehicleType.BasicCarBody:
                    massBenefit = DataCenter.BasicCarBodyMassBenefit;
                    break;
                case VehicleType.BodyWithMachineGun:
                    massBenefit = DataCenter.BodyWithMachineGunMassBenefit;
                    break;
                case VehicleType.BodyWithSpeakers:
                    massBenefit = DataCenter.BodyWithSpeakersMassBenefit;
                    break;
                case VehicleType.WithParachute:
                    massBenefit = DataCenter.BodyWithParachuteMassBenefit;
                    break;
                default:
                    break;
            }
            return massBenefit;
        }
    }

    public VehicleBodyModel(VehicleType vehicleType)
    {
        this.VehicleTypeLevel = vehicleType;
    }
}