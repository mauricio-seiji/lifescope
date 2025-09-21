using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.EventSystems.EventTrigger;

public class RedMembrane : Coccus
{
    [SerializeField] private RedCoccus RedCoccusPrefab;


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
            Instantiate(RedCoccusPrefab, new Vector3(transform.position.x + 0.5f, transform.position.y + 0.5f, 0), Quaternion.identity);
        }
    }
    private void OnCollisionEnter2D(Collision2D collisionObject)
    {
        Coccus otherCoccus = collisionObject.gameObject.GetComponent<Coccus>();
        if (otherCoccus == null) return;

        if (otherCoccus is GreenCoccus)
        {
            UnityEngine.Object.Destroy(otherCoccus.gameObject);
            energy += 10;
        }
    }
}
