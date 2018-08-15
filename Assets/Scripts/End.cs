using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End : MonoBehaviour {
    
    [SerializeField] float[] timeList;
    [SerializeField] Sprite[] graphList;
    [SerializeField] float fadeSpeed;
    private float timer;
    private float speed = 10;
    private int count;
    private int isChange;

	// Use this for initialization
	void Start () {
        timer = 0;
        count = 0;
        isChange = 0;
    }
	
	// Update is called once per frame
	void Update () {
        if(isChange != 0)
        {
            SpriteRenderer spriteRenderer = this.GetComponent<SpriteRenderer>();
            
            if(isChange == 1)
            {
                Color color = spriteRenderer.color;
                color.a = spriteRenderer.color.a - fadeSpeed;
                spriteRenderer.color = color;
                if(spriteRenderer.color.a < 0)
                {
                    isChange = 2;
                    spriteRenderer.sprite = graphList[count];
                }
            }
            else if(isChange == 2)
            {
                Color color = spriteRenderer.color;
                color.a = spriteRenderer.color.a + fadeSpeed;
                spriteRenderer.color = color;
                if (spriteRenderer.color.a > 1)
                {
                    isChange = 0;
                }
            }
        }
        Debug.Log(isChange);
        Debug.Log(count);
        timer += Time.deltaTime * speed;
        if(count < timeList.Length - 1 && timer >= timeList[count])
        {
            count++;
            isChange = 1;
        }
	}
}
