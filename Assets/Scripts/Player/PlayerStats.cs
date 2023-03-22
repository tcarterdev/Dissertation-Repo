using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    public Movement movement;

    public int currentPlayerHealth;

    public int maxPlayerHealth = 100;

    public TMP_Text playerHealthText;

    public int currentChiPoints;
    public int maxChiPoints = 100;

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







}
