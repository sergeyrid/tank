using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cam : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject tank;
    public GameObject tank1;
    public int zoomSpeed = 10, zoomMin = 10, zoomMax = 100;
    Vector3 offset = new Vector3(0,0,-1);
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = (tank.transform.position + tank1.transform.position) / 2 + offset;
        float zoom = Input.GetAxis("Mouse ScrollWheel");
        if (zoom < -0.1)
        {
            if (GetComponent<Camera>().orthographicSize < zoomMax)
            {
                GetComponent<Camera>().orthographicSize += zoomSpeed;
            }
        }
        if (zoom > 0.1)
        {
            if (GetComponent<Camera>().orthographicSize > zoomMin)
            {
                GetComponent<Camera>().orthographicSize -= zoomSpeed;
            }
        }
    }
}
