using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveController : MonoBehaviour {
    [SerializeField] float moveSpeed;
    private bool clicked;

	// Use this for initialization
	void Start () {
        clicked = false;
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit2D = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Input.mousePosition, Mathf.Infinity, 1<<10);
            if (hit2D.collider != null && hit2D.collider.name == "Player")
            {
                clicked = true;
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            clicked = false;
            if (UIScoreManager.Instance != null)
            {
                if (!UIScoreManager.Instance.scoreEnough)
                    UIScoreManager.Instance.ResetScoreUI();
            }
        }
        if(clicked)
        {
            Vector3 delta = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            if (System.Math.Abs(delta.x) > Mathf.Epsilon)
            {
                float deltaAngle = Mathf.Atan2(delta.y, delta.x) / Mathf.PI * 180;
                Quaternion target = Quaternion.Euler(0, 0, deltaAngle);
                transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * moveSpeed);
            }
            Vector3 newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 10f);
            transform.position = Vector3.MoveTowards(transform.position, newPos, Time.deltaTime * moveSpeed);
        }


        if (transform.position.x < -8.0f)
        {
            transform.position = new Vector3(-8.0f, transform.position.y, transform.position.z);
        }
        else if (transform.position.x > 8.0f)
        {
            transform.position = new Vector3(8.0f, transform.position.y, transform.position.z);

        }
        if (transform.position.y > 4.0f)
        {
            transform.position = new Vector3(transform.position.x, 4.0f, transform.position.z);
        }
        else if (transform.position.y < -4.0f)
        {
            transform.position = new Vector3(transform.position.x, -4.0f, transform.position.z);
        }
	}


    private void OnMouseDrag()
    {
        //Vector3 delta = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        //if (System.Math.Abs(delta.x) > Mathf.Epsilon)
        //{
        //    float deltaAngle = Mathf.Atan2(delta.y, delta.x) / Mathf.PI * 180;
        //    Quaternion target = Quaternion.Euler(0, 0, deltaAngle);
        //    transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * moveSpeed);
        //}
        //Vector3 newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 10f);
        //transform.position = Vector3.MoveTowards(transform.position, newPos, Time.deltaTime * moveSpeed);
    }

    private void OnMouseUp()
    {
        //if(UIScoreManager.Instance != null)
        //{
        //    if(!UIScoreManager.Instance.scoreEnough)
        //        UIScoreManager.Instance.ResetScoreUI();
        //}
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "AirWall")
        {
            collision.gameObject.GetComponent<AirWallAlphaChange>().alphaState = AirWallAlphaChange.AirWallStates.Show;
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "AirWall")
        {
            collision.gameObject.GetComponent<AirWallAlphaChange>().alphaState = AirWallAlphaChange.AirWallStates.Hide;
        }
    }
}
