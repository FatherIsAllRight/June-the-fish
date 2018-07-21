using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveController : MonoBehaviour {
    [SerializeField] float moveSpeed;
    public Vector2 myPlayerSpeed;
    public Vector2 myPlayerA;

	// Use this for initialization
	void Start () {
        myPlayerSpeed = new Vector2(0, 0);
        myPlayerA = new Vector2(0, 0);
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    private void OnMouseDrag()
    {
        Vector3 delta = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        if (System.Math.Abs(delta.x) > Mathf.Epsilon)
        {
            float deltaAngle = Mathf.Atan2(delta.y, delta.x) / Mathf.PI * 180;
            Quaternion target = Quaternion.Euler(0, 0, deltaAngle);
            transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * moveSpeed);
            //transform.rotation = Quaternion.Euler(0, 0, deltaAngle);
            //transform.Rotate(0, 0, deltaAngle);
        }
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 10);
    }
}
