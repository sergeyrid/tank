using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class terrain : MonoBehaviour
{
    // Start is called before the first frame update
    Mesh mesh;
    public Material material;
    MeshFilter meshFilter;
    MeshRenderer meshRenderer;
    MeshCollider meshCollider;
    void Start()
    {
        meshFilter = gameObject.GetComponent<MeshFilter>();
        meshRenderer = gameObject.GetComponent<MeshRenderer>();
        meshCollider = gameObject.GetComponent<MeshCollider>();
        mesh = new Mesh();
        
        Vector3[] points = {new Vector2(0,0), new Vector2(0,1), new Vector2(1,0), new Vector2(1,1), new Vector2(2,0.25f)};
        int[] tri = {
            0,1,2,
            1,2,3,
            2,4,3
        };

        mesh.vertices = points;
        mesh.triangles = tri;
        mesh.RecalculateNormals();
        mesh.RecalculateTangents();
        mesh.RecalculateBounds();
        meshFilter.mesh = mesh;
        meshRenderer.material = material;
        meshCollider.sharedMesh = mesh;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
