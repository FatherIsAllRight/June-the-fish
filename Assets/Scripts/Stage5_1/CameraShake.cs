using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour {

    private Camera myCamera;
    private Vector3 myDefaultPosition;

    private bool doShake = false;
    [SerializeField] float myCameraShake_Max = 1;
    [SerializeField] AnimationCurve myCameraShake_Curve;
    [SerializeField] float myCameraShake_Speed = 10;
    [SerializeField] float myCameraShake_Time = 0.5f;
    private float myCameraShake_Ratio;
    private float myCameraShake_Process = 0;

    //========================================================================
    private static CameraShake instance = null;
    public static CameraShake Instance { get { return instance; } }

    // Use this for initialization
    void Awake()
    {

        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

        myDefaultPosition = this.transform.position;
        myCamera = this.GetComponent<Camera>();
    }

    // Use this for initialization
    void Start()
    {
        //      myTargetOrthographicSize = myCamera.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (doShake)
        {
            myCameraShake_Process += Time.deltaTime * myCameraShake_Ratio;

            if (myCameraShake_Process >= 1)
            {
                doShake = false;
                this.transform.position = myDefaultPosition;

            }
            else
            {
                float t_shake = myCameraShake_Curve.Evaluate(myCameraShake_Process) * myCameraShake_Max;
                Vector3 t_targetPosition = myDefaultPosition + t_shake * (Vector3)Random.insideUnitCircle.normalized;
                this.transform.position = Vector3.Lerp(this.transform.position, t_targetPosition, Time.deltaTime * myCameraShake_Speed);

            }
        }
    }

    public void DoCameraShake()
    {
        doShake = true;
        myCameraShake_Ratio = 1 / myCameraShake_Time;
        myCameraShake_Process = 0;
    }
}
