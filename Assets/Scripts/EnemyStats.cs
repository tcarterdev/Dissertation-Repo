using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{

    public int enemyHealth;
    public int enemyMaxHealth = 100;
    private ObjectStats objstats;

    public AudioSource deathsound;
    // Start is called before the first frame update

    private void Awake()
    {
        enemyHealth = enemyMaxHealth;
    }
    void Start()
    {
        ObjectStats stats = objstats.GetComponent<ObjectStats>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyHealth <= 0)
        {
            deathsound.Play();
            Destroy(this.gameObject);
            
        }

    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Light")
        {
            enemyHealth -= 10;
        }
        if (collision.gameObject.tag == "Medium")
        {
            enemyHealth -= 20;
        }
        if (collision.gameObject.tag == "Heavy")
        {
            enemyHealth -= 30;
        }
    }


}
