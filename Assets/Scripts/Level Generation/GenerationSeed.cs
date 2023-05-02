using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GenerationSeed : MonoBehaviour
{
    [Header("Seed Settings")]
    public bool randomSeed;
    public int seed;

    [SerializeField] TMP_Text seedUI;

    

    public void GenerateRandomSeed()
    {
        if (randomSeed) 
        {
            seed = Random.Range(-100000, 100000);
            seedUI.SetText("Seed: " + seed);

        }
        
    }

    public void SetSeed(int seed) {
        Random.InitState(seed); 
    }
}
