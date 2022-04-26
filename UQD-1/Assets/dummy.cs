using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dummy : MonoBehaviour
{

    public Animator animator;

    public GameObject player;
    public float sensivity;
    public float playerSpeed;
    private CharacterController cc;
    public Vector3 movement;
    public float yValue;

    // Start is called before the first frame update
    void Start()
    {




        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        robotRun();
    }

    void robotRun()
    {

        movement = new Vector3(Input.GetAxis("Horizontal") * Time.deltaTime * playerSpeed, yValue, Input.GetAxis("Vertical") * Time.deltaTime * playerSpeed);


        movement = transform.TransformDirection(movement);

        cc.Move(movement);

        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {

            Debug.Log("Walk");
            animator.SetBool("SwitchIdle", false);
        }
        else
        {
            Debug.Log("Idle");
            animator.SetBool("SwitchIdle", true);
        }

    }

    public void bakış()
    {
       


        transform.Rotate(0f, Input.GetAxis("Mouse Y") * Time.deltaTime * sensivity, 0f);

    }

}
