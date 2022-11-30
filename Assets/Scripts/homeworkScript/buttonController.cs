using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BNG;
public class ButtonController : MonoBehaviour
{
    public float downTime = 10f;
    public bool isGameStart = false;

    void Start()
    {

    }

    void Update()
    {

    }

    //버튼 눌렸을 때 게임 시작
    public void StartGame()
    {
        if (isGameStart)
            return;
        isGameStart = true;
        TargetController.Instance.StartGame(downTime);
    }

    public void StopGame()
    {
        isGameStart = false;
        
    }

    //난이도 버튼 누르면 호출하는 함수
    public void SetDownTime(float downTime)
    {
        this.downTime = downTime;
    }
}
