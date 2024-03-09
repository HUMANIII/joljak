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
}
