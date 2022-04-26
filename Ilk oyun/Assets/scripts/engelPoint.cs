using UnityEngine;

public class engelPoint : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject engel;
    public Vector3 tempV3;
    private bool kilit;
    void Start()
    {
        tempV3 = transform.position;
       // Instantiate(engel, transform);

    }

    // Update is called once per frame
    void Update()
    {   
        if (Mathf.FloorToInt(Time.time) % 6 ==0 && !kilit)
        {
            kilit = true;
            tempV3 = transform.position;
            Instantiate(engel,transform);
        }else if (Mathf.FloorToInt(Time.time) % 6 == 1)
        {
            kilit = false;
        }

    }
}
