using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharControler : MonoBehaviour
{
    
    public CharacterController controller;
    private Vector3 playerVelocity;
    private bool isPlayerGrounded;
    private Vector3 target;
    private Vector3 previousPosition;

    private Vector3 _lookDirection;
    private Quaternion _lookRotation;

    [SerializeField]
    public GameObject CameraTarget;
    public float cameraSize;
    public float cameraMaxMoveSize;
    public float sprintSpeedMultiplyer;
    public float cameraMoveSpeed;
    private Vector3 standartCameraPosition;
    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;
    float moveSpeed = 4f;
    public float RotationSpeed = 1f;
    Vector3 north, east;


    // Start is called before the first frame update
    void Start()
    {
        previousPosition = gameObject.transform.position;
        
        standartCameraPosition = CameraTarget.transform.position - gameObject.transform.position;
        north = Camera.main.transform.forward;
        north.y = 0;
        north = Vector3.Normalize(north);
        east = Quaternion.Euler(new Vector3(0, 90, 0)) * north;

    }


   
    // Update is called once per frame
    void Update()
    {
        

        Point();
        RotateCharacter();
        Move();
        CameraMoveOnCharacter();
        previousPosition = gameObject.transform.position;
    }

    private void LateUpdate()
    {
        
    }
    void RotateCharacter()
    {
        //find the vector pointing from our position to the target
        _lookDirection = (target - transform.position).normalized;

        //create the rotation we need to be in to look at the target
        _lookRotation = Quaternion.LookRotation(_lookDirection);

        //rotate us over time according to speed until we are in the required rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * RotationSpeed);
    }

    void Point()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            //if this hits any thing.
            target = hit.point;

            //if this hits an actor or an item. 
            //Curently I am doing else if it doesn't hit anything.
            //Transform objectHit = hit.transform;
            //target = objectHit;
            
        }

    }
    void Move()
    {
        isPlayerGrounded = controller.isGrounded;
        //if (isPlayerGrounded && playerVelocity.y < 0)
        //{
        //    playerVelocity.y = 0f;
        //}

        
        Vector3 moveDirectionForward = transform.right * Input.GetAxis("HorizontalKey");
        Vector3 moveDirectionSide = transform.forward * Input.GetAxis("VerticalKey");

        Vector3 move = moveDirectionForward + moveDirectionSide;
        //move.Normalize();
        

        if (Input.GetKey(KeyCode.LeftShift))
        {
            move = move * sprintSpeedMultiplyer;
        }
        
            controller.Move(move * Time.deltaTime * moveSpeed);



        //this might be buggy watch out
        //gameObject.transform.forward = move;

        print(isPlayerGrounded);

        if (Input.GetKey(KeyCode.Space) && isPlayerGrounded)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }


        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        //Vector3 direction = new Vector3(Input.GetAxis("HorizontalKey"), 0, Input.GetAxis("VerticalKey"));
        //Vector3 sideMovement = east * moveSpeed * Time.deltaTime * Input.GetAxis("HorizontalKey");
        //Vector3 upMovement = north * moveSpeed * Time.deltaTime * Input.GetAxis("VerticalKey");
        //Vector3 heading = Vector3.Normalize(sideMovement + upMovement);

        //transform.forward = heading;
        //transform.position += sideMovement;
        //transform.position += upMovement;
    }
    
    void CameraMoveOnCharacter()
    {
        Vector3 currentRelativeCamneraPosition = CameraTarget.transform.position - gameObject.transform.position;
        Vector3 optimalCameraPosition = gameObject.transform.position + standartCameraPosition;

        if (CameraTarget.transform.position != standartCameraPosition)
        {
            if (previousPosition != gameObject.transform.position)
            {
                CameraTarget.transform.position = Vector3.Lerp(CameraTarget.transform.position, optimalCameraPosition, 0.01f);
                if (cameraSize < cameraMaxMoveSize)
                    Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, cameraMaxMoveSize, 0.00125f);
            }
            else
            {
                Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, cameraSize, 0.0125f);
                CameraTarget.transform.position = Vector3.MoveTowards(CameraTarget.transform.position, optimalCameraPosition, Time.deltaTime * cameraMoveSpeed);

            }
               

        }
        

    }
}
