using UnityEngine;
using UnityEngine.UI;

public class CarRacingGameManager : MonoBehaviour {


    private static bool _isGameFinished;

    public int PanelIdToDisplayWhenFinished;

    private static CarRacingGameManager _carRacingGameManager;

    public Text whoWonText;



    void Awake()
    {
        _carRacingGameManager = GetComponent<CarRacingGameManager>();
        _isGameFinished = false;
    }

    void Start()
    {
        UpdateConfigPanels();
    }

    /// <summary>
    /// pulls data from storage and if any, updates the panels accordingly
    /// </summary>
    private void UpdateConfigPanels()
    {
        var configs = Repository.GetUserCarSettings();
        if (configs!=null )
        {
            for (int i = 0; i < configs.Count; i++)
            {
                DataCenter._DataCenter.starterConfigs[i].SelectCarBodyDdl.value = (int)configs[i].VehicleBodySelected;
                DataCenter._DataCenter.starterConfigs[i].SelectCarBranModelDdl.value = (int)configs[i].BrandSelected;
                DataCenter._DataCenter.starterConfigs[i].SelectCarColorDdl.value = (int)configs[i].ColorSelected;
                DataCenter._DataCenter.starterConfigs[i].SelectCarEngineDdl.value = (int)configs[i].EngineSelected;
                DataCenter._DataCenter.starterConfigs[i].SelectCarTireDdl.value = (int)configs[i].TireSelected;
            }
        }
    }

    public static void FinishGame(GameObject carThatFinished)
    {
        if(!_isGameFinished)
        {
            PanelManager.ShowPanelWithId(_carRacingGameManager.PanelIdToDisplayWhenFinished);
            RaceStarterModel starterCar = carThatFinished.transform.parent.gameObject.GetComponent<VehicelController>().RaceStarter;
            _carRacingGameManager.whoWonText.text = starterCar.BrandModel.BrandModelTypeLevel.ToString().ToLower() + " won!";
            _isGameFinished = true;
        }
    }
}
