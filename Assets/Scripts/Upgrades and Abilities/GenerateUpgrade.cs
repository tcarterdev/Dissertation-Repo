using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class GenerateUpgrade : MonoBehaviour
{
    [SerializeField] List<AbilityCard> upgradeCards;

    public AbilityCard PickUpgrade()
    {
       int randomNumber = Random.Range(0, upgradeCards.Count);
        AbilityCard newAbility = upgradeCards[randomNumber];   
        return newAbility;
    }

    public void RemoveUpgrade(AbilityCard abilityToRemove)
    { 
    
        upgradeCards.Remove(abilityToRemove);

    }
  



}
