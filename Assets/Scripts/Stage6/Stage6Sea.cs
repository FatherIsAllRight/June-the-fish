using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage6Sea : MonoBehaviour {
    [SerializeField] Transform sea1;
    [SerializeField] Transform sea2;
    public Vector3 pos;

    public float speed = 1.0f;
    // Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (sea1.position.y < -45f)
        {
            
            sea1.position += 2 * pos;
            return;
        }
        if (sea2.position.y < -45f)
        {
            
            sea2.position += 2 * pos;
            return;
        }
        sea1.position -= new Vector3(0, Time.deltaTime * speed, 0);
        sea2.position -= new Vector3(0, Time.deltaTime * speed, 0);
	}
}
