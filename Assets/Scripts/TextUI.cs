using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TextUI : MonoBehaviour
{
    int num = 2;
    private Text WindSpeed;
    void Start()
    {
        WindSpeed =GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        WindSpeed.text = "Wind Speed is: " + num.ToString();
    }
}
