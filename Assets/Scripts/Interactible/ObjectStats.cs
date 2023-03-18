using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectStats : MonoBehaviour
{
    [SerializeField] int objectHealth;
    [SerializeField] int objectSelfImpact;
    public GameObject lightGameObject;
    public GameObject mediumGameObject;
    public GameObject heavyGameObject;



    void Start()
    {

        objectSelfImpact = 20;

        if (this.gameObject.tag == "Light")
        {
            objectHealth = 25;
        }
        if (this.gameObject.tag == "Medium")
        {
            objectHealth = 50;
        }
        if (this.gameObject.tag == "Heavy")
        {
            objectHealth = 120;
        }

    }

    void Update()
    {
        if (objectHealth < 0 && this.gameObject.tag == "Light")
        { 
            Destroy(gameObject);

        }

        if (objectHealth < 0 && this.gameObject.tag == "Medium")
        {
            Instantiate(lightGameObject);
            Destroy(gameObject);
        }

        if (objectHealth < 0 && this.gameObject.tag == "Heavy")
        {
            Instantiate(mediumGameObject);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision != null && collision.gameObject.tag == "Wall")
        {
            objectHealth -= objectSelfImpact;
        }
    }
}
