using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeScript : MonoBehaviour
{


    public thirdScript touchPad;
    public float sss;

    private Vector2 dir;

    public FixedJoystick fj;

    // Start is called before the first frame update
    void Start()
    {
        //GameObject obj = GameObject.FindGameObjectWithTag("button");

        //touchPad = obj.GetComponent<thirdScript>();
    }

    // Update is called once per frame
    void Update()
    {
        //dir = touchPad.GetDirection();
        //transform.Translate(new Vector2(dir.x, dir.y) * Time.deltaTime * sss);


        dir = Vector3.up * fj.Vertical + Vector3.right * fj.Horizontal;
        transform.position += new Vector3(dir.x, dir.y) * Time.deltaTime * sss;

    }
}
