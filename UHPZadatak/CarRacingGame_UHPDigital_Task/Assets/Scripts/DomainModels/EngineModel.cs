public class EngineModel
{
    public enum EngineType
    {
        V8Engine = 0,
        V8Turbo = 1,
        V8UltraTurbo = 2,
        RocketEngine = 3
    }

    public EngineType EngineTypeLevel { get; set; }

    public float EngineBenefit
    {
        get
        {
            var engineBenefit = DataCenter.V8EngineBenefit;
            switch (EngineTypeLevel)
            {
                case EngineType.V8Engine:
                    engineBenefit = DataCenter.V8EngineBenefit;
                    break;
                case EngineType.V8Turbo:
                    engineBenefit = DataCenter.V8TurboEngineBenefti;
                    break;
                case EngineType.V8UltraTurbo:
                    engineBenefit = DataCenter.V8UltraTurboEngineBenefit;
                    break;
                case EngineType.RocketEngine:
                    engineBenefit = DataCenter.RocketEngineEngineBenefit;
                    break;
                default:
                    break;
            }
            return engineBenefit;
        }
    }

    public EngineModel(EngineType engineType)
    {
        this.EngineTypeLevel = engineType;
    }
}