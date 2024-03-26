using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.SceneManagement;
using System.Reflection;

public class CardBase : ClickableGameObject, IDamagable, IPoolable
{
    public int CardID;
    public int CardCost { get; protected set; }
    public int CardDamage { get { return CardBaseDamage + CardAddDamage; } }

    public int StackCount { get; set; }

    private readonly PoolType poolType = PoolType.Card;
    public PoolType PoolType { get => poolType; }
    public bool CanUse { get; protected set; } = true;

    [HideInInspector]
    protected int CardBaseDamage;
    protected int CardAddDamage;
    protected int CardInitHP;
    [HideInInspector]
    public int CardCurHP;
    [HideInInspector]
    public CardTags CardTags;

    [SerializeField]
    protected GameObject BackSide;
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
    [SerializeField]
    protected TextMeshPro cardCost;

    protected IAttack attack;
    protected IDie die;
    protected ISetTarget setTarget;
    public CardState CardState { get; set; }
    public bool CanSummon { get; set; }
    protected bool isOpened = false;

    protected virtual void ResetCard()
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
        CardTags = (CardTags)data.Option;
        ResetCard();
        cardName.text = data.Name;
        cardAtk.text = CardDamage.ToString();
        cardHp.text = CardCurHP.ToString();
        cardCost.text = CardCost.ToString();
        SetTags();
    }

    public void OnReturn()
    {
        CardID = default;
        CardCost = default;
        CardBaseDamage = default;
        CardInitHP = default;
        CardTags = CardTags.None;
        cardName.text = string.Empty;
        cardAtk.text = CardDamage.ToString();
        cardHp.text = CardCurHP.ToString();
        cardCost.text = CardCost.ToString();
        attack = default;        
        setTarget = default;
        DestroyImmediate(die as MonoBehaviour);
    }

    protected void SetTags()
    {
        switch(CardTags)
        {
            case CardTags.Toxic:
                attack = new ToxicAttack();
                break;
        }
        attack ??= new NormalAttack();
        setTarget ??= new NormalSetTarget();
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

    public void Attack()
    {
        var damagable = setTarget.SetTarget(cgm, this);
        attack.Attack(damagable, this);
        Debug.Log(cardName.text + "Attacked");
    }

    public void Damaged(int amount)
    {
        CardCurHP = Mathf.Max(0, CardCurHP - amount);
        cardHp.text = CardCurHP.ToString();

        if(CardCurHP <= 0)
        {
            die.Die();
        }
    }

    public override ClickableType OnClickSuccessed()
    {
        Debug.Log("CardBase Clicked");        
        switch (CardState)
        {
            case CardState.Deck:
                cgm.Player.DrawCard();
                break;
            case CardState.Hand:
                cgm.Player.SummonObj = this;
                cgm.Player.Prey.Clear();
                CheckCanSummon();
                break;
            case CardState.Field:
                if(cgm.Player.SummonObj != null)
                {
                    cgm.Player.Prey.AddLast(this);
                    CheckCanSummon();
                }
                break;
        }
        return ClickableType.Card;
    }

    public void SetCardOpen()
    {
        BackSide.SetActive(false);
    }

    public void SetCardClose() 
    {
        BackSide.SetActive(true);
    }

    private void CheckCanSummon()
    {
        cgm.Player.CanSummon = cgm.Player.SummonObj.CardCost <= cgm.Player.Prey.Count;
        if(cgm.Player.CanSummon)
        {
            foreach (var card in cgm.Player.Prey)
            {
                cgm.ObjectPool.ReturnObject(card.gameObject);
                cgm.Field.RemoveField(card);
            }
        }
    }

}
