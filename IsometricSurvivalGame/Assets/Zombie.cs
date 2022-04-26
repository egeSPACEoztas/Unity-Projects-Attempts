using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]

public class Zombie : MonoBehaviour
{

    //Things zombie should do 
        //Notice player.
            //Can see but not hear.
            //Implement hearing
        //loose player.
            //
        //wander
        //run
        //target player
        //idle

    public string playerTag = "Player";

    private NavMeshAgent agent;//This zombie's nav mesh agent
    private Animator Anim;//This zombie's animator
    private Transform player = null;
    private Vector3 wanderManager;//Player position/transform
    private bool wandering=false, idle = true, chasing = false, attacking = false, noticedPlayer; // states of the zombie.
    private Vector3 lastKnownPos;

    public LayerMask checkLayers;//Layers to check when searching for the player (after the check interval)
    private float idleTime;
    private float timeTilNextDecision;
    

    [SerializeField]
    public float hp = 100.0f, 
                maxWanderDist = 4f, 
                minWanderDist = 1f,
                moveSpeed = 0.7f,
                damage = 20f,
                fieldOfView =60f, //FoV of the zombie 
                viewDistance = 30f;// distance zombie notices player.
    

    void SearchPlayer()
    {
        //only search if player or other targets are lost.
        //Curently only searches player, can change if other npc's are added.
        if (player == null)
        {
            //print("Have the player so not gonna search.");
            return;
        }

        RaycastHit hit = new RaycastHit();
        Vector3 checkPosition = player.position - transform.position;
        if ((Vector3.Angle(checkPosition, transform.forward) < fieldOfView) && (Vector3.Distance(player.position,transform.position)<=viewDistance))
        { //Check if player is inside the field of view
            //print("Player is in zombies fov.");
            if (Physics.Raycast(transform.position, checkPosition, out hit, viewDistance, checkLayers))
            {
                if (hit.collider.tag == playerTag)
                {//do this..
                   noticedPlayer = true;
                    idleTime = Random.Range(5.0f, 10.0f);
                    // lastChaseInterval = Time.time + Random.Range(minChase, maxChase);
                }
            }
        }
        else if (meleeDistance())
        {
            noticedPlayer = true;
            Vector3 direction = (player.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 6f);
            //chasePlayer();
            //lastChaseInterval = Time.time + Random.Range(minChase, maxChase);
        }
        else
        {
            noticedPlayer = false;
        }

    }
    bool meleeDistance()
    {
        if (Vector3.Distance(transform.position, player.position) < 2.0f)
        {
            return true;
        }

        return false;
    }

    void chase()
    {
        // if (canRun)
        //Anim.SetBool("isRunning", true);
        // can  implement running later. Right now only needs to move.
        //chasing = true;
        agent.SetDestination(player.position);
        lastKnownPos = player.position;
        agent.isStopped=false;
        //wandering = false;
        //idle = false;

        // in the future add zombie animations, sound efects etc.
    }

    void stopMove()
    {
        // Anim.SetBool("isChasing", false);

        //if (canRun)
        //    Anim.SetBool("isRunning", false);

        //Anim.SetBool("isIdle", true);
        
        agent.isStopped=true;
        
        //eatingBodie = false;
    }

    void Attack(GameObject attackObjcet)
    {
        if (!meleeDistance())
        {
            return;
        }

        //Whole lotta code should be here. For example:
        //Attack animation for zombie.
        //Deal dammage
        //print("Zombie Attacks!");
    }

    void ChooseWanderLocation()
    {
        float randomDegree = Random.Range(1f, 360f);
        float randomWanderDist = Random.Range(minWanderDist, maxWanderDist);
        
        float x = randomWanderDist * Mathf.Cos(randomDegree * Mathf.Deg2Rad);
        float z = randomWanderDist * Mathf.Sin(randomDegree * Mathf.Deg2Rad);
        Vector3 wanderTargetPosition =new  Vector3(x,transform.position.y+0.3f,z);

        print(wanderTargetPosition);
        RaycastHit hit = new RaycastHit();
            print("Wandering");
            if (Physics.Raycast(transform.position, wanderTargetPosition, out hit, randomWanderDist, checkLayers))
            {
            //We hit something then we should only move to that place.

                  wanderManager=hit.transform.position;
            }
        //else there is nothing and we can move fully.
        Transform wantderTargetTransform;

        wanderManager = wanderTargetPosition;
    }
    
    void Wander()
    {
        agent.SetDestination(wanderManager);
        agent.isStopped = false;
    }

    float Idle()
    {


        // Calculates the time until the enemy may decide to change its movement
        idleTime = idleTime - Time.deltaTime;
        idle = true;
        wandering = false;
        chasing = false;
        agent.isStopped = true;
        return idleTime;
    }

    void ZombieStateFunction()
    {
        print("State func");
        SearchPlayer();
        if (noticedPlayer/* or going to the last know pos*/)
        {
            print("noticed player");
            if (meleeDistance())
            {
                stopMove();
                wandering = false;
                idle = false;
                attacking = true;
                chasing = false;
            }
            else
            {
                //chase();
                wandering = false;
                idle = false;
                attacking = false;
                chasing = true;
            }
        }
        else
        {
            print("lost the player");
            if (wandering)
            {
                print("Decided to wander");
                float dist = agent.remainingDistance; 
                if (dist != Mathf.Infinity && agent.pathStatus == NavMeshPathStatus.PathComplete && agent.remainingDistance == 0)
                {
                    
                    agent.isStopped = true;
                    idleTime = Random.Range(5.0f, 10.0f); 
                    Idle();
                    //Arrived.
                }
                else
                {
                    agent.isStopped = false;
                    wandering = true;
                    idle = false;
                    attacking = false;
                    chasing = false;
                }
                //should choose a wander location but not in update loop. 
                //This problem also present in idle.
                //find a way to determine state machine variables out of update loop.
                //maybe implement locks?
            }
            else
            {
                
                 Idle();
                print("idle with time remaining: "+ idleTime);
                // if not wandering it is idle
                if (idleTime  <= 0)
                {
                    print("Is Idle, idle time:" + idle);
                    attacking = false;
                    chasing = false;
                    idle = false;
                    wandering = true;
                    ChooseWanderLocation();
                }
            }
            

        }
        //chasing

    }


    // Start is called before the first frame update
    void Start()
    {

        agent = GetComponent<NavMeshAgent>();
        Anim = GetComponent<Animator>();
        idleTime = Random.Range(5.0f, 10.0f);
        agent.speed = moveSpeed;
        agent.acceleration = moveSpeed * 40;
        agent.angularSpeed = 999;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject newPlayer = GameObject.FindWithTag(playerTag);
        if (newPlayer != null)
            print("Got the player");
        player = newPlayer.transform;
        if (player != null)
            print("Got the player position");
        SearchPlayer();

        ZombieStateFunction();

        if (idle)
        {
            //idle animation
            //idle sound

        }
        else if (wandering)
        {
            Wander();
        }
        else if (chasing)
        {
            chase();
        }
        else if (attacking)
        {
            Attack(player.gameObject);
        }

    }
}
