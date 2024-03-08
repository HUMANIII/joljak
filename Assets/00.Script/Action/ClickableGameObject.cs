using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ClickableType
{
    None,
    Card,
    Field,
    Count,
}
public abstract class ClickableGameObject : MonoBehaviour
{
    protected CardGameManager cgm;

    protected virtual void Awake()
    {
        cgm = GameObject.FindWithTag(Tags.Managers).GetComponent<CardGameManager>();
    }
    protected void OnMouseDown()
    {
        cgm.Player.CurClickedGameObject = gameObject;
    }
    public abstract ClickableType OnClickSuccessed();
}
