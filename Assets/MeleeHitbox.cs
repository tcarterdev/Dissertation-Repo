using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeHitbox : MonoBehaviour
{
    private Attack attack;
    private PlayerStats pstats;

    private void Start()
    {
        attack = GetComponent<Attack>();
        pstats = GetComponent<PlayerStats>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            pstats.currentPlayerHealth -= 20;
        }
        
    }


}
