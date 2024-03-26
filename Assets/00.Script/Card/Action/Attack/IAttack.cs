using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttack
{
    public void Attack(in List<IDamagable> targets, in CardBase Attacker);
}
