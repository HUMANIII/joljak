using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using UnityEngine;
//using UnityEngine.Purchasing.MiniJSON;

public class AESJsonLoader : MonoBehaviour
{
    const string RESOURCE_PATH = "/Resources/Data/JsonData/";
    const string KEY = "asd123";

    public static void Save<T>(string ID, T data)
    {
        try
        {
            string jsonString = JsonConvert.SerializeObject(data, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });


            if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
            {
                jsonString = EncryptRijndael.Encrypt256(jsonString, KEY);
                File.WriteAllText(Application.persistentDataPath + "/" + ID + ".json", jsonString);
            }
            else
            {
                //Commented for convenient testing on PC
                //jsonString = EncryptAES256.Encrypt256(jsonString, KEY);
                File.WriteAllText(Application.dataPath + RESOURCE_PATH + ID + ".json", jsonString);
            }
        }
        catch (Exception e)
        {
            Debug.Log("Error Save :" + e.ToString());
        }
    }
    public static T Load<T>(string ID, T dafaultValue)
    {
        try
        {
            if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
            {
                if (!File.Exists(Application.persistentDataPath + "/" + ID + ".json"))
                {
                    Debug.Log("There is no file name " + ID);
                    return dafaultValue;
                }
                string data = File.ReadAllText(Application.persistentDataPath + "/" + ID + ".json");
                data = EncryptRijndael.Decrypt256(data, KEY);            
                return JsonConvert.DeserializeObject<T>(data);
            }
            else
            {
                if (!File.Exists(Application.dataPath + RESOURCE_PATH + ID + ".json"))
                {
                    Debug.Log("There is no file name " + ID);
                    return dafaultValue;
                }
                string data = File.ReadAllText(Application.dataPath + RESOURCE_PATH + ID + ".json");
                //data = EncryptAES256.Decrypt256(data, KEY);
                return JsonConvert.DeserializeObject<T>(data);
            }
        }
        catch (Exception e)
        {
            Debug.Log("Error LoadData :" + e.ToString());
            return dafaultValue;
        }
    }

    public static void DeleteData(string ID)
    {
        try
        {
            if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
            {
                if (File.Exists(Application.persistentDataPath + "/" + ID + ".json"))
                {
                    File.Delete(Application.persistentDataPath + "/" + ID + ".json");
                }
            }
            else
            {
                if (File.Exists(Application.dataPath + RESOURCE_PATH + ID + ".json"))
                {
                    File.Delete(Application.dataPath + RESOURCE_PATH + ID + ".json");
                    File.Delete(Application.dataPath + RESOURCE_PATH + ID + ".json.meta");
                }
            }
        }
        catch (Exception e)
        {
            Debug.Log("Error DeleteData :" + e.ToString());
        }
    }
}
