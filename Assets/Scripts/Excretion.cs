using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class Excretion : BaseLife
{
    private float energy = 100f;

    void Start()
    {
        energy = 100;
    }

    void Update()
    {
        BaseLifeUpdate();
        //if not eaten, excretion must loose energy to disappear
        energy -= 1 * Time.deltaTime;

        //Checks if should disappear
        if (energy <= 0) UnityEngine.Object.Destroy(this.gameObject);
    }
}
