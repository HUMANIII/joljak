using System.Collections.Generic;

public class NormalSetTarget : ISetTarget
{
    public List<IDamagable> SetTarget(in CardGameManager cgm, in CardBase Attacker)
    {
        List<IDamagable> rtn = new();
        if(cgm.Field.HasCard(Attacker, out var pos))
        {
            switch(pos.x)
            {
                case 0:
                    if (cgm.Field.field[1][pos.y].SettedCard != null)
                        rtn.Add(cgm.Field.field[1][pos.y].SettedCard);
                    else
                        rtn.Add(cgm.Enemy);
                    break;
                case 1:
                    if (cgm.Field.field[0][pos.y].SettedCard != null)
                        rtn.Add(cgm.Field.field[0][pos.y].SettedCard);
                    else 
                        rtn.Add(cgm.Player);
                    break;
            }
        }
        return rtn;
    }
}
