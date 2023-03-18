using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    float projectileLifeTime = 2.5f;
    [SerializeField] int projectileDamage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        projectileLifeTime -= Time.deltaTime;

        if (projectileLifeTime < 0)
        {
            ProjectilePooler.Instance.inactiveProjectiles.Add(gameObject);
            this.gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            PlayerStats.Instance.TakeDamage(projectileDamage);
            
        }

        ProjectilePooler.Instance.inactiveProjectiles.Add(gameObject);
        this.gameObject.SetActive(false);
    }
}
