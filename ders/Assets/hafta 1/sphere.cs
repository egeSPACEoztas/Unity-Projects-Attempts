using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sphere : MonoBehaviour
{

    private SphereCollider sphereCollider;

    // Start is called before the first frame update
    void Start()
    {
        //aaaa
        SphereCollider sphereCollider = GetComponent<SphereCollider>();
        sphereCollider.radius = 0.1f ; 


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
