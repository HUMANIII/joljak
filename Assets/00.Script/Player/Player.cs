using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour, IDamagable
{
    private  CardGameManager cardGameManager;
    public int DamagedAmount { get; private set; }

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

    private Transform handPos;

    private void Awake()
    {
        cardGameManager = GetComponent<CardGameManager>();

        CurClickedType = ClickableType.None;
        PrevClickedType = ClickableType.None;
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
        BaseDeck.Clear();
        //BaseDeck.AddFirst(DataTableMgr.GetTable<CardTable>().dic[1].);
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
            CurDeck.AddLast(card);
        }
    }

    public void DrawCard()
    {
        if(CurDeck.Count == 0)
        {
            Debug.Log("Deck is Empty");
            return;
        }
        var count = Random.Range(0, CurDeck.Count - 1);
        var card = CurDeck.ElementAt(count);
        hand.AddLast(card);
        CurDeck.Remove(card);
        card.transform.parent = handPos;
        SetHandCardPos();
    }
    public void SetCardPos(CardFieldPos cardFieldPos)
    {
        if(PrevClickedType != ClickableType.Card)
            return;

        PrevClickedGameObject.transform.parent = cardFieldPos.transform;
        PrevClickedGameObject.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
        PrevClickedGameObject.transform.localScale = Vector3.one;

        SetHandCardPos();
    }

    private void SetHandCardPos()
    {
        int count = 0;
        foreach (var card in hand)
        {
            card.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            card.transform.localScale = Vector3.one;
            card.SetStackOrder(count);
            count++;
        }
    }
}
