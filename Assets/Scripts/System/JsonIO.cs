using UnityEngine;
using System.IO;

public class JsonIO
{
    private const string dataPath = "Assets/Resources/";

    public static void SaveToJson<T>(T t, string fileName)
    {
        IsExists(dataPath + fileName + ".json");
        string jsonString = JsonUtility.ToJson(t, true);
        File.WriteAllText(dataPath + fileName + ".json", jsonString);
    }

    public static T LoadFromJson<T>(string fileName)
    {
        IsExists(dataPath + fileName + ".json");
        string jsonString = File.ReadAllText(dataPath + fileName + ".json");
        return JsonUtility.FromJson<T>(jsonString);
    }

    private static void IsExists(string path)
    {
        if (File.Exists(path)) return;
        else Create(path);
    }

    private static void Create(string path)
    {
        File.Create(path).Close();
    }
}
