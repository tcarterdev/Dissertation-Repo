using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerationSeed : MonoBehaviour
{
    [Header("Seed Settings")]
    public bool randomSeed;
    public int seed;

    public void GenerateRandomSeed()
    {
        if (randomSeed) 
        {
            seed = Random.Range(-100000, 100000); 
            
        }
        
    }

    public void SetSeed(int seed) {
        Random.InitState(seed); 
    }
}
