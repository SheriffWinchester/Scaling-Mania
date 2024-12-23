using UnityEngine;
using UnityEngine.AI;

public class RandomWalk : MonoBehaviour
{
    public float walkRadius = 10f; // Radius within which the character will walk
    public float waitTime = 2f; // Time to wait at each destination before moving to the next

    private NavMeshAgent agent;
    private float timer;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        timer = waitTime;
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= waitTime)
        {
            Vector3 newPos = RandomNavSphere(transform.position, walkRadius, -1);
            agent.SetDestination(newPos);
            timer = 0;
        }
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;
        randDirection += origin;
        randDirection.y = origin.y; // Keep the Y position constant
        NavMeshHit navHit;
        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);
        return navHit.position;
    }
}