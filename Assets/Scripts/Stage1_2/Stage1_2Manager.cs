﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1_2Manager : MonoBehaviour
{

    private Vector3 windLastPosition;
    private Vector3 bugLastPosition;
    [SerializeField] GameObject ladybug;
    private bool isPushedByWind;

    private float bugGravityScale = 0.2f;
    private float forceAbsoluteVaule = 0.01f;
    public bool bugStop = false;
    //[SerializeField] Animator hint;
    [SerializeField] Animator leaf;
    [SerializeField] Vector3 arrivePos;
    [SerializeField] GameObject littleWind;
    [SerializeField] CircleCollider2D windCollider;

    // Use this for initialization
    void Start () {
        isPushedByWind = false;
    }
	
	// Update is called once per frame
	void Update () {
        if(UIScoreManager.Instance.scoreEnough)
        {
            NextScene.Instance.changeScene(3);
        }

        if(ladybug.transform.position == arrivePos)
        {
            littleWind.SetActive(true);
        }
        if (littleWind.activeSelf && !littleWind.GetComponent<Animation>().isPlaying)
        {
            windCollider.isTrigger = true;
        }

        if(bugStop)
        {
            ladybug.transform.position = Vector3.MoveTowards(ladybug.transform.position, arrivePos, Time.deltaTime * 1.2f);
            ladybug.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            ladybug.GetComponent<Rigidbody2D>().isKinematic = true;
            //hint.SetBool("ShowHint", false);
        }
        if (!isPushedByWind)
        {
            if(ladybug.transform.position.y > -2) {
                if(ladybug.GetComponent<Rigidbody2D>().velocity.y > 0)
                {
                    bugLastPosition = ladybug.transform.position;
                    ladybug.GetComponent<Rigidbody2D>().gravityScale = bugGravityScale;
                }
                else
                {
                    if(ladybug.transform.position.y > ((bugLastPosition.y + 2) / 2 - 2))
                    {
                        ladybug.GetComponent<Rigidbody2D>().gravityScale = bugGravityScale;
                        if(ladybug.GetComponent<Rigidbody2D>().velocity.y < Mathf.Lerp(((bugLastPosition.y + 2) / 2 - 2), -2f, 50f * Time.deltaTime) - ((bugLastPosition.y + 2) / 2 - 2))
                        {
                            ladybug.GetComponent<Rigidbody2D>().velocity = new Vector3(ladybug.GetComponent<Rigidbody2D>().velocity.x,
                            Mathf.Lerp(((bugLastPosition.y + 2) / 2 - 2), -2f, 50f * Time.deltaTime) - ((bugLastPosition.y + 2) / 2 - 2));
                        }
                    }
                    else
                    {
                        ladybug.GetComponent<Rigidbody2D>().gravityScale = 0f;
                        ladybug.GetComponent<Rigidbody2D>().velocity = new Vector3(ladybug.GetComponent<Rigidbody2D>().velocity.x,
                            Mathf.Lerp(ladybug.transform.position.y, -2f, 50f * Time.deltaTime) - ladybug.transform.position.y);
                    }
                }
            }
            else
            {
                ladybug.GetComponent<Rigidbody2D>().gravityScale = 0f;
                ladybug.GetComponent<Rigidbody2D>().velocity = new Vector3(ladybug.GetComponent<Rigidbody2D>().velocity.x,
                    Mathf.Lerp(ladybug.transform.position.y, -2f, 50f * Time.deltaTime) - ladybug.transform.position.y);
            }
        }
        else
        {
            ladybug.GetComponent<Rigidbody2D>().gravityScale = 0f;
            if (ladybug.transform.position.y > -2)
            {
                if (ladybug.GetComponent<Rigidbody2D>().velocity.y > 0)
                {
                    bugLastPosition = ladybug.transform.position;
                    ladybug.GetComponent<Rigidbody2D>().gravityScale = bugGravityScale;
                }
            }
        }
	}

    private void ladybugMove()
    {
        ladybug.GetComponent<Rigidbody2D>().velocity = new Vector3(ladybug.GetComponent<Rigidbody2D>().velocity.x,
            Mathf.Lerp(ladybug.transform.position.y, -2f, 50f * Time.deltaTime) - ladybug.transform.position.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (bugStop)
            return;
        if (collision.gameObject.name == "beat-fly")
        {
            isPushedByWind = true;
            windLastPosition = transform.position;
            leaf.SetTrigger("Start");
        }
        //hint.SetBool("ShowHint", true);

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (bugStop)
            return;
        
        if(collision.gameObject.name == "beat-fly")
        {
            isPushedByWind = true;
            Rigidbody2D myRigid2D = collision.gameObject.GetComponent<Rigidbody2D>();
            Vector3 forceValue = (transform.position - windLastPosition) * forceAbsoluteVaule;
            Vector3 forceDirection = collision.transform.position - transform.position;
            myRigid2D.AddForce(forceDirection.normalized * forceValue.magnitude);
            //myRigid2D.gravityScale = 0.05f;
            windLastPosition = transform.position;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (bugStop)
            return;
        if(collision.gameObject.name == "beat-fly")
        {
            isPushedByWind = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "LittleWind")
        {
            UIScoreManager.Instance.PlusScore();
            collision.gameObject.SetActive(false);
        }
    }
}
