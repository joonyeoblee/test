using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar_Slider : MonoBehaviour
{
    public Animator animator;
    public Canvas HpCanvas;
    public Slider HpBar;
    public float maxHealth = 100;

    public GameObject DamageTextPrefab;

    void Start()
    {
        if (HpBar == null)
        {
            HpCanvas = GetComponentInChildren<Canvas>();
            HpBar = GetComponentInChildren<Slider>();
        }
        HpBar.maxValue = maxHealth;
        HpBar.value = maxHealth;
    }


    void Update()
    {
    }

    public void setHpBar(int damage) 
    {
        float lastHpValue = HpBar.value;
        HpBar.value = HpBar.value - damage;

        if (HpBar.value == 0) {
            damage = (int) lastHpValue;

            animator.SetBool("death", true);
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
            {
                //string objTag= transform.parent.gameObject.tag;
                if (gameObject.CompareTag("Enemy2"))
                    //GetComponent<PatrolWayPoint>().setStop();
                if (gameObject.CompareTag("Enemy1"))
                    //GetComponent<Enemy1Control>().setStop();

                HpBar.gameObject.SetActive(false);
                GetComponent<CapsuleCollider>().enabled = false;
                Destroy(gameObject, 3f);    // 3�� �Ŀ� ������Ʈ �ı�
            }
        }

        GameObject damageUI = Instantiate(DamageTextPrefab) as GameObject;
        damageUI.GetComponent<DamageText>().damage = damage;
        damageUI.transform.SetParent(HpCanvas.transform, false);

        if (HpBar.value > 0)
        {                

//            Debug.Log(gameObject.tag + " ������ " + damage + " ���� ");
        }


        //if (HpBar.value > 500)
        //{
        //    HpBar.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().material = MatGreen;
        //}
        //else if (HpBar.value > 0)
        //{
        //    HpBar.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().material = MatRed;
        //}
    }
}
