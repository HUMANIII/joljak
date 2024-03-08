using UnityEngine;

public class CardFieldPos : ClickableGameObject
{
    public bool IsMouseOver { get; private set; }

    public override ClickableType OnClickSuccessed()
    {
        Debug.Log("CardFieldPos Clicked");
        if(cgm.Player.CurClickedType == ClickableType.None)
        {
            return ClickableType.Field;
        }
        if (cgm.Player.PrevClickedGameObject.layer == gameObject.layer && cgm.Player.PrevClickedType == ClickableType.Card)
        {
            cgm.Player.SetCard(this);
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