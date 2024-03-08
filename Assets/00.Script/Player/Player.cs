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
        var card = cgm.ObjectPool.GetObject(PoolType.Card);
        card.layer = Layers.Player;
        var cb = card.GetComponent<CardBase>();
        BaseDeck.AddLast(cb);
        cb.SetCard(ID);
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
        card.transform.SetParent(cgm.handPos);
        SetHandCardPos();
    }
    public void SetCard(CardFieldPos cardFieldPos)
    {
        if(PrevClickedType != ClickableType.Card)
            return;

        PrevClickedGameObject.transform.parent = cardFieldPos.transform;
        PrevClickedGameObject.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
        PrevClickedGameObject.transform.localScale = Vector3.one;
        var cb = PrevClickedGameObject.GetComponent<CardBase>();
        cb.SetStackOrder(0);
        if(hand.Contains(cb))
        {
            hand.Remove(cb);
        }

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
