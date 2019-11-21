using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    public GameObject block;
    TerrainGenerator tgen = new TerrainGenerator();
    // Start is called before the first frame update
    void Start()
    {
        tgen = gameObject.GetComponentInParent<TerrainGenerator>();
        tgen.Generate();
        int xsz = tgen.width;
        int ysz = tgen.height;
        
        for(int j = 0; j < ysz; j++)
        {
            for(int i = 0; i < xsz; i++)
            {
                if(tgen.terrain[j][i] != 0)
                {
                    GameObject bl = Instantiate(block, new Vector3(i - xsz/2, j - ysz/2, 0), Quaternion.Euler(-90,0,0));
                    bl.transform.localScale = new Vector3(0.1f,0.1f,0.1f);
                    Block blk = bl.transform.gameObject.GetComponent<Block>();
                    

                    if(j == ysz-1)
                    {
                        if(i == 0)
                        {
                            blk.setState(1);
                        }
                        else
                        {
                            if(i == xsz-1)
                            {
                                blk.setState(3);
                            }
                            else
                                blk.setState(2);
                        }
                    }
                    else
                    {
                        blk.setState(9);
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
