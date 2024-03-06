using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardBase : MonoBehaviour
{
    public int CardID;
    public int CardCost;
    public int CardAttack;
    public int CardHealth;
    public CardOption CardOption;

    private TextMeshProUGUI cardName;

    public void Awake()
    {
        if(cardName == null)
        {
            cardName = GetComponent<TextMeshProUGUI>();
        }
    }

    public void SetCard(int id)
    {
        CardID = id;
        CardCost = 1;
        CardAttack = 1;
        CardHealth = 1;
        CardOption = CardOption.None;
        cardName.text = "Card " + id;
    }
}
