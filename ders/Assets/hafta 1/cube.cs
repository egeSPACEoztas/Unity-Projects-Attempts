using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class cube : MonoBehaviour
{
    public Transform sparkle;
    private BoxCollider boxCollider;
    // Start is called before the first frame update
    void Start()
    {
        BoxCollider boxCollider = GetComponent<BoxCollider>();
        boxCollider.center =new Vector3(-10f,0f,0f);
        sparkle.GetComponent<ParticleSystem>().enableEmission = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("TRIGGERED");
        sparkle.GetComponent<ParticleSystem>().enableEmission = true;
        StartCoroutine(stopSparkles());
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("HALA TRIGGERLANIYORUM");
    }
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Triggerlanmam bitti");
    }
    
    IEnumerator stopSparkles()
    {
        yield return new WaitForSeconds(0.4f);
        sparkle.GetComponent<ParticleSystem>().enableEmission = false;
    }
}

