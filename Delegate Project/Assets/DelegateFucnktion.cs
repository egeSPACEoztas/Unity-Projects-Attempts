using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelegateFucnktion : MonoBehaviour
{
    // Start is called before the first frame update
    

    public delegate void RotateDelegate();
    public static RotateDelegate rotateDelegate;
    

    void Start()
    {
        

    }

    private void OnEnable()
    {
        rotateDelegate += rotate;
        rotateDelegate += increment;
    }

    private void OnDisable()
    {
        rotateDelegate -= rotate;
        rotateDelegate -= increment;
    }
    // Update is called once per frame
    void Update()
    {
        rotateDelegate.Invoke();    
    }

    public void rotate()
    {
        gameObject.transform.Rotate(45, 30, 60);
    }

    public void increment()
    {
        transform.localScale += Vector3.one;
    }
}
