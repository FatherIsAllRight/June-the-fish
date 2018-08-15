using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeatherCatch : MonoBehaviour {
    private Animator animator;
    [SerializeField] BackGroundChangeAlpha[] bgList;
	// Use this for initialization
	void Start () {
        animator = gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            Stage5Manager.Instance.phrase = 0;
            int l = Stage5Manager.Instance.spritePhrase[0].spriteList.Count;
            for (int i = 0; i < l; i++)
                Stage5Manager.Instance.spritePhrase[0].spriteList[i].gameObject.SetActive(true);
            bgList[0].alphaState = BackGroundChangeAlpha.AlphaStates.Hide;
            bgList[1].alphaState = BackGroundChangeAlpha.AlphaStates.Hide;
            bgList[1].gameObject.GetComponent<Animator>().SetTrigger("Move");
            bgList[2].alphaState = BackGroundChangeAlpha.AlphaStates.Show;
            gameObject.SetActive(false);
            UIScoreManager.Instance.InitScore(2, 3);
        }
	}
}
