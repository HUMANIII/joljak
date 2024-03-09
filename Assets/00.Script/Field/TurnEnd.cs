using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnEnd : MonoBehaviour
{
    private CardGameManager cgm;

    private void Awake()
    {
        cgm = GameObject.FindWithTag(Tags.Managers).GetComponent<CardGameManager>();
    }

    private void OnMouseDown()
    {
        cgm.TurnEnd();
    }
}
