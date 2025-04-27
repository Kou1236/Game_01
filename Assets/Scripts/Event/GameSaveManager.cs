using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GameSaveManager : Singleton<GameSaveManager>
{
    public GameSave gameSave;

    public void SaveGame(){
        Debug.Log(Application.persistentDataPath);
        if (!Directory.Exists(Application.persistentDataPath + "/SaveData"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/SaveData");
        }
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/SaveData/gameSave.dat");
        var json = JsonUtility.ToJson(gameSave);
        bf.Serialize(file, json);
        file.Close();
    }

    public void LoadGame(){
        BinaryFormatter bf = new BinaryFormatter();
        if (File.Exists(Application.persistentDataPath + "/SaveData/gameSave.dat"))
        {
            FileStream file = File.Open(Application.persistentDataPath + "/SaveData/gameSave.dat", FileMode.Open);
            JsonUtility.FromJsonOverwrite((string)bf.Deserialize(file), gameSave);
            file.Close();
        }
    }
    
}
