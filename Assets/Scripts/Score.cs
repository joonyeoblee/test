using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Score : MonoBehaviour
{
    // Start is called before the first frame update
    YellowScore newYellowScore;
    public GameObject Center;
    private Text myText;
    void Start()
    {
        newYellowScore = Center.GetComponent<YellowScore>();
        myText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if(newYellowScore.know == 1){
            myText.text = "10";
        }
        else{
            myText.text = "0";
        }
    }
}
