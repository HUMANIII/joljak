using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardBase : MonoBehaviour
{
    public int CardID;
    [HideInInspector]
    public int CardCost;
    [HideInInspector]
    public int CardAttack { get { return CardBaseAtk + CardAddAtk; } }
    [HideInInspector]
    private int CardBaseAtk;
    private int CardAddAtk;
    private int CardInitHP;
    [HideInInspector]
    public int CardCurHP;
    [HideInInspector]
    public CardOption CardOption;

    [SerializeField]
    private TextMeshPro cardName;
    [SerializeField]
    private TextMeshPro cardAtk;
    [SerializeField]
    private TextMeshPro cardHp;

    private void Init()
    {
        CardCurHP = CardInitHP;
        CardAddAtk = 0;
    }

    public void SetCard(int id)
    {        
        CardID = id;
        var data = DataTableMgr.GetTable<CardTable>().dic[id];
        CardCost = data.Cost;
        CardBaseAtk = data.Attack;
        CardInitHP = data.HP;
        CardOption = (CardOption)data.Option;
        Init();
        cardName.text = data.Name;
        cardAtk.text = CardAttack.ToString();
        cardHp.text = CardCurHP.ToString();
    }
}
