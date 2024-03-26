using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable
{
    private CardGameManager cgm;
    public int DamagedAmount { get; private set; }

    public int ID;

    public int[] WaveID = new int[10];

    private void Awake()
    {
        cgm = GetComponent<CardGameManager>();
    }

    public void Damaged(int amount)
    {
        DamagedAmount += amount;
    }

    public void StartGame(int ID)
    {
        this.ID = ID;
    }

    public void SetCard()
    {
        var data = DataTableMgr.GetTable<EnemyTable>().dic[ID];


    }
}
