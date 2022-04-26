using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class gameCtrl : MonoBehaviour
{

    public int currentGold;
    public int totalGold;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentGold++;
        }
            
    }

    public void playerSave()
    {
        PlayerPrefs.SetInt("Gold", currentGold+totalGold);
    }

    public void playerPrefsLoad()
    {
        totalGold = PlayerPrefs.GetInt("Gold");
        currentGold = 0;
    }


    public void DeleteAll()
    {
        PlayerPrefs.DeleteKey("Gold");
        //PlayerPrefs.DeleteAll();
    }

    public void BinarySave()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerinfo.txt");

        PlayerData data = new PlayerData();
        data.CurrentGold = currentGold;
        data.totalGold = totalGold + currentGold;

        bf.Serialize(file, data);
        file.Close();
        Debug.Log("Saved In:" + Application.persistentDataPath);
    }


    public void BinaryLoad()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.txt", FileMode.Open);
        PlayerData data = (PlayerData)bf.Deserialize(file);
        file.Close();
        totalGold = data.totalGold;
        currentGold = 0;
        Debug.Log("Loaded");
    }



    [Serializable]
    class PlayerData
    {
        public int CurrentGold;
        public int totalGold;
    }
}
