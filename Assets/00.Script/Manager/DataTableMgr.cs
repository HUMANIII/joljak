using System;
using System.Collections.Generic;


public static class DataTableMgr
{
    private static Dictionary<Type, DataTable> tables = new();

    static DataTableMgr()
    {
        tables.Clear();
        tables.Add(typeof(CardTable), new CardTable());
    }

    public static T GetTable<T>() where T : DataTable
    {
        var id = typeof(T);
        if (!tables.ContainsKey(id))
        {
            return null;
        }
        return tables[id] as T;
    }

    public static void LoadAll()
    {
        foreach (var item in tables)
        {
            item.Value.Load();
        }
    }
}