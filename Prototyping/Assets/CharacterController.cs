using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterController : MonoBehaviour
{
    public NavMeshAgent Agent;
    public Transform Target;
    public Animator animator;
    public LineRenderer line;
    int State;
    bool Sneak , HasCover , isSellected;

    void Update()
    {
        Target.Rotate(0f,1f,0f);
        Agent.destination = Target.position;
        Debug.Log(Agent.isOnOffMeshLink);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Sneak)
            {
                Sneak = false;
            }

            else
            {
                Sneak = true;
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Physics.Raycast(ray, out hit);
            if(hit.transform.tag != "Character")
            {
                isSellected = true;
                Target.position = hit.point;
            }

            else
            {
                //isSellected = false;
            }
            //Target.rotation = Quaternion.LookRotation(hit.normal);
        }

        if(Agent.isOnOffMeshLink)
        {
            animator.SetInteger("State", 5);
        }

        else if(Agent.hasPath)
        {
            Agent.acceleration = 450;
            if(Sneak)
            {
                animator.SetInteger("State", 3);
            }

            else
            {
                animator.SetInteger("State", 1);
            }
        }

        else
        {
            Agent.acceleration = 0;
            if(HasCover)
            {
                animator.SetInteger("State", 6);
            }

            else if(!Sneak)
            {
                animator.SetInteger("State", 4);
            }

            else if(Sneak)
            {
                animator.SetInteger("State", 6);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Cover")
        {
            HasCover = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Cover")
        {
            HasCover = false;
        }
    }
}