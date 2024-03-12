using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public static Dictionary<int, StringData> stringTable = new();

    private void Awake()
    {
        stringTable = DataTableMgr.GetTable<StringTable>().dic;
    }
}
