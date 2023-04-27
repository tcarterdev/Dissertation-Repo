using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public enum AttackType { melee, ranged };
public class Attack : MonoBehaviour
{
    public float attackRange;
    public float attackCoolDown;
    public bool iscoolingDown;
    public float cooldownDuration;
    public AttackType attackType;
    public PlayerStats playerStats;
    [Header("Melee Attack")]
    public GameObject meleeParticle;
    public Transform attackpoint;
    private int meleeDamage = 20;
    [Header("Ranged Attack")]
    public GameObject projectilePrefab;
    public float projectileForce;
    public Transform projectilesSpawnPoint;

    
    public bool CanAttack()
    {
        if (iscoolingDown == true)
        {
            return false;
        }
        if (Vector3.Distance(transform.position, PlayerStats.Instance.transform.position) < attackRange)
        {
            return true;
        }
        else
        {
            return false;
        }
            
    }
    public void Ranged()
    {
        GameObject newObject;
        if (ProjectilePooler.Instance.inactiveProjectiles.Count > 0)
        {
            
            newObject = ProjectilePooler.Instance.RemoveFromPool();
        }
        else
        {
            newObject = Instantiate(projectilePrefab, projectilesSpawnPoint.position, projectilePrefab.transform.rotation);
        }

        Rigidbody rb = newObject.GetComponent<Rigidbody>();

        rb.AddForce(this.transform.forward * projectileForce, ForceMode.Impulse);

        StartCoroutine(AttackCoolDown(cooldownDuration));

    }

    public void Melee()
    {
       
    }

    IEnumerator AttackCoolDown(float cooldownDuration)
    {
        iscoolingDown = true;
        yield return new WaitForSeconds(cooldownDuration);
        iscoolingDown = false;
    }
    IEnumerator MeleeAttackCoolDown(float cooldownDuration)
    {
        iscoolingDown = true;
        yield return new WaitForSeconds(cooldownDuration);
        iscoolingDown = false;
    }

}
