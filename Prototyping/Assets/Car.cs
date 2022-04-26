using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public WheelCollider[] WC;
    public Transform[] Models;

    public float Speed;
    public float SteeringSpeed;

    Vector3 v;
    Quaternion q;

    void Start()
    {
        
    }

    void Update()
    {
        for (int x = 0; x < WC.Length; x++)
        {
            WC[x].GetWorldPose(out v, out q);
            Models[x].position = v;
            Models[x].rotation = q;
        }
        WC[2].motorTorque = Input.GetAxis("Vertical") * Speed;
        WC[3].motorTorque = Input.GetAxis("Vertical") * Speed;
        WC[0].steerAngle = Input.GetAxis("Horizontal") * SteeringSpeed;
        WC[1].steerAngle = Input.GetAxis("Horizontal") * SteeringSpeed;
    }
}
