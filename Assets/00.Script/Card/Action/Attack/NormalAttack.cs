using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class NormalAttack : MonoBehaviour, IAttack
{
    private CardBase cardBase;

    private void Awake()
    {
        cardBase = GetComponent<CardBase>();
    }

    public void Attack(IDamagable damagable)
    {
        damagable.Damaged(cardBase.CardDamage);
    }
}