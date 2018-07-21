using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2Manager : MonoBehaviour {

    [SerializeField] float myForce;
    private Vector2 myBugSpeedLast;
    private Vector2 myPlayerSpeedLast;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.name == "beat-fly")
        {
            Debug.Log("dafsf");

        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.name == "beat-fly")
        {
            Rigidbody2D myRigid2D = collision.gameObject.GetComponent<Rigidbody2D>();
            Vector3 a = collision.transform.position - transform.position;
            myRigid2D.AddRelativeForce(a * myForce);
            myRigid2D.gravityScale = 0.05f;
        }
    }
}
