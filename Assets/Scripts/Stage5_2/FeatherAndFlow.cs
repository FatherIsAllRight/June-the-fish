using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeatherAndFlow : MonoBehaviour {

    [SerializeField] Animator feather;
    [SerializeField] int featherState;
    [SerializeField] Animator wave;
    [SerializeField] int waveState;
    private Animator ripple;
    [SerializeField] float percentage;
	// Use this for initialization
	void Start () {
        ripple = gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!feather.enabled)
            return;
        AnimatorStateInfo info = ripple.GetCurrentAnimatorStateInfo(0);
        if (info.IsName("Blank"))
            gameObject.SetActive(false);
        if(info.normalizedTime >= percentage)
        {
            feather.SetInteger("State", featherState);
            wave.SetInteger("Wave", waveState);
            //gameObject.SetActive(false);
        }
	}
}
