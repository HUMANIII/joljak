using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardGameManager : MonoBehaviour
{
    public ObjectPool ObjectPool { get; private set; }
    public Player Player { get; private set; }
    public Enemy Enemy { get; private set; }
    public Field Field { get; private set; }
        
    public Transform handPos;
    public Transform deckPos;

    //test region
    private int EnemyInfo;
    private int EnemyCurWaveInfo;

    private void Awake()
    {
        ObjectPool = GameObject.FindWithTag(Tags.ObjectPool).GetComponent<ObjectPool>();
        Player = gameObject.AddComponent<Player>();
        Enemy = gameObject.AddComponent<Enemy>();
        Field = GameObject.FindWithTag(Tags.Field).GetComponent<Field>();
    }

    public void TurnEnd()
    {
        Debug.Log("TurnEnd");
        PlayerCardsAttack();
        SetEnemyCard();
        EnemyCardsAttack();
    }

    private void PlayerCardsAttack()
    {
        for (int i = 0; i < 4; i++)
        {
            if (Field.field[0][i].SettedCard == null)
                continue;
            Field.field[0][i].SettedCard.Attack();
        }
    }

    private void SetEnemyCard()
    {
        var temp = DataTableMgr.GetTable<EnemyTable>().dic[EnemyInfo];
        for (int i = 0; i < 4; i++)
        {
            if (Field.field[1][i].SettedCard != null)
                continue;
            if (Field.field[2][i].SettedCard != null)
                continue;
            Field.SetField(Field.field[2][i].SettedCard, Field.field[1][i]);
            Field.UnsetField(Field.field[2][i]);
        }

        var EWData = DataTableMgr.GetTable<EnemyWaveTable>().dic[EnemyCurWaveInfo];

        if (Field.field[2][0].SettedCard == null)
        {
            var card = ObjectPool.GetObject(PoolType.Card);
            var cb = card.GetComponent<CardBase>();
            cb.SetCard(EWData.CardID1);
            Field.SetField(cb, Field.field[2][0]);
        }
        if (Field.field[2][1].SettedCard == null)
        {
            var card = ObjectPool.GetObject(PoolType.Card);
            var cb = card.GetComponent<CardBase>();
            cb.SetCard(EWData.CardID2);
            Field.SetField(cb, Field.field[2][1]);
        }
        if (Field.field[2][2].SettedCard == null)
        {
            var card = ObjectPool.GetObject(PoolType.Card);
            var cb = card.GetComponent<CardBase>();
            cb.SetCard(EWData.CardID3);
            Field.SetField(cb, Field.field[2][2]);
        }
        if (Field.field[2][3].SettedCard == null)
        {
            var card = ObjectPool.GetObject(PoolType.Card);
            var cb = card.GetComponent<CardBase>();
            cb.SetCard(EWData.CardID4);
            Field.SetField(cb, Field.field[2][3]);
        }   
        
        EnemyCurWaveInfo++;
    }

    private void EnemyCardsAttack()
    {
        for (int i = 0; i < 4; i++)
        {
            if (Field.field[1][i].SettedCard == null)
                continue;
            Field.field[1][i].SettedCard.Attack();
        }
    }

    public void Init(int EnemyID)
    {
        EnemyInfo = EnemyID;
    }
    private void Test()
    {
        var card = ObjectPool.GetObject(PoolType.Card);
        var cb = card.GetComponent<CardBase>();
        cb.SetCard(1);

        Field.SetField(cb, Field.field[1][0]);
    }
}
