//Create a GameObject anTExd attach a Rigidbody2D component to it (Add Component > Physics2D > Rigidbody2D)
//Attach this script to the GameObject

//This script moves a GameObject up or down when you press the up or down arrow keys.
//The velocity is set to the Vector2() value.  Unchanging the Vector2() keeps the
//GameObject moving at a contant rate.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour
{
    private Rigidbody2D rb;

    private float t = 0.0f;
    private bool moving = false;
    bool firstTime = true;
    public int speed = 100;
    public GameObject bullet;
    public float bulletSpeed = 50;
    int cooldown = 100, curCooldown = 0;
    bool reversed = false;
    public GameObject dot;
    List<GameObject> dotList = new List<GameObject>();

    public float dt = 0.2f;

    void FixedUpdate ()
    {
        rb = GetComponent <Rigidbody2D>();
        float xmove = Input.GetAxis ("Horizontal");
        bool fire = Input.GetMouseButtonDown(0);

        Vector3 gunPosition = Camera.main.ScreenPointToRay(Input.mousePosition).GetPoint(0);
        gunPosition.z = 0;
        gunPosition.x -= rb.position.x;
        gunPosition.y -= rb.position.y;
        float curBullSpeed = bulletSpeed * Mathf.Sqrt(gunPosition.x * gunPosition.x + gunPosition.y * gunPosition.y);
        if (curBullSpeed > 150)
        {
            curBullSpeed = 150;
        }
        Vector3 gunRotation = new Vector3(0, 0, Mathf.Asin(gunPosition.normalized.y) / Mathf.PI * 180);
        if (gunPosition.x < 0)
        {
            if (!reversed)
            {
                rb.transform.eulerAngles = new Vector3(0, 180, -gameObject.transform.eulerAngles.z);
            }
            else
            {
                rb.transform.eulerAngles = new Vector3(0, 180, gameObject.transform.eulerAngles.z);
            }
            reversed = true;
            gunRotation.y = 180;
            gunRotation.x = 180;
            gunRotation.z = -Mathf.Asin(gunPosition.normalized.y) / Mathf.PI * 180;
        }
        else
        {
            if (reversed)
            {
                rb.transform.eulerAngles = new Vector3(0, 0, -gameObject.transform.eulerAngles.z);
            }
            reversed = false;
        }
        gameObject.transform.GetChild(0).GetChild(0).GetChild(0).transform.eulerAngles = gunRotation;

        rb.velocity = new Vector2 (xmove * speed, rb.velocity.y);

        float angle = rb.transform.eulerAngles.z;

        Vector3 bulletPosition = gameObject.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).position;
        float bulletDirection = gameObject.transform.GetChild(0).GetChild(0).GetChild(0).transform.eulerAngles.z;
        Quaternion bulletRotation = Quaternion.Euler(0, 0, bulletDirection);
        bulletDirection = bulletDirection / 180 * Mathf.PI;
        Vector2 bulletVelocity = new Vector2(curBullSpeed * Mathf.Cos(bulletDirection),
                                             curBullSpeed * Mathf.Sin(bulletDirection));

        DrawPath(bulletPosition, bulletVelocity);

        if (fire && curCooldown == 0)
        {
            curCooldown = cooldown;
            Rigidbody2D bulletrb = Instantiate(bullet, bulletPosition, bulletRotation).GetComponent<Rigidbody2D>();
            bulletrb.velocity = bulletVelocity;

            rb.velocity += new Vector2(-bulletVelocity.x / 5, 0);
        }
        
        if (curCooldown > 0)
        {
            curCooldown--;
        }
        if (angle > 110 && angle < 250)
        {
            rb.transform.eulerAngles = new Vector3(rb.transform.eulerAngles.x, rb.transform.eulerAngles.y, 0);
        }
    }

    void DrawPath(Vector3 position, Vector3 velocity)
    {
        for (float t = 0; t < 2; t += dt)
        {
            float x = velocity.x * t + position.x;
            float y = velocity.y * t - 49 * t * t + position.y;
            if(firstTime)
                dotList.Add(Instantiate(dot, new Vector3(x, y, 0), Quaternion.Euler(0, 0, 0)));
            else
            {
                dotList[(int)(t/dt + 0.1)].transform.position = new Vector3(x, y, 0);
            }
        }
        if(firstTime)
            firstTime = false;
    }
}