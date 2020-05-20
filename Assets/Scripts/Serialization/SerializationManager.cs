using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SerializationManager
{
    private static string saveFolderName = "saves";

    public static bool Save(string saveName, object saveData)
    {
        BinaryFormatter formatter = GetBinaryFormatter();

        // Get save location path and file object.
        if (!Directory.Exists($"{Application.persistentDataPath}/{saveFolderName}"))
        {
            Directory.CreateDirectory($"{Application.persistentDataPath}/{saveFolderName}");
        }
        string path = $"{Application.persistentDataPath}/{saveFolderName}/{saveName}.sav";
        
        FileStream file = File.Create(path);

        // Serialize save data.
        formatter.Serialize(file, saveData);

        file.Close();

        return true;
    }

    public static object Load(string path)
    {
        // I hope the file you're loading exists.
        if (!File.Exists(path))
        {
            return null;
        }

        BinaryFormatter formatter = GetBinaryFormatter();

        FileStream file = File.Open(path, FileMode.Open);

        try
        {
            object save = formatter.Deserialize(file);
            file.Close();
            return save;
        }
        catch
        {
            Debug.LogErrorFormat($"Failed to load file at location: {file}");
            file.Close();
            return null;
        }
    }

    public static BinaryFormatter GetBinaryFormatter()
    {
        BinaryFormatter formatter = new BinaryFormatter();

        return formatter;
    }
}
