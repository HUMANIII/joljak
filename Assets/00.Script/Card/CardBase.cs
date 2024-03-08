using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.SceneManagement;
using System.Reflection;

public class CardBase : ClickableGameObject, IDamagable, IPoolable
{
    public int CardID;
    [HideInInspector]
    public int CardCost;
    [HideInInspector]
    public int CardDamage { get { return CardBaseDamage + CardAddDamage; } }

    public int StackCount { get; set; }

    private readonly PoolType poolType = PoolType.Card;
    public PoolType PoolType { get => poolType; }

    [HideInInspector]
    protected int CardBaseDamage;
    protected int CardAddDamage;
    protected int CardInitHP;
    [HideInInspector]
    public int CardCurHP;
    [HideInInspector]
    public CardOption CardOption;

    [SerializeField]
    protected SpriteRenderer Background;
    [SerializeField]
    protected SpriteRenderer Illust;
    [SerializeField]
    protected SpriteRenderer Option;
    [SerializeField]
    protected TextMeshPro cardName;
    [SerializeField]
    protected TextMeshPro cardAtk;
    [SerializeField]
    protected TextMeshPro cardHp;

    protected IAttack attack;
    protected IDie die;
    [HideInInspector]
    public CardState CardState { get; private set; }

    protected virtual void Init()
    {
        CardCurHP = CardInitHP;
        CardAddDamage = 0;
        SetStackOrder(0);
    }

    public virtual void SetCard(int id)
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

    protected void SetOption()
    {
        attack ??= gameObject.AddComponent<NormalAttack>();
        die ??= gameObject.AddComponent<NormalDie>();        
    }

    public void SetStackOrder(int count)
    {
        StackCount = count;
        Background.sortingOrder = StackCount * 10 + 0;
        Illust.sortingOrder = StackCount * 10 + 1;
        Option.sortingOrder = StackCount * 10 + 2;
        cardAtk.sortingOrder = StackCount * 10 + 3;
        cardHp.sortingOrder = StackCount * 10 + 4;
        cardName.sortingOrder = StackCount * 10 + 5;
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

    public override ClickableType OnClickSuccessed()
    {
        Debug.Log("CardBase Clicked");        
        return ClickableType.Card;
    }
}
