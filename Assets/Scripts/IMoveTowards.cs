using UnityEngine;

public interface IMoveTowards
{
    public Vector3 MoveTowardsClosestTarget(string targetTag, Vector3 currentPosition, float speed);
}

public class MoveTowards : IMoveTowards
{
    public Vector3 MoveTowardsClosestTarget(string targetTag, Vector3 currentPosition, float speed)
    {
        GameObject closestTarget;
        GameObject[] allTargets = GameObject.FindGameObjectsWithTag(targetTag);

        if (allTargets.Length == 0)
            return Vector3.zero;

        float shortestDistance = Mathf.Infinity;
        GameObject currentClosest = null;

        foreach (GameObject target in allTargets)
        {
            // Calculate the squared distance to avoid expensive square root operations
            // for comparison, as the relative order of distances remains the same.
            float distanceSqr = (target.transform.position - currentPosition).sqrMagnitude;

            if (distanceSqr < shortestDistance)
            {
                shortestDistance = distanceSqr;
                currentClosest = target;
            }
        }

        closestTarget = currentClosest;

        if (closestTarget != null)
        {
            Vector3 newPosition = Vector3.MoveTowards(
                currentPosition,
                closestTarget.transform.position,
                speed * Time.deltaTime
            );

            return newPosition;
        }
        return Vector3.zero;
    }
}
