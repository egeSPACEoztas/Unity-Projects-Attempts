using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class küp : MonoBehaviour
{
    // Start is called before the first frame update

    public float kuvvet;
    public float rotasyon;
    public bool kilit = false;
    public Vector3 denemeV3;
    public Quaternion denemeQ;
    public Vector2 denemeV2;

    public enum Element  {Ateş,Hava, Su,Toprak };
     public Element element = Element.Hava;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // transform.Translate(1f * Time.deltaTime, 2f*Time.deltaTime, 1f*Time.deltaTime);
        //time.DeltaTime
        //transform.position += new Vector3(5f, 12f, 16f)*kuvvet*Time.deltaTime ;
        //transform.Rotate(45 * Time.deltaTime * rotasyon, 0* Time.deltaTime * rotasyon, 0 * Time.deltaTime * rotasyon);
        /*if(Time.time >= 5&& !kilit)
        {

            transform.localScale += new Vector3(5, 5, 5);
            kilit = true;
        }
        */
        if (element == Element.Ateş)
        {
            Debug.Log("Ateş");
        }
        else if(element == Element.Hava)
        {
            Debug.Log("Hava");
        }
        else if (element == Element.Su)
        {
            Debug.Log("Su");
        }
        else if (element == Element.Toprak)
        {
            Debug.Log("Toprak");
        }
    }
}