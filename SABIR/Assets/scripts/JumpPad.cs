using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{

    public float jumpforce;
  
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
       
    }

    private void OnTriggerStay(Collider other)
    {
        if(player.jumpPadMultiplyer < 3)
            player.jumpPadMultiplyer += jumpforce;
    }

    private void OnTriggerExit(Collider other)
    {
        player.jumpPadMultiplyer = 1;
    }

}
