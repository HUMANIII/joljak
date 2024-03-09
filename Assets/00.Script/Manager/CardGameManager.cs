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
            if (Field.field[1][i].SettedCard != null)
            {
                Field.field[0][i].SettedCard.Attack(Field.field[1][i].SettedCard);
            }
            else
            {
                Field.field[0][i].SettedCard.Attack(Enemy);
            }            
        }
    }

    private void SetEnemyCard()
    {
        
    }

    private void EnemyCardsAttack()
    {
        for (int i = 0; i < 4; i++)
        {
            if (Field.field[1][i].SettedCard == null)
            break;

            if (Field.field[0][i].SettedCard != null)
            {
                Field.field[1][i].SettedCard.Attack(Field.field[0][i].SettedCard);
            }
            else
            {
                Field.field[1][i].SettedCard.Attack(Player);
            }
            
        }
    }

    private void Test()
    {
        var card = ObjectPool.GetObject(PoolType.Card);
        var cb = card.GetComponent<CardBase>();
        cb.SetCard(1);

        Field.SetField(cb, Field.field[1][0]);
    }
}
