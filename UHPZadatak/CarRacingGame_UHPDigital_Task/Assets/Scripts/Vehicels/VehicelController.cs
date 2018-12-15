using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// attach this to your car prefab. Respponsbile for car controlling.
/// (velocity, when it starts, stops..)
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class VehicelController : MonoBehaviour {

    /// <summary>
    /// velocity of this vehicles. Caluclated based on settings made
    /// for this vehicle.
    /// </summary>
    private Vector2 _velocity;

    private Rigidbody2D _rigidBody;

    private bool _canMove;

    /// <summary>
    /// race starter that this object instance is reffering to.
    /// This should get set immeditately on "start racing" btn click.
    /// </summary>
    public RaceStarterModel RaceStarter;

    public static Dictionary<string, GameObject> StarterIdObjectInstance = new Dictionary<string, GameObject>();


    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        GetConfiguration();
    }


    void Update()
    {
        HandleState();
    }


    private void HandleState()
    {
        if (_canMove)
        {
            _rigidBody.velocity = _velocity;
        }
    }


    private void GetConfiguration()
    {
        _canMove = DataCenter.ShouldCarsMoveFromStart;
    }


    public void InitRaceStarter(RaceStarterModel raceStarter)
    {
        StarterIdObjectInstance.Add(raceStarter.RaceStarterId, this.gameObject);
        this.RaceStarter = raceStarter;
        this._velocity = raceStarter.RaceStarterVelocity;
        this._canMove = true;
    }


    public static GameObject GetStarterObjectInstanceByRaceStarterId(string starterId)
    {
        return StarterIdObjectInstance[starterId];
    }


}
