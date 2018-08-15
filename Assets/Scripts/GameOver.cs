using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour {
    private bool moveCamera = false;
    public float speed;
    private Vector3 target;
    [SerializeField] SpriteRenderer word;
	// Use this for initialization
	void Start () {
        target = new Vector3(0, -5.4f, 0);
	}
	
	// Update is called once per frame
	void Update () {
        if(NextScene.Instance.maskStatus == NextScene.MaskStatus.Playing && Input.GetMouseButtonDown(0))
        {
            moveCamera = true;
        }
        if(moveCamera)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * speed);
        }
        if(Input.GetMouseButtonDown(0) && transform.position == target)
        {
            NextScene.Instance.changeScene(10);
        }
        if (transform.position.y <= -2f)
        {
            Color color = word.color;
            color.a = Mathf.Min(color.a + 0.003f, 1);
            word.color = color;
        }
	}
}
