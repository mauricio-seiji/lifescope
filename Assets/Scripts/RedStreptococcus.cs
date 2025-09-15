using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class RedStreptococcus : BaseLife
{
    private void OnCollisionEnter2D(Collision2D collisionObject)
    {
        Coccus otherCoccus = collisionObject.gameObject.GetComponent<Coccus>();
        if (otherCoccus == null) return;

        if (otherCoccus is GreenCoccus) UnityEngine.Object.Destroy(otherCoccus.gameObject); ;
    }
}
