using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bgemisi : MonoBehaviour
{

    public float BgemisiHızı ;
    private Vector3 v3;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*transform.Translate(0,0, -BgemisiHızı * Time.deltaTime);*/
        kamikaze();
        if (transform.position.y <= -3.7f)
            Destroy(gameObject);
        
    }

    void kamikaze()
    {
        v3 = GameObject.FindGameObjectWithTag("Player").transform.position - transform.position ;
        transform.position += v3.normalized * BgemisiHızı * Time.deltaTime;
        transform.rotation = Quaternion.LookRotation(v3,transform.up);
    }
}
