using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.SceneManagement;

public class CardBase : MonoBehaviour, IDamagable
{
    public int CardID;
    [HideInInspector]
    public int CardCost;
    [HideInInspector]
    public int CardDamage { get { return CardBaseDamage + CardAddDamage; } }
    [HideInInspector]
    private int CardBaseDamage;
    private int CardAddDamage;
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

    private IAttack attack;
    private IDie die;

    private void Init()
    {
        CardCurHP = CardInitHP;
        CardAddDamage = 0;
    }

    public void SetCard(int id)
    {        
        CardID = id;
        var data = DataTableMgr.GetTable<CardTable>().dic[id];
        CardCost = data.Cost;
        CardBaseDamage = data.Attack;
        CardInitHP = data.HP;
        CardOption = (CardOption)data.Option;
        Init();
        cardName.text = data.Name;
        cardAtk.text = CardDamage.ToString();
        cardHp.text = CardCurHP.ToString();
        SetOption();
    }

    private void SetOption()
    {
        attack ??= gameObject.AddComponent<NormalAttack>();
        die ??= gameObject.AddComponent<NormalDie>();        
    }

    public void Attack(IDamagable damagable)
    {
        attack.Attack(damagable);
    }

    public void Damaged(int amount)
    {
        CardCurHP -= amount;
        cardHp.text = CardCurHP.ToString();

        if(CardCurHP <= 0)
        {
            die.Die();
        }
    }
}
