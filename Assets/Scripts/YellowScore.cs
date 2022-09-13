using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowScore : MonoBehaviour
{
    // Start is called before the first frame update
    public int know = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Arrow"){
            know = 1;
        }
    }
}
