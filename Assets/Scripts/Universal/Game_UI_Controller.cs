using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game_UI_Controller : MonoBehaviour {
    
    private static Game_UI_Controller instance = null;
    public static Game_UI_Controller Instance { get { return instance; } }

    [SerializeField] Text myScoreText;
    private int myWindScore;
    public int GetScore() { return myWindScore; }

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
        myWindScore = 1;
        myScoreText.text = myWindScore.ToString();
        //DontDestroyOnLoad(this.gameObject);
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Game_UI_Controller.Instance.GetScore() == 2)
        {
            NextScene.Instance.changeScene(0);
        }
	}

    public void GetLittleWind(int num)
    {
        myWindScore += num;
        myScoreText.text = myWindScore.ToString();
    }

    public void LoseLittleWind(int num)
    {
        myWindScore -= num;
        myScoreText.text = myWindScore.ToString();
    }
}