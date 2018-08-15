using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeatherFly : MonoBehaviour {
    private bool fly;
    [SerializeField] Vector3 target;
    [SerializeField] float speed;
    private Animator feather;
	// Use this for initialization
	void Start () {
        feather = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if(fly)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        }
	}

    public bool BlowUp()
    {
        AnimatorStateInfo info = feather.GetCurrentAnimatorStateInfo(0);
        float limit = info.normalizedTime - (int)(info.normalizedTime);
        if (info.IsName("FeatherFlow3") && (info.normalizedTime < 0.17f || info.normalizedTime > 0.91f))
        {
            feather.enabled = false;
            fly = true;
            return true;
        }
        return false;
    }
}