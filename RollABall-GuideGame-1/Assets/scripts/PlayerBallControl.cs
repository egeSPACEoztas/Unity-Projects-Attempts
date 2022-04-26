using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerBallControl : MonoBehaviour
{
    private SphereCollider sphereCollider;
    private int doubleJumpable;
    private Rigidbody sphereRigid;
    private bool graviticBool;



    public float rotationSpeed;
    public float goSpeed;
    public float jumpStrength;
    public float graviticStrength;

    // Start is called before the first frame update
    void Start()
    {
        //graviticBool = false;
        //sphereCollider = gameObject.GetComponent<SphereCollider>();
        //sphereRigid = gameObject.GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //GraviticEffect();
        //Movement();
        
        //Jump();
        
    }

   

    //public void Movement()// rigid body is not used because it would interferer with transfor.translate funct. so must do self gravity
    //{

    //    transform.Translate(Input.GetAxis("Horizontal") * goSpeed * Time.deltaTime, 0, Input.GetAxis("Vertical") * goSpeed * Time.deltaTime, Space.World);

    //}

    //public void Jump()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space) && doubleJumpable <2)// her şekilde yukarı atıyor
    //    {//yere degmesi lazım
    //        sphereRigid.AddRelativeForce(0,  jumpStrength * 10,0,ForceMode.Impulse);

    //    }
    //}

    //private void GraviticEffect()
    //{
    //    if(!graviticBool)
    //        sphereRigid.AddRelativeForce(0, -graviticStrength * 9, 0, ForceMode.Force);
    //}



    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Platform"))
    //    {
    //        doubleJumpable = 0;
    //        sphereRigid.useGravity = false;
    //        sphereRigid.velocity = Vector3.zero;
    //        sphereRigid.angularVelocity = Vector3.zero;
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.CompareTag("Platform"))
    //    {
    //      sphereRigid.useGravity = true;
    //    }
    //}
}
