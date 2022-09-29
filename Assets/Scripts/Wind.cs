using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{
    float speedLR;
    float speedUD;

   
    TextUI text;
    public Rigidbody rb;
    void Start()
    {
        text = GameObject.Find("WindSpeed").GetComponent<TextUI>();
        speedLR = text.speedLR;
        speedUD = text.speedUD;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(Vector3.left * speedLR);
        rb.AddForce(Vector3.up * speedUD);
        
        
    }
}
