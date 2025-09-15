using UnityEngine;

public class GreenCoccus : Coccus
{
    [SerializeField] private float minEnergyRestoreRate = 1f;
    [SerializeField] private float maxEnergyRestoreRate = 5f;
    private float energyRestoreRate = 5f;

    [SerializeField] private GreenCoccus greenCoccusPrefab;

    Renderer objectRenderer;

    void Start()
    {
        CoccusStart();
        energyRestoreRate = UnityEngine.Random.Range(minEnergyRestoreRate, maxEnergyRestoreRate);

        objectRenderer = GetComponent<Renderer>();
        objectRenderer.material.color = Color.green;
        energy = 100;
    }

    void Update()
    {
        CoccusUpdate();
        energy += energyRestoreRate * Time.deltaTime;

        if (energy >= energyToReproduce)
        {
            Instantiate(greenCoccusPrefab, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
            energy = 80;
        }
    }
}
