using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Score : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject Center;
    private Text myText;
    void Start()
    {
        myText = GetComponent<Text>();
        Center = GameObject.Find("Target");
    }

    // Update is called once per frame
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "Target"){
            Vector3 p1 = transform.position;
            Vector3 p2 = this.Center.transform.position;
            Debug.Log(p1 - p2);
            Debug.Log(Vector3.Distance(p1,p2));
        }

    }
}
