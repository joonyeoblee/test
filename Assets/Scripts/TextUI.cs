using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TextUI : MonoBehaviour
{
    public float speedLR;
    public float speedUD;
    private Text WindSpeed;
    float result;
    void Start()
    {
        speedLR = Random.Range(-2.0f, 2.0f);
        speedUD = Random.Range(-2.0f, 2.0f);
        result = Mathf.Round(speedLR + speedUD);
        WindSpeed =GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {

        WindSpeed.text = "Wind Speed is: " + result.ToString();
    }
}
