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

    private Button _button;

    void Awake()
    {
        this._button = GetComponent<Button>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        this._button.enabled = false;
        for (int i = 0; i < DataCenter._DataCenter.starterConfigs.Length; i++)
        {
            RegisterRaceStarterCar(DataCenter._DataCenter.starterConfigs[i], i);
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

        UserCarSettings userSettings = RacingCarsFactory.MakeUserSettings(config, racer.RaceStarterId);
        Repository.SaveCarStartersConfigs(userSettings);
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
