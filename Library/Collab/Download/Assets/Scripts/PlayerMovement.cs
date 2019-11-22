using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    CharacterController controller;
    void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 movement = new Vector2(0,0);
        Vector2 gvel = new Vector2(0,0);
        if(Input.GetKey(KeyCode.W))
        {
            movement += new Vector2(0,20f) * Time.deltaTime;
        }
        gvel -= new Vector2(0,10f) * Time.deltaTime;
        movement += gvel;
        controller.Move(movement);
        transform.position = new Vector3(transform.position.x,transform.position.y,0);
    }
}
