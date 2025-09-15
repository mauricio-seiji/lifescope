using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class GreenMembrane : MonoBehaviour
{
    [SerializeField] private float minEnergyRestoreRate = 1f;
    [SerializeField] private float maxEnergyRestoreRate = 5f;
    private float energyRestoreRate = 5f;
    private float energy = 100f;
    private float energyToReproduce = 120f;

    [SerializeField] private GreenCoccus greenCoccusPrefab;

    Renderer objectRenderer;

    void Start()
    {
        energyRestoreRate = UnityEngine.Random.Range(minEnergyRestoreRate, maxEnergyRestoreRate);

        objectRenderer = GetComponent<Renderer>();
        objectRenderer.material.color = Color.green;
        energy = 100;
    }

    
    void Update()
    {
        energy += energyRestoreRate * Time.deltaTime;

        if (energy >= energyToReproduce)
        {
            Instantiate(greenCoccusPrefab, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
            energy = 80;
        }
    }
}
