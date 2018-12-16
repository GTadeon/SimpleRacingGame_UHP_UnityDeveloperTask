using SQLite4Unity3d;
using System;

/// <summary>
/// Registered users table
/// </summary>
[Serializable]
public class UserCarSettings
{

    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public string StarterCarId { get; set; }

    public BrandModel.BrandModelType BrandSelected { get; set; }

    public ColorModel.BodyColor ColorSelected { get; set; }

    public VehicleBodyModel.VehicleType VehicleBodySelected { get; set; }

    public TireModel.TireType TireSelected { get; set; }

    public EngineModel.EngineType EngineSelected { get; set; }

}
