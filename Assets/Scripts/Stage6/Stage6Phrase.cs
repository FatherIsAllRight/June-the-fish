using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage6Phrase : MonoBehaviour {

    public GameObject BlackWind;
    public GameObject WhiteWind;
    public int whiteCount = 2;
    public int blackCount = 3;
    public GameObject Cover;
    public int coverCount = 1;

    // Use this for initialization
    void Start () {
        for(int i = 0; i < whiteCount; i++)
        {
            StartCoroutine(GenerateWhiteWind());
        }
        for (int i = 0; i < blackCount; i++)
        {
            StartCoroutine(GenerateBlackWind());
        }

    }
	
	// Update is called once per frame
	void Update () {
    }

    IEnumerator GenerateWhiteWind()
    {
        while (true)
        {
            Instantiate(WhiteWind);
            yield return new WaitForSeconds(5f);
        }
    }

    IEnumerator GenerateBlackWind()
    {
        while (true)
        {
            Instantiate(BlackWind);
            yield return new WaitForSeconds(Random.Range(2f, 5f));
        }
    }

    IEnumerator GenerateCover()
    {
        while (true)
        {
            Instantiate(Cover);
            yield return new WaitForSeconds(Random.Range(10f, 15f));
        }
    }

    public void addWhiteWind(int num)
    {
        if(num > 0) {
            for (int i = 0; i < num; i++)
            {
                StartCoroutine(GenerateWhiteWind());
                whiteCount++;
            }
        }
        else {
            for (int i = 0; i < num && whiteCount > 0; i--)
            {
                StopCoroutine(GenerateWhiteWind());
                whiteCount--;  
            }
        }
    }

    public void addBlackWind(int num)
    {
        if (num > 0)
        {
            for (int i = 0; i < num; i++)
            {
                StartCoroutine(GenerateBlackWind());
                blackCount++;
            }
        }
        else
        {
            for (int i = 0; i < num && blackCount > 0; i--)
            {
                StopCoroutine(GenerateBlackWind());
                blackCount--;
            }
        }
    }
}
