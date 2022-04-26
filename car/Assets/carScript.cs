using TMPro;
using UnityEngine;
public class carScript : MonoBehaviour
{

    public WheelCollider[] wheels;
    public Transform[] wheelPosition;
    private Rigidbody carRigid;



    public GameObject cameraPosition;
    public float cameraSmoother;
    public Vector3 lookingVector;
    
    public Transform leftRay45;
    public float leftRay45Lenght;

    public Transform rightRay45;
    public float rightRay45Lenght;

    public Transform frontRayA;
    public float frontRayALenght;

    public Transform frontRayB;
    public float frontRayBLenght;

    public Transform frontRayC;
    public float frontRayCLenght;
    
    public Transform frontRayD;
    public float frontRayDLenght;

    public Transform frontRayE;
    public float frontRayELenght;

    private Quaternion q;
    private Vector3 v;

    public float MotorTorque;
    public float steeringAngle;
    public int steeringDirection;
    public int steeringAngleMax;
    public float carBrakeTorque;

    private float smoothingLeft1 = 0f;
    private float smoothingLeft2 = 0f; 
    private float smoothingRight1 = 0f; 
    private float smoothingRight2 = 0f;

    public float carSpeedKMH;
    public float carSpeedMillesPerHour;
    public float carSpeedMS;
    private const float KMH = 3.6f;
    private const float MH = 2.237f;
    

    public TMP_Text text1;
    public TMP_Text text2;
    public TMP_Text text3;
    

    // Start is called before the first frame update
    void Start()
    {
        AlingFrontRays();
        carRigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //wayFind();
        ConvertUnits();
        CarSpeedUIShow();
        RayLeft();
        RayRight();
    }

    private void FixedUpdate()
    {
        carMovement();
        wheelAlignment();
        
        if (Input.GetKeyDown(KeyCode.Space)) {
            breakCar();
        }

        cameraMovement();

    }

    void carMovement()
    {
        for(int i=2; i <= 3; ++i)
        {
            wheels[i].motorTorque = MotorTorque;
        }              

        for(int i=0; i<=1; ++i)
        {
            wheels[i].steerAngle = steeringAngle;
        }

    }

    void wheelAlignment()
    {
        for(int i = 0; i < wheels.Length; ++i)
        {
            wheels[i].GetWorldPose(out v, out q);
            wheelPosition[i].rotation = q;
            wheelPosition[i].position = v;
        }
    }


    void wayFind()
    {

        //sağ ray
        Debug.DrawRay(rightRay45.position, rightRay45.forward * rightRay45Lenght, Color.red, 1.0f);
        if(Physics.Raycast(rightRay45.position, rightRay45.forward, rightRay45Lenght)){
            
            steeringDirection = -1;

        }
        else
        {   
            if(steeringDirection!=1)
                steeringDirection = 0; 
        }

        //sol ray
        Debug.DrawRay(leftRay45.position, leftRay45.forward * leftRay45Lenght, Color.green, 1.0f);
        if (Physics.Raycast(leftRay45.position, leftRay45.forward, leftRay45Lenght))
        {

            steeringDirection = 1;
        }
        else
        {
            if (steeringDirection != -1)
                steeringDirection = 0;
        }
    }


    //private void RayLeft()
    //{
    //    Debug.DrawRay(leftRay45.transform.position, leftRay45.transform.forward * leftRay45Lenght, Color.yellow, 1.0f);
    //    if(Physics.Raycast(leftRay45.transform.position, leftRay45.transform.forward, leftRay45Lenght))
    //    {

    //        smoothingLeft1 += 0.01f;
    //        smoothingLeft1 = Mathf.Clamp(smoothingLeft1, 0, 1);
    //        smoothingLeft1 = Mathf.Lerp(steeringAngle, steeringAngleMax, smoothingLeft1);
    //    }
    //    else
    //    {
    //        smoothingLeft1 = 0;
    //        if (steeringAngle > 0)
    //        {
    //            smoothingLeft2 += 0.01f;
    //            smoothingLeft2 = Mathf.Clamp(smoothingLeft2, 0, 1);
    //            smoothingLeft2 = Mathf.Lerp(steeringAngle, 0, smoothingLeft2);
    //        }
    //        else
    //            smoothingLeft2 = 0;
    //    }
    //}

    //private void RayRight()
    //{
    //    Debug.DrawRay(rightRay45.transform.position, rightRay45.transform.forward * rightRay45Lenght, Color.red, 1.0f);
    //    if (Physics.Raycast(rightRay45.transform.position, rightRay45.transform.forward, rightRay45Lenght))
    //    {

