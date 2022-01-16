using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveSystem : MonoBehaviour
{
    public static SaveSystem Instance;
    private string _filePath;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        _filePath = Application.persistentDataPath + "/eggs.ted";
    }

    public void Save(List<Util.Egg> saveData)
    {
        var dataStream = new FileStream(_filePath, FileMode.Create);
        var converter = new BinaryFormatter();
        
        converter.Serialize(dataStream, saveData);
        dataStream.Close();
    }

    public List<Util.Egg> Load()
    {
        if (File.Exists(_filePath))
        {
            var dataStream = new FileStream(_filePath, FileMode.Open);
            var converter = new BinaryFormatter();

            if (dataStream.Length == 0)
            {
                dataStream.Close();
                return null;
            }

            var saveData = converter.Deserialize(dataStream) as List<Util.Egg>;

            dataStream.Close();
            return saveData;
        }

        Debug.LogError("Save file not found in " + _filePath);
        return null;
    }
}