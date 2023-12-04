using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public class TraslatePlayerData : MonoBehaviour
{
    public bool IsLoadGame;

    public static void SavePlayerData(Player player, string NameFile)
    {
        string path = Application.persistentDataPath + "/SaveData";
        BinaryFormatter formatter = new BinaryFormatter();

        if(!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        FileStream stream = new FileStream(path + NameFile, FileMode.Create);

        PlayerData data = new PlayerData(player);

        formatter.Serialize(stream, data);
        
        stream.Close();
    }

    public static PlayerData LoadPlayerData(Player player, string NameFile)
    {
        string path = Application.persistentDataPath + "/SaveData" + "/" + NameFile;
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();

            FileStream stream = new FileStream(path, FileMode.Open);

            // Player sett
            PlayerData data = (PlayerData)formatter.Deserialize(stream);
            stream.Close();

            return data;
        }
        else
        {
            return null;
        }
    }
}
