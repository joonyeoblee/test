using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaserBean : MonoBehaviour
{
    public GameObject Character;
    public LineRenderer LaserL, LaserR, LaserFinger;
    public float InitLaserDistance;
    float LaserDistance=30f;
    public ParticleSystem LasereyeHit, LaserfingerHit;

    public Animator animator;
    public AudioSource source1, source2;
    public AudioClip eyeLaserAudio;

    [Range( -3.0f,  3.0f)]
    public float AudioPitch =1f;

    GameObject enemy1, enemy2;
    public GameObject LaserRain;

    public int DamageRange = 10;
    bool laserDelay=true;

    //Dictionary<KeyCode, Action> keyDictionary;

    void Start()
    {
        //keyDictionary = new Dictionary<KeyCode, Action>
        //{
        //    { KeyCode.Z, Laser_Eye },
        //    { KeyCode.X, Laser_Finger }
        //};
        
        source1.clip = eyeLaserAudio;
        source2.clip = eyeLaserAudio;

        source1.loop = true;
        source2.loop = true;
        enemy1 = GameObject.Find("DR_SquidgameSet"); 
        enemy2 = GameObject.Find("DR_SquidgameManager");
    }
    void Update()
    {
        Laser_Eye();        // Z key  눈 레이저
        Laser_Finger();     // X key  왼 손가락 레이저
        Laser_Rain();       // C key  레인 레이저
        Laser_Combo();      // B key  허리케인
        source1.pitch = AudioPitch;
        source2.pitch = AudioPitch;
    }

    void Laser_Combo()
    {
        if (Input.GetKey(KeyCode.B))
        {
            animator.SetBool("combo", true);
        }                       
    }

    public void SetCombo()
    {
        animator.SetBool("combo", false);
    }

    void Laser_Rain()
    {
        if (Input.GetKey(KeyCode.C))
            LaserRain.SetActive(true);
        else if (Input.GetKeyUp(KeyCode.C))
            LaserRain.SetActive(false);
    }
    void Laser_Finger()
    {
        if (Input.GetKey(KeyCode.X))
        {
            if (!source2.isPlaying)
                source2.Play();

            animator.SetBool("LaserFinger", true);
            
            LaserDistance = InitLaserDistance;
            RaycastHit hit;
            if (Physics.Raycast(LaserFinger.transform.position + new Vector3(0f, 0.9f, 0), LaserFinger.transform.right * -1f, out hit, LaserDistance))
            {                
                if (hit.collider)  // 캐스트 히트면 파티클 활성화
                {
                    // Debug.DrawRay(LaserFinger.transform.position + new Vector3(0f, 0.9f, 0), LaserFinger.transform.right * -20f, Color.green, 3.0f);                    
                    LaserDistance = hit.distance;

                    LaserfingerHit.gameObject.transform.position = hit.point + new Vector3(0f, 0.9f, 0);       
                    LaserfingerHit.transform.LookAt(LaserFinger.transform.position); 
                    LaserfingerHit.gameObject.SetActive(true);
                }
                else
                {
                    LaserfingerHit.gameObject.SetActive(false);
                }
            }
            
            Vector3 startPoint = LaserFinger.transform.localPosition + new Vector3(-0.4f, 0.9f, 0);
            Vector3 endPoint = startPoint - new Vector3(LaserDistance / 2, 0, 0); // 캐스트 히트 거리를 빼서 레이저 길이를 줄인다.
            LaserFinger.enabled = true;
            LaserFinger.SetPosition(0, startPoint);
            LaserFinger.SetPosition(1, endPoint);
        } else if (Input.GetKeyUp(KeyCode.X))
        {
            animator.SetBool("LaserFinger", false);
            LaserFinger.enabled = false;
            LaserfingerHit.gameObject.SetActive(false);

            if (source2.isPlaying)
                source2.Stop();
        }
    }

    IEnumerator WaitForIt()
    {
        yield return new WaitForSeconds(0.3f);
        laserDelay = true;
    }


    void Laser_Eye()
    {
        
        if (Input.GetKey(KeyCode.Z))
        {            
            LaserDistance = InitLaserDistance;
            if (animator.GetBool("skate"))
                animator.SetBool("cruise", true);

            LaserL.transform.localRotation = animator.GetBool("skate") ? Quaternion.Euler(-12, 4, 0) : Quaternion.Euler(2, 4, 0);
            LaserR.transform.localRotation = animator.GetBool("skate") ? Quaternion.Euler(168, -4, 0) : Quaternion.Euler(182, -4, 0);

            RaycastHit hit;
            if (Physics.Raycast(LaserL.transform.position, LaserL.transform.forward , out hit, LaserDistance))
            {
                if (hit.collider)  // 캐스트 히트면 파티클 활성화
                {
                    switch(hit.collider.tag)
                    {
                        case "Enemy1":          // 츄리닝 맨 타격
                            if (laserDelay)     // 적 데이미의 딜레이를 준다.
                            {
                                hit.transform.gameObject.GetComponent<HpBar_Slider>().setHpBar(UnityEngine.Random.Range(50, DamageRange));
                                laserDelay = false;
                                StartCoroutine(WaitForIt());
                            }                                
                            break;
                        case "Enemy2":          // 관리자 타격
                            if (laserDelay)     
                            {
                                enemy2.GetComponent<HpBar_Slider>().setHpBar(UnityEngine.Random.Range(50, DamageRange));
                                laserDelay = false;
                                StartCoroutine(WaitForIt());
                            }
                            break;
                    }
                    Debug.DrawRay(LaserL.transform.position, LaserL.transform.forward  * LaserDistance, Color.red, 0.2f); // 왼쪽눈 레이저 캐스트 드로우
                    Debug.DrawRay(LaserR.transform.position, LaserR.transform.forward  * LaserDistance, Color.red, 0.2f); // 오른쪽눈

                    LaserDistance = hit.distance;

                    // HitFlameMat.SetColor("_LaserHitColor", lineRenderer.material.GetColor("_LaserBeamColor"));
                    LasereyeHit.gameObject.transform.position = hit.point;       // 레이저 히트 파티클 위치 = 레이케스트 히트 위치로
                    LasereyeHit.transform.LookAt(LaserL.transform.position); ;   // 파티클이 캐랙터를 바라보게 
                    LasereyeHit.gameObject.SetActive(true);
                }
                else
                {
                    LasereyeHit.gameObject.SetActive(false);
                }
            }

            Vector3 startPoint = transform.position - Character.transform.position + new Vector3(0, .5f, 0);
            Vector3 endPoint = startPoint + new Vector3(0, 0f, startPoint.z + LaserDistance / 2);

            LaserL.enabled = true;
            LaserL.SetPosition(0, startPoint);
            LaserL.SetPosition(1, endPoint);

            LaserR.enabled = true;
            LaserR.SetPosition(0, startPoint);
            LaserR.SetPosition(1, endPoint);
            if (!source1.isPlaying)
                source1.Play();
        }
        else if (Input.GetKeyUp(KeyCode.Z))
        {
            LaserL.enabled = false;
            LaserR.enabled = false;
            LasereyeHit.gameObject.SetActive(false);

            if (source1.isPlaying)
                source1.Stop();
        }
    }
}
