using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage5_2Manager : MonoBehaviour {

    [SerializeField] GameObject partical;
    [SerializeField] Transform fogMask;
    [SerializeField] GameObject fog;
    [SerializeField] float scaleSpeed;
    [SerializeField] Animator[] stoneAni;
    [SerializeField] GameObject[] windList;

    private int phrase;
    private float maskScale;
    private int maskChange; //-1 down; 0 not change; 1 up

    [SerializeField] GameObject[] rippleList;
    [SerializeField] GameObject feather;
    [SerializeField] GameObject whaleObj;
    [SerializeField] Animator whale;
    [SerializeField] Animator spring;
    private bool changeStage;
    [SerializeField] Vector3 targetPos;
    [SerializeField] float speed;

    [SerializeField] AudioSource[] audioList;
	// Use this for initialization
	void Start () {
        gameObject.GetComponent<PlayerMoveController>().enabled = false;
        partical.SetActive(false);
        phrase = -1;
        maskScale = 0.6f;
        maskChange = 0;
        changeStage = false;
	}
	
	// Update is called once per frame
	void Update () {

        if(changeStage)
        {
            if (transform.position == targetPos)
                NextScene.Instance.changeScene(8);
          
            transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * speed);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, 90), Time.deltaTime * 2);
            scaleSpeed = 0.8f;
            ScaleUp(5.0f);
        }

        if(UIScoreManager.Instance.scoreEnough)
        {
            if(phrase < 3)
            {
                stoneAni[phrase - 1].enabled = true;
            }
            else if(!whaleObj.activeSelf)
            {
                whaleObj.SetActive(true);
            }
        }
        switch (phrase)
        {
            case -1:
                AnimatorStateInfo info = gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);
                if(info.normalizedTime >= 1.0f)
                {
                    phrase++;
                }
                break;
            case 0:
                ScaleUp(maskScale);
                break;
            case 1:
                if(maskChange > 0)
                {
                    ScaleUp(maskScale);
                }
                AnimatorStateInfo stoneInfo1 = stoneAni[0].GetCurrentAnimatorStateInfo(0);
                if(stoneInfo1.IsName("InteractiveStoneBreak") && stoneInfo1.normalizedTime >= 1.0f)
                {
                    stoneAni[0].gameObject.SetActive(false);
                    phrase++;
                    UIScoreManager.Instance.InitScore(2, 3);
                }
                break;
            case 2:
                if (maskChange > 0)
                {
                    ScaleUp(maskScale);
                }
                else if(maskChange < 0)
                {
                    ScaleDown(maskScale);
                }
                AnimatorStateInfo stoneInfo2 = stoneAni[1].GetCurrentAnimatorStateInfo(0);
                if (stoneInfo2.IsName("InteractiveStoneBreak") && stoneInfo2.normalizedTime >= 1.0f)
                {
                    stoneAni[1].gameObject.SetActive(false);
                    phrase++;
                    UIScoreManager.Instance.InitScore(3, 4);
                }
                break;
            case 3:
                if (maskChange > 0)
                {
                    ScaleUp(maskScale);
                }
                else if (maskChange < 0)
                {
                    ScaleDown(maskScale);
                }
                break;
        }
	}

    private void ScaleUp(float target)
    {
        if(fogMask.localScale.x < target)
        {
            float d = scaleSpeed * Time.deltaTime;
            fogMask.localScale += new Vector3(d, d, 0);
        }
        else{
            if(phrase == 0)
            {
                gameObject.GetComponent<Animator>().enabled = false;
                gameObject.GetComponent<PlayerMoveController>().enabled = true;
                partical.SetActive(true);
                feather.SetActive(true);
                UIScoreManager.Instance.InitScore(1, 3);
                UIScoreManager.Instance.progress.SetActive(true);
                phrase++;
            }
            maskChange = 0;
        }
    }

    private void ScaleDown(float target)
    {
        if (fogMask.localScale.x > target)
        {
            float d = scaleSpeed * Time.deltaTime;
            fogMask.localScale -= new Vector3(d, d, 0);
        }
        else
        {
            maskChange = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "LittleWind")
        {
            collision.gameObject.SetActive(false);
            UIScoreManager.Instance.PlusScore();
            switch (phrase)
            {
                case 1:
                    maskScale += 0.3f;
                    maskChange = 1;
                    break;
                case 2:
                    maskScale += 0.35f;
                    maskChange = 1;
                    break;
                case 3:
                    maskScale += 0.4f;
                    maskChange = 1;
                    break;
            }
        }
        if(collision.gameObject.name == "Whale")
        {
            whale.SetBool("Push", true);
            spring.SetBool("Spring", true);
            audioList[1].Play();
            if (feather.GetComponent<FeatherFly>().BlowUp())
            {
                changeStage = true;
                UIScoreManager.Instance.InitScore(0, 0);
                //fogMask.gameObject.SetActive(false);
                //fog.SetActive(false);
                gameObject.GetComponent<PlayerMoveController>().enabled = false;
                gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
                phrase++;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "InteractiveStone")
        {
            if (UIScoreManager.Instance.scoreEnough)
            {
                stoneAni[phrase - 1].SetBool("Break", true);
                if(!audioList[0].isPlaying)
                    audioList[0].Play();
                //collision.gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
                rippleList[phrase - 1].SetActive(true);
            }
        }
    }

    private void OnMouseUp()
    {
        if(phrase == 2 && !UIScoreManager.Instance.scoreEnough)
        {
            windList[0].SetActive(true);
            windList[1].SetActive(true);
            maskScale = 0.9f;
            maskChange = -1;
        }
        else if (phrase == 3 && !UIScoreManager.Instance.scoreEnough)
        {
            windList[2].SetActive(true);
            windList[3].SetActive(true);
            windList[4].SetActive(true);
            maskScale = 1.7f;
            maskChange = -1;
        }
    }
}
