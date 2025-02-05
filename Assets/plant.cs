using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plant : MonoBehaviour
{
    private Rigidbody2D plantbody; //refer to plant rigidbody 2d component


    // Start is called before the first frame update
    void Start()
    {
        plantbody = GetComponent<Rigidbody2D>(); //intialization
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.gameObject.name);
    }
}
