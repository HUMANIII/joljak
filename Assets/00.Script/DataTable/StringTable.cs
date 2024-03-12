using CsvHelper.Configuration;
using CsvHelper;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using UnityEngine;
using System;
using System.Text;

public class StringTable : DataTable
{
    public enum Language
    {
        Korean,
        English,
    }
    public static Language Lang = Language.Korean;

    public static event Action OnLanguageChanged;

    private string path = "StringTable";

    public Dictionary<int, StringData> dic = new();
    public StringTable()
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
            var records = csv.GetRecords<StringData>();
            dic.Clear();
            foreach (var record in records)
            {
                dic.Add(record.ID, record);
            }
        }
    }

    public List<StringData> GetAllCharacterData()
    {
        Debug.Log("dataTable Loaded");
        return new List<StringData>(dic.Values);
    }

    public static void ChangeLanguage(Language language)
    {
        Lang = language;
        OnLanguageChanged?.Invoke();
    }
}
