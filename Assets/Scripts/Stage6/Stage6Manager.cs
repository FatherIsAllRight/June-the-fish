using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage6Manager : MonoBehaviour {
    public GameObject Phrase;

    private GameObject current = null;
    private int phrase = 0;
    private int score = 0;
    [SerializeField] Animator playerAni;
    [SerializeField] Animator upgradeAni;
    private bool upgrade = false;
    [SerializeField] Animator birdAni;

    // Use this for initialization
    void Start () {
        current = Instantiate(Phrase);
        UIScoreManager.Instance.InitScore(3, 3);
        upgradeAni.transform.position = UIScoreManager.Instance.scoreUIList[UIScoreManager.Instance.totalScore - 1].transform.position;
    }
	
	// Update is called once per frame
	void Update () {

        if(birdAni.GetCurrentAnimatorStateInfo(0).IsName("bird3"))
        {
            if(birdAni.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f)
            {
                //NextScene.Instance.SetMaskColor(new Color(0, 0, 0, 0));
                //NextScene.Instance.GameOver(4);
                SceneManager.LoadScene(9);
            }
                
            return;
        }

        if (UIScoreManager.Instance.scoreEnough)
        {
            //phrase++;
            birdAni.SetInteger("State", phrase + 1);
            if(phrase == 2)
                birdAni.GetComponent<SpriteRenderer>().sortingOrder = 15;
            upgradeAni.SetTrigger("Upgrade");
            upgrade = true;
            UIScoreManager.Instance.scoreEnough = false;
            /*switch(phrase)
            {
                case 1:
                    current.GetComponent<Stage6Phrase>().addBlackWind(2);
                    current.GetComponent<Stage6Phrase>().addWhiteWind(1);
                    UIScoreManager.Instance.scoreEnough = false;
                    UIScoreManager.Instance.InitScore(4, 4);
                    upgradeAni.transform.position = UIScoreManager.Instance.scoreUIList[UIScoreManager.Instance.totalScore - 1].transform.position;
                    break;
                case 2:
                    current.GetComponent<Stage6Phrase>().addBlackWind(3);
                    current.GetComponent<Stage6Phrase>().addWhiteWind(1);
                    UIScoreManager.Instance.scoreEnough = false;
                    UIScoreManager.Instance.InitScore(5, 4);
                    upgradeAni.transform.position = UIScoreManager.Instance.scoreUIList[UIScoreManager.Instance.totalScore - 1].transform.position;
                    break;
                case 3:
                    NextScene.Instance.changeScene(0);
                    break;
            }*/
        }
        if(upgradeAni.GetCurrentAnimatorStateInfo(0).IsName("Upgrade") && upgradeAni.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f && upgrade)
        {
            
            phrase++;
            //upgradeAni.SetBool("Upgrade", false);
            upgrade = false;
            switch (phrase)
            {
                case 1:
                    current.GetComponent<Stage6Phrase>().addBlackWind(2);
                    current.GetComponent<Stage6Phrase>().addWhiteWind(1);
                    UIScoreManager.Instance.scoreEnough = false;
                    UIScoreManager.Instance.InitScore(4, 4);
                    upgradeAni.transform.position = UIScoreManager.Instance.scoreUIList[UIScoreManager.Instance.totalScore - 1].transform.position;
                    //birdAni.SetInteger("State", 1);
                    break;
                case 2:
                    current.GetComponent<Stage6Phrase>().addBlackWind(3);
                    current.GetComponent<Stage6Phrase>().addWhiteWind(1);
                    UIScoreManager.Instance.scoreEnough = false;
                    UIScoreManager.Instance.InitScore(5, 4);
                    upgradeAni.transform.position = UIScoreManager.Instance.scoreUIList[UIScoreManager.Instance.totalScore - 1].transform.position;
                    //birdAni.SetInteger("State", 2);
                    break;
                case 3:
                    //NextScene.Instance.SetMaskColor(new Color(0, 0, 0, 0));
                    //birdAni.SetInteger("State", 3);
                    birdAni.GetComponent<SpriteRenderer>().sortingOrder = 15;
                    //NextScene.Instance.changeScene(4);
                    break;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "WhiteWind")
        {
            //score++;
            //Game_UI_Controller.Instance.GetLittleWind(1);
            playerAni.SetTrigger("Eat");
            Destroy(collision.gameObject);
            if (upgradeAni.GetCurrentAnimatorStateInfo(0).IsName("Upgrade"))
                return;
            UIScoreManager.Instance.PlusScore();
            
        }
        if (collision.tag == "BlackWind")
        {
            //score = 0;
            //Game_UI_Controller.Instance.GetLittleWind(-1);

            playerAni.SetTrigger("Hurt");
            Destroy(collision.gameObject);
            if (upgradeAni.GetCurrentAnimatorStateInfo(0).IsName("Upgrade"))
                return;
            UIScoreManager.Instance.ResetScoreUI();
        }
    }
}
