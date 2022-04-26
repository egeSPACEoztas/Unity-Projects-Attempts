using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CgemisiEnemyBullet : MonoBehaviour
{
    
    // Start is called before the first frame update
    public float extraSpeed;
    private GameObject obj;
    private Cgemisi cgemisi;
    void Start()
    {

        //burada A gemisi için yapılmış düşman mermisi



        obj = GameObject.FindGameObjectWithTag("düşmanC");
        cgemisi = obj.GetComponent<Cgemisi>();


    }

    // Update is called once per frame
    void Update()
    {


        transform.Translate(0, 0, -(cgemisi.CgemisiHızı + extraSpeed) * Time.deltaTime);
        if (transform.position.y <= -3.7f)
            Destroy(gameObject);
    }
}
