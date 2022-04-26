using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{


    public float playerSpeed;
    private CharacterController cc;
    private Vector3 movement;
    public float sensivity;

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
        
    }

    // Update is called once per frame
    void Update()
    {
        hareket();
        mauseKilit();
        bakış();

    }


    public void hareket()
    {
        movement = new Vector3(Input.GetAxis("horizontal") * Time.deltaTime * playerSpeed, 0.0f, Input.GetAxis("Vertical") * Time.deltaTime * playerSpeed);
        movement = transform.TransformDirection(movement);
        cc.Move(movement);
    }

    public void bakış()
    {
        Camera.main.transform.Rotate(Input.GetAxis("Mouse Y") * Time.deltaTime * sensivity, 0f, 0f);


        transform.Rotate(0f, Input.GetAxis("Mouse X") * Time.deltaTime * sensivity, 0f);

    }

    public void mauseKilit()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
