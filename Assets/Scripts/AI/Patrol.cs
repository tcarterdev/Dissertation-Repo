using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Patrol : MonoBehaviour
{
   public Transform[] waypoints;
    public Transform currentWaypoint;
   [SerializeField] private int _currentWaypointIndex = 0;
   [SerializeField] private float patrolSpeed = 2f;
 
   [SerializeField] private float _waitTime = 1f;
   [SerializeField]private float _waitCounter = 0f;
   private bool _waiting = false;
    NavMeshAgent _agent;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        SetNewWaypoint();

    }
    public void PerformPatrol()
    {


        if (Vector3.Distance(transform.position, currentWaypoint.position) < _agent.stoppingDistance)
        {
            float waitTime = Random.Range(0f, 3f);
            StartCoroutine(WaitCoroutine(waitTime));
        }
        
    }

    public void SetNewWaypoint()
    {
        currentWaypoint = waypoints[Random.Range(0, waypoints.Length)];
        if (Vector3.Distance(currentWaypoint.position, transform.position) < 2.5f)
        {
            SetNewWaypoint();
        }
        else
        {
        
        _agent.SetDestination(currentWaypoint.position);
        }


       
    }

    IEnumerator WaitCoroutine(float waitTime)
    {

        yield return new WaitForSeconds(waitTime);
        SetNewWaypoint();

    }

}