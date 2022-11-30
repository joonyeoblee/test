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
        //�ö�������� Ư�� �� �ڿ� ������
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
    //�Ѹ�����
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Arrow")
        {
            SetIsHit();
        }
    }

    //�¾��� �� �������°�
    public void SetIsHit()
    {
        Debug.Log(transform + "Hit");
        isUpped = false;
        anim.SetBool("isHit", !isUpped);
        timer = 0f;
        TargetController.Instance.ScorePlus();
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
