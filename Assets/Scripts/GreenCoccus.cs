using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class GreenCoccus : Coccus
{
    [SerializeField] private float minEnergyRestoreRate = 1f;
    [SerializeField] private float maxEnergyRestoreRate = 5f;
    private float energyRestoreRate = 5f;

    [SerializeField] private GreenCoccus greenCoccusPrefab;

    void Start()
    {
        CoccusStart();
        energyRestoreRate = UnityEngine.Random.Range(minEnergyRestoreRate, maxEnergyRestoreRate);
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
