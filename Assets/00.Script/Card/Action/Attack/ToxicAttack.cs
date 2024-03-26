using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToxicAttack : IAttack
{
    public void Attack(in List<IDamagable> targets, in CardBase Attacker)
    {
        foreach (var target in targets)
        {
            target.Damaged(int.MaxValue);
        }
        Debug.Log("ToxicAttack");
    }
}