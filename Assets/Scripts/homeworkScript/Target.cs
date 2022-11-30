using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private float timer;
    public float downTime;

    public bool isUpped;

    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();

    }

    void Update()
    {
        //올라와있으면 특정 초 뒤에 내려감
        if (isUpped)
        {
            timer += Time.deltaTime;
            if (timer >= downTime)
            {
                timer = 0;
                SetDown();
            }
        }
    }
    //총맞을때
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Arrow")
        {
            SetIsHit();
        }
    }

    //맞았을 때 내려가는거
    public void SetIsHit()
    {
        Debug.Log(transform + "Hit");
        isUpped = false;
        anim.SetBool("isHit", !isUpped);
        timer = 0f;
        TargetController.Instance.isNextTarget = true;
    }
    public void SetUp()
    {
        isUpped = true;
        anim.SetBool("isHit", !isUpped);
    }

    public void SetDown()
    {
        isUpped = false;
        anim.SetBool("isHit", !isUpped);
        TargetController.Instance.isNextTarget = true;
    }
}
