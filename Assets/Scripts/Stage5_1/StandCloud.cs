using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandCloud : MonoBehaviour {

    public enum CloudStates : int
    {
        Hide = -1,
        Show = 1
    }

    public CloudStates alphaState { get; set; }
    private SpriteRenderer spriteRenderer;

	// Use this for initialization
	void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();
        alphaState = CloudStates.Hide;
	}
	
	// Update is called once per frame
	void Update () {
        if(alphaState == CloudStates.Show)
        {
            Color color = spriteRenderer.color;
            color.a = Mathf.Min(color.a + 0.015f, 1.0f);
            spriteRenderer.color = color;
        }
        else
        {
            Color color = spriteRenderer.color;
            color.a = Mathf.Max(color.a - 0.03f, 0);
            spriteRenderer.color = color;
        }
	}
}
