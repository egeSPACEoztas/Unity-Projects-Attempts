using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egemisi : MonoBehaviour
{



    public GameObject slavedShip;
    public bool slaved;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void Orbit()
    {

    }

    public void UnSlave()
    {
        slavedShip.GetComponent<Dgemisi>().numOfSlaveShips--;
        slaved = false;


    }

    public void SlaveTo()
    {
        if (slaved)
            return;
        else
        {
            if (FindClosestAvailableShipD() != null)
            {
                slavedShip = FindClosestAvailableShipD();
                slavedShip.GetComponent<Dgemisi>().numOfSlaveShips++;
                slaved = true;
            }
            else if (FindClosestAvailableShipC() != null)
            {
                slavedShip = FindClosestAvailableShipD();
                slavedShip.GetComponent<Cgemisi>().numOfSlaveShips++;
                slaved = true;
            }
        }
    }


    public GameObject FindClosestAvailableShipD()
    {

        // burada bir yerde bizim bulduğumuz geminin dolu olup olmadığına bakmamız lazım
        GameObject[] shipsInSpace;
        shipsInSpace = GameObject.FindGameObjectsWithTag("düşmanD");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in shipsInSpace)
        {
            if (go.GetComponent<Dgemisi>().numOfSlaveShips<10)
            {
                Vector3 diff = go.transform.position - position;
                float curDistance = diff.sqrMagnitude;
                if (curDistance < distance)
                {
                    closest = go;
                    distance = curDistance;
                }
            }
           
        }
        return closest;
    }




    public GameObject FindClosestAvailableShipC()
    {
        GameObject[] shipsInSpace;
        shipsInSpace = GameObject.FindGameObjectsWithTag("düşmanC");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in shipsInSpace)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }
}
