using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crysanthemum : MonoBehaviour {
    private Animator myAnimator;
    [SerializeField] MyStartPoint startPoint;

	// Use this for initialization
	void Start () {
        myAnimator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if(myAnimator.GetCurrentAnimatorStateInfo(0).IsName("TitleAni") && myAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.95f)
        {
            startPoint.transform.localScale = new Vector3(1, 1, 1);
            startPoint.StartPlayInitAni();
            gameObject.SetActive(false);

        }
	}
}
