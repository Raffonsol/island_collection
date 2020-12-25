using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGenerator : MonoBehaviour
{
    private ForestObjects trees;

    bool loaded = false;
    Vector3[] vertices;
    Color[] colors;
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
        vertices = GameObject.Find("Mesh Generator").GetComponent<LandGenerator>().vertices; 
        colors = GameObject.Find("Mesh Generator").GetComponent<LandGenerator>().colors; 
        loaded = true;
        this.trees = GetComponent<ForestObjects>();

        generateLayer(waterLevel, beachLevel, trees.beachTrees, trees.numberB);
        generateLayer(beachLevel, snowLevel, trees.forestTrees, trees.numberF);
        generateLayer(snowLevel, 500f, trees.hillTrees, trees.numberH);
        generateLayer(waterLevel, 500f, trees.valleys, trees.numberV, true);

    }

    void generateLayer(float level1, float level2, GameObject[] prefabs, int[] number, bool colorMatch = false) {
        int point;

        for (var i = 0; i < prefabs.Length; i++)
        {
            for (var j = 0; j < number[i]; j++)
            {
                do
                    point = rnd.Next(0, vertices.Length);
                while (vertices[point].y < level1 || vertices[point].y > level2);

                GameObject prefab = prefabs[i];
                Quaternion rotation = Quaternion.Euler(0, rnd.Next(0, 360), 0);
Vector3 vert = vertices[point]; vert.y+=20f;
                GameObject instd = Instantiate(prefab, vert, rotation);
                if (colorMatch) {
                    Component[] mesh = instd.GetComponentsInChildren<MeshRenderer>();
                    mesh[mesh.Length - 1].GetComponent<MeshRenderer>().material.SetColor("_Color", colors[point]);
                }

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("Mesh Generator").GetComponent<LandGenerator>().vertices.Length > 0 && !loaded) 
            load();
    }


}
