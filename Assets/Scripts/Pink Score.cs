using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinkScore : MonoBehaviour
{
    // Start is called before the first frame update
     // Start is called before the first frame update
    public int know = 0;
    float timer;
    void Start()
    {
        timer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Arrow"){
            know = 2;
            timer += Time.deltaTime; 
            if(timer > 3){
                know = 0;
                timer = 0;
            }
        }
    }
}
