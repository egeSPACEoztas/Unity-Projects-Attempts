using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kötüKlass : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject oyunObjesi;
    void Start()
    {
        AudioSource sesşeysi;
        sesşeysi = oyunObjesi.GetComponent<AudioSource>();
        sesşeysi.volume = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
