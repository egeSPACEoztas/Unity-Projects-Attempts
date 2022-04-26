using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleScript : MonoBehaviour
{

    public CharacterController cc;
    public float playerSpeed;
    private Vector3 movement;
    private float yMove;
    public GameObject panel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        hareket();
    }

    void hareket()
    {
        movement = new Vector3(Input.GetAxis("Horizontal") * Time.deltaTime * playerSpeed, yMove, Input.GetAxis("Vertical") * Time.deltaTime * playerSpeed);

        movement = transform.TransformDirection(movement);

        cc.Move(movement);
    }
}
