using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayScript : MonoBehaviour
{

    public float rayDistance;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit rayHit;
        if(Physics.Raycast(transform.position,transform.forward,out rayHit, rayDistance)){
            // rayHit.transform.Translate(transform.forward);
            Rigidbody sphereRigid = rayHit.transform.GetComponent<Rigidbody>();
            sphereRigid.AddForce(transform.forward * 10);
            MeshRenderer sphereMeshRend = rayHit.transform.GetComponent<MeshRenderer>();
            sphereMeshRend.material.color = Color.red;
            AudioSource sphereSource = rayHit.transform.gameObject.AddComponent<AudioSource>();

        }
        
        
        Debug.DrawRay(transform.position, transform.forward* rayDistance, Color.green);
    }
}
