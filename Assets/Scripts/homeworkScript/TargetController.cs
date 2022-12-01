using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BNG;
using TMPro;
using System;

public class TargetController : MonoBehaviour
{
    private float timer;
    
    public TextMeshPro hitText;
    public TextMeshPro missText;
    public TextMeshPro bulletText;
    public static TargetController Instance { get; private set; }

    public Target[] targets;

    public float downTime;

    public bool isNextTarget;

    public int targetLeft;
    public int score = 0;
    public int useBullet = 0;

    public ButtonController buttonController;

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        } else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        RaycastWeapon.OnPistolFired += RaycastWeapon_OnPistolFired;
    }

    // Update is called once per frame
    void Update()
    {
        if (isNextTarget)
        {
            timer += Time.deltaTime;
        }

        if (timer > 2f)
        {
            TryUpTarget();
            timer = 0;
        }
    }

    public void StartGame(float downTime)
    {
        targetLeft = 10;

        isNextTarget = true;
        foreach (Target t in targets)
        {
            t.downTime = downTime;
        }
        PutDownAllTarget();

        hitText.text = ""; 
        missText.text = "";
        bulletText.text = "";
    }

    private void TryUpTarget()
    {
        if(targetLeft > 0)
        {
            targetLeft--;
            RamdomlyUpTarget();
        }
        else
        {
            StopGame();
        }
    }

    //9개 타겟 중 하나 무작위로 올려줌
    public void RamdomlyUpTarget()
    {
        isNextTarget = false;
        int i = UnityEngine.Random.Range(0, 9);
        targets[i].SetUp();
        
    }

    public void StopGame()
    {
        buttonController.StopGame();
        //게임끝나면 전부 올리기
        PutUpAllTarget();
        //결과 띄우기
        hitText.text = "Hit score\n" + score.ToString(); 
        missText.text = "Miss score\n" + (10 - score).ToString(); 
        bulletText.text = "Using bullet\n" + useBullet; 
    }

    public void PutDownAllTarget()
    {
        foreach(Target t in targets)
        {
            t.SetDown();
        }
    }
    public void PutUpAllTarget()
    {
        foreach(Target t in targets)
        {
            t.SetUp();
        }
    }
     public void ScorePlus()
    {
       score++;
    }
    public void RaycastWeapon_OnPistolFired(object sender, EventArgs e)
    {
        if(buttonController.isGameStart)
            useBullet++;
    }

}
