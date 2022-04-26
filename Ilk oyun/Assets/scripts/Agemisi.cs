using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agemisi : MonoBehaviour
{
    // Start is called before the first frame update
    public float AgemisiHızı;
    public GameObject bullet;
    public Vector3 tmpV3;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("ateşEtme",0.3f,2f);
    }

    // Update is called once per frame
    void Update()
    {
        

        transform.Translate(0,0, -AgemisiHızı * Time.deltaTime);
        if (transform.position.y <= -3.7f)
            Destroy(gameObject);
    }

    private void ateşEtme()
    {
        Instantiate(bullet, transform.position+tmpV3,transform.rotation);
    }
}
