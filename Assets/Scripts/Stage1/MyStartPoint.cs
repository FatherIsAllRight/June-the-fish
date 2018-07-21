using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyStartPoint : MonoBehaviour {
    [SerializeField] GameObject myPlayer;
    [SerializeField] GameObject myBug;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnMouseDown()
    {
        myPlayer.SetActive(true);
        myBug.SetActive(true);
        gameObject.SetActive(false);
    }
}
