using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update

    TextUI TextUI;
    float angle;
    void Start()
    {
        angle = TextUI.angle;
        transform.rotation = Quaternion.AngleAxis(angle,Vector3.up);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
