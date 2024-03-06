using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    private static object _lock = new object();
    private static bool applicationIsQuitting = false;
    private static bool isInitialized = false;

    public static GameManager Instance
    {
        get
        {
            if (applicationIsQuitting)
            {
                Debug.LogWarning("[Singleton] Instance '" + typeof(GameManager) +
                    "' already destroyed on application quit." +
                    " Won't create again - returning null.");
                return null;
            }

            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = (GameManager)FindObjectOfType(typeof(GameManager));

                    if (FindObjectsOfType(typeof(GameManager)).Length > 1)
                    {
                        Debug.LogError("[Singleton] Something went really wrong " +
                            " - there should never be more than 1 singleton!" +
                            " Reopening the scene might fix it.");
                        return _instance;
                    }

                    if (_instance == null)
                    {
                        GameObject singleton = new GameObject();
                        _instance = singleton.AddComponent<GameManager>();
                        singleton.name = "(singleton) " + typeof(GameManager).ToString();

                        DontDestroyOnLoad(singleton);

                        Debug.Log("[Singleton] An instance of " + typeof(GameManager) +
                            " is needed in the scene, so '" + singleton +
                            "' was created with DontDestroyOnLoad.");
                    }
                    else
                    {
                        Debug.Log("[Singleton] Using instance already created: " +
                            _instance.gameObject.name);
                    }
                }

                return _instance;
            }
        }
    }

    public static void Init()
    {
        if (isInitialized)
            return;

        if (_instance == null)
        {
            _instance = (GameManager)FindFirstObjectByType(typeof(GameManager));
            var gameMgrs = FindObjectsOfType(typeof(GameManager));
            if (gameMgrs.Length > 1)
            {
                foreach (var gameMgr in gameMgrs)
                {
                    if (!ReferenceEquals(_instance, (GameManager)gameMgr))
                    {
                        Destroy(gameMgr);
                    }
                }
                //return;
            }
            else
            {
                if (_instance == null)
                {
                    GameObject singleton = new GameObject();
                    _instance = singleton.AddComponent<GameManager>();
                    singleton.name = "(singleton) " + typeof(GameManager).ToString();

                    DontDestroyOnLoad(singleton);

                    Debug.Log("[Singleton] An instance of " + typeof(GameManager) +
                                           " is needed in the scene, so '" + singleton +
                                                              "' was created with DontDestroyOnLoad.");
                }
                else
                {
                    Debug.Log("[Singleton] Using instance already created: " +
                                           _instance.gameObject.name);
                }
            }
            isInitialized = true;
        }

        _instance.LoadData();
    }

    private void LoadData()
    {
    }

    private void OnDestroy()
    {
        applicationIsQuitting = true;
    }


    public void test()
    {
        Debug.Log(DataTableMgr.GetTable<CardTable>().dic[999].Name);
    }
}
