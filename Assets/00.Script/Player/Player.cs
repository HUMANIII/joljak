using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour, IDamagable
{
    private  CardGameManager cgm;
    public int DamagedAmount { get; private set; }

    [SerializeField] 
    private readonly float handPosGap = 2;
    [SerializeField] 
    private readonly float handRotGap = 0.1f;

    public readonly LinkedList<CardBase> Prey = new();
    public CardBase SummonObj;
    public bool CanSummon { get; set; } = false;

    private readonly LinkedList<CardBase> hand = new();
    private readonly LinkedList<CardBase> BaseDeck = new();
    private readonly LinkedList<CardBase> CurDeck = new();

    private GameObject curClickedGameObject;
    public GameObject PrevClickedGameObject { get; private set;}
    public GameObject CurClickedGameObject 
    {
        get { return curClickedGameObject; }
        set
        {
            if(value.TryGetComponent<ClickableGameObject>(out var GO))
            {
                PrevClickedGameObject = curClickedGameObject;
                CurClickedType = GO.OnClickSuccessed();
                PrevClickedType = CurClickedType;
                curClickedGameObject = value;
                Debug.Log("CurClickedType : " + CurClickedType);
            }
        }
    }
    public ClickableType CurClickedType { get; private set; }
    public ClickableType PrevClickedType { get; private set; }
    

    private void Awake()
    {
        cgm = GetComponent<CardGameManager>();

        CurClickedType = ClickableType.None;
        PrevClickedType = ClickableType.None;
        Init();
    }

    public void Init()
    {
        DamagedAmount = 0;
        SetBaseDeck();
        SetDeck();
        hand.Clear();
    }

    public void SetBaseDeck()
    {
        //기본 덱 설정
        BaseDeck.Clear();
        AddCard(1);
        AddCard(1);
        AddCard(1);
        AddCard(2);
        AddCard(3);
        AddCard(3);
        AddCard(3);
        AddCard(4);
    }

    public void AddCard(int ID)
    {
        var card = cgm.ObjectPool.GetObject(PoolType.Card).GetComponent<CardBase>();
        card.SetCard(ID);
        BaseDeck.AddLast(card);
    }

    public void Damaged(int amount)
    {
        DamagedAmount += amount;
    }

    public void SetDeck()
    {        
        CurDeck.Clear();
        foreach (var card in BaseDeck)
        {
            card.transform.SetParent(cgm.deckPos);
            card.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            CurDeck.AddLast(card);
            card.CardState = CardState.Deck;
        }
        ShuffleDeck();
    }

    public void AddToDeck(CardBase card)
    {
        card.transform.SetParent(cgm.deckPos);
        card.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
        card.CardState = CardState.Deck;
        CurDeck.AddLast(card);
        ShuffleDeck();
    }

    public void ShuffleDeck()
    {
        var temp = CurDeck.ToList();
        CurDeck.Clear();
        while(temp.Count > 0)
        {
            var count = UnityEngine.Random.Range(0, temp.Count - 1);
            CurDeck.AddLast(temp[count]);
            temp.RemoveAt(count);
        }
    }

    public void DrawCard()
    {
        if(CurDeck.Count == 0)
        {
            Debug.Log("Deck is Empty");
            return;
        }
        var card = CurDeck.First.Value;
        hand.AddLast(card);
        CurDeck.Remove(card);
        card.gameObject.layer = Layers.Player;
        card.transform.SetParent(cgm.handPos);
        card.SetCardOpen();
        card.CardState = CardState.Hand;
        SetHandCardPos();
    }

    public void DrawPreyCard()
    {
        var card = cgm.ObjectPool.GetObject(PoolType.Card);
        var cb = card.GetComponent<CardBase>();
        cb.SetCard(999);
        cb.SetCardOpen();
        cb.CardState = CardState.Hand;
        hand.AddLast(cb);
        card.layer = Layers.Player;        
        card.transform.SetParent(cgm.handPos);                
        SetHandCardPos();
    }

    public void SetCard(CardFieldPos cardFieldPos)
    {
        if(PrevClickedType != ClickableType.Card)
            return;

        //SummonObj
        //var cb = PrevClickedGameObject.GetComponent<CardBase>();
        SummonObj.SetStackOrder(0);
        if(hand.Contains(SummonObj))
        {
            hand.Remove(SummonObj);
        }
        cgm.Field.SetField(SummonObj, cardFieldPos);
        SummonObj.CardState = CardState.Field;
        SetHandCardPos();
    }

    private void SetHandCardPos()
    {
        int count = 0;
        var leftSide = -hand.Count / 2 * handPosGap;
        var camPos = Camera.main.transform.position;
        camPos.y += 10;
        foreach (var card in hand)
        {
            card.transform.localPosition = Vector3.right * (leftSide + (count * handPosGap));
            var rot = card.transform.position - camPos;
            rot.x = (leftSide + (count * handPosGap)) * handRotGap;
            card.transform.rotation = Quaternion.LookRotation(rot);

            card.transform.SetLocalPositionAndRotation(Vector3.right * (leftSide + (count * handPosGap)), Quaternion.LookRotation(rot));

            card.SetStackOrder(count + 1);
            count++;
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.D))
        {
            DrawCard();
        }
    }
}
