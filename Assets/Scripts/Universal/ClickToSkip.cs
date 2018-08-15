using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ClickToSkip : MonoBehaviour {
    public int sceneID;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetMouseButtonDown(0) && NextScene.Instance.maskStatus == NextScene.MaskStatus.Playing)
        {
            NextScene.Instance.changeScene(sceneID);
        }
	}
}
