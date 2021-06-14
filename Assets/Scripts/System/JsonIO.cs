using UnityEngine;
using System.IO;

public class JsonIO
{
    private static readonly string dataPath = Application.streamingAssetsPath;
    private static readonly string directoryName = "SaveData";

    public static void SaveToJson<T>(T t, string fileName)
    {
        CheckForPath(directoryName, fileName + ".json");
        string jsonString = JsonUtility.ToJson(t, true);
        File.WriteAllText(dataPath + "/" + directoryName + "/" + fileName + ".json", jsonString);
    }

    public static T LoadFromJson<T>(string fileName)
    {
        CheckForPath(directoryName, fileName + ".json");
        string jsonString = File.ReadAllText(dataPath + "/" + directoryName + "/" + fileName + ".json");
        return JsonUtility.FromJson<T>(jsonString);
    }

    private static void CheckForPath(string directoryName, string fileName)
    {
        if (!Directory.Exists(dataPath)) CreateDirectory(dataPath);
        if (!Directory.Exists(dataPath + "/" + directoryName)) CreateDirectory(dataPath + "/" + directoryName);
        if (!File.Exists(dataPath + "/" + directoryName + "/" + fileName)) CreateFile(dataPath + "/" + directoryName + "/" + fileName);
    }

    private static void CreateFile(string path)
    {
        File.Create(path).Close();
    }

    private static void CreateDirectory(string path)
    {
        Directory.CreateDirectory(path);
    }
}
