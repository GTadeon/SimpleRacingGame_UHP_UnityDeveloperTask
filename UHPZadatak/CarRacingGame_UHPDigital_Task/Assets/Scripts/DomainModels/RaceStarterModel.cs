using UnityEngine;

public class RaceStarterModel
{
    public string RaceStarterId { get; set; }

    public BrandModel BrandModel { get; set; }

    public ColorModel Color { get; set; }

    public TireModel Tire { get; set; }

    public VehicleBodyModel VehicleBody { get; set; }

    public EngineModel Engine { get; set; }


    //caluclated using algorithm that I find suitable for this
    //demo task.
    /// <summary>
    /// V_final= (Vdefault/MassBenefit) X EngineBenefit X FractionBenefit
    /// </summary>
    public Vector2 RaceStarterVelocity
    {
        get
        {
            var velocity = (DataCenter.DefaultVehicleVelocity / VehicleBody.MassBenefit) * Engine.EngineBenefit * Tire.FrictionBenefit;
            return new Vector2(velocity, 0f);
        }
    }
}