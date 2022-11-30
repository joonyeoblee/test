using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BNG;

public class TargetController : MonoBehaviour
{
    private float timer;

    public static TargetController Instance { get; private set; }

    public Target[] targets;

    public float downTime;

    public bool isNextTarget;

    public int targetLeft;

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
        //buttonController = GetComponent<ButtonController>();
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
        int i = Random.Range(0, 9);
        targets[i].SetUp();
        
    }

    public void StopGame()
    {
        buttonController.StopGame();
    }

    public void PutDownAllTarget()
    {
        foreach(Target t in targets)
        {
            t.SetDown();
        }
    }

}
