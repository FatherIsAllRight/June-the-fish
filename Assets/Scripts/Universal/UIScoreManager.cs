using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScoreManager : MonoBehaviour {
    private static UIScoreManager instance = null;
    public static UIScoreManager Instance { get { return instance; } }

    private int currentScore;
    public int totalScore;
    public float lengthLimit;
    public GameObject scoreUI;
    [HideInInspector] public GameObject[] scoreUIList;
    public bool scoreEnough = false;
    public float yPosUI;
    public bool needResetScore = false;
    public GameObject progress;
    public GameObject mask;
	// Use this for initialization

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
        //DontDestroyOnLoad(this.gameObject);
    }

	void Start () {
        InitScore();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void InitScore()
    {
        scoreEnough = false;
        needResetScore = false;
        currentScore = 0;
        scoreUIList = new GameObject[totalScore];
        float dis = 2 * lengthLimit / (totalScore + 1);
        for (int i = 0; i < totalScore; i++)
        {
            scoreUIList[i] = Instantiate(scoreUI);
            scoreUIList[i].transform.position = new Vector3(dis * (i + 1) - lengthLimit, yPosUI, 0);
        }
        if (totalScore == 1)
        {
            progress.SetActive(false);
            return;
        }

        float l = 2 * lengthLimit - dis * 2;
        progress.transform.localScale = new Vector3(l / 3, 1, 1);
        mask.transform.position = new Vector3(-l, yPosUI, 0);
    }

    public void InitScore(int total, float length)
    {
        scoreEnough = false;
        for (int i = 0; i < scoreUIList.Length; i++)
        {
            Destroy(scoreUIList[i]);
        }
        totalScore = total;
        lengthLimit = length;
        InitScore();
    }

    public void PlusScore()
    {
        if (currentScore == totalScore)
            return;
        scoreUIList[currentScore].GetComponent<Animator>().SetBool("Get", true);
        if (totalScore > 1){
            float l = 2 * lengthLimit - 2 * lengthLimit / (totalScore + 1) * 2;
            mask.transform.position = new Vector3(-l + l * currentScore / (totalScore - 1), yPosUI, 0);
        }

        currentScore++;
        if(currentScore == totalScore)
        {
            scoreEnough = true;
        }
        else
        {
            scoreEnough = false;
        }
    }

    public void ResetScoreUI()
    {
        for (int i = 0; i < totalScore; i++)
        {
            scoreUIList[i].GetComponent<Animator>().SetBool("Get", false);
        }
        currentScore = 0;
        if (totalScore > 1)
        {
            float l = 2 * lengthLimit - 2 * lengthLimit / (totalScore + 1) * 2;
            mask.transform.position = new Vector3(-l + l * currentScore / (totalScore - 1), yPosUI, 0);
        }
        needResetScore = true;
    }
}
