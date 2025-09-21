using System.Drawing;
using UnityEngine;
using UnityEngine.UIElements;

public class Coccus : BaseLife
{
    [SerializeField] public float energyToReproduce = 120f;
    public float energy = 100f;

    [SerializeField] private float minSpeed = 0f;
    [SerializeField] private float maxSpeed = 4f;

    [SerializeField] private float minScale = 0.2f;
    [SerializeField] private float maxScale = 0.6f;
    private float scale = 1f;
    private Vector3 newScale = new Vector3(2f, 2f, 2f);

    public void CoccusStart()
    {
        direction = UnityEngine.Random.insideUnitCircle.normalized;
        speed = UnityEngine.Random.Range(minSpeed, maxSpeed);
        GetComponent<Rigidbody2D>().linearVelocity = direction * speed;

        scale = UnityEngine.Random.Range(minScale, maxScale);
        newScale = new Vector3(scale, scale, 1f);
        transform.localScale = newScale;
    }

    public void CoccusUpdate()
    {
        BaseLifeUpdate();

        //Checks if should die due to lack of energy
        if (energy <= 0) UnityEngine.Object.Destroy(this.gameObject);
    }
}
