using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyStartPoint : MonoBehaviour {
    [SerializeField] GameObject myPlayer;
    [SerializeField] GameObject myBug;
    [SerializeField] Animator water;
    [SerializeField] GameObject UIObj;
    private Animator myAnimator;

	// Use this for initialization
	void Start () {
        myAnimator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if(!myBug.activeSelf && myAnimator.GetCurrentAnimatorStateInfo(0).IsName("BubbleDisappearAni") && myAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            myPlayer.SetActive(true);
            myBug.SetActive(true);
            gameObject.SetActive(false);
            UIObj.SetActive(true);
        }
	}

    private void OnMouseDown()
    {
        //gameObject.SetActive(false);
        GetComponent<Animator>().SetInteger("State", 2);
        water.SetInteger("State", 1);
    }

    public void StartPlayInitAni()
    {
        myAnimator.SetInteger("State", 1);
    }
}
