using UnityEngine;

public abstract class BaseLife : MonoBehaviour
{
    [SerializeField] public float speed = 1f;
    public Vector2 direction;

    bool ReachedBoundary()
    {
        return transform.position.x <= -GlobalVariables.SCREENBOUNDS.x ||
               transform.position.x >= GlobalVariables.SCREENBOUNDS.x ||
               transform.position.y <= -GlobalVariables.SCREENBOUNDS.y ||
               transform.position.y >= GlobalVariables.SCREENBOUNDS.y;
    }

    public void BaseLifeUpdate()
    {
        if (ReachedBoundary())
        {
            direction = UnityEngine.Random.insideUnitCircle.normalized;
            GetComponent<Rigidbody2D>().linearVelocity = direction * speed;
        }
    }
}
