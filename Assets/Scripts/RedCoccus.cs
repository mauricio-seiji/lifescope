using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class RedCoccus : Coccus
{
    [SerializeField] private float minEnergyDecayRate = 2f;
    [SerializeField] private float maxEnergyDecayRate = 5f;
    private float energyDecayRate = 2f;

    private float noKillingPeriod = 2f;

    [SerializeField] private RedCoccus redCoccusPrefab;
    [SerializeField] private Excretion ExcretionPrefab;

    void Start()
    {
        CoccusStart();

        energyDecayRate = UnityEngine.Random.Range(minEnergyDecayRate, maxEnergyDecayRate);
        energy = 100;
    }

    void Update()
    {
        CoccusUpdate();
        if (noKillingPeriod >= 0) noKillingPeriod -= Time.deltaTime;

        //Red loses a little energy each update (starving)
        energy -= energyDecayRate * Time.deltaTime;

        if (energy >= energyToReproduce)
        {
            energy = 100;

            Instantiate(redCoccusPrefab, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);

            //Red poops after reproducing
            Instantiate(ExcretionPrefab, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
        }
    }

    private void OnCollisionEnter2D(Collision2D collisionObject)
    {
        Coccus otherCoccus = collisionObject.gameObject.GetComponent<Coccus>();
        if (otherCoccus == null) return;

        //Red kills Green after the No Killing Period
        if (noKillingPeriod <= 0 && otherCoccus is GreenCoccus)
        {
            UnityEngine.Object.Destroy(otherCoccus.gameObject);
            energy += 20;
        }
    }
}
