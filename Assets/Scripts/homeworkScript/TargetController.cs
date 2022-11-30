using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BNG;

public class TargetController : MonoBehaviour
{
    private float timer;
    public float uptime = 10f;
    public bool isHit;
    public bool gameStart = false;
    public bool myturn = false;
    private Animator anim;
    public GameObject button;
    GameObject buttonMain;
    Button buttonController;
    buttonController buttonMaincontroller;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        buttonMain = GameObject.Find("Button");
        buttonController = button.GetComponent<Button>();
        buttonMaincontroller = buttonMain.GetComponent<buttonController>();
    }

    // Update is called once per frame
    void Update()
    {
        //게임시작전 2초뒤 자동올라옴
        if (isHit && !gameStart)
        {
            timer += Time.deltaTime;
            if (timer >= 2f)
            {
                timer = 0;
                setUp();
            }
        }
        if(isHit && gameStart){
            timer += Time.deltaTime;
            if (timer >= 2f)
            {
                timer = 0;
                buttonMaincontroller.TargetUp();
                isHit = false;
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
    //내려가는
    public void SetIsHit()
    {
        Debug.Log("Hit");
        isHit = true;
        anim.SetBool("isHit", isHit);
    }
    //올라오는
    public void setUp(){
        isHit = false;
        anim.SetBool("isHit", isHit);
    }
    public void setDown(){
        anim.SetBool("isHit", true);
    }
}
