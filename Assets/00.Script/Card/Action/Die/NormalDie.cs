using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalDie : MonoBehaviour, IDie
{
    private CardGameManager cardGameManager;
    private void Awake()
    {
        cardGameManager = GameObject.FindWithTag(Tags.Managers).GetComponent<CardGameManager>();
    }
    public void Die()
    {
        if(!cardGameManager.ObjectPool.ReturnObject(gameObject))
            Destroy(gameObject);
        Debug.Log("Normal Die");
    }
}