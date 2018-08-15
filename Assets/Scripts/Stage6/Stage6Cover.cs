using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage6Cover : MonoBehaviour {

    private float ySpeed = -0.05f;
    private float xSpeed = 0f;
    private float scaleSpeed = 0f;
    [SerializeField] Sprite[] sprites;
    private SpriteRenderer spr;
    private float alphaSpeed;
    // Use this for initialization
    void Start()
    {
        this.transform.position = new Vector3(Random.Range(-7f, 7f), Random.Range(6f, 10f), 0);
        spr = GetComponent<SpriteRenderer>();
        spr.sprite = sprites[Random.Range(0, sprites.Length)];
        float scale = Random.Range(0.1f, 0.4f);
        this.transform.localScale = new Vector3(scale, scale, 1);
        if(transform.position.x < -2f)
        {
            xSpeed = Random.Range(-0.02f, -0.005f);
        }
        else if(transform.position.x > 2f)
        {
            xSpeed = Random.Range(0.005f, 0.02f);
        }
        else
            xSpeed = Random.Range(-0.005f, 0.005f);
        ySpeed = Random.Range(-0.01f, -0.04f);
        scaleSpeed = Random.Range(0.001f, 0.002f);
        alphaSpeed = Random.Range(0.002f, 0.005f);
        //xSpeed = Random.Range(0.02f, 0.05f);
        //xAccelerate = Random.Range(-0.001f, 0.001f);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(new Vector3(xSpeed, ySpeed, 0));
        this.transform.localScale += new Vector3(scaleSpeed, scaleSpeed, 0);
        Color color = spr.color;
        color.a = Mathf.Min(color.a + alphaSpeed, 1f);
        spr.color = color;
        //xSpeed += xAccelerate;
        if (this.transform.position.y < -15)
        {
            Destroy(this.gameObject);
        }
    }
}
