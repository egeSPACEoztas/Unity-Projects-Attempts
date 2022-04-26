using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float BulletForce;
    Rigidbody BulletBody;

    private AudioSource audioSource;
    public AudioClip explosion;
    private int aPuanı = 70;
    private int bPuanı = 100;
    private int cPuanı = 300;
    private int büyükMeteorPuanı = 50;
    private int küçükMeteoraPuanı = 20;
    public GameObject explosionEffect;



    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.volume = 0.4f;
        audioSource.clip = explosion;
        
        BulletBody = gameObject.AddComponent<Rigidbody>();
        BulletBody.useGravity = false;
        BulletBody.AddForce(transform.forward*BulletForce);
        Destroy(gameObject, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y >= 200.2)
            Destroy(gameObject);
    }


    private void patlama(Collider other)
    {
        GameObject obj = Instantiate(explosionEffect, other.transform);
        obj.transform.localPosition = Vector3.zero;
        obj.transform.SetParent(null);

        Destroy(obj, 2f);
    }

    private void OnTriggerEnter(Collider other)
    {
        


        switch (other.tag)
        {
            case "meteor":
                audioSource.Play();
                //Destroy(gameObject);
                Destroy(other.gameObject);
                if (other.name == "büyük(Clone)")
                    GameManager.score += büyükMeteorPuanı;
                else if (other.name == "küçük(Clone)")
                    GameManager.score += küçükMeteoraPuanı;
                break;
                
            case "düşmanA":
                audioSource.Play();
                // Destroy(gameObject);
                Destroy(other.gameObject);
                GameManager.score += aPuanı;
                patlama(other);
                break;
            case "düşmanB":
                audioSource.Play();
                // Destroy(gameObject);
                Destroy(other.gameObject);
                GameManager.score += bPuanı;
                patlama(other);
                break;
            case "düşmanC":
                audioSource.Play();
                other.GetComponent<Cgemisi>().health--;
                if (other.GetComponent<Cgemisi>().health == 0)
                {
                    Destroy(other.gameObject);
                    GameManager.score += cPuanı;
                    patlama(other);
                }
                break;
            
          /*  default:
                Destroy(gameObject);
                Destroy(other.gameObject);
                break;
          */
        }
        
        
    }
}
