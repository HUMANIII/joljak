using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Field : MonoBehaviour
{
    public CardFieldPos[][] field = new CardFieldPos[3][];

    private void Awake()
    {
        for (int i = 0; i < 3; i++)
        {
            field[i] = new CardFieldPos[4];
            for (int j = 0; j < 4; j++)
            {
                field[i][j] = transform.GetChild(i).GetChild(j).GetComponent<CardFieldPos>();
            }
        }
    }

    public void SetField(CardBase card, CardFieldPos cardFieldPos)
    {
        cardFieldPos.SettedCard = card;
    }
}
