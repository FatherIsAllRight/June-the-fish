using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zues : MonoBehaviour {
    [SerializeField] SpriteRenderer feather;
    [SerializeField] SpriteRenderer face;
    private bool show;
    private Animator aniFace;
    private Animator aniFeather;
    private bool hide;
	// Use this for initialization
	void Start () {
        show = true;
        aniFace = face.GetComponent<Animator>();
        aniFeather = feather.GetComponent<Animator>();
        hide = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(show)
        {
            Color color = face.color;
            color.a += 0.02f;
            if (color.a >= 1.0f){
                show = false;
                feather.GetComponent<EdgeCollider2D>().isTrigger = true;
            }
            face.color = color;
            feather.color = color;
        }
        AnimatorStateInfo faceInfo = aniFace.GetCurrentAnimatorStateInfo(0);
        if(faceInfo.IsName("ZuesSneeze") && faceInfo.normalizedTime >= 0.9f)
        {
            aniFeather.SetInteger("State", 2);
            hide = true;
        }
        if(hide)
        {
            Color color = face.color;
            color.a -= 0.02f;
            if (color.a <= 0)
            {
                face.gameObject.SetActive(false);
                hide = false;
            }
            face.color = color;
        }
	}
}