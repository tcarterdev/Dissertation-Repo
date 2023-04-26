using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyStates {patrolling, chasing, attacking };
public class EnemyBrain : MonoBehaviour
{
    private Attack attack;
    private Patrol patrol;
    private Chase chase;
    [SerializeField] private float _awarenessRadius = 20f;
    public EnemyStates currentState;

    public Animator ninjaAnim;


    private void Start()
    {
        patrol = GetComponent<Patrol>();
        chase = GetComponent<Chase>();
        attack = GetComponent<Attack>();
    }

    private void Update()
    {
        UpdateCharacterState();
        //Perform Current State
        if (currentState == EnemyStates.patrolling)
        {
            ninjaAnim.SetBool("isPatrolling", true);
            ninjaAnim.SetBool("isChasing", false);
            ninjaAnim.SetBool("isAttacking", false);
            patrol.PerformPatrol();
        }
        else if (currentState == EnemyStates.chasing)
        {
            ninjaAnim.SetBool("isPatrolling", false);
            ninjaAnim.SetBool("isChasing", true);
            ninjaAnim.SetBool("isAttacking", false);
            chase.PerformChase();
        }
        else if (currentState == EnemyStates.attacking)
        {
            ninjaAnim.SetBool("isAttacking", true);
            ninjaAnim.SetBool("isPatrolling", false);
            ninjaAnim.SetBool("isChasing", false);
            attack.CanAttack();
            
            


        }

       

    }

    private void UpdateCharacterState()
    {
        //Checking Attack Type
        if (attack.CanAttack())
        {
            if (attack.attackType == AttackType.ranged)
            {
                attack.Ranged();
            }
            else
            {
                attack.Melee();
            }
        }


        float disToPlayer = Vector3.Distance(transform.position, PlayerStats.Instance.transform.position);
        

        if (disToPlayer < _awarenessRadius)
        {
            currentState = EnemyStates.chasing;

        }
        else
        {
            currentState = EnemyStates.patrolling;
        }

        if (disToPlayer < _awarenessRadius && attack.CanAttack())
        {
            currentState = EnemyStates.attacking;
        }

    }




    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _awarenessRadius);
    }
}
