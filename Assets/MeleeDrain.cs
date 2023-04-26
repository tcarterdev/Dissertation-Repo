using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeDrain : MonoBehaviour
{
    public int meleeAttack;
    public PlayerStats playerStats;


    public void Start()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        meleeAttack -= playerStats.currentPlayerHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
