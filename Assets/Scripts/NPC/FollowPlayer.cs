using UnityEngine;
using UnityEngine.AI;

public class FollowPlayer : MonoBehaviour
{
    public Transform npcStand; 
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if(npcStand.gameObject.activeInHierarchy)
        {
            if (Vector3.Distance(npcStand.position, transform.position) > 2f)
            {
                agent.SetDestination(npcStand.position);
            }
        }
    }
}
