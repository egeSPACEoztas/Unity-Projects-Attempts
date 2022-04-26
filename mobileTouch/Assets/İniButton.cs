using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class İniButton : MonoBehaviour, IPointerEnterHandler
{
    public GameObject Cube;
    public Vector3 v3;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData data)
    {
        Instantiate(Cube, v3, transform.rotation);

    }
}
