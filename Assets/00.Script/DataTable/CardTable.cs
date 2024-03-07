using CsvHelper.Configuration;
using CsvHelper;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;

public class CardTable : DataTable
{
    private readonly string path = "CardTable";

    public Dictionary<int, CardData> dic = new();

    public CardTable()
    {
        StringBuilder sb = new();
        sb.Append(filePath);
        sb.Append(path);
        filePath = sb.ToString();
        Load();
    }
    public override void Load()
    {
        var csvStr = Resources.Load<TextAsset>(filePath);
        using (TextReader reader = new StringReader(csvStr.text))
        {
            var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture));
            var records = csv.GetRecords<CardData>();
            dic.Clear();
            foreach (var record in records)
            {
                dic.Add(record.ID, record);
            }
        }
    }

    public List<CardData> GetAllCharacterData()
    {
        Debug.Log("dataTable Loaded");
        return new List<CardData>(dic.Values);
    }
}
