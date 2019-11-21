using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
0 - air
1 - grass
2 - dirt
3 - stone
*/

public class TerrainGenerator : MonoBehaviour
{
    public int height = 10, width = 10;
    public int curvature = 3;
    public int allToEarth = 4;
    int grassHeight = 1;
    int dirtHeight = 4, dirtCurvature = 3;
    public List<List<int>> terrain = new List<List<int>>();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Generate()
    {
        for (int i = 0; i < height; i++)
        {
            List<int> x = new List<int>();
            for (int j = 0; j < width; j++)
            {
                x.Add(0);
            }
            terrain.Add(x);
        }
        int lastHeight = height / allToEarth, curHeight;
        for (int i = 0; i < width; i++)
        {
            curHeight = Random.Range(lastHeight - curvature, lastHeight + curvature + 1);
            if (curHeight >= height)
            {
                curHeight = height - 1;
            }
            else if (curHeight < 0)
            {
                curHeight = 0;
            }
            lastHeight = curHeight;
            int curDirtHeight = Random.Range(dirtHeight - dirtCurvature, dirtHeight + dirtCurvature + 1);
            terrain[curHeight][i] = 1;
            while (curHeight > 0)
            {
                curHeight--;
                if (curDirtHeight > 0)
                {
                    curDirtHeight--;
                    terrain[curHeight][i] = 2;
                }
                else
                {
                    terrain[curHeight][i] = 3;
                }
            }
        }
    }
}
