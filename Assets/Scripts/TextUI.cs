using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TextUI : MonoBehaviour
{
    public float speed;
    private Text WindSpeed;
    void Start()
    {
        speed = Random.Range(0.1f, 4.0f);
        WindSpeed =GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        WindSpeed.text = "Wind Speed is: " + speed.ToString();
    }
}
