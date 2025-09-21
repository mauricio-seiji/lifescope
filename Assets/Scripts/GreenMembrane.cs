using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.EventSystems.EventTrigger;

public class GreenMembrane : CellNucleus
{
    [SerializeField] private float minEnergyRestoreRate = 1f;
    [SerializeField] private float maxEnergyRestoreRate = 5f;
    private float energyRestoreRate = 1f;
    private float energy = 100f;
    private float energyToReproduce = 300f;

    [SerializeField] private GreenCoccus greenCoccusPrefab;

    void Start()
    {
        energyRestoreRate = UnityEngine.Random.Range(minEnergyRestoreRate, maxEnergyRestoreRate);
        energy = 100;
    }
    
    void Update()
    {
        energy += energyRestoreRate * Time.deltaTime;

        if (energy >= energyToReproduce)
        {
            Instantiate(greenCoccusPrefab, new Vector3(transform.position.x + 0.5f, transform.position.y + 0.5f, 0), Quaternion.identity);
            energy = 80;
        }
    }
}
