using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectStats : MonoBehaviour
{
    [SerializeField] int objectHealth;
    [SerializeField] int objectSelfImpact;
    [SerializeField] int objectBreakForce;
    [SerializeField] public int objectDamage;
    public GameObject lightGameObject;
    public GameObject mediumGameObject;
    public GameObject heavyGameObject;
    public EnemyStats enemyStats;


    void Start()
    {
        //Set Object Health
        objectSelfImpact = 20;

        //set object force when broken
        objectBreakForce = 20;

        if (this.gameObject.tag == "Light")
        {
            objectDamage = 5;
            objectHealth = 25;
        };
        if (this.gameObject.tag == "Medium")
        {
            objectDamage = 15;
            objectHealth = 50;
        }
        if (this.gameObject.tag == "Heavy")
        {
            objectDamage = 25;
            objectHealth = 120;
        }

    }

    void Update()
    {
        

    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision != null && collision.gameObject.tag == "Wall")
        {
            objectHealth -= objectSelfImpact;

            if (objectHealth <= 0)
            {
                ObjectBreak();
                Destroy(this.gameObject);
            }
        }

        

    }

    private void ObjectBreak()
    {
        if (tag == "Medium")
        { 
           Rigidbody rb = Instantiate(lightGameObject, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(Vector3.up * objectBreakForce, ForceMode.Impulse);
        }




        if (tag == "Heavy")
        {
            Rigidbody rb = Instantiate(mediumGameObject, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(Vector3.up * objectBreakForce, ForceMode.Impulse);
        }

    }
}
