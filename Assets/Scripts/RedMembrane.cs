using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.EventSystems.EventTrigger;

public class RedMembrane : Coccus
{
    [SerializeField] private RedCoccus redCoccusPrefab;


    void Start()
    {
        CoccusStart();
    }

    void Update()
    {
        CoccusUpdate();

        if (energy >= energyToReproduce)
        {
            energy = 100;
            CreateCoccus(redCoccusPrefab.gameObject, new Vector3(transform.position.x + 0.5f, transform.position.y + 0.5f, 0));
        }
    }
    private void OnCollisionEnter2D(Collision2D collisionObject)
    {
        Coccus otherCoccus = collisionObject.gameObject.GetComponent<Coccus>();
        if (otherCoccus == null) return;

        if (otherCoccus is GreenCoccus)
        {
            otherCoccus.isDead = true;
            energy += 10;
        }
    }
}
