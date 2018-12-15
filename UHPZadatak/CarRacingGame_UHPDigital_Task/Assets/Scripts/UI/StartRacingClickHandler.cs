using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// attach to video in list view of videos that should be played
/// when clicked
/// </summary>
[RequireComponent(typeof(Button))]
public class StartRacingClickHandler : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{

    public static StartRacingClickHandler _StartRacingClickHandler;

    private Button _button;

    //todo_: u zasbeni fajl stavit!
    [System.Serializable]
    public class StarterConfigInputs
    {
        public Dropdown SelectCarBranModelDdl;
        public Dropdown SelectCarColorDdl;
        public Dropdown SelectCarTireDdl;
        public Dropdown SelectCarBodyDdl;
        public Dropdown SelectCarEngineDdl;
    }

    public StarterConfigInputs[] starterConfigs;


    void Awake()
    {
        this._button = GetComponent<Button>();
        _StartRacingClickHandler = this.GetComponent<StartRacingClickHandler>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        this._button.enabled = false;
        for (int i = 0; i < starterConfigs.Length; i++)
        {
            RegisterRaceStarterCar(starterConfigs[i], i);
        }
        StartFollowingTheFastestCar();
    }

    public void OnPointerDown(PointerEventData eventData){}

    public void OnPointerUp(PointerEventData eventData){}


    private void RegisterRaceStarterCar(StarterConfigInputs config, int trackIndex)
    {
        RaceStarterModel racer = RacingCarsFactory.MakeRaceStarter(config);
        DataCenter.RegisterRaceStarters(racer);
        RacingCarsFactory.InstantiateCarObject(racer, trackIndex);
    }

    //we'll only keep fastest in camera's focus
    private void StartFollowingTheFastestCar()
    {
        var fastestCar = DataCenter.GeneratedRaceStarters.OrderByDescending(x => x.RaceStarterVelocity.x).ToList().First();
        var toFollow = VehicelController.GetStarterObjectInstanceByRaceStarterId(fastestCar.RaceStarterId);
        //Debug.Log("should follow the fastest, that is with id: " + fastestCar.RaceStarterId + " go name: " + toFollow.gameObject.name);
        CameraFollow2D.SetTargetToFollow(toFollow);
    }
}
