using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
public class EnemyMovement : MonoBehaviour
{
    private UnityEngine.AI.NavMeshAgent agent;
    private Transform target;

    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
        StartCoroutine(UpdatePath());
    }

    IEnumerator UpdatePath()
    {
        while (target != null)
        {
            agent.SetDestination(target.position);
            Debug.Log("Moving towards target: " + target.name);
            yield return new WaitForSeconds(0.5f); // Adjust update frequency as needed
        }
    }
}
