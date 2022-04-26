using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveLoadBinary : MonoBehaviour
{
    public int CurrentGold, TotalGold;
    public String Name;
    public Vector3 Position;

    void Start()
    {
    //    BinaryLoad();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CurrentGold++;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            BinarySave();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            BinaryDelete();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            BinaryLoad();
        }
    }

    public void BinarySave()
    {
        BinaryFormatter BF = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerinfo.txt");

        PlayerData data = new PlayerData();
        data.CurrentGold = CurrentGold;
        data.TotalGold = TotalGold + CurrentGold;
        data.Name = Name;
        //data.Position = Position;
        BF.Serialize(file, data);
        file.Close();
        Debug.Log("Saved in " + Application.persistentDataPath);
    }

    public void BinaryLoad()
    {
        BinaryFormatter BF = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/playerinfo.txt", FileMode.Open);
        PlayerData data = (PlayerData)BF.Deserialize(file);
        file.Close();
        TotalGold = data.TotalGold;
        CurrentGold = 0;
        Name = data.Name;
        //Position = data.Position;
        Debug.Log("Loaded");
    }

    public void BinaryDelete()
    {
        BinaryFormatter BF = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerinfo.txt");

        PlayerData data = new PlayerData();
        data.CurrentGold = CurrentGold;
        data.TotalGold = 0;
        Name = null;
        BF.Serialize(file, data);
        file.Close();
        Debug.Log("Saved in " + Application.persistentDataPath);
    }
}

[Serializable]
class PlayerData
{
    public int CurrentGold, TotalGold;
    public string Name;
}
