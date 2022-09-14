using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{
    float speed;
    TextUI text;
    public Rigidbody rb;
    void Start()
    {
        text = GameObject.Find("WindSpeed").GetComponent<TextUI>();
        speed = text.speed;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(Vector3.left * speed);
    }
}
