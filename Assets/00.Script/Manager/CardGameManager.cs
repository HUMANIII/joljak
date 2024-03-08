using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardGameManager : MonoBehaviour
{
    public ObjectPool ObjectPool { get; private set; }
    public Player Player { get; private set; }
    public Field Field { get; private set; }
        
    public Transform handPos;
    public Transform deckPos;

    private void Awake()
    {
        ObjectPool = GameObject.FindWithTag(Tags.ObjectPool).GetComponent<ObjectPool>();
        Player = gameObject.AddComponent<Player>();
        Field = GameObject.FindWithTag(Tags.Field).GetComponent<Field>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            //Test1();
        }
    }

    private int testCounter = -3;
    private void Test1()
    {
        var obj = ObjectPool.GetObject(PoolType.Card);
        obj.transform.position = Vector3.right * testCounter;
        testCounter++;
        var rot = obj.transform.position - Camera.main.transform.position;
        obj.transform.rotation = Quaternion.LookRotation(rot);

        var cd = obj.GetComponent<CardBase>();
        cd.SetCard(Random.Range(1, 5));
        cd.SetStackOrder(testCounter + 3);
        obj.layer = Layers.Player;
    }
}
