using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform target;
    private float stoppingDistance = 1.5f; // Distance at which the enemy stops

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (agent == null)
        {
            Debug.LogError("NavMeshAgent component not found on " + gameObject.name);
        }
    }

    public void SetTarget(Transform newTarget)
    {
        if (newTarget != null && newTarget != this.transform) // Ensure the target is not itself
        {
            target = newTarget;
            //Debug.Log("Target set to " + target.name);
            StartCoroutine(UpdatePath());
        }
    }

    IEnumerator UpdatePath()
    {
        while (target != null)
        {
            float distanceToTarget = Vector3.Distance(transform.position, target.position);

            if (distanceToTarget <= stoppingDistance)
            {
                agent.isStopped = true;
                //Debug.Log("Enemy stopped within 1.4 meters of the target.");
            }
            else
            {
                agent.isStopped = false;
                agent.SetDestination(target.position);
                //Debug.Log("Moving towards target: " + target.name + " at position " + target.position);
            }

            yield return new WaitForSeconds(0.5f); // Adjust update frequency as needed
        }
    }
}
