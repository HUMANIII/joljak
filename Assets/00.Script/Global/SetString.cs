using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SetString : MonoBehaviour
{
    public int ID;
    private TextMeshProUGUI text;
    private Text textLagacy;

    private void Awake()
    {
        if(GameManager.Instance == null)
        {
            var obj = GameManager.Instance;
        }
        StringTable.OnLanguageChanged += SetText;        
    }

    private void OnEnable()
    {
        if(text == null && textLagacy == null)
        {
            TryGetComponent(out text);
            TryGetComponent(out textLagacy);
        }    
        SetText();
    }

    public void SetText()
    {
        var message = GameManager.stringTable[ID].Value;
        if(text != null)
        {
            text.text = message;
            text.text = message.Replace("\\n", "\n");
        }
        else if(textLagacy != null)
        {
            textLagacy.text = message;
            textLagacy.text = message.Replace("\\n", "\n");
        }
    }

    private void OnDestroy()
    {
        StringTable.OnLanguageChanged -= SetText;
    }
}
