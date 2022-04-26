using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;




public class ZOOM : MonoBehaviour
{

    public Camera cam;
    public float OrthographicSpeed;
    public float PerspectiveSpeed;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount == 2) {
            Touch Touch1 = Input.touches[0];
            Touch Touch2 = Input.touches[1];

            Vector2 prevTouchOne = Touch1.position - Touch1.deltaPosition;
            Vector2 prevTouchTwo = Touch2.position - Touch2.deltaPosition;

            float prevMagnitude = (prevTouchTwo - prevTouchOne).magnitude;
            float currMagnitude = (Touch2.position - Touch1.position).magnitude;


            float difference = currMagnitude - prevMagnitude;

            if (cam.orthographic)
            {
                cam.orthographicSize += difference * OrthographicSpeed;
                cam.orthographicSize = Mathf.Max(cam.orthographicSize,0.1f);
            }
            else
            {
                cam.fieldOfView += difference * PerspectiveSpeed;
                cam.fieldOfView = Mathf.Clamp(cam.fieldOfView, 0.1f, 179.9f);

            }
        
        }
    }
}
