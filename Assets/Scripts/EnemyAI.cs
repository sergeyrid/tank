using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private Rigidbody2D rb;

    public int width = 500, height = 50;
    private float t = 0.0f;
    private bool moving = false;
    public int speed = 100;
    public GameObject bullet;
    public float bulletSpeed = 200;
    int cooldown = 100, curCooldown = 0;
    bool reversed = false;
    private GameObject player;

    void FixedUpdate()
    {
        player = GameObject.FindGameObjectsWithTag("playerTank")[0];
        Vector3 playerPosition = player.GetComponent<Rigidbody2D>().position;
        float playerRotation = player.GetComponent<Rigidbody2D>().rotation;

        rb = GetComponent<Rigidbody2D>();
        Vector3 selfPosition = rb.position;
        float selfRotation = rb.rotation;

        float curBullSpeed = bulletSpeed / 2;
        float sinsq = calculateSinSq(playerPosition.x, playerPosition.y, selfPosition.x, selfPosition.y, curBullSpeed);
        while ((sinsq > 1 || sinsq < 0) && curBullSpeed < bulletSpeed * 1.5f)
        {
            curBullSpeed += 1;
            sinsq = calculateSinSq(playerPosition.x, playerPosition.y, selfPosition.x, selfPosition.y, curBullSpeed);
        }
        if (sinsq > 1 || sinsq < 0)
        {
            sinsq = 0;
        }
        curBullSpeed -= 2;
        Vector3 gunRotation = new Vector3(0, 0, Mathf.Asin(Mathf.Sqrt(sinsq)) / Mathf.PI * 180);
        if (playerPosition.x < selfPosition.x)
        {
            gunRotation = new Vector3(180, 180, -Mathf.Asin(Mathf.Sqrt(sinsq)) / Mathf.PI * 180);
        }

        gameObject.transform.GetChild(0).GetChild(0).GetChild(0).transform.eulerAngles = gunRotation;

        if (gameObject.transform.GetChild(0).GetChild(0).GetChild(0).transform.eulerAngles.z > 90 &&
            gameObject.transform.GetChild(0).GetChild(0).GetChild(0).transform.eulerAngles.z < 270)
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
        }
        else
        {
            if (reversed)
            {
                rb.transform.eulerAngles = new Vector3(0, 0, -gameObject.transform.eulerAngles.z);
            }
            reversed = false;
        }

        if (Mathf.Abs(selfPosition.x - playerPosition.x) > 200)
        {
            if (selfPosition.x < playerPosition.x)
            {
                rb.velocity = new Vector2(speed, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(-speed, rb.velocity.y);
            }
        }
        else if (Mathf.Abs(selfPosition.x - playerPosition.x) < 100)
        {
            if (selfPosition.x < playerPosition.x)
            {
                rb.velocity = new Vector2(-speed, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(speed, rb.velocity.y);
            }
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        float angle = rb.transform.eulerAngles.z;
        if (curCooldown == 0)
        {
            curCooldown = cooldown + Random.Range(0, 50);
            Vector3 bulletPosition = gameObject.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).position;
            float bulletDirection = gameObject.transform.GetChild(0).GetChild(0).GetChild(0).transform.eulerAngles.z;
            Quaternion bulletRotation = Quaternion.Euler(0, 0, bulletDirection);
            bulletDirection = bulletDirection / 180 * Mathf.PI;
            Vector2 bulletVelocity = new Vector2(curBullSpeed * Mathf.Cos(bulletDirection),
                                                 curBullSpeed * Mathf.Sin(bulletDirection));
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

    float calculateSinSq(float x0, float y0, float x1, float y1, float curBullSpeed)
    {
        float g = 100, v = curBullSpeed;
        float a = (x1 - x0) * (x1 - x0) / (v * v);
        float b = (y1 - y0) * (y1 - y0);
        float c = y1 * g - y0 * g;
        float d = g * g / 4;
        b = (2 * b + c * a + a * v * v) / (a * v * v + b);
        c = (b + c * a + d * a * a) / (a * v * v + b);
        d = b * b - 4 * c;
        if (d < 0)
        {
            return -1;
        }
        return (b + Mathf.Sqrt(d)) / 2;
    }
}
