//Create a GameObject and attach a Rigidbody2D component to it (Add Component > Physics2D > Rigidbody2D)
//Attach this script to the GameObject

//This script moves a GameObject up or down when you press the up or down arrow keys.
//The velocity is set to the Vector2() value.  Unchanging the Vector2() keeps the
//GameObject moving at a contant rate.

using UnityEngine;

public class Controls : MonoBehaviour
{
    private Rigidbody2D rb;

    private float t = 0.0f;
    private bool moving = false;
    public int speed = 100;
    public GameObject bullet;
    float bulletSpeed = 100;
    int cooldown = 5, curCooldown = 0;

    void FixedUpdate ()
    {
        rb = GetComponent <Rigidbody2D>();
        float xmove = Input.GetAxis ("Horizontal");
        bool fire = Input.GetMouseButtonDown(0);

        rb.velocity = new Vector2 (xmove * speed, rb.velocity.y);
        float angle = rb.transform.rotation.eulerAngles.z;
        if (fire && curCooldown == 0)
        {
            curCooldown = cooldown;
            float bulletDirection = rb.rotation + gameObject.transform.GetChild(0).GetChild(0).GetChild(0).transform.rotation[2];
            Quaternion bulletRotation = Quaternion.Euler(0, 0, bulletDirection);
            Vector3 bulletPosition = new Vector3(rb.position.x + 10, rb.position.y + 4, 0);
            Vector2 bulletVelocity = new Vector2(100 * Mathf.Sin(bulletDirection),
                                                 -100 * Mathf.Cos(bulletDirection));
            Rigidbody2D bulletrb = Instantiate(bullet, bulletPosition, bulletRotation).GetComponent<Rigidbody2D>();
            bulletrb.velocity = bulletVelocity;
        }
        if (curCooldown > 0)
        {
            curCooldown--;
        }
        if (angle > 90 && angle < 270)
        {
            rb.MovePosition(new Vector2(rb.position.x, rb.position.y + 10));
            rb.MoveRotation(0);
        }
    }
}