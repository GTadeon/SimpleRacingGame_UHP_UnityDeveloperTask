using UnityEngine;
using UnityEngine.UI;

public class RacingCarsFactory : MonoBehaviour, IRacingCarsFactory
{
    [Tooltip("car prefab that will be used in game as car instance")]
    public GameObject CarPrefabInstance;
    [Tooltip("place here your spawn points. Each track = should have one spawn point for car so that they can have enough space between and that it looks nice visually.")]
    public Transform[] RacingCarsSpawnPoints;
    private static RacingCarsFactory _racingCarsFactory;

    public string tireSpawnSlotTag;
    public string bodySpawnSlotTag;
    public string engineSpawnSlotTag;

    public Sprite CarSpriteAmbulance;
    public Sprite CarSpriteTaxi;
    public Sprite CarSpritePolice;
    public Sprite CarSpriteCommonCityCar;
    public Sprite CarSpriteVan;

    public GameObject NoCarTires;
    public GameObject WinterCarTire;
    public GameObject BigCarTire;

    public GameObject CarMachineGun;
    public GameObject CarSpeaker;
    public GameObject CarParachute;

    public GameObject CarTurboEngine;
    public GameObject CarUltraTurboEngine;
    public GameObject CarRocketEngine;

    private static int _createdCarsIncrement=0;
    
    void Awake()
    {
        Init();
    }

    private void Init()
    {
        _racingCarsFactory = GetComponent<RacingCarsFactory>();
    }


    private static string GenerateStarterId()
    {
        _createdCarsIncrement++;
        return "carStarterNo." + _createdCarsIncrement.ToString();
    }


    public static RaceStarterModel MakeRaceStarter(StartRacingClickHandler.StarterConfigInputs starterInputConfigs)
    {
        var raceStarter = new RaceStarterModel();
        raceStarter.RaceStarterId = GenerateStarterId();
        raceStarter.BrandModel = new BrandModel((BrandModel.BrandModelType)starterInputConfigs.SelectCarBranModelDdl.value);
        raceStarter.Color = new ColorModel((ColorModel.BodyColor)starterInputConfigs.SelectCarColorDdl.value);
        raceStarter.Tire = new TireModel((TireModel.TireType)starterInputConfigs.SelectCarTireDdl.value);
        raceStarter.VehicleBody = new VehicleBodyModel((VehicleBodyModel.VehicleType)starterInputConfigs.SelectCarBodyDdl.value);
        raceStarter.Engine = new EngineModel((EngineModel.EngineType)starterInputConfigs.SelectCarEngineDdl.value);
        return raceStarter;
    }


    public static void InstantiateCarObject(RaceStarterModel raceStarter, int trackIndex)
    {
        var carInstance = Instantiate(_racingCarsFactory.CarPrefabInstance, _racingCarsFactory.RacingCarsSpawnPoints[trackIndex], false) as GameObject;
        carInstance.GetComponent<VehicelController>().InitRaceStarter(raceStarter);

        DecorateCarInstanceByConfig(raceStarter, carInstance);
    }


