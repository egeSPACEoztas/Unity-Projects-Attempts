using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoad : MonoBehaviour
{
    public int Gold , CurrentGold;
    
    void Start()
    {
        PlayerPrefsLoad();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            CurrentGold++;
        }

        if(Input.GetKeyDown(KeyCode.D))
        {
            PlayerPrefsSave();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            PlayerPrefsDelete();
        }
    }

    public void PlayerPrefsSave()
    {
        PlayerPrefs.SetInt("Gold" , Gold + CurrentGold);
    }

    public void PlayerPrefsLoad()
    {
        Gold = PlayerPrefs.GetInt("Gold");
        CurrentGold = 0;
    }

    public void PlayerPrefsDelete()
    {
        //PlayerPrefs.DeleteAll();
        PlayerPrefs.DeleteKey("Gold");
    }
}
