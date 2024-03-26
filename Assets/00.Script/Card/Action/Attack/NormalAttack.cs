using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class NormalAttack : IAttack
{
    public void Attack(in List<IDamagable> targets, in CardBase Attacker)
    {
        foreach(var target in targets)
        {
            target.Damaged(Attacker.CardDamage);
        }
    }
}