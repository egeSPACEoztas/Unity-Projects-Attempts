using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class zombi : MonoBehaviour
{
    private NavMeshAgent agent;
    public  float distance;
    private GameObject player;
    private Animator animatör;
    public GameObject bloodEfect;


    public float zDamage;
    public float hp;
   

    // Start is called before the first frame update
    void Start()
    {
        animatör = GetComponent<Animator>();

        agent = GetComponent<NavMeshAgent>();
        
        player = GameObject.FindGameObjectWithTag("Player");
        
        findAllRigidBodies(true);
    }

    // Update is called once per frame
    void Update()
    {
        if(agent.enabled)
            zombiYakınlığı();
     

    }

    public void HealthHit(float damage, RaycastHit hitPoint)
    {
        hp -= damage;
        Instantiate(bloodEfect, hitPoint.point, hitPoint.transform.rotation);
        if( hp <= 0)
        {
            animatör.enabled = false;
            agent.enabled = false;
            findAllRigidBodies(false);
            //Destroy(gameObject);
        }

    }

    void zombiYakınlığı()
    {

        distance = Vector3.Distance(transform.position, player.transform.position);

        //Debug.Log(distance);

        if (distance > 15)
        {
            agent.isStopped = true;
            animatör.SetFloat("distance", distance);
           

           
            // zombie idle
        }
        else if (15 >= distance && distance > 2)
        {
            agent.isStopped=false;
            animatör.SetFloat("distance", distance);
            agent.SetDestination(player.transform.position);
            
        }
        else if (distance <= 2)
        {
             agent.isStopped = true;
             animatör.SetFloat("distance", distance);
            

        }
    }

    public void findAllRigidBodies(bool x)
    {
        Rigidbody[] allRigid = GetComponentsInChildren<Rigidbody>();

        foreach(Rigidbody sample in allRigid)
        {
            sample.isKinematic = x;
        }
    }

    public void AttackPlayer()
    {
        player tmpPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<player>();
        tmpPlayer.updateHP(zDamage);
        Debug.Log("Zombies attack!");
    }
}
