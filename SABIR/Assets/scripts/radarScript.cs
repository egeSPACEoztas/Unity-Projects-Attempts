using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class radarScript : MonoBehaviour
{

    private Quaternion lhs;
    private Quaternion rhs;
    private RaycastHit ZombieHit;

    // Start is called before the first frame update
    void Start()
    {
        //GameObject lightGameObject = new GameObject("The Light");
    }

    // Update is called once per frame
    void Update()
    {
        radar();
    }

    void radar()
    {
        float arcAngle = 120.0f;
        int numLines = 120;
        for (int l = 0; l < numLines; l++)
        {
            Vector3 shootVec = transform.rotation * Quaternion.AngleAxis(-1 * arcAngle / 2 + (l * arcAngle / numLines), Vector3.up) * Vector3.forward;
            RaycastHit hit;
           
            if (Physics.Raycast(transform.position, shootVec, out hit, 100f))
            {
                if(hit.transform.tag == "Zombie")
                {
                    if(hit.collider.gameObject.GetComponent<Light>()== null)
                    {
                        hit.collider.gameObject.AddComponent<Light>();
                       
                    }

                    if (hit.distance < 4.7f)
                    {
                        hit.collider.gameObject.GetComponent<Light>().color = Color.red;
                    }
                    else
                        hit.collider.gameObject.GetComponent<Light>().color = Color.blue;
                    // Set color and position


                    //hit.collider.gameObject.GetComponent<Light>().transform.position = hit.point;



                }
                Debug.DrawLine(transform.position, hit.point, Color.green);
            }
        }
    }
}
