using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Mask : MonoBehaviour {


    public Transform playerPos;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = playerPos.position - new Vector3(0.15f, 0f, 0f);
	}
}
