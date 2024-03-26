using System.Collections.Generic;

public interface ISetTarget
{
    public List<IDamagable> SetTarget(in CardGameManager cgm, in CardBase Attacker);
}
