using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    private CardGameManager cgm;

    [SerializeField]
    private bool isPreyDeck;

    private void Awake()
    {
        cgm = GameObject.FindWithTag(Tags.Managers).GetComponent<CardGameManager>();
    }

    private void OnMouseDown()
    {
        if(isPreyDeck)
        {
            cgm.Player.DrawPreyCard();
        }
        else
        {
            cgm.Player.DrawCard();
        }
    }
}
