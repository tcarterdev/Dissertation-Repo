using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Chase : MonoBehaviour
{

    [SerializeField] float chaseSpeed;
    [SerializeField] NavMeshAgent agent;

    public void PerformChase()
    {
        agent.SetDestination(PlayerStats.Instance.transform.position);
    }
}
