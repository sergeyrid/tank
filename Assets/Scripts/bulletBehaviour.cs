using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
        Vector3 bulletRotation = new Vector3(0, 0, Mathf.Asin(rb.velocity.normalized.y) / Mathf.PI * 180);
        if (rb.velocity.x < 0)
        {
            bulletRotation.y = 180;
        }
        gameObject.transform.eulerAngles = bulletRotation;
    }
}
