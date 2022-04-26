using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventExample : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {

        DelegateFucnktion.rotateDelegate.Invoke();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
