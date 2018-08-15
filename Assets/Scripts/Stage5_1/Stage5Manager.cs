using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DeltaTimeList
{
    public List<float> deltaTimeList = new List<float>();
};

[System.Serializable]
public class SpritesList
{
    public List<SpriteRenderer> spriteList = new List<SpriteRenderer>();
};
[System.Serializable]
public class WindsList
{
    public List<GameObject> windList = new List<GameObject>();
};

public class Stage5Manager : MonoBehaviour {
    private static Stage5Manager instance = null;
    public static Stage5Manager Instance { get { return instance; } }

    [SerializeField] AudioSource[] audios;
    public int phrase;

    [SerializeField] public List<DeltaTimeList>  deltaTimePhrase = new List<DeltaTimeList>();
    [SerializeField] public List<SpritesList> spritePhrase = new List<SpritesList>();
    [SerializeField] public List<WindsList> windPhrase = new List<WindsList>();
    private bool[] windMemory = new bool[4];

    private float delta = 0.0f;
    private int currentIndex;
    private int breakCount;
    private bool pausePhrase = false;
    [SerializeField] GameObject[] footObjs;
    [SerializeField] GameObject[] lightings;
    [SerializeField] Animator[] featherAnis;

    private float hintTimer = 0f;
    [SerializeField] HintManager hintManager;

    [SerializeField] GameObject zues;
    [SerializeField] Animator zuesFace;
    [SerializeField] Animator zuesFeather;
    [SerializeField] GameObject[] zuesWind; 

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
        currentIndex = 0;
        breakCount = 0;
        phrase = -1;
        hintTimer = 0f;
    }

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (featherAnis[0].gameObject.activeSelf &&featherAnis[0].GetCurrentAnimatorStateInfo(0).normalizedTime > 0.95f)
        {
            featherAnis[0].GetComponent<CapsuleCollider2D>().isTrigger = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            if(phrase >=0 && phrase < 3)
            {
                int l = windPhrase[phrase].windList.Count;
                for (int i = 0; i < l; i++)
                {
                    if(windMemory[i])
                        windPhrase[phrase].windList[i].SetActive(true);
                }
            }
        }

        if(pausePhrase)
        {
            if(UIScoreManager.Instance.scoreEnough)
            {
                if(phrase == 3)
                {
                    Debug.Log("next scene");
                    NextScene.Instance.changeScene(7);
                    return;
                }
                hintTimer = 0;
                ResetWindMemory();
                int l = spritePhrase[phrase].spriteList.Count;
                for (int i = 0; i < l; i++)
                {
                    spritePhrase[phrase].spriteList[i].gameObject.SetActive(false);
                }
                phrase++;
                Debug.Log("next phrase  " + phrase);
                pausePhrase = false;
                switch(phrase)
                {
                    case 1:
                        UIScoreManager.Instance.InitScore(3, 3);
                        break;
                    case 2:
                        UIScoreManager.Instance.InitScore(4, 4);
                        break;
                    case 3:
                        UIScoreManager.Instance.InitScore(2, 3);
                        return;
                    default:
                        break;
                }
                hintTimer = 0;
                l = spritePhrase[phrase].spriteList.Count;
                for (int i = 0; i < l; i++)
                {
                    spritePhrase[phrase].spriteList[i].gameObject.SetActive(true);
                }
            }
            return;
        }

        if(phrase == 3)
        {
            zues.SetActive(true);
            AnimatorStateInfo animatorStateInfo = zuesFeather.GetCurrentAnimatorStateInfo(0);
            if(animatorStateInfo.IsName("Falling"))
            {
                pausePhrase = true;
                for (int i = 0; i < zuesWind.Length; i++)
                    zuesWind[i].SetActive(true);
            }
        }
        else if(phrase >= 0)
        {
            delta += Time.deltaTime;
            if (delta >= deltaTimePhrase[phrase].deltaTimeList[currentIndex])
            {
                delta = 0;
                currentIndex++;
                if (currentIndex >= deltaTimePhrase[phrase].deltaTimeList.Count)
                {
                    //phrase pause
                    if (breakCount == currentIndex - 1)
                    {
                        pausePhrase = true;
                        audios[1].Play();
                    }
                    else
                    {
                        audios[0].Play();
                        for (int i = 0; i < 4; i++)
                        {
                            lightings[i].GetComponent<LightingDisplay>().disappear = false;
                        }
                        int l = spritePhrase[phrase].spriteList.Count;
                        for (int i = 0; i < l; i++)
                        {
                            Color color = new Color(1, 1, 1, 2);
                            spritePhrase[phrase].spriteList[i].GetComponent<SpriteRenderer>().color = color;
                        }
                        l = windPhrase[phrase].windList.Count;
                        for (int i = 0; i < l; i++)
                        {
                            windPhrase[phrase].windList[i].GetComponent<WindMove>().ResetWind();
                        }
                        ResetWindMemory();
                        UIScoreManager.Instance.ResetScoreUI();
                        CameraShake.Instance.DoCameraShake();
                    }
                    currentIndex = 0;
                    breakCount = 0;
                }
                else
                {
                    CheckCurrentFootPrint();
                }
            }
            if(hintManager.skipHint)
            {
                hintTimer += Time.deltaTime;
                if (hintTimer >= 30f)
                {
                    hintManager.GetComponent<Animator>().SetInteger("HintState", 1);
                    hintManager.skipHint = false;
                    hintTimer = 0;
                }
            }
        }
	}

    private void ResetWindMemory()
    {
        for (int i = 0; i < 4; i++)
        {
            windMemory[i] = false;
        }
    }

    private void CheckCurrentFootPrint()
    {
        if (spritePhrase[phrase].spriteList[currentIndex - 1].color.a >= 0.9)
        {
            footObjs[1].transform.position = spritePhrase[phrase].spriteList[currentIndex - 1].transform.position + new Vector3(0.43f, 0.54f, 0f);
            footObjs[1].GetComponent<FootDisplay>().disappear = false;
            audios[3].Play();
            windPhrase[phrase].windList[currentIndex - 1].SetActive(true);
            windMemory[currentIndex - 1] = true;
            breakCount++;
        }
        // break false
        else
        {
            footObjs[0].transform.position = spritePhrase[phrase].spriteList[currentIndex - 1].transform.position;
            footObjs[0].GetComponent<FootDisplay>().disappear = false;
            audios[2].Play();
        }
    }

    private bool FinishCurrentPhrase()
    {
        return false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "StandCloud")
        {
            collision.gameObject.GetComponent<StandCloud>().alphaState = StandCloud.CloudStates.Show;
        }
        else if(collision.gameObject.tag == "Feather")
        {
            featherAnis[0].gameObject.SetActive(false);
            featherAnis[1].gameObject.SetActive(true);
        }
        else if(collision.gameObject.tag == "Zues")
        {
            zuesFace.SetBool("Sneeze", true);
            zuesFeather.SetInteger("State", 1);
        }
        else if(collision.gameObject.tag == "LittleWind")
        {
            collision.gameObject.SetActive(false);
            UIScoreManager.Instance.PlusScore();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "StandCloud")
        {
            collision.gameObject.GetComponent<StandCloud>().alphaState = StandCloud.CloudStates.Hide;
        }
    }
}
