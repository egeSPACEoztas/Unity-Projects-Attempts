using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TouchManager : MonoBehaviour
{

    public GameObject YellowCube;
    public GameObject BlueCube;
    public TMP_Text YellowText;
    public TMP_Text BlueText;


    public TMP_Text YellowCubeIdText;
    public TMP_Text YellowCubeDeltaTimeText;
    public TMP_Text YellowCubeDeltaPositionText;
    public TMP_Text YellowCubeTapCountText;
    public TMP_Text YellowCubePressureText;
    public TMP_Text YellowCubeRadiusText;
    public TMP_Text isTouchPressureSupported;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

#if UNITY_STANDALONE

#elif UNITY_ANDROID
        //if (false)// Input.touchCount == 1
        //{

        //    if (true)
        //    {
        //        Touch YellowCubeTouch = Input.touches[0];


        //        switch (YellowCubeTouch.phase)
        //        {
        //            case TouchPhase.Began:
        //                YellowCubeMovement(YellowCubeTouch);

        //                YellowText.text = "Touch Began";

        //                break;
        //            case
        //                TouchPhase.Moved:
        //                YellowCubeMovement(YellowCubeTouch);

        //                YellowText.text = "Single Touch Moved";

        //                break;
        //            case TouchPhase.Stationary:
        //                YellowCubeMovement(YellowCubeTouch);

        //                YellowText.text = "Single Touch Stationary";

        //                break;
        //            case TouchPhase.Ended:
        //                YellowCubeMovement(YellowCubeTouch);

        //                YellowText.text = "Single Touch Ended";

        //                break;
        //            case TouchPhase.Canceled:
        //                YellowCubeMovement(YellowCubeTouch);

        //                YellowText.text = "Single Touch Cancled";

        //                break;
        //        }
        //    }



        //}
        //else if(Input.touchCount == 2)
        //{



        
            Touch YellowCubeTouch = Input.touches[0];

        YellowCubeIdText.text = "FingerID: " + YellowCubeTouch.fingerId;
        YellowCubeDeltaTimeText.text = "DeltaTime: " + YellowCubeTouch.deltaTime;
        YellowCubeDeltaPositionText.text = "DeltaPosition: " + YellowCubeTouch.deltaPosition;
        YellowCubeTapCountText.text = "TapCount: " + YellowCubeTouch.tapCount;
        YellowCubePressureText.text = "Pressure: " + YellowCubeTouch.pressure;
        YellowCubeRadiusText.text = "Radius: " + YellowCubeTouch.radius;
        isTouchPressureSupported.text = "PressureSuppor :" + Input.touchPressureSupported;




            switch (YellowCubeTouch.phase)
            {
                case TouchPhase.Began:
                    YellowCubeMovement(YellowCubeTouch);

                YellowText.text = "Yellow Touch Began"+ YellowCubeTouch.position;

                    break;
                case
                    TouchPhase.Moved:
                    YellowCubeMovement(YellowCubeTouch);

                    YellowText.text = "Yellow Touch Moved" + YellowCubeTouch.position;

                    break;
                case TouchPhase.Stationary:
                    YellowCubeMovement(YellowCubeTouch);

                    YellowText.text = "Yellow Touch Stationary" + YellowCubeTouch.position;

                    break;
                case TouchPhase.Ended:
                    YellowCubeMovement(YellowCubeTouch);

                    YellowText.text = "Yellow Touch Ended" + YellowCubeTouch.position;

                    break;
                case TouchPhase.Canceled:
                    YellowCubeMovement(YellowCubeTouch);

                    YellowText.text = "Yellow Touch Cancled" + YellowCubeTouch.position;

                    break;
            }

            Touch BlueCubeTouch = Input.touches[1];


            switch (BlueCubeTouch.phase)
            {
                case TouchPhase.Began:
                    BlueCubeMovement(BlueCubeTouch);

                    YellowText.text = "Blue Touch Began" + BlueCubeTouch.position;

                    break;
                case
                    TouchPhase.Moved:
                    BlueCubeMovement(BlueCubeTouch);

                    BlueText.text = "Blue Touch Moved" + BlueCubeTouch.position;

                    break;
                case TouchPhase.Stationary:
                    BlueCubeMovement(BlueCubeTouch);

                    BlueText.text = "Blue Touch Stationary" + BlueCubeTouch.position;

                    break;
                case TouchPhase.Ended:
                    BlueCubeMovement(BlueCubeTouch);

                    BlueText.text = "Blue Touch Ended" + BlueCubeTouch.position;

                    break;
                case TouchPhase.Canceled:
                    BlueCubeMovement(BlueCubeTouch);

                    BlueText.text = "Blue Touch Cancled" + BlueCubeTouch.position;

                    break;
            }
        //}
#endif

    }

    private void YellowCubeMovement(Touch YellowCubeTouch)
    {
        YellowCube.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(YellowCubeTouch.position.x, YellowCubeTouch.position.y, 10));

    }
    private void BlueCubeMovement(Touch BlueCubeTouch)
    {
        BlueCube.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(BlueCubeTouch.position.x, BlueCubeTouch.position.y, 10));

    }
}