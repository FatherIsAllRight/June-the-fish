using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor.Animations;

public class Stage6WhiteWind : MonoBehaviour {

    private float sacle = 1f;
    private float ySpeed = -0.05f;
    private float xSpeed = 0f;
    private float xAccelerate = 0f;
    //[SerializeField] AnimatorController[] anis;

    // Use this for initialization
    void Start () {
        this.transform.position = new Vector3(Random.Range(-6f, 6f), 6f, 0);
        //GetComponent<Animator>().runtimeAnimatorController = anis[Random.Range(0, anis.Length)];
        ySpeed = Random.Range(-0.03f, -0.07f);
        if(this.transform.position.x > 0)
        {
            xSpeed = Random.Range(-0.03f, 0f);
        }
        else
        {
            xSpeed = Random.Range(0f, 0.03f);
        }
        xAccelerate = Random.Range(-0.0006f, 0.0006f);
    }
	
	// Update is called once per frame
	void Update () {
        this.transform.Translate(new Vector3(xSpeed, ySpeed, 0));
        xSpeed += xAccelerate;
        if (this.transform.position.y < -6)
        {
            Destroy(this.gameObject);
        }
    }
}
