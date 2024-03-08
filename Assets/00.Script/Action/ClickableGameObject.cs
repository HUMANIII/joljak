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
        cgm = FindObjectOfType<CardGameManager>();
    }
    protected void OnMouseDown()
    {
        cgm.Player.CurClickedGameObject = gameObject;
    }
    public abstract ClickableType OnClickSuccessed();
}
