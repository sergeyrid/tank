using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class COLLISION_WITH_Objects : MonoBehaviour
{
    public int explosion_radius = 7;
    public GameObject explosion;
   // Basic Collisions in 3D with another collider
void OnCollisionEnter2D(Collision2D collision) {
  
    Collider2D[] hitColliders = Physics2D.OverlapCircleAll(this.transform.position, explosion_radius);
    for(int i = 0; i < hitColliders.Length; i++)
    {
        // if (hitColliders[i].gameObject.name != "playerTank" && hitColliders[i].gameObject.name != "enemyTank(Clone)")
        // {
            
        // }

        // if (hitColliders[i].gameObject.name == "playerTank")
        // {
        //     Debug.Log("OUCH!");
        //     hitColliders[i].gameObject.GetComponent<healthSystem>().injure(15);
        // }
        Destroy(hitColliders[i].gameObject);
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
