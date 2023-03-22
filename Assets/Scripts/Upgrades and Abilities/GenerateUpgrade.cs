using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class GenerateUpgrade : MonoBehaviour
{
    [SerializeField] List<AbilityCard> upgradeCards;
    [SerializeField] PlayerInput playerInput;
    [SerializeField] TMP_Text upgradeTextUI;

    public void PickUpgrade()
    {
       int randomNumber = Random.Range(0, upgradeCards.Count);

       upgradeTextUI.SetText("You Got: " + upgradeCards[randomNumber].cardName);

       

    }



}
