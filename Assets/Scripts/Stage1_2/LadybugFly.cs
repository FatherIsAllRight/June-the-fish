using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadybugFly : MonoBehaviour {
    [SerializeField] float speed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(transform.position.x < -7.0f)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(-7.0f, transform.position.y, transform.position.z), speed);
            //transform.position = Vector3.MoveTowards(transform.position, new Vector3(-7.0f, transform.position.y, 0), Time.deltaTime * speed);
        }
        else if(transform.position.x > 7.0f)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(7.0f, transform.position.y, transform.position.z), speed);
            //transform.position = Vector3.MoveTowards(transform.position, new Vector3(7.0f, transform.position.y, 0), Time.deltaTime * speed);
        }
	}
}
