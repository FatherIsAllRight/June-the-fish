using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Stage1Manager : MonoBehaviour {

    [SerializeField] float speed = 1;
    private bool recordTrail = false;
    private TrailRenderer myTrail;
    private int bugState = 0;
    [SerializeField] GameObject[] myLittleBubbles;
    [SerializeField] GameObject myLittleWinds;

    // Use this for initialization
    void Start()
    {
        myTrail = gameObject.GetComponent<TrailRenderer>();
        //myTrail.time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(Game_UI_Controller.Instance.GetScore() == 4)
        {
            NextScene.Instance.changeScene(2);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "Bug" && bugState < myLittleBubbles.Length)
        {
            myLittleBubbles[bugState].SetActive(false);
            bugState++;
            if(bugState == myLittleBubbles.Length)
            {
                myLittleWinds.SetActive(true);
            }
        }
        if(collision.tag == "LittleWind")
        {
            Game_UI_Controller.Instance.GetLittleWind(1);
            collision.gameObject.SetActive(false);
        }
    }
}