using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class virtualDPad : MonoBehaviour
{
    public TextAlignment directionText;
    public GameObject Sprite;

    private Touch theTouch;
    private Vector2 touchStartPositiom, touchEndPosition;
    private string direction;

    private float xDir;
    private float yDir;






    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            theTouch = Input.GetTouch(0);

            if (theTouch.phase == TouchPhase.Began)
            {
                touchStartPositiom = theTouch.position;

            }
            else if(theTouch.phase ==  TouchPhase.Moved || theTouch.phase == TouchPhase.Ended)
            {
                touchEndPosition = theTouch.position;

                float x = touchEndPosition.x - touchStartPositiom.x;
                float y = touchEndPosition.y - touchStartPositiom.y;
                if(Mathf.Abs(x) == 0 && Mathf.Abs(y) == 0)
                {
                    direction = "Tapped";

                }
                else if(Mathf.Abs(x)> Mathf.Abs(y))
                {
                    direction = x > 0 ? "Right" : "Left";

                    xDir = x > 0 ? 2 : -2;
                    Sprite.transform.Translate(xDir * Time.deltaTime, 0, 0);

                }
                else
                {
                    direction = y > 0 ? "Up" : "Down";

                    yDir = y > 0 ? 2 : -2;
                    Sprite.transform.Translate(0, yDir * Time.deltaTime, 0);
                }

            }
        }
    }
}
