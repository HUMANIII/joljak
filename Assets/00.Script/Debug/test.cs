using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public List<CardBase> cardList;

    private void Start()
    {
        GameManager.Instance.test();
        SetCard();
    }

    public void SetCard()
    {
        foreach (var card in cardList)
        {
            card.SetCard(999);
        }
    }
}
