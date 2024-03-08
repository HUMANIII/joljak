using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    public CardBase[][] field = new CardBase[3][];

    private void Awake()
    {
        for (int i = 0; i < 3; i++)
        {
            field[i] = new CardBase[4];
            for (int j = 0; j < 4; j++)
            {
                field[i][j] = null;
            }
        }
    }

    public void SetField(CardBase card, int x, int y)
    {
        field[x][y] = card;
    }
}
