using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;


public class thirdScript : MonoBehaviour, IPointerEnterHandler,
    IPointerExitHandler,
    IPointerDownHandler,
    IDragHandler,
    IPointerUpHandler,
    IPointerClickHandler
{


    public TMP_Text State;
    public Vector2 direction;

    private Vector2 origin;
    private Vector2 currentPos;



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
        Debug.Log("Enter");
        State.text = "Enter";
    }
    public void OnPointerClick(PointerEventData data)
    {
        Debug.Log("Click");
        State.text = "Click";
    }

    public void OnPointerExit(PointerEventData data)
    {
        Debug.Log("Exit");
        State.text = "Exit";
    }

    public void OnPointerDown(PointerEventData data)
    {
        Debug.Log("Down");
        State.text = "Down";
        origin = data.position;
    }



    public void OnPointerUp(PointerEventData data)
    {
        Debug.Log("Up");
        State.text = "Up";
        direction = Vector2.zero;

    }
    public void OnDrag(PointerEventData data)
    {
        Debug.Log("Drag");
        State.text = "Drag";
        currentPos = data.position;
        Vector2 rawPos = currentPos - origin;
        direction = rawPos.normalized;


    }

    public Vector2 GetDirection()
    {
        return direction;
    }


}
