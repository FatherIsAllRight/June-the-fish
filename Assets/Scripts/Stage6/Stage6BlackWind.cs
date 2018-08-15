using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor.Animations;

public class Stage6BlackWind : MonoBehaviour {

    private float ySpeed = -0.05f;
    private float xSpeed = 0f;
    private float xAccelerate = 0f;
    //[SerializeField] AnimatorController[] anis;

    // Use this for initialization
    void Start()
    {
        this.transform.position = new Vector3(Random.Range(-9f, 9f), 6f, 0);
        //GetComponent<Animator>().runtimeAnimatorController = anis[Random.Range(0, anis.Length)];
        float scale = Random.Range(0.5f, 2f);
        this.transform.localScale = new Vector3(scale, scale, 0);
        ySpeed = Random.Range(-0.01f, -0.07f);
        xSpeed = Random.Range(0.06f, 0.06f);
        xAccelerate = Random.Range(-0.001f, 0.001f);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(new Vector3(xSpeed, ySpeed, 0));
        xSpeed += xAccelerate;
        if (this.transform.position.y < -6)
        {
            Destroy(this.gameObject);
        }
    }
}
