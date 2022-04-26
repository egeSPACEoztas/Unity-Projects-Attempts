using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUnit : MonoBehaviour
{
    [SerializeField]
    protected bool isSelected;

    private GameManager gameManager;

    public Outline outline;

    public GameObject basicUnitObject;

    public int selectableListIndex;

    // Start is called before the first frame update
    protected virtual void Start()
    { 
        outline = gameObject.GetComponent<Outline>();
        basicUnitObject = gameObject;
    }
    

    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        if (gameManager.AllUnitsAvailableCheck())
        {
            gameManager.AddUnitToAllUnitsList(this.gameObject.GetComponent<BaseUnit>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeSelected()
    {
        isSelected = !isSelected;
    }

    public bool IsThisBaseUnitSelected()
    {
        return isSelected;
    }

    public void SelectedOutline()
    {
        outline.OutlineMode = Outline.Mode.OutlineAndSilhouette;
        outline.OutlineColor = Color.green;
        outline.OutlineWidth = 5f;
        outline.enabled = true;
    }

    public void FloatedOnOutline()
    {
        outline.OutlineMode = Outline.Mode.OutlineAndSilhouette;
        outline.OutlineColor = Color.white;
        outline.OutlineWidth = 3f;
        outline.enabled = true;
    }

    public void SelectedAndFloatedOnOutline()
    {
        outline.OutlineMode = Outline.Mode.OutlineAndSilhouette;
        outline.OutlineColor = Color.white;
        outline.OutlineWidth = 5f;
        outline.enabled = true;
    }

    public void NoOutline()
    {
        outline.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Someone entered Selectable zone.");
        if (other.CompareTag("MainCamera"))
        {
            gameManager.sellectableUnits.Add(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            gameManager.sellectableUnits.Remove(this);
        }
    }
}
