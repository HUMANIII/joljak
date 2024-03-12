using CsvHelper.Configuration;
using CsvHelper;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using UnityEngine;

public class EnemyTable : DataTable
{
    private readonly string path = "EnemyTable";

    public Dictionary<int, EnemyData> dic = new();

    public EnemyTable()
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
            var records = csv.GetRecords<EnemyData>();
            dic.Clear();
            foreach (var record in records)
            {
                dic.Add(record.ID, record);
            }
        }
    }

    public List<EnemyData> GetAllCharacterData()
    {
        Debug.Log("dataTable Loaded");
        return new List<EnemyData>(dic.Values);
    }
}
