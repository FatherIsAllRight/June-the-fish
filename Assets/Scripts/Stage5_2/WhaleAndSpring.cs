using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhaleAndSpring : MonoBehaviour {
    [SerializeField] Animator whaleAni;
    [SerializeField] Animator springAni;
    private CapsuleCollider2D whaleCollider2D;
    [SerializeField] AudioSource whaleAudio;
	// Use this for initialization
	void Start () {
        whaleCollider2D = whaleAni.GetComponent<CapsuleCollider2D>();
	}
	
	// Update is called once per frame
	void Update () {
        AnimatorStateInfo whaleInfo = whaleAni.GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo springInfo = springAni.GetCurrentAnimatorStateInfo(0);
        if(whaleInfo.IsName("WhaleIdle"))
        {
            whaleCollider2D.enabled = true;
        }
        else if(whaleInfo.IsName("WhaleShow") && whaleInfo.normalizedTime > 0.9f)
        {
            whaleAudio.Play();
            whaleCollider2D.enabled = false;
        }
        else
        {
            whaleCollider2D.enabled = false;
        }
        if(springInfo.IsName("Spring"))
        {
            springAni.SetBool("Spring", false);
            whaleAni.SetBool("Push", false);
        }
	}
}
