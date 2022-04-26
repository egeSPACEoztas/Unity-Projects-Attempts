using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dgemisi : MonoBehaviour
{

    [Header("InsideObjects")]
    private GameObject Obj;
    public GameManager gm;
    public GameObject strikeCraft;
    public GameObject screenCraft;
    public GameObject strikeCraftSpawnEffect;
    

    

    public GameObject spawnPointObj;



    [Header("Atributes")]
    public GameObject[] slaveShips; //burada bağlı olan E gemileri duracak
    public int numOfSlaveShips;
    private Vector3 clamp;
    private Vector3 spawnPoint;
    public float DgemisiHızı;
    public int Health;
    private float sagSol;
    private int patternInt;




    // Start is called before the first frame update
    void Start()
    {
        Obj = GameObject.FindGameObjectWithTag("GameController");
        gm = Obj.GetComponent<GameManager>();

        InvokeRepeating(nameof(SpawnInPattern), 0.5f, 0.8f);

    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        if (gm.kilitTime)
            return;

        clamp = transform.position;
        clamp.x = Mathf.Clamp(transform.position.x, -7.0f, 7.0f);
        transform.position = clamp;

        if (transform.position.y >= 9.5f)
        {
            transform.Translate(0, 0, -DgemisiHızı * Time.deltaTime);
        }
        else
        {
            sagSol = Mathf.Sin(Time.time) / 300f;
            sagSol += sagSol/2;
            transform.Translate(sagSol * DgemisiHızı, 0, 0);
        }

    }

    private void StrikeSpawn()
    {
        spawnPoint = spawnPointObj.transform.position;

        Instantiate(strikeCraft, spawnPoint,transform.rotation);
    }

    private void ScreenerSpawn()
    {
        spawnPoint = spawnPointObj.transform.position;

        Instantiate(strikeCraft, spawnPoint, transform.rotation);
    }

    private void SpawnInPattern()
    {


        if (patternInt < 2)
        {
            ScreenerSpawn();
            ++patternInt;
        }
        else if (patternInt == 2)
        {
            StrikeSpawn();
            patternInt = 0;
        }
    }

    public void RemoveSlavedShip(GameObject Slave)
    {
        int i = 0;
        foreach (GameObject sampleShip in slaveShips)
        {

            if (Slave == sampleShip)
            {
                slaveShips[i] = null;
            }
            ++i;
        }
    }
    //C# DA NEDEN PUSH BACK YOK NEDEN ARRAYA EKLEYEMIYPORUM NALET OLSUN
   
 
}
