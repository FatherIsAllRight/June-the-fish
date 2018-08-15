using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugTryFly : MonoBehaviour {

    private Animator bugAni;
    private bool up = false;

	// Use this for initialization
	void Start () {
        bugAni = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if(bugAni != null && bugAni.GetCurrentAnimatorStateInfo(0).IsName("LadybugFly"))
        {
            //up = true;
        }
	}
}
