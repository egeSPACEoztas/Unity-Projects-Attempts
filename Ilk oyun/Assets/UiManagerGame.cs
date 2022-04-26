using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class UiManagerGame : MonoBehaviour
{

   
    public void returnToMenu() { 

        SceneManager.LoadScene("menu");
        Time.timeScale = 1;

    }
}
