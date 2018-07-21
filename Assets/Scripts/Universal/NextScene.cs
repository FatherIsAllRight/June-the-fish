using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour {

    public enum MaskStatus: int
    {
        SceneStart = -1,
        SceneEnd = 1,
        Playing = 0
    }

    private static NextScene instance = null;
    public static NextScene Instance { get { return instance; } }

    private int nextSceneID;
    public MaskStatus maskStatus { get; set; }
    private SpriteRenderer spriteRenderer;

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
    }

    // Use this for initialization
    void Start () {
        maskStatus = MaskStatus.SceneStart;
        nextSceneID = 0;
        spriteRenderer = GetComponent<SpriteRenderer>();
        Color color = spriteRenderer.color;
        color.a = 1f;
        spriteRenderer.color = color;
    }
	
	// Update is called once per frame
	void Update () {
        if(maskStatus == MaskStatus.SceneStart)
        {
            Color color = spriteRenderer.color;
            color.a = color.a - 0.01f;
            spriteRenderer.color = color;
            if (color.a <= 0)
            {
                maskStatus = MaskStatus.Playing;
            }
        }
        else if(maskStatus == MaskStatus.SceneEnd)
        {
            Color color = spriteRenderer.color;
            color.a = color.a + 0.01f;
            spriteRenderer.color = color;
            if (color.a >= 1)
            {
                SceneManager.LoadScene(nextSceneID);
            }
        }
    }

    public void changeScene(int sceneID)
    {
        nextSceneID = sceneID;
        maskStatus = MaskStatus.SceneEnd;
    }
}