    //        smoothingRight1 += 0.01f;
    //        smoothingRight1 = Mathf.Clamp(smoothingRight1, 0, 1);
    //        smoothingRight1 = Mathf.Lerp(steeringAngle, -steeringAngleMax, smoothingRight1);
    //    }
    //    else
    //    {
    //        smoothingRight1 = 0;
    //        if (steeringAngle < 0)
    //        {
    //            smoothingRight2 += 0.01f;
    //            smoothingRight2 = Mathf.Clamp(smoothingRight2, 0, 1);
    //            smoothingRight2 = Mathf.Lerp(steeringAngle, 0, smoothingRight2);
    //        }
    //        else
    //            smoothingRight2 = 0;

    //    }
    //}



    private void RayLeft()
    {
        
        Debug.DrawRay(leftRay45.position, leftRay45.transform.forward * leftRay45Lenght, Color.blue, 1.0f);
        if(Physics.Raycast(leftRay45.position, leftRay45.forward, leftRay45Lenght))
        {
            smoothingLeft1 += 0.01f;
            smoothingLeft1 = Mathf.Clamp(smoothingLeft1, 0, 1);
            //smoothingLeft1 = Mathf.Clamp01(smoothingLeft1);
            steeringAngle = Mathf.Lerp(steeringAngle, steeringAngleMax, smoothingLeft1);
            //Debug.Log("Left Ray Activated");         
        }
        else
        {
            smoothingLeft1 = 0;
            if (steeringAngle > 0)
            {
                smoothingLeft2 += 0.01f;
                smoothingLeft2 = Mathf.Clamp(smoothingLeft2, 0, 1);
                steeringAngle = Mathf.Lerp(steeringAngle, 0, smoothingLeft2);
                //Debug.Log("Left Ray Deactivated");
            }
            else
                smoothingLeft2 = 0f;
        }

    }

    private void RayRight() //krımızı
    {
        
        Debug.DrawRay(rightRay45.transform.position, rightRay45.transform.forward * rightRay45Lenght, Color.red, 1);
        if (Physics.Raycast(rightRay45.transform.position, rightRay45.transform.forward, rightRay45Lenght))
        {

            smoothingRight1 += .01f;
            smoothingRight1 = Mathf.Clamp(smoothingRight1, 0, 1);
            steeringAngle = Mathf.Lerp(steeringAngle, -steeringAngleMax, smoothingRight1);
            //Debug.Log("Right Ray Activated");

        }
        else if(steeringAngle != 1)
        {
            smoothingRight1 = 0;
            if (steeringAngle < 0)
            {

                smoothingRight2 += .01f;
                smoothingRight2 = Mathf.Clamp(smoothingRight2, 0, 1);
                steeringAngle = Mathf.Lerp(steeringAngle, 0, smoothingRight2);
                //Debug.Log("Right Ray Deactivated");
            }
            else
                smoothingRight2 = 0f;
        }

    }

    private void RaysForward()
    {
        Debug.DrawRay(frontRayA.transform.position, frontRayA.transform.forward * frontRayALenght, Color.red, 1);
        Debug.DrawRay(frontRayB.transform.position, frontRayB.transform.forward * frontRayBLenght, Color.red, 1);
        Debug.DrawRay(frontRayC.transform.position, frontRayC.transform.forward * frontRayCLenght, Color.red, 1);
        Debug.DrawRay(frontRayD.transform.position, frontRayD.transform.forward * frontRayDLenght, Color.red, 1);
        Debug.DrawRay(frontRayE.transform.position, frontRayE.transform.forward * frontRayELenght, Color.red, 1);




    }


    private void breakCar()
    {
        for(int i= 2; i <= 3; i++)
        {
            wheels[i].brakeTorque = carBrakeTorque;
        }
    }

    private void ConvertUnits()
    {

        carSpeedKMH = carRigid.velocity.magnitude * KMH;
        carSpeedMillesPerHour = carRigid.velocity.magnitude * MH;
        carSpeedMS = carRigid.velocity.magnitude;

    }

    private void CarSpeedUIShow()
    {

        text1.text = carSpeedKMH.ToString();
        text2.text = carSpeedMillesPerHour.ToString();
        text3.text = carSpeedMS.ToString();
    }


    void cameraMovement()
    {
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, cameraPosition.transform.position,cameraSmoother * Time.fixedDeltaTime);
        Camera.main.transform.LookAt(transform.position + lookingVector );
    }
    private void AlingFrontRays()
    {


        // arabanın önündeki sensörlerin pozisyonu
        // <<sol A^^^^B^^^^C^^^^D^^^^E sag>>
        frontRayA.transform.position = (leftRay45.transform.position + rightRay45.transform.position * 0) / 2;
        frontRayB.transform.position = (leftRay45.transform.position + rightRay45.transform.position / 2) / 2;
        frontRayC.transform.position = (leftRay45.transform.position + rightRay45.transform.position) / 2;
        frontRayD.transform.position = (leftRay45.transform.position / 2 + rightRay45.transform.position) / 2;
        frontRayE.transform.position = (leftRay45.transform.position * 0 + rightRay45.transform.position) / 2;
    }
}

