using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class bulet : NetworkBehaviour
{

    private Rigidbody bulletRigid;
    public float forceMag;
    public int Damage;
    // Start is called before the first frame update
    void Start()
    {
        if (!isServer)
            return;
        bulletRigid = GetComponent<Rigidbody>();
        bulletRigid.AddForce(transform.up * Time.fixedDeltaTime * forceMag);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        TankerScript tanker = other.GetComponent<TankerScript>();
        if (tanker != null)
        {
            tanker.tankerHealth(Damage);

        }
        
    }
}
