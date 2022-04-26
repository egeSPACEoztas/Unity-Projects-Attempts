using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enumObjesiScript : MonoBehaviour
{
    public enum İtemler { kılıç, kalkan, yay,mızrak};
    public İtemler item = İtemler.kılıç;
    public GameObject kılıç;
    public GameObject kalkan;
    public GameObject yay;
    public GameObject mızrak;

    public enum Elementler { Ateş, Hava, Su, Toprak};
    public Elementler seçliElement = Elementler.Ateş;

    // Start is called before the first frame update
    void Start()
    {
        kılıç.GetComponent<Renderer>().enabled = false;
        kalkan.GetComponent<Renderer>().enabled = false;
        yay.GetComponent<Renderer>().enabled = false;
        mızrak.GetComponent<Renderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {


        if (item == İtemler.kılıç)
        {
            kılıç.GetComponent<Renderer>().enabled = true;
            kalkan.GetComponent<Renderer>().enabled = false;
            yay.GetComponent<Renderer>().enabled = false;
            mızrak.GetComponent<Renderer>().enabled = false;
        }
        else if (item == İtemler.kalkan)
        {
            kılıç.GetComponent<Renderer>().enabled = false;
            kalkan.GetComponent<Renderer>().enabled = true;
            yay.GetComponent<Renderer>().enabled = false;
            mızrak.GetComponent<Renderer>().enabled = false;
        }
        else if (item == İtemler.yay)
        {
            kılıç.GetComponent<Renderer>().enabled = false;
            kalkan.GetComponent<Renderer>().enabled = false;
            yay.GetComponent<Renderer>().enabled = true;
            mızrak.GetComponent<Renderer>().enabled = false;
        }
        else if (item == İtemler.mızrak)
        {
            kılıç.GetComponent<Renderer>().enabled = false;
            kalkan.GetComponent<Renderer>().enabled = false;
            yay.GetComponent<Renderer>().enabled = false;
            mızrak.GetComponent<Renderer>().enabled = true;
        }



        if (seçliElement == Elementler.Ateş)
        {
            Debug.Log("Avatar Ateş ulusundan.");
        }
        else if (seçliElement == Elementler.Hava)
        {
            Debug.Log("Avatar Hava ulusundan");
        }
        else if (seçliElement == Elementler.Su)
        {
            Debug.Log("Avatar Su ulusundan");
        }
        else if (seçliElement == Elementler.Toprak)
        {
            Debug.Log("Avatar Toprak ulusundan");
        }

    }
}
