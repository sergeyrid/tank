using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionWithObjects : MonoBehaviour
{
    public float explosion_radius = 7;
    public GameObject explosion;

   // Basic Collisions in 3D with another collider
void OnCollisionEnter2D(Collision2D collision) {
  
    Collider2D[] hitColliders = Physics2D.OverlapCircleAll(this.transform.position, explosion_radius / 1.5f);
    for(int i = 0; i < hitColliders.Length; i++)
    {
        if (hitColliders[i].gameObject.tag != "playerTank" && hitColliders[i].gameObject.tag != "enemyTank" && hitColliders[i].gameObject.tag != "Unbreakable")
        {
            Destroy(hitColliders[i].gameObject);
        }
    }

    hitColliders = Physics2D.OverlapCircleAll(this.transform.position, explosion_radius);
    for(int i = 0; i < hitColliders.Length; i++)
    {
        if (hitColliders[i].gameObject.tag != "playerTank" && hitColliders[i].gameObject.tag != "enemyTank" && hitColliders[i].gameObject.tag != "Unbreakable")
        {
            if (hitColliders[i].gameObject.GetComponent<Rigidbody2D>() == null)
            {
                hitColliders[i].gameObject.AddComponent<Rigidbody2D>();
                hitColliders[i].gameObject.GetComponent<Rigidbody2D>().velocity = 10 * (hitColliders[i].gameObject.transform.position - this.transform.position);
                hitColliders[i].gameObject.GetComponent<Rigidbody2D>().gravityScale = 4;
                Destroy(hitColliders[i].gameObject, 15 + Random.Range(-5,5));
            }
        }

        if (hitColliders[i].gameObject.tag == "playerTank")
        {
            hitColliders[i].gameObject.GetComponent<healthSystem>().injure(15);
        }

        if (hitColliders[i].gameObject.tag == "enemyTank")
        {
            hitColliders[i].gameObject.GetComponent<healthSystem>().injure(15);
        }
    }
    explosion.transform.position = this.gameObject.transform.position;
    Instantiate(explosion);

    Destroy(this.gameObject);
  
}
     
void OnCollisionStay2D(Collision2D collision) {
//   Debug.Log("OnCollisionStay with GO: " + collision.gameObject.name, this);
}
 
void OnCollisionExit2D(Collision2D collision) {
//   Debug.Log("OnCollisionExit with GO: " + collision.gameObject.name, this);
}

}
