using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1_2_Target : MonoBehaviour {

    private static Stage1_2_Target instance = null;
    public static Stage1_2_Target Instance { get { return instance; } }
    [SerializeField] Stage1_2Manager stage2;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "beat-fly")
        {
            stage2.bugStop = true;
            //collision.GetComponent<Rigidbody2D>.
        }
    }
}
