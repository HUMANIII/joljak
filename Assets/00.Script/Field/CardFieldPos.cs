using Unity.VisualScripting;
using UnityEngine;

public class CardFieldPos : ClickableGameObject
{
    public CardBase SettedCard { get; set; }

    public bool IsMouseOver { get; private set; }

    public override ClickableType OnClickSuccessed()
    {
        Debug.Log("CardFieldPos Clicked");
        if(cgm.Player.PrevClickedGameObject == null)
            return ClickableType.Field;
        if(cgm.Player.CurClickedType == ClickableType.None)
            return ClickableType.Field;
        if(!cgm.Player.CanSummon)
        {
            Debug.Log("Can't Summon Cause by lack of prey");
            return ClickableType.Field;
        }
        if (cgm.Player.PrevClickedGameObject.layer == gameObject.layer &&
            cgm.Player.PrevClickedType == ClickableType.Card &&
            cgm.Player.CanSummon)
        {
            cgm.Player.SetCard(this); 
            cgm.Player.Prey.Clear();
            cgm.Player.SummonObj = null;
            return ClickableType.None;
        }
        return ClickableType.Field;
    }

    private void OnMouseEnter()
    {
        //Debug.Log("Mouse Enter");
        IsMouseOver = true;
    }

    private void OnMouseExit()
    {
        //Debug.Log("Mouse Exit");
        IsMouseOver = false;
    }

}