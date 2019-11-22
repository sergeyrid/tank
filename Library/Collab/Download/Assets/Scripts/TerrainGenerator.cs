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
    public int beginH = 10;
    public int endH = 10;
    public int height = 10, width = 10;
    public int curvature = 3;
    public int allToEarth = 4;
    int grassHeight = 1;
    int dirtHeight = 4, dirtCurvature = 3;
    public List<List<int>> terrain = new List<List<int>>();
    List<int> hmap = new List<int>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void genHmap(int stp, int endp, int curv)
    {
        if(endp - stp > 1)
        {
            int pos = (endp + stp)/2;
            int curHeight = (hmap[stp] + hmap[endp])/2 + Random.Range(-curv,curv);
            if(curHeight >= height)
                curHeight = height-1;
            if(curHeight < 0)
                curHeight = 0;
            hmap[pos] = curHeight;
            genHmap(stp, pos, curv/2);
            genHmap(pos,endp, curv/2);
        }
    }
    
    void pillar(int x, int h)
    {
        int curDirtHeight = Random.Range(dirtHeight - dirtCurvature, dirtHeight + dirtCurvature + 1);
        terrain[h][x] = 1;
        for(int j = h - 1; j >= 0; j--)
        {
            if (curDirtHeight >= 0)
            {
                terrain[j][x] = 2;
            }
            else
            {
                terrain[j][x] = 3;
            }
            curDirtHeight--;
        }
    }
    public void Generate()
    {
        for(int i = 0; i < width; i++)
        {
            hmap.Add(0);
        }

        for (int i = 0; i < height; i++)
        {
            List<int> x = new List<int>();
            for (int j = 0; j < width; j++)
            {
                x.Add(0);
            }
            terrain.Add(x);
        }

        if(endH >= height)
            endH = height-1;
        if(beginH >= height)
            beginH = height-1;

        if(beginH < 0)
            beginH = 0;
        if(endH < 0)
            endH = 0;
        

        hmap[0] = beginH;
        hmap[width-1] = endH;

        genHmap(0,width-1, curvature);
        for(int i = 0; i < width; i++)
        {
            // int curDirtHeight = Random.Range(dirtHeight - dirtCurvature, dirtHeight + dirtCurvature + 1);
            // int j = height;
            // terrain[j][i] = 1;
            // while (hmap[i] > 0)
            // {
            //     j--;
            //     if (curDirtHeight > 0)
            //     {
            //         terrain[j][i] = 2;
            //     }
            //     else
            //     {
            //         terrain[j][i] = 3;
            //     }
            // }
            pillar(i,hmap[i]);
        }
    }
    public void OldGenerate()
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
