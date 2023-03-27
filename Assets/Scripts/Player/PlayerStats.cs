using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEditorInternal;
using JetBrains.Annotations;
using System;

public class PlayerStats : MonoBehaviour
{
    public Movement movement;
  

    [Header("Player Statistics")]
    public int currentPlayerHealth;

    public int maxPlayerHealth = 100;
    public int currentChiPoints;
    public int maxChiPoints = 100;

  

    [Header("Upgrades")]
    GenerateUpgrade generateUpgrade;
    public List<AbilityCard> abilities;

    [Header("UI Elements")]
    public TMP_Text playerHealthText;
    public TMP_Text chiPointsText;

    public TMP_Text dashChargesText;

    private static PlayerStats _instance;
    public static PlayerStats Instance { get { return _instance; } }

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

    private void Start()
    {
        

        currentPlayerHealth = maxPlayerHealth;
        currentChiPoints = maxChiPoints;

        playerHealthText.SetText("Health: " + currentPlayerHealth);
        chiPointsText.SetText("Chi: " + currentChiPoints);

        dashChargesText.SetText("Dash: " + movement.dashCharge);

    }

    public void TakeDamage(int damageAmount)
    {
        currentPlayerHealth -= damageAmount;
        playerHealthText.SetText("Health: " + currentPlayerHealth);

        if (currentPlayerHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void AddNewAbilityToInv(AbilityCard newAbility)
    {

        abilities.Add(newAbility);  
          

    }

    //PLAYER ACTIVE ABILITIES







}
