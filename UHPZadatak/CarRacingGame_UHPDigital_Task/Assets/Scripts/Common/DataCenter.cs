using System.Collections.Generic;
using System.Linq;
using UnityEngine;


/// <summary>
/// used for fetching all the needed data for this racing game.
/// (speed caluclations, constants, etc..). 
/// TODO: pull simple stuff from external source like db or config file.
/// </summary>
public class DataCenter : MonoBehaviour {

    public const float DefaultVehicleVelocity = 10f;

    public const bool ShouldCarsMoveFromStart = false;



    //better tires = greater friction benefit
    #region Tires
    public const float NoTireFrictionBenefit = 0f;

    public const float BasicRoadTireFrictionBenefit = 1f;

    public const float WinterTireFrictionBenefit = 2f;

    public const float UltraBigTireFrictionBenefit = 4f;
    #endregion


    //better body = greater mass benefit = slower car, but greater mass benefit also = better items with special effects
    #region body 
    public const float BasicCarBodyMassBenefit = 1f;

    public const float BodyWithMachineGunMassBenefit = 5f; //todo machine with chance of miss  (fires randomly at every track, hoping to hit the opponent)

    public const float BodyWithSpeakersMassBenefit = 10f; // chance of chilling the opponents (at start), having the chance to take the lead

    public const float BodyWithParachuteMassBenefit = 20f; //if wind blows , it will lift the car up and transport to finish
    #endregion


    //better engine = better engine benefit = faster car (todo: implement accelaration - e.g. conenct with engine weight and type)
    #region engine 
    public const float V8EngineBenefit = 1f;

    public const float V8TurboEngineBenefti = 2f; //todo machine with chance of miss  (fires randomly at every track, hoping to hit the opponent)

    public const float V8UltraTurboEngineBenefit = 3f; //todo: slowe accelaration but way faster final speed than V8Turbo!

    public const float RocketEngineEngineBenefit = 5f; //todo: slowe accelarattion but way faster final speed than V8UltraTurbo!
    #endregion



    /// <summary>
    ///all created cars
    /// </summary>
    public static List<RaceStarterModel> GeneratedRaceStarters = new List<RaceStarterModel>();


    void Awake()
    {
        GeneratedRaceStarters.Clear();
    }

    public static void RegisterRaceStarters(RaceStarterModel raceStarter)
    {
        GeneratedRaceStarters.Add(raceStarter);
        //Debug.Log("reguistirao sam " + raceStarter.Color.BodyColorSelection + " c" + raceStarter.Engine.EngineTypeLevel + " e");
    }

    public static RaceStarterModel GetRaceStarterById(string raceStarterId)
    {
        return GeneratedRaceStarters.Where(x => x.RaceStarterId == raceStarterId).First();
    }

}