    //adjust car sprite color, spawn objects if needed by config demand (machine gun , parachute, bigger tires, etc...)
    private static void DecorateCarInstanceByConfig(RaceStarterModel raceStarter, GameObject carInstance)
    {
        var carSprite = carInstance.GetComponent<SpriteRenderer>();
        //put the appropriate car class sprite (van, ambulance, viper, bmw... whatever)
        switch (raceStarter.BrandModel.BrandModelTypeLevel)
        {
            case BrandModel.BrandModelType.Ambulance:
                carSprite.sprite = _racingCarsFactory.CarSpriteAmbulance;
                break;
            case BrandModel.BrandModelType.Police:
                carSprite.sprite = _racingCarsFactory.CarSpritePolice;
                break;
            case BrandModel.BrandModelType.Taxi:
                carSprite.sprite = _racingCarsFactory.CarSpriteTaxi;
                break;
            case BrandModel.BrandModelType.Van:
                carSprite.sprite = _racingCarsFactory.CarSpriteVan;
                break;
            case BrandModel.BrandModelType.CommonCityCar:
                carSprite.sprite = _racingCarsFactory.CarSpriteCommonCityCar;
                break;
            default:
                carSprite.sprite = _racingCarsFactory.CarSpriteAmbulance;
                break;
        }


        //color the car as needed
        switch (raceStarter.Color.BodyColorSelection)
        {
            case ColorModel.BodyColor.White:
                carSprite.color = Color.white;
                break;
            case ColorModel.BodyColor.Black:
                carSprite.color = Color.black;
                break;
            case ColorModel.BodyColor.Red:
                carSprite.color = Color.red;
                break;
            case ColorModel.BodyColor.Green:
                carSprite.color = Color.green;
                break;
            default:
                break;
        }

        //put appropriate tire size as needed
        var tireSpawnSlots = carInstance.transform;
        switch (raceStarter.Tire.TireTypeLevel)
        {
            case TireModel.TireType.BasicRoadTires:
                break;
            case TireModel.TireType.WinterTires:
                for (int i = 0; i < carInstance.transform.childCount; i++)
                {
                    if (carInstance.transform.GetChild(i).gameObject.tag.ToLower() == _racingCarsFactory.tireSpawnSlotTag.ToLower())
                    {
                        Instantiate(_racingCarsFactory.WinterCarTire, carInstance.transform.GetChild(i).gameObject.transform, false);
                    }
                }
                break;
            case TireModel.TireType.UltraBigTires:
                for (int i = 0; i < carInstance.transform.childCount; i++)
                {
                    if (carInstance.transform.GetChild(i).gameObject.tag.ToLower() == _racingCarsFactory.tireSpawnSlotTag.ToLower())
                    {
                        Instantiate(_racingCarsFactory.BigCarTire, carInstance.transform.GetChild(i).gameObject.transform, false);
                    }
                }
                break;
            case TireModel.TireType.NoTires:
                for (int i = 0; i < carInstance.transform.childCount; i++)
                {
                    if (carInstance.transform.GetChild(i).gameObject.tag.ToLower() == _racingCarsFactory.tireSpawnSlotTag.ToLower())
                    {
                        Instantiate(_racingCarsFactory.NoCarTires, carInstance.transform.GetChild(i).gameObject.transform, false);
                    }
                }
                break;
            default:
                break;
        }


        //set machine gun/parachute/stereo or nothing as needed
        switch (raceStarter.VehicleBody.VehicleTypeLevel)
        {
            case VehicleBodyModel.VehicleType.BasicCarBody:
                break;
            case VehicleBodyModel.VehicleType.BodyWithMachineGun:
                for (int i = 0; i < carInstance.transform.childCount; i++)
                {
                    if (carInstance.transform.GetChild(i).gameObject.tag.ToLower() == _racingCarsFactory.bodySpawnSlotTag.ToLower())
                    {
                        Instantiate(_racingCarsFactory.CarMachineGun, carInstance.transform.GetChild(i).gameObject.transform, false);
                        break;
                    }
                }
                break;
            case VehicleBodyModel.VehicleType.BodyWithSpeakers:
                for (int i = 0; i < carInstance.transform.childCount; i++)
                {
                    if (carInstance.transform.GetChild(i).gameObject.tag.ToLower() == _racingCarsFactory.bodySpawnSlotTag.ToLower())
                    {
                        Instantiate(_racingCarsFactory.CarSpeaker, carInstance.transform.GetChild(i).gameObject.transform, false);
                        break;
                    }
                }
                break;
            case VehicleBodyModel.VehicleType.WithParachute:
                for (int i = 0; i < carInstance.transform.childCount; i++)
                {
                    if (carInstance.transform.GetChild(i).gameObject.tag.ToLower() == _racingCarsFactory.bodySpawnSlotTag.ToLower())
                    {
                        Instantiate(_racingCarsFactory.CarParachute, carInstance.transform.GetChild(i).gameObject.transform, false);
                        break;
                    }
                }
                break;
            default:
                break;
        }

        //put rocket engine if/as needed
        switch (raceStarter.Engine.EngineTypeLevel)
        {
            case EngineModel.EngineType.V8Engine:
                break;
            case EngineModel.EngineType.V8Turbo:
                for (int i = 0; i < carInstance.transform.childCount; i++)
                {
                    if (carInstance.transform.GetChild(i).gameObject.tag.ToLower() == _racingCarsFactory.engineSpawnSlotTag.ToLower())
                    {
                        Instantiate(_racingCarsFactory.CarTurboEngine, carInstance.transform.GetChild(i).gameObject.transform, false);
                        break;
                    }
                }
                break;
            case EngineModel.EngineType.V8UltraTurbo:
                for (int i = 0; i < carInstance.transform.childCount; i++)
                {
                    if (carInstance.transform.GetChild(i).gameObject.tag.ToLower() == _racingCarsFactory.engineSpawnSlotTag.ToLower())
                    {
                        Instantiate(_racingCarsFactory.CarUltraTurboEngine, carInstance.transform.GetChild(i).gameObject.transform, false);
                        break;
                    }
                }
                break;
            case EngineModel.EngineType.RocketEngine:
                for (int i = 0; i < carInstance.transform.childCount; i++)
                {
                    Debug.Log("na tagu sam " + carInstance.transform.GetChild(i).gameObject.tag + " to nije jednako "+ _racingCarsFactory.engineSpawnSlotTag);
                    if (carInstance.transform.GetChild(i).gameObject.tag.ToLower() == _racingCarsFactory.engineSpawnSlotTag.ToLower())
                    {
                        Debug.Log("instnaciram rocket engine");
                        Instantiate(_racingCarsFactory.CarRocketEngine, carInstance.transform.GetChild(i).gameObject.transform, false);
                        break;
                    }
                }
                break;
            default:
                break;
        }
    }


    void IRacingCarsFactory.InstantiateCarObject(RaceStarterModel raceStarter, int trackIndex)
    {
        InstantiateCarObject(raceStarter, trackIndex);
    }

    RaceStarterModel IRacingCarsFactory.MakeRaceStarter(StartRacingClickHandler.StarterConfigInputs starterInputConfigs)
    {
        return MakeRaceStarter(starterInputConfigs);
    }

}
