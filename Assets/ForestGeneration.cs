using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ForestGeneration : MonoBehaviour
{
    private readonly Random _random = new Random();  

    private ForestObjects trees;

    bool loaded = false;
    Vector3[] vertices;
    System.Random rnd;

    public float waterLevel = -50f;
    public float beachLevel = -40f;
    public float snowLevel = 5f;

    // Start is called before the first frame update
    private void Start()
    {
        
        rnd  = new System.Random();
    }

    void load()
    {
        vertices = GameObject.Find("Mesh Generator").GetComponent<MeshGenerator>().vertices; 
        loaded = true;
        this.trees = GetComponent<ForestObjects>();
        int point;
        for (var i = 0; i < trees.prefab.Length; i++)
        {
            for (var j = 0; j < trees.number[i]; j++)
            {
                do
                    point = rnd.Next(0, vertices.Length);
                while (vertices[point].y <= waterLevel);

                int beachTreeIndex = rnd.Next(0, trees.beachTrees.Length);
                int hillTreeIndex = rnd.Next(0, trees.hillTrees.Length);

                GameObject prefab = trees.prefab[i];
                if (vertices[point].y >= waterLevel && vertices[point].y <= beachLevel)
                {
                    prefab = trees.beachTrees[beachTreeIndex];
                } else if (vertices[point].y >= snowLevel)
                {
                    prefab = trees.hillTrees[hillTreeIndex];
                }
                Quaternion rotation = Quaternion.Euler(0, rnd.Next(0, 360), 0);
                Instantiate(prefab, vertices[point], rotation);
           
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("Mesh Generator").GetComponent<MeshGenerator>().vertices.Length > 0 && !loaded) 
            load();
    }


}
