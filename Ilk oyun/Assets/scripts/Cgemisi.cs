using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cgemisi : MonoBehaviour
{
    public float CgemisiHızı;
    public GameObject bullet;
    public GameObject bulletFire;


    private float sagSol;
    private Vector3 clamp;
    private Vector3 targetLock;
    public int numOfSlaveShips;
    public int health;
    private GameObject Obj;
    private GameManager gm;


    void Start()
    {

       // GameManager.instance.timeKeep();



        InvokeRepeating(nameof(CifteAtes), 0.3f, 1f);
        Obj = GameObject.FindGameObjectWithTag("GameController");
         gm = Obj.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        hareket();
       // CifteAtes();
    }
    private void CifteAtes()
    {
        //
        Instantiate(bullet, bulletFire.transform.position + new Vector3(-1f, 0f, 0f), transform.rotation);
        Instantiate(bullet, bulletFire.transform.position + new Vector3(1f, 0f, 0f), transform.rotation);
    }
    private void hareket()
    {
        if (gm.kilitTime)
            return;

        clamp = transform.position;
        clamp.x = Mathf.Clamp(transform.position.x, -7.0f, 7.0f);
        transform.position = clamp;

        

        if (transform.position.y >= 9f)
            transform.Translate(0, 0, -CgemisiHızı * Time.deltaTime);
        else
        {
            sagSol = Mathf.Sin(Time.time) / 300f;
            transform.Translate(sagSol*CgemisiHızı, 0, 0);

            targetLock = transform.position - GameObject.FindGameObjectWithTag("Player").transform.position;
            transform.rotation = Quaternion.LookRotation(targetLock,transform.up);
            //transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform.position);
        }


    }

   

}
