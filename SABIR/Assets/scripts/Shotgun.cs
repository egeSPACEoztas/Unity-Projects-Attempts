using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Shotgun : MonoBehaviour
{
    public GameObject bulletEffect;


    [Header("SG Values of bullet")]
    public int SGMagazine;
    public int SGBulletReserve;
    public int SGBulletInMagasine;
    public float pompalıForce;

    [Header("Bullet Text")]
    public TMP_Text textBulletInMagasine;
    public TMP_Text textBulletReserves;


    private float fireDelay = 0;

    [Header("")]
    public AudioClip SGFireSound;
    private ParticleSystem muzzleParticle;
    private AudioSource SGAudioSource;
    private Animation ShotAnimation;

    [Range(0f, 100f)]
    public float SgDMG;
    // Start is called before the first frame update
    void Start()
    {


        muzzleParticle = GetComponentInChildren<ParticleSystem>();
        SGAudioSource = GetComponent<AudioSource>();
        ShotAnimation = GetComponent<Animation>();


    }

    // Update is called once per frame
    void Update()
    {


        textBulletInMagasine.text = SGBulletInMagasine.ToString();
        textBulletReserves.text = SGBulletReserve.ToString();




        fire();

    }

    void fire()
    {
        if (Input.GetMouseButton(0) && fireDelay < Time.time && SGBulletInMagasine != 0)
        {
            --SGBulletInMagasine;
            SGBulletInMagasine = Mathf.Clamp(SGBulletInMagasine, 0, SGMagazine);


            if (SGBulletInMagasine == 0 && SGBulletReserve != 0)
            {
                if (SGBulletReserve < SGMagazine)
                {
                    SGBulletInMagasine = SGBulletReserve;
                    SGBulletReserve = 0;
                }
                else 
                {
                    SGBulletInMagasine = SGMagazine;
                    SGBulletReserve -= SGMagazine;
                }
               
                
            }
            SGBulletReserve = Mathf.Clamp(SGBulletReserve, 0, 120);



            fireDelay = Time.time + 1f;

            Debug.Log("BANG!");
            ShotAnimation.Play("shotgunAnime");
            muzzleParticle.Play();

            // m4AudioSource.clip = m4FireSound;

            // m4AudioSource.Play();

            AudioSource.PlayClipAtPoint(SGFireSound, transform.position);


            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f));
            RaycastHit raycastHit;
            if (Physics.Raycast(ray, out raycastHit, 100f))
            {
                Debug.Log("rayhit");
                if (raycastHit.transform.gameObject.CompareTag("Zombie"))
                {
                    Debug.Log("Yep zombie");
                    zombi dusman = raycastHit.transform.GetComponentInParent<zombi>();

                    dusman.HealthHit(SgDMG * 8 / (Mathf.Sqrt(raycastHit.distance)), raycastHit);
                    if (dusman.hp <= 0)
                    {
                        Debug.Log("Hp<0");
                        dusman.findAllRigidBodies(false);
                        raycastHit.transform.gameObject.GetComponent<Rigidbody>().AddForce( transform.root.transform.forward * pompalıForce,ForceMode.Impulse);
                    }
                    
                }
                else
                {

                    Debug.Log("ZOMBI DEĞIL");
                    Debug.Log(raycastHit.transform.gameObject);
                    GameObject tmpOBJ = Instantiate(bulletEffect, raycastHit.point, Quaternion.LookRotation(raycastHit.normal));
                    Destroy(tmpOBJ, 4f);

                }
            }
        }
    }
}
