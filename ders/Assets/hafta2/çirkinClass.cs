using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class çirkinClass : MonoBehaviour
{
    // Start is called before the first frame update

    void Start()
    {
        GameObject çirkinObje = GameObject.FindGameObjectWithTag("ses");
        çirkinObje.GetComponent<Rigidbody>().useGravity = false;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
