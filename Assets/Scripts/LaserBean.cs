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
        Laser_Eye();        // Z key  �� ������
        Laser_Finger();     // X key  �� �հ��� ������
        Laser_Rain();       // C key  ���� ������
        Laser_Combo();      // B key  �㸮����
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
                if (hit.collider)  // ĳ��Ʈ ��Ʈ�� ��ƼŬ Ȱ��ȭ
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
            Vector3 endPoint = startPoint - new Vector3(LaserDistance / 2, 0, 0); // ĳ��Ʈ ��Ʈ �Ÿ��� ���� ������ ���̸� ���δ�.
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
                if (hit.collider)  // ĳ��Ʈ ��Ʈ�� ��ƼŬ Ȱ��ȭ
                {
                    switch(hit.collider.tag)
                    {
                        case "Enemy1":          // �򸮴� �� Ÿ��
                            if (laserDelay)     // �� ���̹��� �����̸� �ش�.
                            {
                                hit.transform.gameObject.GetComponent<HpBar_Slider>().setHpBar(UnityEngine.Random.Range(50, DamageRange));
                                laserDelay = false;
                                StartCoroutine(WaitForIt());
                            }                                
                            break;
                        case "Enemy2":          // ������ Ÿ��
                            if (laserDelay)     
                            {
                                enemy2.GetComponent<HpBar_Slider>().setHpBar(UnityEngine.Random.Range(50, DamageRange));
                                laserDelay = false;
                                StartCoroutine(WaitForIt());
                            }
                            break;
                    }
                    Debug.DrawRay(LaserL.transform.position, LaserL.transform.forward  * LaserDistance, Color.red, 0.2f); // ���ʴ� ������ ĳ��Ʈ ��ο�
                    Debug.DrawRay(LaserR.transform.position, LaserR.transform.forward  * LaserDistance, Color.red, 0.2f); // �����ʴ�

                    LaserDistance = hit.distance;

                    // HitFlameMat.SetColor("_LaserHitColor", lineRenderer.material.GetColor("_LaserBeamColor"));
                    LasereyeHit.gameObject.transform.position = hit.point;       // ������ ��Ʈ ��ƼŬ ��ġ = �����ɽ�Ʈ ��Ʈ ��ġ��
                    LasereyeHit.transform.LookAt(LaserL.transform.position); ;   // ��ƼŬ�� ĳ���͸� �ٶ󺸰� 
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
