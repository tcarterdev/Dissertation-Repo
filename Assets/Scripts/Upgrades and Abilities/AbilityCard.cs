using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/CardUpgrade", order = 1)]
public class AbilityCard : ScriptableObject
{
    public string cardName;
    public string cardType;
    public string cardDescription;
    public Image cardImage;

    [Header("Player Stats")]
    [SerializeField] PlayerStats playerStats;
}
