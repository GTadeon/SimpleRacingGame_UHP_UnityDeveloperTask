
public interface IRacingCarsFactory
{

    RaceStarterModel MakeRaceStarter(StartRacingClickHandler.StarterConfigInputs starterInputConfigs);

    void InstantiateCarObject(RaceStarterModel raceStarter, int trackIndex);

}
