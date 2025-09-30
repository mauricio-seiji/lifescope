using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.EventSystems.EventTrigger;

public class PinkMembrane : Coccus
{
    [SerializeField] private PinkCoccus pinkCoccusPrefab;


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
            CreateCoccus(pinkCoccusPrefab.gameObject, new Vector3(transform.position.x + 1f, transform.position.y + 1f, 0));
        }
    }

    private void OnCollisionEnter2D(Collision2D collisionObject)
    {
        Excretion excretion = collisionObject.gameObject.GetComponent<Excretion>();
        if (excretion == null) return;

        if (excretion is Excretion)
        {
            UnityEngine.Object.Destroy(excretion.gameObject);
            energy += 10f;
        }
    }
}
