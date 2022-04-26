using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject Parent, Cam;

    public float RotationMultiplier, PositionMultiplier, MovementMultiplier, MinScrollLimit , MaxScrollLimit , LerpSpeed;

    float Scroll;

    void Update()
    {
        if(Input.GetMouseButton(1))
        {
            Parent.transform.Rotate(0, Input.GetAxis("Mouse X") *  RotationMultiplier, 0 , Space.World);
        }

        if(Input.GetMouseButton(2))
        {
            Parent.transform.Translate(Input.GetAxis("Mouse X") * MovementMultiplier , 0 , Input.GetAxis("Mouse Y") * MovementMultiplier , Space.Self);
        }

        if(Scroll < MaxScrollLimit && MinScrollLimit > Scroll)
        {

            Scroll += Input.GetAxis("Mouse ScrollWheel");

        }

        Cam.transform.Translate(0, 0, Input.GetAxis("Mouse ScrollWheel") * PositionMultiplier, Space.Self);
    }
}
