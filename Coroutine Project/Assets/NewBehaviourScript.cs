using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

    public GameObject Obj;
    public GameObject Obj2;

    // Start is called before the first frame update
    //void Start()
    //{
    //    StartCoroutine(WaitAtStart());

    //    -After 0 seconds, prints "Starting 0.0 seconds"
    //   - After 0 seconds, prints "Coroutine started"
    //   - After 2 seconds, prints "Coroutine ended: 2.0 seconds"
    //    print("Starting " + Time.time + " seconds");

    //    Start function WaitAndPrint as a coroutine.

    //   coroutine = WaitAndPrint(2.0f);
    //    StartCoroutine(coroutine);

    //    print("Coroutine started");
    //}

    private IEnumerator WaitAtStart()
    {
        Instantiate(Obj);
        yield return new WaitForSeconds(3);
        yield return StartCoroutine(WaitAtStart());
        //Debug.Log("adsgadfa");
//Instantiate(Obj);
 //yield return new WaitForSeconds(3);
   //Instantiate(Obj);
    }

    
    void WaitAndPrint()
    {
        // suspend execution for 5 seconds

        //yield return new WaitForSeconds(5);
        
        print("WaitAndPrint " + Time.time);
    }



    private IEnumerator Distance()
    {
        float x = Vector3.Distance(Obj.transform.position, Obj2.transform.position);
        while (x > 1)
        {

            Vector3 y = Obj.transform.position - Obj2.transform.position;
            Obj.transform.Translate(-y/10 * Time.deltaTime);
            Debug.Log(x);
            x = Vector3.Distance(Obj.transform.position, Obj2.transform.position);
            yield return null;
        }


    }
    void Start()
    {
        print("Starting " + Time.time);

        // Start function WaitAndPrint as a coroutine
        // yield return StartCoroutine("WaitAndPrint");
        // print("Done " + Time.time);
        StartCoroutine("Distance");
    }
}
