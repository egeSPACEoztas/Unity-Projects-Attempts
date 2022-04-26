using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class spaceShipControl : MonoBehaviour
{
    public int health;
    public Image[] healthBar;


    public float gemiHizi;
    private float deger;
    public Vector3 kisitlama;
    private float ilerigeri;
    public Transform bulletPoint;
    public GameObject Bullet;
    public static int bNum;

    private GameObject Obj;
    private GameManager gm;

    public bool timeStop;
    
    void Start()
    {
        Obj = GameObject.FindGameObjectWithTag("GameController");
        gm = Obj.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Rotation();
        ObjeHaraketi();
       
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(Bullet, bulletPoint.position, transform.rotation);
            ++bNum;
        }

    }

    private void ObjeHaraketi()
    {
        kisitlama = transform.position;
        kisitlama.x = Mathf.Clamp(transform.position.x, -9.0f, 9.0f);
        transform.position = kisitlama;

        //Y üzerinde hareket
        /*
        kisitlama = transform.position;
        kisitlama.x = Mathf.Clamp(transform.position.y, -9.0f, 9.0f);
        transform.position = kisitlama;
        transform.Translate(Input.GetAxis("Ve") * Time.deltaTime * gemiHizi, 0, 0);
        */

        //deger += Time.deltaTime;
        if (gm.kilitTime)
            return;
      
            ilerigeri = Mathf.Sin(Time.time) / 500f;
            transform.Translate(Input.GetAxis("Horizontal") * Time.deltaTime * gemiHizi, 0, ilerigeri,Space.World);

        

    }

    private void Rotation()
    {
        if (Input.GetAxis("Horizontal") > 0)
        {
            transform.eulerAngles = new Vector3(-90, 0, -30  *Input.GetAxis("Horizontal"));
        }
        else if(Input.GetAxis("Horizontal") < 0)
        {
            transform.eulerAngles = new Vector3(-90, 0, -30 * Input.GetAxis("Horizontal"));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
       if (other.CompareTag("düşmanB"))
        {
            Destroy(other.gameObject);
            health--;
        }
       else if (other.CompareTag("enemeyBullet"))
        {
            other.gameObject.SetActive(false);
            health--;
        }
        else if (other.CompareTag("CgemisiEnemeyBullet"))
        {
            Destroy(other.gameObject);
            health--;
        }
        else if (other.CompareTag("meteor"))
        {
            Destroy(other.gameObject);
            health--;
        }
        else if (other.CompareTag("düşmanA"))
        {
            Destroy(other.gameObject);
            health--;

        }
        ShowHealth();
    }

    private void ShowHealth()
    {

        if (health == 3)
        {
            
            healthBar[2].gameObject.SetActive(true);
            healthBar[1].gameObject.SetActive(true);
        }
        else if (health == 2)
        {
            healthBar[2].gameObject.SetActive(false);
            healthBar[1].gameObject.SetActive(true);
            healthBar[0].gameObject.SetActive(true);
        }
        else if (health == 1)
        {
            healthBar[1].gameObject.SetActive(false);
            healthBar[0].gameObject.SetActive(true);
        }
        else if (health == 0)
        {
            healthBar[0].gameObject.SetActive(false);
        }
    }
}
