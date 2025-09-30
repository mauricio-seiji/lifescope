using UnityEngine;
using UnityEngine.EventSystems;

public class GreenCoccus : Coccus, IPointerClickHandler
{
    [SerializeField] private float minEnergyRestoreRate = 1f;
    [SerializeField] private float maxEnergyRestoreRate = 5f;
    private float energyRestoreRate = 5f;

    [SerializeField] private GreenCoccus greenCoccusPrefab;
    void Awake()
    {
        CoccusAwake();
    }
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
            CreateCoccus(greenCoccusPrefab.gameObject, new Vector3(transform.position.x + 0.5f, transform.position.y + 0.5f, 0));
            energy = 80;
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        CreateCoccus(greenCoccusPrefab.gameObject, new Vector3(transform.position.x + 1f, transform.position.y + 1f, 0));
    }
}
