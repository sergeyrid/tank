using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class healthSystem : MonoBehaviour
{
    public float hp = 100;
    public GameObject healthbar;
    // Start is called before the first frame update
    void Start()
    {
        updHp();
    }
    void updHp()
    {
        healthbar.GetComponent<MeshRenderer>().material.SetFloat("_HP", hp);
    }
    // Update is called once per frame
    public void injure(float damage)
    {
        hp -= damage;
        if(hp <= 0)
        {
            Destroy(gameObject);
            SceneManager.LoadScene("Main menu");
        }
        healthbar.GetComponent<MeshRenderer>().material.SetFloat("_HP", hp);
    }

    void Update()
    {
        
    }
}
