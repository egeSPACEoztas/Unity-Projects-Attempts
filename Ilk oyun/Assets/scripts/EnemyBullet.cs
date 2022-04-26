using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyBullet : MonoBehaviour
{
    // Start is called before the first frame update
    public float extraSpeed;
    private GameObject obj;
    private Agemisi agemisi;
    void Start()
    {
        //burada A gemisi için yapılmış düşman mermisi
        obj = GameObject.FindGameObjectWithTag("düşmanA");
         agemisi = obj.GetComponent<Agemisi>();
    }

    // Update is called once per frame
    void Update()
    {


        transform.Translate(0, 0, -(agemisi.AgemisiHızı + extraSpeed) * Time.deltaTime );
        if (transform.position.y <= -3.7f)
            Destroy(gameObject);
    }

}
