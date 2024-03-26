using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public List<CardBase> cardList;

    private void Start()
    {
        SetCard();
    }


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            //cardList[0].Attack(cardList[1]);
        }
    }

    public void SetCard()
    {
        foreach (var card in cardList)
        {
            //card.SetCard(999);
        }
    }
}
