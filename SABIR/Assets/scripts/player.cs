using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;




public class player : MonoBehaviour
{


    public GameObject[] guns;



    private float Ydirection;

    public float playerHP;

    public TMP_Text HP;

    public TMP_Text TopText;

    public GameObject EnterPanel;

    private float Mesafe;
    public float carLookRayLength;
    public float playerSpeed;
    private CharacterController cc;
    
    
    private Vector3 movement;

    public float sensivity;

    public static float jumpPadMultiplyer;
    private  float  yMove;
    public float jumpVariable;
    public float gravityVariable;
    
    // Start is called before the first frame update
    void Start()
    {
        jumpPadMultiplyer = 1f;
        cc = GetComponent<CharacterController>();

        HP.text = playerHP.ToString();

    }

    // Update is called once per frame
    void Update()
    {
        hareket();
        mauseKilit();
        bakış();
        DistanceToZombie();
        DistanceToZombie();
        ChooseGun();
        carEnter();
    }


    public void hareket()
    {
        movement = new Vector3(Input.GetAxis("Horizontal") * Time.deltaTime * playerSpeed, yMove, Input.GetAxis("Vertical") * Time.deltaTime * playerSpeed);

        movement = transform.TransformDirection(movement);

        cc.Move(movement);

        Debug.Log(cc.isGrounded);

        if (cc.isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                yMove = jumpVariable * jumpPadMultiplyer;
            }
        }
        else
        {
            yMove -= gravityVariable;
        }
      
    }

    public void bakış()
    {

        Ydirection += Input.GetAxis("Mouse Y");
        Ydirection = Mathf.Clamp(Ydirection, -90, 90);
        Camera.main.transform.localEulerAngles = new Vector3(-Ydirection, 0, 0);

        //Camera.main.transform.Rotate(-Input.GetAxis("Mouse Y") * Time.deltaTime * sensivity, 0f, 0f);


        transform.Rotate(0f, Input.GetAxis("Mouse X") * Time.deltaTime * sensivity, 0f);

    }

    public void ChooseGun()
    {
        //0 m4 1 sg 2 sniper
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            guns[0].SetActive(true);
            guns[1].SetActive(false);
            guns[2].SetActive(false);
            guns[2].GetComponent<sniper>().CloseZoom();
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            guns[0].SetActive(false);
            guns[1].SetActive(true);
            guns[2].SetActive(false);
            guns[2].GetComponent<sniper>().CloseZoom();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) )
        {

            guns[0].SetActive(false);
            guns[1].SetActive(false);
            guns[2].SetActive(true);
        }
    }


    public void mauseKilit()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void DistanceToZombie()
    {
        RaycastHit HitDistance;
        if(Physics.Raycast(transform.position,transform.forward,out HitDistance, 500f))
        {
            if(HitDistance.transform.gameObject.tag == "Zombie")
            {
                Mesafe = Vector3.Distance(transform.position, HitDistance.transform.position);
                TopText.text = " " + Mesafe.ToString("F2") + "m";
            }
        }

       
    }


    public void updateHP(float deltaHP)
    {


        playerHP -= deltaHP;

        HP.text = playerHP.ToString();

    }

    void carEnter()
    {
        RaycastHit carHit;
        Debug.DrawRay(transform.position, transform.forward, Color.red, 1f);
        if (Physics.Raycast(transform.position, transform.forward, out carHit, carLookRayLength))
        {
            if (carHit.transform.gameObject.CompareTag("car"))
            {
                EnterPanel.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    carHit.transform.gameObject.GetComponent<carScript>().enabled = true;
                    carHit.transform.gameObject.GetComponentInChildren<Camera>().enabled = true;
                    

                    gameObject.SetActive(false);
                }

                //buradan sonra girmesi için input beklenecek ondan sonrada giriş algoritması ya çağırılacak yada işlenecek
            }

           

        }
        else
        {
            EnterPanel.SetActive(false);
        }
    }

}
