using System.Collections.Generic;
using UnityEngine;

public enum PoolType
{
    Card,
    Count,
    HumanModel,
    WeaponModel,
}
public class ObjectPool : MonoBehaviour
{
    private readonly Dictionary<PoolType, Stack<GameObject>> effectQueues = new();
    [SerializeField]
    private GameObject[] prefabs;

    private void Awake()
    {
        for (int i = 0; i < (int)PoolType.Count; i++)
        {
            Instantiate(new GameObject(((PoolType)i).ToString()), transform);       
            effectQueues.Add((PoolType)i, new Stack<GameObject>());
        }
    }

    public GameObject GetObject(PoolType poolType)
    {
        if (!effectQueues.ContainsKey(poolType))
            return null;

        if (effectQueues[poolType].Count == 0)
        {
            var obj = Instantiate(prefabs[(int)poolType]);
            obj.transform.parent = gameObject.transform.GetChild((int)poolType);
            return obj;
        }
        GameObject GO = effectQueues[poolType].Pop();
        GO.SetActive(true);
        return GO;
    }

    public bool ReturnObject(GameObject GO)
    {
        GO.SetActive(false);
        if(!GO.TryGetComponent<IPoolable>(out var poolable))
            return false;
        poolable.OnReturn();
        effectQueues[poolable.PoolType].Push(GO);
        //GO.transform.parent = gameObject.transform.GetChild((int)poolable.PoolType);
        return true;
    }

    public void Clear()
    {
        foreach (var effectQueue in effectQueues)
        {
            foreach (var effect in effectQueue.Value)
            {
                Destroy(effect);
            }
        }
    }

    private void OnDestroy()
    {
        Clear();
    }
}
public interface IPoolable
{
    public PoolType PoolType { get; }
    public void OnReturn();
}