using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update

    TextUI TextUI;
    float angle;

    Rigidbody rb;
    float X;
    float Y;
    void Start()
    {
        TextUI = GameObject.Find("WindSpeed").GetComponent<TextUI>();
        X = TextUI.speedLR;
        Y = TextUI.speedUD;
        Debug.Log(X + Y);
        transform.rotation = Quaternion.Euler(X,Y,0);
    }

    // Update is called once per frame
    void Update()
    {
    
    }
}
