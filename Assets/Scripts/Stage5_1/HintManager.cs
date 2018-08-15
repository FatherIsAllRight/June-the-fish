using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintManager : MonoBehaviour {

    [SerializeField] SpriteRenderer frame;
    [SerializeField] SpriteRenderer content;
    public Sprite[] contentList;
    private int contentIndex;
    private bool showFrame;
    private bool changeContent;
    private bool showContent;
    private bool hintFinish;
    public bool skipHint;
    private Animator animator;
	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        changeContent = false;
        showFrame = false;
        contentIndex = 0;
        showContent = false;
        hintFinish = false;
        skipHint = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (skipHint)
            return;
        if(!showFrame)
        {
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            if(stateInfo.IsName("HintStay") && stateInfo.normalizedTime >= 1.0f)
                showFrame = true;
        }
        else if(!changeContent)
        {
            Color frameC = frame.color;
            frameC.a += 0.05f;
            if (frameC.a >= 1.0f)
            {
                changeContent = true;
                showContent = true;
                content.sprite = contentList[0];
            }
            frame.color = frameC;
        }
        else if(hintFinish)
        {
            Color frameC = frame.color;
            frameC.a -= 0.05f;
            if (frameC.a <= 0)
            {
                animator.SetInteger("HintState", 0);
                changeContent = false;
                showFrame = false;
                contentIndex = 0;
                showContent = false;
                hintFinish = false;
                skipHint = true;
            }
            frame.color = frameC;
        }
        else if(showFrame && changeContent)
        {
            ChangeContent();
        }
	}

    void ChangeContent()
    {
        Color color = content.color;
        if(showContent)
        {
            color.a += 0.04f;
            if (color.a >= 5)
                showContent = false;
        }
        else
        {
            color.a -= 0.04f;
            if (color.a < 0)
            {
                color.a = 0;
                showContent = true;
                contentIndex++;
                if(contentIndex >= contentList.Length)
                {
                    hintFinish = true;
                }
                else
                    content.sprite = contentList[contentIndex];
            }
        }
        content.color = color;
    }
}
