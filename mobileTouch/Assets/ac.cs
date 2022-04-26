using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ac : MonoBehaviour
{

    public GameObject Cube;
    public TMP_Text AccX;
    public TMP_Text AccY;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        Cube.transform.position += new Vector3(Input.acceleration.x, Input.acceleration.y, 0f);
        AccX.text = "AccX: " + Input.acceleration.x.ToString();
        AccY.text = "AccY: " + Input.acceleration.y.ToString();


    }
}
