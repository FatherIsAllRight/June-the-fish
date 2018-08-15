using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirWallAlphaChange : MonoBehaviour {

    public enum AirWallStates : int
    {
        Show = 1,
        Hide = -1
    }

    public AirWallStates alphaState { get; set; }
    private SpriteRenderer spriteRenderer;

	// Use this for initialization
	void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        if(alphaState == AirWallStates.Show)
        {
            Color color = spriteRenderer.color;
            color.a = Mathf.Min(color.a + 0.04f, 1.0f);
            spriteRenderer.color = color;
        }
        else
        {
            Color color = spriteRenderer.color;
            color.a = Mathf.Max(color.a - 0.04f, 0);
            spriteRenderer.color = color;
        }
	}

}
