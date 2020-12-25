using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InteractableType {
    vegetation,
    rocksAndHilss,
    npc,
}

public class Interactable : MonoBehaviour
{
    public InteractableType type = InteractableType.vegetation;
    public float acquistionRange = 15f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
