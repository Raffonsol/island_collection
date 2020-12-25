using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftObjects : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionStay(Collision collision) {
        if (collision.gameObject.GetComponent<Interactable>() != null)
        {
            // Debug.Log(collision.gameObject.name);
            Rigidbody rigid = collision.gameObject.GetComponent<Rigidbody>(); 
            rigid.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionX;
            rigid.MovePosition(collision.gameObject.transform.position + collision.gameObject.transform.up);
            // rigid.constraints = RigidbodyConstraints.FreezeAll;
        }
        
    }
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.GetComponent<Interactable>() != null)
        collision.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
    }
}
