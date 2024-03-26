using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public enum CardTags
{
    None,
    Toxic,
    Count,
}

public enum OwnerType
{
    None,
    Player,
    Enemy,
    Count,
}

public enum CardState
{
    None,
    Deck,
    Hand,
    Field,
    Count,
}

public enum CardType
{
    None,
    Hwando,
    LongSpear,
    Sechongtong,
    Moktong,
    Gukgong,
    Pyeongon,
    LongSword,
    BirdGun,
    CrossBow,
    Jangtae,
    Count,
}

public struct AttackInfo
{
    public int Damage;
    public CardBase Attacker;
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

public struct EnemyWaveData
{
    public int ID { get; set; }
    public int CardID1 { get; set; }
    public int CardID2 { get; set; }
    public int CardID3 { get; set; }
    public int CardID4 { get; set; }
}

public struct EnemyData
{
    public int ID { get; set; }
    public int Pattern1 { get; set; }
    public int Pattern2 { get; set; }
    public int Pattern3 { get; set; }
    public int Pattern4 { get; set; }
    public int Pattern5 { get; set; }
    public int Pattern6 { get; set; }
    public int Pattern7 { get; set; }
    public int Pattern8 { get; set; }
    public int Pattern9 { get; set; }
    public int Pattern10 { get; set; }
}

public struct StringData
{
    public int ID { get; set; }
    public string Value
    {
        get
        {
            /*
            switch (Application.systemLanguage)
            {
                case SystemLanguage.Korean:
                    return Korean;
                case SystemLanguage.English:
                    return English;
                default:
                    return Korean;            
            }
            */
            string rtn = StringTable.Lang switch
            {
                StringTable.Language.Korean => Korean,
                //StringTable.Language.English => English,
                _ => "Lang Setting Error"
            };
            return rtn.Replace("\\n", "\n");
        }
    }
    public string Korean { private get; set; }
    //public string English { private get; set; }
}