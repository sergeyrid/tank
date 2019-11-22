using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Pause : MonoBehaviour
{
    // Start is called before the first frame update
    float prevTimescale;
    bool paused = false;
    Canvas canvas = new Canvas();
    CanvasRenderer cr = new CanvasRenderer();
    Text txt = null;
    void Start()
    {
        txt = gameObject.GetComponent<Text>();
        txt.enabled = paused;

        Canvas canvas = gameObject.GetComponent<Canvas>();
        prevTimescale = Time.timeScale;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            paused = !paused;

            if(paused)
            {
                prevTimescale = Time.timeScale;
                Time.timeScale = 0;
            }
            else
                Time.timeScale = prevTimescale;
            
            txt.enabled = paused;
        }
    }
}
