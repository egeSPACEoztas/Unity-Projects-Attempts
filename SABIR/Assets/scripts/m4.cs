using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class m4 : MonoBehaviour
{
    public GameObject bulletEffect;


    [Header("m4 Values of bullet")]
    public int m4Magazine;
    public int m4BulletReserve;
    public int m4BulletInMagasine;


    [Header("Bullet Text")]
    public TMP_Text textBulletInMagasine;
    public TMP_Text textBulletReserves;

   
    private float fireDelay = 0;

    [Header("")]
    public AudioClip m4FireSound;
    private ParticleSystem muzzleParticle;
    private AudioSource m4AudioSource;
    private Animation fireAnimation;

    [Range(0f,100f)]
    public float m4DMG;
    // Start is called before the first frame update
    void Start()
    {


        muzzleParticle = GetComponentInChildren<ParticleSystem>();
        m4AudioSource = GetComponent<AudioSource>();
        fireAnimation = GetComponent<Animation>();
        

    }

    // Update is called once per frame
    void Update()
    {

      
        textBulletInMagasine.text = m4BulletInMagasine.ToString();
        textBulletReserves.text = m4BulletReserve.ToString();

        


        fire();
        
    }

     void fire()
    {
        if (Input.GetMouseButton(0) && fireDelay<Time.time  && m4BulletInMagasine!=0)
        {
            --m4BulletInMagasine;
            m4BulletInMagasine = Mathf.Clamp(m4BulletInMagasine, 0, m4Magazine);
            

            if(m4BulletInMagasine == 0 && m4BulletReserve!= 0)
            {

                if (m4BulletReserve < m4Magazine)
                {
                    m4BulletInMagasine = m4BulletReserve;
                    m4BulletReserve = 0;
                }
                else
                {
                    m4BulletInMagasine = m4Magazine;
                    m4BulletReserve -= m4Magazine;
                }

                // fix this bug
                m4BulletInMagasine = m4Magazine;
                m4BulletReserve -= m4Magazine;
            }
            m4BulletReserve = Mathf.Clamp(m4BulletReserve, 0, 120);



             fireDelay  = Time.time + 0.1f;

            Debug.Log("BANG!");

            muzzleParticle.Play();

            fireAnimation.Play("m4Animation");

            // m4AudioSource.clip = m4FireSound;

            // m4AudioSource.Play();

            AudioSource.PlayClipAtPoint(m4FireSound, transform.position);


           Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f));
            RaycastHit raycastHit;
            if(Physics.Raycast(ray,out raycastHit, 100f))
            {
                if(raycastHit.transform.tag == "Zombie")
                {
                    zombi dusman = raycastHit.transform.GetComponentInParent<zombi>();
                    dusman.HealthHit(m4DMG,raycastHit);

                }
                else
                {
                    GameObject tmpOBJ = Instantiate(bulletEffect, raycastHit.point, Quaternion.LookRotation(raycastHit.normal));
                    Destroy(tmpOBJ,4f);
                   
                }
            }
        }
    }

}
