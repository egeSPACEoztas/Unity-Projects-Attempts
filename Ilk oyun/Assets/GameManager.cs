using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Jobs;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public TMP_Text Score;
    public TMP_Text bulletNum;
    public TMP_Text zaman;
    public GameObject[] allMeteors;
    public GameObject[] allEnemies;
    private bool kilitMeteor, kilitDüşman;
    public float BigMeteorScale;
    public static GameManager instance;

    public GameObject pauseCanvas;

    public bool kilitTime=false;

    //düşman puanları
    public static int score;

    void Start()
    {
        
        singleton();
    }

    // Update is called once per frame
    void Update()
    {




        pausefunc();
        MeteorSpawn();
        EnemySpawn();
        BulletCount();
        ScoreCount();
        zamanTut();
    }


    private void BulletCount()
    {
        bulletNum.text = "" + spaceShipControl.bNum;
    }

    private void ScoreCount()
    {
        Score.text = ""+score;
    }

    public void timeKeep()
    {
        
    }

    public void singleton()
    {
        if (instance != null)
        {
             Destroy(this);

        }
        else
        {
            instance = this;
        }
    }

    private void zamanTut()
    {
        zaman.text = "" + Time.time.ToString("F0");
    }

    private void MeteorSpawn() {
        if (Mathf.FloorToInt(Time.time) % 7== 0 && !kilitMeteor)
        {
            int x = Random.Range(0, 2);
            GameObject obj;
            obj = Instantiate(allMeteors[x], new Vector3(Random.Range(-8f, 8f), 10, 1), transform.rotation);
            if (x == 1)
            {
                obj.transform.localScale = Vector3.one * BigMeteorScale;
            }
            kilitMeteor = true;
        }
        else if (Mathf.FloorToInt(Time.time) % 7 == 1)
        {
            kilitMeteor = false;
        }
    }
    private void EnemySpawn()
    {
        if (Mathf.FloorToInt(Time.time) % 4 == 0 && !kilitDüşman)
        {
            int x = Random.Range(0, 10);

            Instantiate(allEnemies[x], new Vector3(Random.Range(-8f, 8f), 10, 1), Quaternion.Euler(-90, 0, 0));

            kilitDüşman = true;
        }
        else if (Mathf.FloorToInt(Time.time) % 4 == 1)
        {
            kilitDüşman = false;
        }
    }
    private void pausefunc()
    {
        if (Input.GetKeyDown(KeyCode.P) && !kilitTime)
        {

            GameObject ship = GameObject.FindGameObjectWithTag("Player");
            ship.GetComponent<spaceShipControl>().timeStop = true;
            Time.timeScale = 0;
            kilitTime = true;
            pauseCanvas.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.P) && kilitTime)
        {
            GameObject ship = GameObject.FindGameObjectWithTag("Player");
            ship.GetComponent<spaceShipControl>().timeStop = false;
            Time.timeScale = 1;
            kilitTime = false;
            pauseCanvas.SetActive(false);
        }

    }

}
