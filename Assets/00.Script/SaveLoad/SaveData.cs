using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class SaveData
{
    public int Version { get; set; }

    public abstract SaveData VersionUp();
}

public class SaveDataV1 : SaveData
{
    public SaveDataV1()
    {
        Version = 1;
    }

    public override SaveData VersionUp()
    {
        return new SaveDataV1();
    }
}
