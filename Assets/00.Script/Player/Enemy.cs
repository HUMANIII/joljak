using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable
{
    private CardGameManager cgm;
    public int DamagedAmount { get; private set; }

    private void Awake()
    {
        cgm = GetComponent<CardGameManager>();
    }

    public void Damaged(int amount)
    {
        DamagedAmount += amount;
    }

    public void SetCard()
    {

    }
}
