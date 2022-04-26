using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : MonoBehaviour
{

    public float turnVar;
    public float floatVar;
    public float healingVar;

    
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
        transform.Rotate(0, turnVar, 0,Space.World);
        
    }

    void FloatMethod()
    {
        if (transform.position.y > 1.0f)
        {
            floatVar = -floatVar;
        }
        else if( transform.position.y < 0.5f)
        {
            floatVar = -floatVar;
        }

        transform.Translate(0,floatVar,0);

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<player>().updateHP(healingVar);
            Destroy(gameObject);
        }
    }
}
