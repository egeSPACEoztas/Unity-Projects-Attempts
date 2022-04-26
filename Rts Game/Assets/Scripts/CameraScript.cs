using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    
    public Vector3 rigPosition;
    public Vector3 camPosition;
    public Quaternion rigRotation;
    public Camera cam;

    public Vector3 currentPositon;
    public Vector3 lastPositon;
    public Vector3 deltaPositon;

    private float zMove;
    private float xMove;
    

    public float camRotateSpeed;
    public float cameraSpeed;
    public float zoomSpeed;

    public float maxHeight;
    public float minHeight;

    public float panBorderThickness;

    // Start is called before the first frame update
    void  Start()
    {
        cam = Camera.main;
        camPosition = cam.transform.position;
        rigPosition = transform.position;
        rigRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        

        MoveCam();
    }


    void MoveCam()
    {   
        currentPositon = Input.mousePosition;
        
        if (Input.GetMouseButton(2))
        {
            Debug.Log(deltaPositon.x);
            /*
            currentPositon = Input.mousePosition;
            deltaPositon = currentPositon - lastPositon;
            lastPositon = currentPositon;
            */
            deltaPositon = currentPositon - lastPositon;
            if (deltaPositon.y >= 1.5)
                {
                    rigRotation.y += deltaPositon.x * camRotateSpeed;
                }
                
            
            
        }
        else
        {
            /* if (!Input.GetKeyUp(KeyCode.Mouse2))
            {
                deltaPositon = Vector3.zero;
                currentPositon = Vector3.zero;
                lastPositon = Vector3.zero;
            }

            */
            if (Input.GetKey(KeyCode.UpArrow) || Input.mousePosition.y >= Screen.height - panBorderThickness)
            {
                zMove += cameraSpeed * Time.deltaTime;
                //yukarı çekmek için
            }
            if (Input.GetKey(KeyCode.DownArrow) || Input.mousePosition.y <= panBorderThickness)
            {
                zMove -= cameraSpeed * Time.deltaTime;
                //aşşağı çekmek için
            }
            if (Input.GetKey(KeyCode.RightArrow) || Input.mousePosition.x >= Screen.width - panBorderThickness)
            {
                xMove += cameraSpeed * Time.deltaTime;
                //yukarı çekmek için
            }
            if (Input.GetKey(KeyCode.LeftArrow) || Input.mousePosition.x <= panBorderThickness)
            {
                xMove -= cameraSpeed * Time.deltaTime;
                //yukarı çekmek için
            }
        }

        lastPositon = currentPositon;


        float scroll = Input.GetAxis("Mouse ScrollWheel");
        //rigPosition.y -= scroll * zoomSpeed * Time.deltaTime *100f;
        cam.orthographicSize -= scroll * zoomSpeed * Time.deltaTime * 100f;

        //rigPosition.y = Mathf.Clamp(rigPosition.y, minHeight, maxHeight);
        transform.Translate(xMove, 0, zMove);
        transform.Rotate(0, deltaPositon.x * camRotateSpeed,0);  
        xMove = 0;
        zMove = 0;

    }
}
