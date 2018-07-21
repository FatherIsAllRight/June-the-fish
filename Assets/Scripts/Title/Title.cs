using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : MonoBehaviour {

    public enum SpriteStatus : int
    {
        SpriteFlyIn = 1,
        SpriteFlyOut = 2,
        NextScene = 3,
        Waiting = 0
    }

    [SerializeField] NextScene MaskNextScene;
    [SerializeField] Animator SpriteAnimator;
    [SerializeField] Transform SpriteTransform;
    private SpriteStatus spriteStatus;

    // Use this for initialization
    void Start () {
        spriteStatus = SpriteStatus.SpriteFlyIn;
    }
	
	// Update is called once per frame
	void Update () {

        //if(MaskNextScene.maskStatus == NextScene.MaskStatus.Playing)

            if(spriteStatus == SpriteStatus.SpriteFlyIn)
            {
                SpriteAnimator.SetInteger("SpriteState", 1);
                Debug.Log(SpriteAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime);
                if (SpriteAnimator.GetCurrentAnimatorStateInfo(0).IsName("TitleSpriteIn") && SpriteAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
                {
                    spriteStatus = SpriteStatus.Waiting;
                    SpriteTransform.position = new Vector3(2, -2, 0);
                }
            }
            else if(spriteStatus == SpriteStatus.Waiting)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    spriteStatus = SpriteStatus.SpriteFlyOut;
                }
            }
            else if (spriteStatus == SpriteStatus.SpriteFlyOut)
            {
                SpriteAnimator.SetInteger("SpriteState", 2);
                Debug.Log(SpriteAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime);
                if (SpriteAnimator.GetCurrentAnimatorStateInfo(0).IsName("TitleSpriteOut") && SpriteAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
                {
                    spriteStatus = SpriteStatus.NextScene;
                }
            }
            else if (spriteStatus == SpriteStatus.NextScene)
            {
                MaskNextScene.changeScene(1);
            }

	}
}
