using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGM : MonoBehaviour {
    [SerializeField] AudioClip[] autioList;
    private bool flag = false;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        if (SceneManager.GetActiveScene().buildIndex != 9)
        {
            flag = false;
        }

        if (this.GetComponent<AudioSource>().isPlaying) {
            return;
        }

        switch (SceneManager.GetActiveScene().buildIndex)
        {
            case 0:
                this.GetComponent<AudioSource>().clip = autioList[0];
                break;
            case 1:
            case 2:
                this.GetComponent<AudioSource>().clip = autioList[1];
                break;
            case 3:
            case 4:
                this.GetComponent<AudioSource>().clip = autioList[2];
                break;
            case 5:
            case 6:
            case 7:
                this.GetComponent<AudioSource>().clip = autioList[3];
                break;
            case 8:
                this.GetComponent<AudioSource>().clip = autioList[4];
                break;
            case 9:
                if(!flag)
                {
                    this.GetComponent<AudioSource>().clip = autioList[5];
                    flag = true;
                }
                else
                {
                    this.GetComponent<AudioSource>().clip = autioList[1];
                }
                break;
        }

        this.GetComponent<AudioSource>().loop = false;
        this.GetComponent<AudioSource>().Play();
	}
}
