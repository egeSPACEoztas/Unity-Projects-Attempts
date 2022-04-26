using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    public float speedWichMeteorsFall;
    // Start is called before the first frame update


    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, -speedWichMeteorsFall * Time.deltaTime, 0);
        if (transform.position.y <= -3.7f)
            Destroy(gameObject);
        
    }
}
