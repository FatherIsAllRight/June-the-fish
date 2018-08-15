using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindMove : MonoBehaviour {

    public Vector3 target;
    public bool show = true;
    public float speed = 1f;
    private Vector3 origin;
	// Use this for initialization
	void Start () {
        //show = true;
        origin = transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () {
        if(show)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, target, Time.deltaTime * speed);
            if(transform.localPosition == target)
            {
                GetComponent<CircleCollider2D>().isTrigger = true;
            }
        }

	}

    public void ResetWind()
    {
        transform.localPosition = origin;
        show = true;
        //GetComponent<Animator>().SetBool("Restart", true);
        GetComponent<CircleCollider2D>().isTrigger = false;
        gameObject.SetActive(false);
    }
}
