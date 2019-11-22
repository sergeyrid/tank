using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    public GameObject block;
    TerrainGenerator tgen = new TerrainGenerator();
    // Start is called before the first frame update
    public Texture textureGrass;
    public Texture textureDirt;
    public Texture textureStone;
    void Start()
    {
        tgen = gameObject.GetComponentInParent<TerrainGenerator>();
        tgen.Generate();
        int xsz = tgen.width;
        int ysz = tgen.height;

        for (int j = 0; j < ysz; j++)
        {
            for (int i = 0; i < xsz; i++)
            {
                if (tgen.terrain[j][i] != 0)
                {
                    GameObject bl = Instantiate(block, transform.position + new Vector3(i*2 - xsz / 2, j*2 - ysz / 2, 0), Quaternion.Euler(0, 0, 0));
                    bl.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                    /*Block blk = bl.transform.GetChild(0).GetComponent<Block>();
                    blk.setup();
                    if (blk.sr != null && j > 0 && tgen.terrain[j][i] == 1)
                    {
                        blk.gameObject.AddComponent<CircleCollider2D>();
                        blk.sr.material.mainTexture = (Resources.Load(@"textures\t_block_grass") as Texture);
                    }*/
                    if(tgen.terrain[j][i] == 1)
                        bl.transform.GetChild(0).GetComponent<MeshRenderer>().materials[0].mainTexture = textureGrass;
                    if(tgen.terrain[j][i] == 2)
                        bl.transform.GetChild(0).GetComponent<MeshRenderer>().materials[0].mainTexture = textureDirt;
                    if(tgen.terrain[j][i] == 3)
                        bl.transform.GetChild(0).GetComponent<MeshRenderer>().materials[0].mainTexture = textureStone;
                }
            }
        }
        Destroy(GameObject.FindGameObjectWithTag("standby"),1.5f);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
