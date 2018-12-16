
public interface IRacingCarsFactory
{

    RaceStarterModel MakeRaceStarter(StarterConfigInputs starterInputConfigs);

    void InstantiateCarObject(RaceStarterModel raceStarter, int trackIndex);

}
