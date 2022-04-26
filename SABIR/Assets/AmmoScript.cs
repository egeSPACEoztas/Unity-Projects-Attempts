using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoScript : MonoBehaviour
{

    public float turnVar;
    public float floatVar;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        TurnMethod();
        FloatMethod();
    }

    void TurnMethod()
    {
        transform.Rotate(0, turnVar, 0, Space.World);

    }

    void FloatMethod()
    {
        if (transform.position.y > 1.0f)
        {
            floatVar = -floatVar;
        }
        else if (transform.position.y < 0.5f)
        {
            floatVar = -floatVar;
        }

        transform.Translate(0, floatVar, 0);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            m4 m4 = other.gameObject.GetComponent<player>().guns[0].GetComponent<m4>();
            m4.m4BulletInMagasine = m4.m4Magazine;
            m4.m4BulletReserve = 3 * m4.m4Magazine;

            Shotgun shotgun = other.gameObject.GetComponent<player>().guns[1].GetComponent<Shotgun>();
            shotgun.SGBulletInMagasine = shotgun.SGMagazine;
            shotgun.SGBulletReserve = 3 * shotgun.SGMagazine;

            sniper sniper = other.gameObject.GetComponent<player>().guns[2].GetComponent<sniper>();
            sniper.SniperBulletInMagasine = sniper.SniperMagazine;
            sniper.SniperBulletReserve = 3 * sniper.SniperMagazine;

            Destroy(gameObject);
        }

    }
}