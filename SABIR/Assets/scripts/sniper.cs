using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class sniper : MonoBehaviour
{
    public GameObject bulletEffect;

    public RawImage scope;
   
    [Header("Sniper Values of bullet")]
    public int SniperMagazine;
    public int SniperBulletReserve;
    public int SniperBulletInMagasine;


    [Header("Bullet Text")]
    public TMP_Text textBulletInMagasine;
    public TMP_Text textBulletReserves;


    private float fireDelay = 0;

    [Header("")]
    public AudioClip SniperFireSound;
    private ParticleSystem muzzleParticle;
    private AudioSource SniperAudioSource;
    private Animation SniperAnimation;


    [Range(0f, 100f)]
    public float SniperDMG;
    // Start is called before the first frame update
    void Start()
    {

        scope.gameObject.SetActive(false);
        muzzleParticle = GetComponentInChildren<ParticleSystem>();
        SniperAudioSource = GetComponent<AudioSource>();
        SniperAnimation = GetComponent<Animation>();


    }

    // Update is called once per frame
    void Update()
    {


        textBulletInMagasine.text = SniperBulletInMagasine.ToString();
        textBulletReserves.text = SniperBulletReserve.ToString();



        zoom();
        fire();

    }

    void zoom()
    {
        if (Input.GetMouseButtonDown(1))
        {
            scope.gameObject.SetActive(true);
            Camera.main.GetComponent<Camera>().fieldOfView = 10.0f;
        }
        else if (Input.GetMouseButtonUp(1))
        {
            scope.gameObject.SetActive(false);
            Camera.main.GetComponent<Camera>().fieldOfView = 60.0f;


        }
            
    }

    public void CloseZoom()
    {
        scope.gameObject.SetActive(false);
        Camera.main.GetComponent<Camera>().fieldOfView = 60.0f;
    }

    void fire()
    {
        if (Input.GetMouseButton(0) && fireDelay < Time.time && SniperBulletInMagasine != 0)
        {
            --SniperBulletInMagasine;
            SniperBulletInMagasine = Mathf.Clamp(SniperBulletInMagasine, 0, SniperMagazine);


            if (SniperBulletInMagasine == 0 && SniperBulletReserve != 0)
            {


                if (SniperBulletReserve < SniperBulletInMagasine)
                {
                    SniperBulletInMagasine = SniperBulletReserve;
                    SniperBulletReserve = 0;
                }
                else
                {
                    SniperBulletInMagasine = SniperMagazine;
                    SniperBulletReserve -= SniperMagazine;
                }
                // fix this bug
               
            }
            SniperBulletReserve = Mathf.Clamp(SniperBulletReserve, 0, 120);



            fireDelay = Time.time + 1.7f;

            Debug.Log("BANG!");
            SniperAnimation.Play("sniperAnimation");
            muzzleParticle.Play();

            // m4AudioSource.clip = m4FireSound;

            // m4AudioSource.Play();

            AudioSource.PlayClipAtPoint(SniperFireSound, transform.position);


            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f));
            RaycastHit raycastHit;
            if (Physics.Raycast(ray, out raycastHit, 100f))
            {
                if (raycastHit.transform.tag == "Zombie")
                {
                    zombi dusman = raycastHit.transform.GetComponentInParent<zombi>();
                    dusman.HealthHit(SniperDMG, raycastHit);

                }
                else
                {
                    GameObject tmpOBJ = Instantiate(bulletEffect, raycastHit.point, Quaternion.LookRotation(raycastHit.normal));
                    Destroy(tmpOBJ, 4f);

                }
            }
        }
    }
}
