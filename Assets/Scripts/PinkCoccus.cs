using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.GraphicsBuffer;

public class PinkCoccus : Coccus
{
    public string targetTag = "Excretion";
    private GameObject closestExcretion;
    private Boolean onCoolDown = false;
    private int CoolDownCounter = 0;
    private int CoolDownDuration = 0;
    [SerializeField] private int MinCoolDownDuration = 1000;
    [SerializeField] private int MaxCoolDownDuration = 2000;

    [SerializeField] private float minEnergyDecayRate = 1f;
    [SerializeField] private float maxEnergyDecayRate = 2f;
    private float energyDecayRate = 1f;

    [SerializeField] private PinkCoccus pinkCoccusPrefab;

    void Start()
    {
        CoccusStart();
        CoolDownDuration = UnityEngine.Random.Range(MinCoolDownDuration, MaxCoolDownDuration);
        energyDecayRate = UnityEngine.Random.Range(minEnergyDecayRate, maxEnergyDecayRate);
        energy = 100;
    }
    void Update()
    {
        CoccusUpdate();

        MoveIfNeeded();

        //White loses a little energy each update (starving)
        energy -= energyDecayRate * Time.deltaTime;

        if (energy >= energyToReproduce)
        {
            energy = 100;
            Instantiate(pinkCoccusPrefab, new Vector3(transform.position.x + 1f, transform.position.y + 1f, 0), Quaternion.identity);
        }

    }

    public void MoveIfNeeded()
    {
        if (onCoolDown)
        {
            CoolDownCounter++;
            if (CoolDownCounter >= CoolDownDuration)
            {
                onCoolDown = false;
                CoolDownCounter = 0;
                CoolDownDuration = UnityEngine.Random.Range(MinCoolDownDuration, MaxCoolDownDuration);
            }
        }
        else
        {
            FindTheClosestExcretion();
        }
    }

    public void FindTheClosestExcretion()
    {
        GameObject[] allTargets = GameObject.FindGameObjectsWithTag(targetTag);

        if (allTargets.Length == 0)
        {
            closestExcretion = null;
            return;
        }

        float shortestDistance = Mathf.Infinity;
        GameObject currentClosest = null;

        foreach (GameObject target in allTargets)
        {
            // Calculate the squared distance to avoid expensive square root operations
            // for comparison, as the relative order of distances remains the same.
            float distanceSqr = (target.transform.position - transform.position).sqrMagnitude;

            if (distanceSqr < shortestDistance)
            {
                shortestDistance = distanceSqr;
                currentClosest = target;
            }
        }

        closestExcretion = currentClosest;

        if (closestExcretion != null)
        {
            Vector3 newPosition = Vector3.MoveTowards(
                transform.position,
                closestExcretion.transform.position,
                speed * Time.deltaTime
            );

            transform.position = newPosition;
        }
    }

    public GameObject GetClosestTarget()
    {
        return closestExcretion;
    }

    private void OnCollisionEnter2D(Collision2D collisionObject)
    {
        Excretion excretion = collisionObject.gameObject.GetComponent<Excretion>();
        if (excretion == null) return;

        if (excretion is Excretion) UnityEngine.Object.Destroy(excretion.gameObject); ;

        energy += 5f;
        onCoolDown = true;
    }
}
