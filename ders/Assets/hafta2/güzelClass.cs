using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class güzelClass : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource audioSource;
  
    public Rigidbody rigidBody;
    void Start()
    {

        audioSource.volume = 0.5f;//en kolay yöntem direkten dışarıdan veriyor
     
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
