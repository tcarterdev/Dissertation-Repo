using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityCard : ScriptableObject
{
    public string cardName;
    public CardType cardType;
    public string cardDescription;
    public Sprite cardImage;
    public int statModifier;
}

    public enum CardType {passive, active };


