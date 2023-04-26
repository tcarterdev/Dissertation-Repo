using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePooler : MonoBehaviour
{
    public List<GameObject> inactiveProjectiles;

    public GameObject RemoveFromPool()
    {
        if (inactiveProjectiles.Count == 0) return null;
        Debug.Log("object removed from pool");
        GameObject objectToRemove = inactiveProjectiles[inactiveProjectiles.Count - 1];

        inactiveProjectiles.RemoveAt(inactiveProjectiles.Count - 1);

        return objectToRemove;

    }

    //Projectile Pooler Singleton

    private static ProjectilePooler _instance;
    public static ProjectilePooler Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
}
