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
