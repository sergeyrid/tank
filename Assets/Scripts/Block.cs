using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    // Start is called before the first frame update
    public int state = 9;
    public Renderer sr = new Renderer();
    void Start()
    {
        
    }

    public void setup()
    {
        sr = gameObject.GetComponent<Renderer>();
        sr.material.SetInt("_State", state);
    }

    public void setTexture(Texture tex)
    {
        sr.material.mainTexture = tex;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
