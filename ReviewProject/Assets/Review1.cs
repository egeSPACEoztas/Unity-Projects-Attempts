using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Review1 : MonoBehaviour
{
    public float moveSpeed;
    public float rotateSpeed;
    public GameObject cannonBall;
    public Transform cannonBallSpawnPoint;
    public float firePower;

    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Fire();
    }


    public void Fire()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            GameObject cnnbll = Instantiate(cannonBall, cannonBallSpawnPoint.position,cannonBallSpawnPoint.rotation);
            cnnbll.GetComponent<Rigidbody>().AddForce(cannonBallSpawnPoint.forward * firePower);
        }
    }

    public void Movement()
    {
        transform.Translate(Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed,0,0);
        transform.Rotate(Input.GetAxis("Vertical") * Time.deltaTime * rotateSpeed, 0, 0);
    }
}
