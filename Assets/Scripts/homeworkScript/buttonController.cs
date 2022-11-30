using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BNG;
public class buttonController : MonoBehaviour
{
    public TargetController[] targets;
    public TargetController useTargets = null;
    GameObject Innerbutton;
    Button button;
    private float timer = 0;
    public bool gameStart;
    public bool stop;
    public bool firstTarget;
    int targetNum;
    void Start()
    {
        Innerbutton = GameObject.Find("InnerButton");
        button = Innerbutton.GetComponent<Button>();
    }

    void Update()
    {
        if(firstTarget){
            timer += Time.deltaTime;
            if(timer >= 2f){
                timer = 0;
                TargetUp();
                firstTarget = false;
            }
        }
    }
    //타켓이 내려가는 함수
    public void TargetUp()
    {
        int i = Random.Range(0, 8);
        targets[i].SetUp();
        gameStart = true;
    }
    //버튼이 눌렸을때 실행할 함수
    public void WhenButtonDown()
    {
        foreach (TargetController k in targets)
        {
            //알아서올라오는 메소드
            k.SetDown();
            k.gameStart = true;
        }
        firstTarget = true;
    }

    public void pushedButton(float downTime){
        foreach (TargetController k in targets)
        {
            //난이도 조절
            k.downTime = downTime;
        }
    }
}
