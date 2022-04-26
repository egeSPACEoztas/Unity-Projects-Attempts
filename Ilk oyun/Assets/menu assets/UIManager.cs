using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    public GameObject mainMenu;
    public GameObject options;

    private GameManager gm;


    public void startButton()
    {
        SceneManager.LoadScene("SampleScene");

    }

    public void optionsButton()
    {
        mainMenu.SetActive(false);
        options.SetActive(true);

    }
    public void menuReturn()
    {
        mainMenu.SetActive(true);
        options.SetActive(false);
    }

    public void quit()
    {
        Application.Quit();
    }

    
}
