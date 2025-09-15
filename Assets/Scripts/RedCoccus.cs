using UnityEngine;

public class RedCoccus : Coccus
{
    [SerializeField] private float minEnergyDecayRate = 1f;
    [SerializeField] private float maxEnergyDecayRate = 5f;
    private float energyDecayRate = 1f;

    private float noKillingPeriod = 2f;

    [SerializeField] private RedCoccus redCoccusPrefab;

    Renderer objectRenderer;

    void Start()
    {
        CoccusStart();

        objectRenderer = GetComponent<Renderer>();
        objectRenderer.material.color = Color.red;
        energyDecayRate = UnityEngine.Random.Range(minEnergyDecayRate, maxEnergyDecayRate);
        energy = 100;
    }
    void Update()
    {
        CoccusUpdate();
        if (noKillingPeriod >= 0) noKillingPeriod -= Time.deltaTime;

        //Red dies a little energy each frame (starving)
        energy -= energyDecayRate * Time.deltaTime;

        if (energy >= energyToReproduce)
        {
            Instantiate(redCoccusPrefab, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
            energy = 100;
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
