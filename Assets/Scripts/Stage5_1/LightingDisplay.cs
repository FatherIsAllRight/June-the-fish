using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingDisplay : MonoBehaviour {

    public float speed;
    public float alphaMax;
    private SpriteRenderer sprite;
    public bool disappear;
    // Use this for initialization
    void Start()
    {
        sprite = gameObject.GetComponent<SpriteRenderer>();
        disappear = true;
    }

    // Update is called once per frame
    void Update()
    {
        Color color = sprite.color;
        if (!disappear)
        {
            color.a = color.a + speed;
            if (color.a >= alphaMax)
                disappear = true;
        }
        else
            color.a = Mathf.Max(color.a - speed / 3, 0);
        sprite.color = color;
    }
}
