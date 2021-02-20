using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using Newtonsoft.Json;

public static class JsonLoader
{
    public static void Save(string root, string name, object obj)
    {
        string path = Application.persistentDataPath + "/" + root;
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        string dir = path + "/" + name;
        string info = JsonConvert.SerializeObject(obj);
        File.WriteAllText(dir, info);
    }

    public static T Load<T>(string root, string name)
    {
        string dir = Application.persistentDataPath + "/" + root + "/" + name;
        if (File.Exists(dir))
        {
            try
            {
                string info = File.ReadAllText(dir);
                T obj = JsonConvert.DeserializeObject<T>(info);
                return obj;
            }
            catch (Exception err)
            {
                Debug.Log(err);
                return default;
            }
        }
        else
        {
            return default;
        }
    }
}
