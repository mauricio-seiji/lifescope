using UnityEngine;
using UnityEngine.EventSystems;

public class PinkCoccus : Coccus, IPointerClickHandler
{
    private bool onCoolDown = false;
    private int CoolDownCounter = 0;
    private int CoolDownDuration = 0;
    [SerializeField] private int MinCoolDownDuration = 1000;
    [SerializeField] private int MaxCoolDownDuration = 2000;

    [SerializeField] private float minEnergyDecayRate = 1f;
    [SerializeField] private float maxEnergyDecayRate = 2f;
    private float energyDecayRate = 1f;

    [SerializeField] private PinkCoccus pinkCoccusPrefab;

    void Awake()
    {
        CoccusAwake();
    }
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

        MoveAfterCoolDown();

        //White loses a little energy each update (starving)
        energy -= energyDecayRate * Time.deltaTime;

        if (energy >= energyToReproduce)
        {
            energy = 100;
            CreateCoccus(pinkCoccusPrefab.gameObject, new Vector3(transform.position.x + 1f, transform.position.y + 1f, 0));
        }

    }
    public void MoveAfterCoolDown()
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
            IMoveTowards moveTowards = new MoveTowards();
            Vector3 newPosition = moveTowards.MoveTowardsClosestTarget("Excretion", transform.position, speed);
            if (newPosition != Vector3.zero)
                transform.position = newPosition;
        }
            
    }
    private void OnCollisionEnter2D(Collision2D collisionObject)
    {
        Excretion excretion = collisionObject.gameObject.GetComponent<Excretion>();
        if (excretion == null) return;

        if (excretion is Excretion) UnityEngine.Object.Destroy(excretion.gameObject); ;

        energy += 5f;
        onCoolDown = true;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        CreateCoccus(pinkCoccusPrefab.gameObject, new Vector3(transform.position.x + 0.5f, transform.position.y + 0.5f, 0));
    }
}
