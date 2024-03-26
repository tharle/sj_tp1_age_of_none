using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveSystem
{
    private const string URL_PATH = "/Slot.1";
    
    /// <summary>
    /// Save only the player, but all the rest of objects in save are untouched.
    /// </summary>
    /// <param name="player"></param>
    public static void Save(Player player)
    {
        SaveData saveData = new SaveData();

        // Load the data for not losing others previus data saved
        Load(data => { saveData = data; });
        saveData.PlayerData = player;

        Save(saveData);
    }

    private static void Save(SaveData data)
    {
        Debug.Log("On save data.");
        string saveDataJson = JsonUtility.ToJson(data);
        string Path = Application.persistentDataPath + URL_PATH;
        File.WriteAllText(Path, saveDataJson);
        Debug.Log($"The data was save at {data.Date}.");
    }

    public static void Load(Action<SaveData> OnLoadGame, Action OnNewGame = null)
    {
        Debug.Log("On load data.");
        SaveData saveData = new SaveData();
        string path = Application.persistentDataPath + URL_PATH;

        if (!File.Exists(path)) 
        {
            // It's not possible find the File, maybe the file not exist or the game never be saved
            OnNewGame?.Invoke();
            return;
        }

        string loadDataJson = File.ReadAllText(path);
        saveData = JsonUtility.FromJson<SaveData>(loadDataJson);
        Debug.Log($"Load data complete. Last time save was {saveData.Date}");
        OnLoadGame?.Invoke(saveData);
    }
}
