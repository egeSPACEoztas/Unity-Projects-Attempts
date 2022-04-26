using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public BaseUnit[] allUnits;
    public int allUnitsIndex;
    public Camera cam;

    [SerializeField]
    private List<BaseUnit> sellectedUnits;
    [SerializeField]
    public List<BaseUnit> sellectableUnits;

    //The selection square we draw when we drag the mouse to select units
    public RectTransform selectionSquareTrans;

    [SerializeField]
    private BaseUnit highlightedUnit;

    public GameObject sellectionBoxObject;
    //To determine if we are clicking with left mouse or holding down left mouse
    float delay = 0.2f;
    float clickTime = 0f;

    //there can be only one game manager script
    private static GameManager _instance;


    public float rayCastDistance;
    //The start and end coordinates of the square we are making
    Vector3 squareStartPos;
    Vector3 squareEndPos;
    //If it was possible to create a square
    bool hasCreatedSquare;
    //The selection squares 4 corner positions
    Vector3 TL, TR, BL, BR;

    void Start()
    {
        hasCreatedSquare = false;
        selectionSquareTrans.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        ColliderFunction();
        SelectUnits();
        HighlightUnit();
    }

    public static GameManager Instance
    {
        get { return _instance; }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    void SelectUnits()
    {
        //Are we clicking with left mouse or holding down left mouse
        bool isClicking = false;
        bool isHoldingDown = false;

        //Click the mouse button
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Mouse button 0 has been clicked down");
            //start counting time
            clickTime = Time.time;

            //We dont yet know if we are drawing a square, but we need the first coordinate in case we do draw a square
            RaycastHit hit;
            //Fire ray from camera
            //if it doesn't hit any special layer stuff start recording positions
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            Debug.Log("just before if");
            if (Physics.Raycast(ray, out hit, 200f))
            {
                //The corner position of the square
                squareStartPos = hit.point;
                Debug.Log("SQUARE START POS IS CHANGED TO HIT POINT");
            }
        }

        //Holding down the mouse button
        if (Input.GetMouseButton(0))
        {
            Debug.Log("Mouse button 0 is holding down");
            if (Time.time - clickTime > delay)
            {
                Debug.Log("Now decided that we are holding the key down");
                isHoldingDown = true;
            }
        }

        //Release the mouse button
        if (Input.GetMouseButtonUp(0))
        {

            //Debug.Log("Mouse Button 0 up");
            //check if time is past click time, if so is clicking
            if (Time.time - clickTime <= delay)
            {
                isClicking = true;
               // Debug.Log("IS CLICKING");
            }


            //Select all units within the square if we have created a square
            if (hasCreatedSquare)
            {
                hasCreatedSquare = false;
               // Debug.Log("MOUSE BUTTON 0 UP SQUARE IS TRUE BUT NOW FALSE");
                //Deactivate the square selection image
                selectionSquareTrans.gameObject.SetActive(false);
                //Debug.Log("SQUARE SHOULD BE DE ACTİVATED");
                //Clear the list with selected unit if not LSHIFT 
                if (!Input.GetKey(KeyCode.LeftShift))
                {
                    sellectedUnits.Clear();
                }

                //Select the units
                foreach (BaseUnit dummy in sellectableUnits)
                {
                    Debug.Log(dummy);

                    //Is this unit within the square
                    if (dummy == null)
                    {
                        //Debug.Log("This is null.");
                    }
                    else if (IsWithinPolygon(dummy.gameObject.transform.position))
                    {
                        //currentUnit.GetComponent<MeshRenderer>().material = selectedMaterial;

                        //Debug.Log("Added a unit from all units into the sellected units");
                        sellectedUnits.Add(dummy);
                    }
                    //Otherwise deselect the unit if it's not in the square
                    else
                    {
                        //currentUnit.GetComponent<MeshRenderer>().material = normalMaterial;
                    }
                }
            }

            // there is a bug here that if you don't end the sellection square in the ground or map it doesn't go out. But because there should be always some map it should be fine.
        }

        //Select one unit with left mouse and deselect all units with left mouse by clicking on what's not a unit
        if (isClicking)
        {
            //Deselect all units
            Debug.Log("Is clicking");
            Debug.Log(!Input.GetKey(KeyCode.LeftShift));
            if (!Input.GetKey(KeyCode.LeftShift))
            {
                foreach(BaseUnit dummy in sellectedUnits)
                {
                    Debug.Log("Isn't clicking shift");
                    dummy.ChangeSelected();
                    dummy.NoOutline();
                }
                sellectedUnits.Clear();
            }
            BaseUnit clickingUnit = SelectBaseUnitWithRaycast();
            Debug.Log(clickingUnit);
            if (clickingUnit != null)
            {
                sellectedUnits.Add(clickingUnit);

            }
                


        }

        //Debug.Log("Just before is holdimg down isHoldingDown: " + isHoldingDown);
        //Drag the mouse to select all units within the square
        if (isHoldingDown)
        {
            //Activate the square selection image
            if (!selectionSquareTrans.gameObject.activeInHierarchy)
            {
                selectionSquareTrans.gameObject.SetActive(true);
            }

            //Get the latest coordinate of the square
            squareEndPos = Input.mousePosition;

            //Display the selection with a GUI image
            DisplaySquare();

            //Highlight the units within the selection square, but don't select the units
            if (hasCreatedSquare)
            {
                foreach (BaseUnit currentUnit in sellectableUnits)
                {
                    Debug.Log(currentUnit);
                    //BaseUnit currentBaseUnit = currentUnit.GetComponentInParent<BaseUnit>();
                    //Is this unit within the square
                    if (currentUnit == null)
                    {
                        //Debug.Log("This is null");
                    }
                    else if (IsWithinPolygon(currentUnit.gameObject.transform.position))
                    {

                        if (!currentUnit.IsThisBaseUnitSelected())
                        {
                            currentUnit.ChangeSelected();
                        }
                        //currentUnit.GetComponent<MeshRenderer>().material = highlightMaterial;
                    }
                    //Otherwise deactivate
                    else
                    {
                        if (currentUnit.IsThisBaseUnitSelected())
                        {
                            currentUnit.ChangeSelected();
                        }
                        //currentUnit.GetComponent<MeshRenderer>().material = normalMaterial;
                    }
                }
            }
        }




    }

    //this is my code probly shite 
    private BaseUnit SelectBaseUnitWithRaycast()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit,rayCastDistance))
        {
            if (hit.transform.gameObject.CompareTag("Friendly"))
            {
                hit.transform.GetComponent<BaseUnit>().ChangeSelected();
                //sellectedUnits.Add(hit.transform.gameObject.GetComponent<BaseUnit>());
                return hit.transform.gameObject.GetComponent<BaseUnit>();
            }

            return null;
            //SELLECT BASEUNIT
        }else
        {
            return null;
        }
        
    }

    //Is a unit within a polygon determined by 4 corners
    bool IsWithinPolygon(Vector3 unitPos)
    {
        bool isWithinPolygon = false;

        //The polygon forms 2 triangles, so we need to check if a point is within any of the triangles
        //Triangle 1: TL - BL - TR
        if (IsWithinTriangle(unitPos, TL, BL, TR))
        {
            return true;
        }

        //Triangle 2: TR - BL - BR
        if (IsWithinTriangle(unitPos, TR, BL, BR))
        {
            return true;
        }

        return isWithinPolygon;
    }

    bool IsWithinTriangle(Vector3 p, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        bool isWithinTriangle = false;

        //Need to set z -> y because of other coordinate system
        float denominator = ((p2.z - p3.z/*A*/) * (p1.x - p3.x/*B*/) + (p3.x - p2.x/*D*/) * (p1.z - p3.z/*F*/));

        float a = ((p2.z - p3.z) * (p.x - p3.x) + (p3.x - p2.x) * (p.z - p3.z)) / denominator;
        float b = ((p3.z - p1.z) * (p.x - p3.x) + (p1.x - p3.x) * (p.z - p3.z)) / denominator;
        float c = 1 - a - b;

        //The point is within the triangle if 0 <= a <= 1 and 0 <= b <= 1 and 0 <= c <= 1
        if (a >= 0f && a <= 1f && b >= 0f && b <= 1f && c >= 0f && c <= 1f)
        {
            isWithinTriangle = true;
        }

        return isWithinTriangle;
    }

    void DisplaySquare()
    {
        Debug.Log("Started displaying square");
        //The start position of the square is in 3d space, or the first coordinate will move
        //as we move the camera which is not what we want
        Vector3 squareStartScreen = cam.WorldToScreenPoint(squareStartPos);

        squareStartScreen.z = 0f;

        //Get the middle position of the square
        Vector3 middle = (squareStartScreen + squareEndPos) / 2f;

        //Set the middle position of the GUI square
        selectionSquareTrans.position = middle;

        //Change the size of the square
        float sizeX = Mathf.Abs(squareStartScreen.x - squareEndPos.x);
        float sizeY = Mathf.Abs(squareStartScreen.y - squareEndPos.y);

        //Set the size of the square
        selectionSquareTrans.sizeDelta = new Vector2(sizeX, sizeY);

        //The problem is that the corners in the 2d square is not the same as in 3d space
        //To get corners, we have to fire a ray from the screen
        //We have 2 of the corner positions, but we don't know which,  
        //so we can figure it out or fire 4 raycasts
        TL = new Vector3(middle.x - sizeX / 2f, middle.y + sizeY / 2f, 0f);
        TR = new Vector3(middle.x + sizeX / 2f, middle.y + sizeY / 2f, 0f);
        BL = new Vector3(middle.x - sizeX / 2f, middle.y - sizeY / 2f, 0f);
        BR = new Vector3(middle.x + sizeX / 2f, middle.y - sizeY / 2f, 0f);
        Debug.Log("Square corners created");
        //From screen to world
        RaycastHit hit;
        int i = 0;
        //Fire ray from camera
        if (Physics.Raycast(Camera.main.ScreenPointToRay(TL), out hit, 200f))
        {
            TL = hit.point;
            i++;
        }
        if (Physics.Raycast(Camera.main.ScreenPointToRay(TR), out hit, 200f))
        {
            TR = hit.point;
            i++;
        }
        if (Physics.Raycast(Camera.main.ScreenPointToRay(BL), out hit, 200f))
        {
            BL = hit.point;
            i++;
        }
        if (Physics.Raycast(Camera.main.ScreenPointToRay(BR), out hit, 200f))
        {
            BR = hit.point;
            i++;
        }

        //Could we create a square?
        hasCreatedSquare = false;
        Debug.Log(i);
        //We could find 4 points
        if (i == 4)
        {
            //Display the corners for debug
            //sphere1.position = TL;
            //sphere2.position = TR;
            //sphere3.position = BL;
            //sphere4.position = BR;

            hasCreatedSquare = true;
        }
    }

    void HighlightUnit()
    {
        for (int i = 0; i < sellectedUnits.Count; i++)
        {
            sellectedUnits[i].GetComponent<BaseUnit>().SelectedOutline();
        }

        BaseUnit currentObject = SelectBaseUnitWithRaycast();
        if (currentObject != null)
        {
            if (!currentObject.GetComponent<BaseUnit>().IsThisBaseUnitSelected())
            {
                Debug.Log("Unit you are hovering on thinks it is NOT selected");
                currentObject.GetComponent<BaseUnit>().FloatedOnOutline();
                //This is where We manipulate the 

                //currentObject.GetComponent<Outline>().
            }
            else if (currentObject.GetComponent<BaseUnit>().IsThisBaseUnitSelected())
            {
                Debug.Log("Unit you are hovering on thinks it is selected");
                currentObject.GetComponent<BaseUnit>().SelectedAndFloatedOnOutline();
            }
        }
        

        //değilse 

    }

    void ColliderFunction()
    {
        BoxCollider camCollider = cam.GetComponent<BoxCollider>();

        camCollider.center = new Vector3(0, 0, rayCastDistance / 4);
        camCollider.size = new Vector3(21.6f * cam.orthographicSize / 7, 14.1f * cam.orthographicSize / 7, rayCastDistance / 2);
    }

    public bool AllUnitsAvailableCheck()
    {
        return (allUnits.Length >= allUnitsIndex);
    }

    public void AddUnitToAllUnitsList(BaseUnit basicUnit)
    {
        foreach (BaseUnit dummy in allUnits)
        {
            if (dummy == basicUnit)
            {
                return;
            }
        }

        allUnits[allUnitsIndex] = basicUnit;
        allUnitsIndex++;
    }

    public void RemoveUnitFromAllUnitsList(BaseUnit basicUnit)
    {

        foreach (BaseUnit dummy in allUnits)
        {
            if (dummy == basicUnit)
            {
                return;
            }
        }
        allUnits[allUnitsIndex] = null;
        --allUnitsIndex;

    }

}

