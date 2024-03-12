using CsvHelper.Configuration;
using CsvHelper;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;

public class EnemyWaveTable : DataTable
{
    private readonly string path = "EnemyWaveTable";

    public Dictionary<int, EnemyWaveData> dic = new();

    public EnemyWaveTable()
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
            var records = csv.GetRecords<EnemyWaveData>();
            dic.Clear();
            foreach (var record in records)
            {
                dic.Add(record.ID, record);
            }
        }
    }

    public List<EnemyWaveData> GetAllCharacterData()
    {
        Debug.Log("dataTable Loaded");
        return new List<EnemyWaveData>(dic.Values);
    }
}
