using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private Rigidbody2D rb;

    private float t = 0.0f;
    private bool moving = false;
    public int speed = 100;
    public GameObject bullet;
    public float bulletSpeed = 100;
    int cooldown = 5, curCooldown = 0;
    bool reversed = false;

    void FixedUpdate ()
    {
        rb = GetComponent <Rigidbody2D>();
        float xmove = Input.GetAxis ("Horizontal");
        bool fire = Input.GetMouseButtonDown(0);

        Vector3 gunPosition = Camera.main.ScreenPointToRay(Input.mousePosition).GetPoint(0);
        gunPosition.z = 0;
        gunPosition.x -= rb.position.x;
        gunPosition.y -= rb.position.y;
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
        if (fire && curCooldown == 0)
        {
            curCooldown = cooldown;
            Vector3 bulletPosition = gameObject.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).position;
            float bulletDirection = gameObject.transform.GetChild(0).GetChild(0).GetChild(0).transform.eulerAngles.z;
            Quaternion bulletRotation = Quaternion.Euler(0, 0, bulletDirection);
            bulletDirection = bulletDirection / 180 * Mathf.PI;
            Vector2 bulletVelocity = new Vector2(bulletSpeed * Mathf.Cos(bulletDirection),
                                                 bulletSpeed * Mathf.Sin(bulletDirection));
            Rigidbody2D bulletrb = Instantiate(bullet, bulletPosition, bulletRotation).GetComponent<Rigidbody2D>();
            bulletrb.velocity = bulletVelocity;
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

}
