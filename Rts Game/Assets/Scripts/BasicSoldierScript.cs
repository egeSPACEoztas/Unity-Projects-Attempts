using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicSoldierScript : BaseUnit
{
    // Start is called before the first frame update
    public Camera cam;
    public NavMeshAgent agent;
    private Transform previousPosition;


    protected override void Start()
    {
        base.Start();        
        cam = Camera.main;
    }
    // Update is called once per frame
    void Update()
    {
        
        if (!isSelected)
            return;
        else
            MoveBasicSoldier();
    }
    
     public void MoveBasicSoldier()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                //MOVE AGENT
                agent.SetDestination(hit.point);
            }
        }
    }

    private void StopAgentIfStationary() { 
        
    }



}
