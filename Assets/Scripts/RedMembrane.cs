using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class RedMembrane : MonoBehaviour
{
    Renderer objectRenderer;

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        objectRenderer.material.color = Color.red;
    }

    private void OnCollisionEnter2D(Collision2D collisionObject)
    {
        Coccus otherCoccus = collisionObject.gameObject.GetComponent<Coccus>();
        if (otherCoccus == null) return;

        if (otherCoccus is GreenCoccus) UnityEngine.Object.Destroy(otherCoccus.gameObject); ;
    }
}
