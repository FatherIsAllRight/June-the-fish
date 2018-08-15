using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1_1Manager : MonoBehaviour {

    [SerializeField] float speed = 1;
    private bool recordTrail = false;
    private TrailRenderer myTrail;
    private int bugState = 0;
    [SerializeField] GameObject[] myLittleBubbles;
    [SerializeField] GameObject myLittleWinds;
    [SerializeField] Animator[] littleWindsAni;
    [SerializeField] Animator bugAnimator;
    [SerializeField] Animation bugFatherAni;
    private bool putTriggerOn = false;
    private bool breatheStart = false;
    private float breathCD = 0;

    // Use this for initialization
    void Start()
    {
        myTrail = gameObject.GetComponent<TrailRenderer>();
        //myTrail.time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(UIScoreManager.Instance != null)
        {
            if(UIScoreManager.Instance.scoreEnough)
                NextScene.Instance.changeScene(2);
            if(UIScoreManager.Instance.needResetScore && putTriggerOn)
            {
                UIScoreManager.Instance.needResetScore = false;
                for (int i = 0; i < littleWindsAni.Length; i++)
                {
                    littleWindsAni[i].gameObject.SetActive(true);
                    littleWindsAni[i].GetComponent<CircleCollider2D>().isTrigger = true;
                }
            }
        }
        if(!putTriggerOn && myLittleWinds.activeSelf && littleWindsAni[0].GetCurrentAnimatorStateInfo(0).IsName("LittleWind1Ani"))
        {
            for (int i = 0; i < littleWindsAni.Length; i++)
            {
                littleWindsAni[i].GetComponent<CircleCollider2D>().isTrigger = true;
            }
            putTriggerOn = true;
        }
        if(breatheStart)
        {
            breathCD += Time.deltaTime;
            if(breathCD > 1.0f)
            {
                bugAnimator.SetInteger("State", 1);
                breatheStart = false;
            }
        }

        if(bugAnimator != null && bugAnimator.GetCurrentAnimatorStateInfo(0).IsName("LadybugBreath") && bugAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f)
        {
            myLittleWinds.SetActive(true);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "Bug" && bugState < myLittleBubbles.Length)
        {
            myLittleBubbles[bugState].SetActive(false);
            bugState++;
            if(bugState == myLittleBubbles.Length)
            {
                breatheStart = true;
                bugFatherAni.Stop();
                //bugAnimator.SetInteger("State", 1);
            }
        }
        if(collision.tag == "LittleWind")
        {
            //Game_UI_Controller.Instance.GetLittleWind(1);
            UIScoreManager.Instance.PlusScore();
            collision.gameObject.SetActive(false);
        }
    }
}