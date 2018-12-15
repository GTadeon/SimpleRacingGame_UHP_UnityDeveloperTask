using UnityEngine;

public class FinishLineCollider : MonoBehaviour {

    public string WhoCanTriggerTag;

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == WhoCanTriggerTag)
        {
            CarRacingGameManager.FinishGame(col.gameObject);
        }
    }
}
