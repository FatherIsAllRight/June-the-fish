using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundChangeAlpha : MonoBehaviour {

    public enum AlphaStates : int
    {
        Show = 1,
        Idle = 0,
        Hide = -1
    }
    public float speed;
    public AlphaStates alphaState { get; set; }
    private SpriteRenderer spriteRenderer;

    // Use this for initialization
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        alphaState = AlphaStates.Idle;
    }

    // Update is called once per frame
    void Update()
    {
        if (alphaState == AlphaStates.Show)
        {
            Color color = spriteRenderer.color;
            color.a = Mathf.Min(color.a + speed, 1.0f);
            spriteRenderer.color = color;
        }
        else if(alphaState == AlphaStates.Hide)
        {
            Color color = spriteRenderer.color;
            color.a = Mathf.Max(color.a - speed, 0);
            spriteRenderer.color = color;
        }
    }
}
