using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(enemy, new Vector3(0, 150, 0), Quaternion.Euler(0, 0, 0));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
