using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carScript : MonoBehaviour
{
    public WheelCollider[] wheels;
    public Transform[] wheelTransforms;
    public float motorForce;
    public float steeReference;
    private Vector3 V;
    private Quaternion Q;
    public player Player;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        carMovement();
        CarRotation();
        SetCollidersAndModels();
        CarPlayerInteraction();
    }

    void carMovement()
    {
        
            for (int i = 2; i <= 3; i++)
            { 
                wheels[i].motorTorque = motorForce* Input.GetAxis("Vertical");
            }
        

       
    }

    void CarRotation()
    {
        for(int i =0; i <= 1; i++)
        {
            wheels[i].steerAngle = steeReference* Input.GetAxis("Horizontal");
        }
    }

    void SetCollidersAndModels()
    {
        for(int i=0; i < wheels.Length; ++i)
        {
            wheels[i].GetWorldPose(out V, out Q);
            wheelTransforms[i].position = V;
            wheelTransforms[i].rotation = Q;
        }
    }

    void CarPlayerInteraction()
    {
        Player.transform.position = transform.TransformPoint(-2f, 1.1f, 0f);
        if (Input.GetKeyDown(KeyCode.E))
        {


            Player.gameObject.SetActive(true);

            
            gameObject.GetComponentInChildren<Camera>().enabled = false;
            gameObject.GetComponent<carScript>().enabled = false;
        }
    }
}
