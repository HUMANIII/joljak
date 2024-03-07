using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardOption
{
    None,
    Flying,
    AntiAir,
}

public struct CardData
{
    public int ID { get; set; }
    public string Name { get; set; }
    public int Cost { get; set; }
    public int Attack { get; set; }
    public int HP { get; set; }
    public int Option { get; set; }
    public string IllustPath { get; set; }
}
